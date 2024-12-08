using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Models.Interfaces;
using Core.Repositories.Interfaces;

namespace Core.Repositories.InMemoryRepos.Base {
    /// <summary>
    /// Базовая реализация репозитория для работы с бд со стандартными методами, благодаря нему можно легко добавлять новые сущности и не реализовывать репозиторий для каждой из них
    /// </summary>
    internal class BaseInMemoryRepo<T> : IRepo<T> where T : class, IGuidModel {
        private readonly List<T> _records = new();
        public async Task AddRecordAsync(T record) {
            _records.Add(record);
            await Task.FromResult(0);
        }

        public async Task DeleteRecordAsync(Guid id) {
            var item = await GetRecordAsync(id);
            if (item == null)
                throw new NullReferenceException("Значение не может быть null");
            _records.Remove(item);
        }

        public async Task<T> GetRecordAsync(Guid id) {
            var item = _records.FirstOrDefault(user => user.Id == id);
            return await Task.FromResult(item);
        }

        public async Task<IEnumerable<T>> GetRecordsAsync() {
            return await Task.FromResult(_records);
        }

        public async Task<T> UpdateRecordAsync(T record) {
            var index = await Task.FromResult(_records.FindIndex(r => r.Id == record.Id));
            if (index == -1)
                throw new KeyNotFoundException($"Запись с ID {record.Id} не найдена.");

            _records[index] = record;
            return record;
        }
    }
}
