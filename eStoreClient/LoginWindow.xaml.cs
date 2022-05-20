using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;
using BusinessLogic;
using BusinessLogic.RequestModel;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly HttpClient apiClient;

        private readonly JsonSerializerOptions jsonOption = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        public LoginWindow(HttpClient apiClient)
        {
            InitializeComponent();
            this.apiClient = apiClient;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;
            if (email.Length == 0 && password.Length == 0)
            {
                MessageBox.Show("Enter email and password", "Login");
                return;
            }
            else if (email.Length == 0)
            {
                MessageBox.Show("Enter email", "Login");
                return;
            }
            else if (password.Length == 0)
            {
                MessageBox.Show("Enter password", "Login");
                return;
            }

            var response = await apiClient.PostAsJsonAsync("member/login", new LoginModel()
            {
                Email = email,
                Password = password,
            });
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                MessageBox.Show("Wrong username or password", "Login");
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var dataString = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<LoginViewModel>(dataString, jsonOption);
                    switch (data.Role)
                    {
                        case 1:
                        {
                            AdminWindow adminWindow = new AdminWindow();
                            adminWindow.Show();
                            Close();
                            break;
                        }
                        case 2:
                        {
                            MemberWindow memberWindow = new MemberWindow(data.Member,
                                App.serviceProvider.GetService<HttpClient>());
                            memberWindow.Show();
                            Close();
                            break;
                        }
                    }
                }
            }
        }
    }
}