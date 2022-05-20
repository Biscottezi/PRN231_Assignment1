using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
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
using BusinessLogic.RequestModel;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for OrderPopup.xaml
    /// </summary>
    public partial class OrderPopup : Window
    {
        private HttpClient apiClient;
        private bool isUpdate;
        OrderViewModel order = null;

        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        public OrderPopup(OrderViewModel order, bool isUpdate)
        {
            InitializeComponent();
            this.isUpdate = isUpdate;
            this.apiClient = App.serviceProvider.GetService<HttpClient>();
            if (isUpdate)
            {
                lvOrderDetails.ItemsSource = order.OrderDetails;
                btnRemoveDetail.IsEnabled = false;
                this.order = order;
            }
            else
            {
                btnAddDetail.Visibility = Visibility.Hidden;
                btnRemoveDetail.Visibility = Visibility.Hidden;
                lvOrderDetails.Visibility = Visibility.Hidden;
            }

            if (order != null)
            {
                txtOrderId.Text = order.OrderId.ToString();
                txtMemberId.Text = order.MemberId.ToString();
                txtOrderDate.Text = order.OrderDate.ToString();
                txtRequiredDate.Text = order.RequiredDate.ToString();
                txtShippedDate.Text = order.ShippedDate.ToString();
                txtFreight.Text = order.Freight.ToString();
            }

            txtOrderId.IsReadOnly = isUpdate;
            btnCreateOrder.Content = isUpdate ? "Update Order" : "Create Order";
        }

        private async void LoadOrderDetail()
        {
            try
            {
                var response = await apiClient.GetAsync($"order-detail/{order.OrderId}/order");
                var dataString = await response.Content.ReadAsStringAsync();
                var orderDetails =
                    JsonSerializer.Deserialize<IEnumerable<OrderDetailViewModel>>(dataString, jsonOptions);
                lvOrderDetails.ItemsSource = orderDetails;
                btnRemoveDetail.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Order Detail");
            }
        }

        private OrderViewModel GetOrderViewModel()
        {
            OrderViewModel order = null;
            try
            {
                order = new OrderViewModel()
                {
                    OrderId = int.Parse(txtOrderId.Text),
                    MemberId = int.Parse(txtMemberId.Text),
                    OrderDate = DateTime.Parse(txtOrderDate.Text),
                    RequiredDate = DateTime.Parse(txtRequiredDate.Text),
                    ShippedDate = DateTime.Parse(txtShippedDate.Text),
                    Freight = decimal.Parse(txtFreight.Text),
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Order");
            }

            return order;
        }

        private async void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = GetOrderViewModel();
            if (order != null)
            {
                try
                {
                    if (isUpdate)
                    {
                        var response = await apiClient.PutAsJsonAsync($"order/{order.OrderId}", new OrderCreateModel()
                        {
                            MemberId = order.MemberId,
                            Freight = order.Freight,
                            OrderDate = order.OrderDate,
                            RequiredDate = order.RequiredDate,
                            ShippedDate = order.ShippedDate,
                        });
                        Close();
                        MessageBox.Show("Information updated successfully", "Update Order");
                    }
                    else
                    {
                        var response = await apiClient.PostAsJsonAsync($"order", new OrderCreateModel()
                        {
                            MemberId = order.MemberId,
                            Freight = order.Freight,
                            OrderDate = order.OrderDate,
                            RequiredDate = order.RequiredDate,
                            ShippedDate = order.ShippedDate,
                        });
                        Close();
                        MessageBox.Show("Order created successfully", "Create Order");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, isUpdate ? "Update Order" : "Create Order");
                }
            }
            else
            {
                MessageBox.Show("Unknown error, please retry.", isUpdate ? "Update Order" : "Create Order");
            }
        }

        private void btnAddDetail_Click(object sender, RoutedEventArgs e)
        {
            OrderDetailPopup orderDetailPopup = new OrderDetailPopup(order);
            orderDetailPopup.ShowDialog();
            LoadOrderDetail();
        }

        private async void btnRemoveDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderDetailViewModel orderDetail = (OrderDetailViewModel)lvOrderDetails.SelectedItem;
                var response =
                    await apiClient.DeleteAsync(
                        $"order-detail?orderId={orderDetail.OrderId}&productId={orderDetail.ProductId}");
                LoadOrderDetail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Order Detail");
            }
        }

        private void lvOrderDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnRemoveDetail.IsEnabled = true;
        }
    }
}