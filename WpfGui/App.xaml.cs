using System.Configuration;
using System.IO;
using System.Windows;
using Core.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WpfGui {
    public partial class App : Application {
        public static IServiceProvider ServiceProvider { get; private set; }
        private void ConfigureServices() {
            var serviceCollection = new ServiceCollection();

            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            //Инициализация сервисов из проекта с логикой
            serviceCollection.AddServices(configuration);
            serviceCollection.AddSingleton<MainViewModel>();
            serviceCollection.AddSingleton<MainWindow>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }


        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);                     

            //Инициализируем сервисы
            ConfigureServices();

            //Для возможности работы с DI в ViewModel окно создаем здесь, убрав из xaml создание этого окна
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
