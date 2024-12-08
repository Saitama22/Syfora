using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DbContexts;
using Core.Handlers.Interfaces;
using Core.Handlers;
using Core.Repositories.DbRepos;
using Core.Repositories.Interfaces;
using Core.Repositories.XmlRepos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Core.Mapper;
using Core.Repositories.InMemoryRepos;

namespace Core.DI {
	/// <summary>
	/// Класс, инициализирующий сервисы для работы логики
	/// </summary>
	public static class DiInit {
		public static IServiceCollection AddServices(this IServiceCollection services, IConfigurationRoot configuration) {	
			if (configuration["ProviderType"] == ProviderTypes.PostgreSqlProvider.ToString()) {
				services.AddDbRepos();
				services.AddDbContexts(configuration.GetConnectionString("PostgreSqlConnection"));
			}
			else if (configuration["ProviderType"] == ProviderTypes.XmlStorageProvider.ToString()) {
				services.AddXmlRepos(configuration);
			}
			
			else if (configuration["ProviderType"] == ProviderTypes.InMemoryProvider.ToString()) {
				services.AddInMemoryRepos();
			}

			services.AddHandlers();
			services.AddAutoMapper(typeof(MappingProfile));

			return services;
		}

		/// <summary>
		/// Реализация репозиториев для хранилища xml
		/// </summary>
		/// <param name="configuration">файл конфигурации, с указанием директории для Xml</param>
		/// <returns></returns>
		private static IServiceCollection AddXmlRepos(this IServiceCollection services, IConfigurationRoot configuration) {
			services.AddScoped<IUserRepo, UserXmlRepo>(provider =>
            {
                return new UserXmlRepo(configuration["XmlStorageDirectory"]);
            });
			return services;
        }

		/// <summary>
		/// Реализация репозиториев через БД, репозитории бд не зависят от конкретной бд
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		private static IServiceCollection AddDbRepos(this IServiceCollection services) {
			services.AddScoped<IUserRepo, UserDbRepo>();
			return services;
		}

		/// <summary>
		/// Реализация репозиториев через хранилище в оперативной памяти
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		private static IServiceCollection AddInMemoryRepos(this IServiceCollection services) {
			services.AddSingleton<IUserRepo, UserInMemoryRepo>();
			return services;
		}

		/// <summary>
		/// Классы для работы с сущностями. Содержат основную логику
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		private static IServiceCollection AddHandlers(this IServiceCollection services) {
			services.AddScoped<IUserHandler, UserHandler>();
			return services;
		}

		public static IServiceCollection AddDbContexts(this IServiceCollection services, string connection) {
			services.AddDbContext<MainDbContext>(options =>
				options.UseNpgsql(connection));
			return services;
		}
	}
}
