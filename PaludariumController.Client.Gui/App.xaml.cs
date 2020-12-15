using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using PaludariumController.Core.Interfaces;
using PaludariumController.Core.Services;
using PaludariumController.Client.InfraStructure;

namespace PaludariumController.Client.Gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;
        public App()
        {
            host = Host.CreateDefaultBuilder()
                   .ConfigureServices((context, services) => {
                       ConfigureServices(context.Configuration, services);
                   })
                   .Build();
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddScoped<ILightsService, LightService>();
            services.AddScoped<ITemperatureService, TemperatureService>();
            services.AddScoped<IDevice, PaludiariumHttpRepository>();
            services.AddHttpClient();           
            
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (host)
            {
                await host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}

