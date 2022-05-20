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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObject;
using DataAccess.Repository;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for OrderManagePage.xaml
    /// </summary>
    public partial class OrderManagePage : Page
    {
        private IOrderRepository orderRepository;
        public OrderManagePage(IOrderRepository orderRepository)
        {
            InitializeComponent();
            this.orderRepository = orderRepository;
            btnDelete.IsEnabled = false;
            LoadOrderList();
        }

        private void LoadOrderList()
        {
            try
            {
                lvOrders.ItemsSource = orderRepository.GetOrderList();
                btnDelete.IsEnabled = false;
                lvOrderDetails.ItemsSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loading Order List");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OrderObject order = (OrderObject)lvOrders.SelectedItem;
                orderRepository.DeleteOrder(order);
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
            OrderObject order = (OrderObject)lvOrders.SelectedItem;
            OrderPopup orderPopup = new OrderPopup(order, true);
            orderPopup.ShowDialog();
            LoadOrderList();
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            LoadOrderList();
        }

        private void btn_Sort_Click(object sender, RoutedEventArgs e)
        {
            if (dp_start.SelectedDate == null || dp_end.SelectedDate == null)
            {
                MessageBox.Show("Choose a start date and end date");
                return;
            }
            DateTime start = (DateTime)dp_start.SelectedDate;
            DateTime end = (DateTime)dp_end.SelectedDate;
            lvOrders.ItemsSource = orderRepository.GetOrdersByDate(start, end);
        }

        private void lvOrders_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OrderObject order = (OrderObject)lvOrders.SelectedItem;
            if(order != null)
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
