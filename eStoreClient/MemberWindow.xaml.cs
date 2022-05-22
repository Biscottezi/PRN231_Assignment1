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
            try
            {
                var response = await apiClient.GetAsync($"member/{this.member.MemberId}");
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new Exception("Internal server error. Please retry.");
                }

                var dataString = await response.Content.ReadAsStringAsync();
                var member = JsonSerializer.Deserialize<MemberViewModel>(dataString, jsonOption);
                this.member = member;
                MapMemberInfo(member);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Updating member");
            }
        }

        private async void MapMemberInfo(MemberViewModel MemberViewModel)
        {
            lbId.Content = MemberViewModel.MemberId.ToString();
            lbEmail.Content = MemberViewModel.Email;
            lbPassword.Content = MemberViewModel.Password;
            lbCompany.Content = MemberViewModel.CompanyName;
            lbCity.Content = MemberViewModel.City;
            lbCountry.Content = MemberViewModel.Country;
            try
            {
                var response = await apiClient.GetAsync($"order/{MemberViewModel.MemberId}/member");
                var dataString = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<IEnumerable<OrderViewModel>>(dataString, jsonOption);
                if (response.StatusCode == HttpStatusCode.InternalServerError)
                {
                    throw new Exception("Internal server error. Please retry.");
                }

                lvOrderHistory.ItemsSource = data;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Loading member's orders");
            }
        }
    }
}