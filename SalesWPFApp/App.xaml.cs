using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider serviceProvider;
        public static ServiceCollection service;

        public App()
        {
            service = new ServiceCollection();
            ConfigureService(service);
            serviceProvider = service.BuildServiceProvider();
        }

        private void ConfigureService(ServiceCollection service)
        {
            service.AddSingleton<LoginWindow>();
            HttpClient apiClient = new HttpClient()
            {
                BaseAddress = new Uri("http:/localhost:5000/api/"),
            };
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            service.AddSingleton(apiClient);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var loginWindow = serviceProvider.GetService<LoginWindow>();
            loginWindow.Show();
        }
    }
}
