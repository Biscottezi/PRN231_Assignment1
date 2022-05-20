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
using Microsoft.Extensions.DependencyInjection;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for AminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        MemberManagePage memberManagePage;
        ProductManagePage productManagePage;
        OrderManagePage orderManagePage;
        public AdminWindow()
        {
            InitializeComponent();
            adminNavFrame.Content = this.memberManagePage;
        }

        private void btnMember_Click(object sender, RoutedEventArgs e)
        {
            adminNavFrame.Content = this.memberManagePage;
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            adminNavFrame.Content = this.productManagePage;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            adminNavFrame.Content = this.orderManagePage;
        }
    }
}
