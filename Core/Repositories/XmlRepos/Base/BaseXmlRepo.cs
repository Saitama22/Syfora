using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using System.Xml.Serialization;
using Core.Models.Interfaces;
using Core.Repositories.Interfaces;
using System.IO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Core.Repositories.XmlRepos.Base { 
    /// <summary>
    /// Базовая реализация репозитория для работы с бд со стандартными методами, благодаря нему можно легко добавлять новые сущности и не реализовывать репозиторий для каждой из них
    /// </summary>
    public class BaseXmlRepo<T> : IRepo<T> where T : class, IGuidModel {
        private readonly string _storagePath;
        private readonly object _lock = new();

        public BaseXmlRepo(string storageDirectory, string entityName) {
            var currentDir = Directory.GetCurrentDirectory();
            var storagePatch = $"{currentDir}\\{storageDirectory}";
            Directory.CreateDirectory(storagePatch);

            _storagePath = $"{storagePatch}\\{entityName}.xml";

            if (!File.Exists(_storagePath)) {
                using (File.Create(_storagePath)) {
                }
                SaveRecords(new List<T>());
            }
        }

        private void SaveRecords(List<T> records) {
            lock (_lock) {
                using var stream = new FileStream(_storagePath, FileMode.Create);
                var serializer = new XmlSerializer(typeof(List<T>));
                serializer.Serialize(stream, records);
            }
        }

        public async Task AddRecordAsync(T record) {
            var records = (await GetRecordsAsync()).ToList();
            records.Add(record);
            SaveRecords(records);
        }

        public async Task DeleteRecordAsync(Guid id) {
            var records = (await GetRecordsAsync()).ToList();
            var recordOnDelete = records.FirstOrDefault(r => r.Id == id);
            if (recordOnDelete != null) {
                records.Remove(recordOnDelete);
                SaveRecords(records);
            }
        }

        public async Task<T> GetRecordAsync(Guid id) {
            var records = (await GetRecordsAsync()).ToList();
            return records.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<T> GetRecords() {           
            lock (_lock) 
            {
                using var stream = new FileStream(_storagePath, FileMode.Open);
                var serializer = new XmlSerializer(typeof(List<T>));
                var records = (List<T>)serializer.Deserialize(stream);
                return records;
            }
        }
        public async Task<IEnumerable<T>> GetRecordsAsync() {
            return await Task.Run(GetRecords);
        }

        public async Task<T> UpdateRecordAsync(T record) {
            var records = (await GetRecordsAsync()).ToList();
            var index = records.FindIndex(r => r.Id == record.Id);
            if (index == -1)
                throw new KeyNotFoundException($"Запись с ID {record.Id} не найдена.");

            records[index] = record;
            SaveRecords(records);
            
            return record;
        }
    }
}
