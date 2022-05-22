using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLogic;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for MemberManagePage.xaml
    /// </summary>
    public partial class MemberManagePage : Page
    {
        private readonly HttpClient apiClient;

        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        public MemberManagePage()
        {
            InitializeComponent();
            this.apiClient = App.serviceProvider.GetService<HttpClient>();
            LoadMemberList();
            btnDelete.IsEnabled = false;
        }

        private async void LoadMemberList()
        {
            try
            {
                var response = await apiClient.GetAsync("member");
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new Exception("Internal server error. Please retry.");
                }

                var dataString = await response.Content.ReadAsStringAsync();
                var members = JsonSerializer.Deserialize<IEnumerable<MemberViewModel>>(dataString, jsonOptions);
                lvMembers.ItemsSource = members;
                btnDelete.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loading Member List");
            }
        }

        private async void lvMembers_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MemberViewModel member = (MemberViewModel)lvMembers.SelectedItem;
            MemberPopup memberPopup = new MemberPopup(member, apiClient, true);
            memberPopup.ShowDialog();
            LoadMemberList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            MemberPopup memberPopup = new MemberPopup(null, apiClient, false);
            memberPopup.ShowDialog();
            LoadMemberList();
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MemberViewModel member = (MemberViewModel)lvMembers.SelectedItem;
                var response = await apiClient.DeleteAsync($"member/{member.MemberId}");
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new Exception("Internal server error. Please retry.");
                }

                LoadMemberList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Deleting Member");
            }
        }

        private void lvMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = true;
        }
    }
}