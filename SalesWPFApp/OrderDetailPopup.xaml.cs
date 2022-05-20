using System;
using System.Collections.Generic;
using System.Linq;
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
using BusinessObject;
using DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for OrderDetailPopup.xaml
    /// </summary>
    public partial class OrderDetailPopup : Window
    {
        private IOrderDetailRepository orderDetailRepository;
        private OrderObject order;
        public OrderDetailPopup(OrderObject order)
        {
            InitializeComponent();
            this.orderDetailRepository = App.serviceProvider.GetService<IOrderDetailRepository>();
            this.order = order;
            txtOrderId.Text = order.OrderId.ToString();
            txtOrderId.IsReadOnly = true;
        }

        private OrderDetailObject GetOrderDetail()
        {
            OrderDetailObject orderDetail = null;
            try
            {
                orderDetail = new OrderDetailObject()
                {
                    OrderId = int.Parse(txtOrderId.Text),
                    ProductId = int.Parse(txtProductId.Text),
                    Quantity = int.Parse(txtQuantity.Text),
                    UnitPrice = decimal.Parse(txtPrice.Text),
                    Discount = double.Parse(txtDiscount.Text),
                };
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Order Detail");
            }
            return orderDetail;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var orderDetail = GetOrderDetail();
            if(orderDetail != null)
            {
                try
                {
                    orderDetailRepository.CreateOrderDetail(orderDetail);
                    Close();
                    MessageBox.Show("Order Detail created successfully", "Create Order Detail");
                }
                catch(Exception ex)
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
