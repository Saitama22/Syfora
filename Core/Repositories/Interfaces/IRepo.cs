using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Interfaces;

namespace Core.Repositories.Interfaces {
	/// <summary>
	/// Интерфейс репозитория со стандартными методами
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IRepo<T> where T : IGuidModel {
		Task AddRecordAsync(T record);
		Task DeleteRecordAsync(Guid id);
		Task<T> GetRecordAsync(Guid id);
        Task<IEnumerable<T>> GetRecordsAsync();
		Task<T> UpdateRecordAsync(T record);
	}
}
