using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.DbContexts {
    /// <summary>
    /// Основной DbContext для взаимодействия с БД
	/// Можно изменить бд, добавив строку подключения
    /// </summary>
    public class MainDbContext : DbContext {
		public MainDbContext(DbContextOptions<MainDbContext> options)
		: base(options) {
		}
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder options) {
			
		}
	}
}
