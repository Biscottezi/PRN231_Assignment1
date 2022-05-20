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
    /// Interaction logic for OrderPopup.xaml
    /// </summary>
    public partial class OrderPopup : Window
    {
        private IOrderRepository orderRepository;
        private IOrderDetailRepository orderDetailRepository;
        private bool isUpdate;
        OrderObject order = null;
        public OrderPopup(OrderObject order, bool isUpdate)
        {
            InitializeComponent();
            this.orderDetailRepository = App.serviceProvider.GetService<IOrderDetailRepository>();
            this.orderRepository = App.serviceProvider.GetService<IOrderRepository>();
            this.isUpdate = isUpdate;
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

        private void LoadOrderDetail()
        {
            try
            {
                lvOrderDetails.ItemsSource = orderDetailRepository.GetOrderDetailByOrderId(order.OrderId);
                btnRemoveDetail.IsEnabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Order Detail");
            }
        }

        private OrderObject GetOrderObject()
        {
            OrderObject order = null;
            try
            {
                order = new OrderObject()
                {
                    OrderId = int.Parse(txtOrderId.Text),
                    MemberId = int.Parse(txtMemberId.Text),
                    OrderDate = DateTime.Parse(txtOrderDate.Text),
                    RequiredDate = DateTime.Parse(txtRequiredDate.Text),
                    ShippedDate = DateTime.Parse(txtShippedDate.Text),
                    Freight = decimal.Parse(txtFreight.Text),
                };
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Get Order");
            }
            return order;
        }

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var orderObject = GetOrderObject();
            if (orderObject != null)
            {
                try
                {
                    if (isUpdate)
                    {
                        orderRepository.UpdateOrder(orderObject);
                        Close();
                        MessageBox.Show("Information updated successfully", "Update Order");
                    }
                    else
                    {
                        orderRepository.CreateOrder(orderObject);
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

        private void btnRemoveDetail_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderDetailObject orderDetail = (OrderDetailObject)lvOrderDetails.SelectedItem;
                orderDetailRepository.DeleteOrderDetail(orderDetail);
                LoadOrderDetail();
            }
            catch(Exception ex)
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
