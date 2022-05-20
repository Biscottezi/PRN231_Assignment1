using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;
using BusinessLogic;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {
        MemberViewModel member;
        private readonly HttpClient apiClient;

        private readonly JsonSerializerOptions jsonOption = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        public MemberWindow(MemberViewModel MemberViewModel, HttpClient apiClient)
        {
            InitializeComponent();
            this.apiClient = apiClient;
            this.member = MemberViewModel;
            MapMemberInfo(MemberViewModel);
        }

        private void lvOrderHistory_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OrderViewModel order = (OrderViewModel)lvOrderHistory.SelectedItem;
            lvOrderDetail.ItemsSource = order.OrderDetails;
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MemberPopup memberPopup = new MemberPopup(this.member, apiClient, true);
            _ = memberPopup.ShowDialog();
            var response = await apiClient.GetAsync($"member/{this.member.MemberId}");
            var dataString = await response.Content.ReadAsStringAsync();
            var member = JsonSerializer.Deserialize<MemberViewModel>(dataString, jsonOption);
            this.member = member;
            MapMemberInfo(member);
        }

        private async void MapMemberInfo(MemberViewModel MemberViewModel)
        {
            lbId.Content = MemberViewModel.MemberId.ToString();
            lbEmail.Content = MemberViewModel.Email;
            lbPassword.Content = MemberViewModel.Password;
            lbCompany.Content = MemberViewModel.CompanyName;
            lbCity.Content = MemberViewModel.City;
            lbCountry.Content = MemberViewModel.Country;
            var response = await apiClient.GetAsync($"order/{MemberViewModel.MemberId}/member");
            var dataString = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<IEnumerable<OrderViewModel>>(dataString, jsonOption);
            lvOrderHistory.ItemsSource = data;
        }
    }
}