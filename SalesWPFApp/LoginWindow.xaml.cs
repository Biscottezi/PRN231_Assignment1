using System.Net.Http;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private HttpClient apiClient;
        public LoginWindow(HttpClient apiClient)
        {
            InitializeComponent();
            this.apiClient = apiClient; 
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
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
            if (memberRepository.LoginAdmin(email, password))
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                Close();
            }
            else
            {
                MemberObject member = memberRepository.LoginMember(email, password);
                if (member != null)
                {
                    MemberWindow memberWindow = new MemberWindow(member, App.serviceProvider.GetService<IOrderRepository>());
                    memberWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Wrong username or password", "Login");
                }
            }
        }
    }
}
