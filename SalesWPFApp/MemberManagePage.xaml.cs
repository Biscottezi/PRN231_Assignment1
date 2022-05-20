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
    /// Interaction logic for MemberManagePage.xaml
    /// </summary>
    public partial class MemberManagePage : Page
    {
        private IMemberRepository memberRepository;
        public MemberManagePage(IMemberRepository memberRepository)
        {
            InitializeComponent();
            this.memberRepository = memberRepository;
            LoadMemberList();
            btnDelete.IsEnabled = false;
        }

        private void LoadMemberList()
        {
            try
            {
                lvMembers.ItemsSource = memberRepository.GetMemberList();
                btnDelete.IsEnabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Loading Member List");
            }
        }

        private void lvMembers_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MemberObject member = (MemberObject)lvMembers.SelectedItem;
            MemberPopup memberPopup = new MemberPopup(member, this.memberRepository, true);
            memberPopup.ShowDialog();
            LoadMemberList();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            MemberPopup memberPopup = new MemberPopup(null, this.memberRepository, false);
            memberPopup.ShowDialog();
            LoadMemberList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MemberObject member = (MemberObject)lvMembers.SelectedItem;
                memberRepository.DeleteMember(member);
                LoadMemberList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Deleting Member");
            }
        }

        private void lvMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = true;
        }
    }
}
