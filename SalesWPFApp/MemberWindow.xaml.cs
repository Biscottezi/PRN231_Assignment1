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
    /// Interaction logic for MemberWindow.xaml
    /// </summary>
    public partial class MemberWindow : Window
    {
        private IOrderRepository orderRepository;
        MemberObject member;
        public MemberWindow(MemberObject memberObject, IOrderRepository orderRepository)
        {
            InitializeComponent();
            this.orderRepository = orderRepository;
            this.member = memberObject;
            MapMemberInfo(memberObject);
        }

        private void lvOrderHistory_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OrderObject order = (OrderObject)lvOrderHistory.SelectedItem;
            lvOrderDetail.ItemsSource = order.OrderDetails;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MemberPopup memberPopup = new MemberPopup(this.member, App.serviceProvider.GetService<IMemberRepository>(), true);
            _ = memberPopup.ShowDialog();
            this.member = App.serviceProvider.GetService<IMemberRepository>().GetMemberById(member.MemberId);
            MapMemberInfo(member);
        }

        private void MapMemberInfo(MemberObject memberObject)
        {
            lbId.Content = memberObject.MemberId.ToString();
            lbEmail.Content = memberObject.Email;
            lbPassword.Content = memberObject.Password;
            lbCompany.Content = memberObject.CompanyName;
            lbCity.Content = memberObject.City;
            lbCountry.Content = memberObject.Country;
            lvOrderHistory.ItemsSource = orderRepository.GetOrdersByMemberId(memberObject.MemberId);
        }
    }
}
