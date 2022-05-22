using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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
    /// Interaction logic for OrderDetailPopup.xaml
    /// </summary>
    public partial class OrderDetailPopup : Window
    {
        private HttpClient apiClient;
        private OrderViewModel order;

        public OrderDetailPopup(OrderViewModel order)
        {
            InitializeComponent();
            this.apiClient = App.serviceProvider.GetService<HttpClient>();
            this.order = order;
            txtOrderId.Text = order.OrderId.ToString();
            txtOrderId.IsReadOnly = true;
        }

        private OrderDetailViewModel GetOrderDetail()
        {
            OrderDetailViewModel orderDetail = null;
            try
            {
                orderDetail = new OrderDetailViewModel()
                {
                    OrderId = int.Parse(txtOrderId.Text),
                    ProductId = int.Parse(txtProductId.Text),
                    Quantity = int.Parse(txtQuantity.Text),
                    UnitPrice = decimal.Parse(txtPrice.Text),
                    Discount = double.Parse(txtDiscount.Text),
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Order Detail");
            }

            return orderDetail;
        }

        private async void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var orderDetail = GetOrderDetail();
            if (orderDetail != null)
            {
                try
                {
                    var response = await apiClient.PostAsJsonAsync("order-detail", new OrderDetailCreateModel()
                    {
                        Discount = orderDetail.Discount,
                        Quantity = orderDetail.Quantity,
                        OrderId = orderDetail.OrderId,
                        ProductId = orderDetail.ProductId,
                        UnitPrice = orderDetail.UnitPrice,
                    });
                    Close();
                    if (response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        throw new Exception("Internal server error. Please retry.");
                    }

                    MessageBox.Show("Order Detail created successfully", "Create Order Detail");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Create Order Detail");
                }
            }
            else
            {
                MessageBox.Show("Unknown error, please retry.", "Create Order Detail");
            }
        }
    }
}