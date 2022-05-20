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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLogic;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for OrderManagePage.xaml
    /// </summary>
    public partial class OrderManagePage : Page
    {
        private HttpClient apiClient;

        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        public OrderManagePage()
        {
            InitializeComponent();
            this.apiClient = App.serviceProvider.GetService<HttpClient>();
            btnDelete.IsEnabled = false;
            LoadOrderList();
        }

        private async void LoadOrderList()
        {
            try
            {
                var response = await apiClient.GetAsync("order");
                var dataString = await response.Content.ReadAsStringAsync();
                var orders = JsonSerializer.Deserialize<IEnumerable<OrderViewModel>>(dataString, jsonOptions);
                lvOrders.ItemsSource = orders;
                btnDelete.IsEnabled = false;
                lvOrderDetails.ItemsSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loading Order List");
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderViewModel order = (OrderViewModel)lvOrders.SelectedItem;
                var response = await apiClient.DeleteAsync($"order/{order.OrderId}");
                LoadOrderList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Deleting Order");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            OrderPopup orderPopup = new OrderPopup(null, false);
            orderPopup.ShowDialog();
            LoadOrderList();
        }

        private void lvOrders_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrderViewModel order = (OrderViewModel)lvOrders.SelectedItem;
            OrderPopup orderPopup = new OrderPopup(order, true);
            orderPopup.ShowDialog();
            LoadOrderList();
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            LoadOrderList();
        }

        private async void btn_Sort_Click(object sender, RoutedEventArgs e)
        {
            if (dp_start.SelectedDate == null || dp_end.SelectedDate == null)
            {
                MessageBox.Show("Choose a start date and end date");
                return;
            }

            DateTime start = (DateTime)dp_start.SelectedDate;
            DateTime end = (DateTime)dp_end.SelectedDate;
            var response = await apiClient.GetAsync($"order?startDate={start.ToString()}&endDate={end.ToString()}");
            var dataString = await response.Content.ReadAsStringAsync();
            var orders = JsonSerializer.Deserialize<IEnumerable<OrderViewModel>>(dataString, jsonOptions);
            lvOrders.ItemsSource = orders;
        }

        private void lvOrders_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OrderViewModel order = (OrderViewModel)lvOrders.SelectedItem;
            if (order != null)
            {
                if (order.OrderDetails.Count == 0)
                {
                    btnDelete.IsEnabled = true;
                }

                lvOrderDetails.ItemsSource = order.OrderDetails;
            }
        }
    }
}