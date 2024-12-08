using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DbContexts;
using Core.Models.Interfaces;
using Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.DbRepos.Base {
	/// <summary>
	/// Базовая реализация репозитория для работы с бд со стандартными методами, благодаря нему можно легко добавлять новые сущности и не реализовывать репозиторий для каждой из них
	/// </summary>
	public class BaseDbRepo<T> : IRepo<T> where T : class, IGuidModel  {
		protected readonly MainDbContext _context;
		protected readonly DbSet<T> _dbSet;

		public BaseDbRepo(MainDbContext dbContext) {
			_context = dbContext;
			_dbSet = _context.Set<T>();
		}

		public async Task AddRecordAsync(T record) {
			await _dbSet.AddAsync(record);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteRecordAsync(Guid id) {
			var record = await GetRecordAsync(id);
			_dbSet.Remove(record);
 			await _context.SaveChangesAsync();
		}

		public async Task<T> GetRecordAsync(Guid id) {
			var record = await _dbSet.FindAsync(id);
			return record;
		}

		public async Task<IEnumerable<T>> GetRecordsAsync() {
			return await _dbSet.ToListAsync();
		}

		public async Task<T> UpdateRecordAsync(T record) {
			var updRecord = _dbSet.Update(record);
			await _context.SaveChangesAsync();
			return updRecord.Entity;
		}
	}
}
