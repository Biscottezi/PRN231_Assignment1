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
    /// Interaction logic for MemberPopup.xaml
    /// </summary>
    public partial class MemberPopup : Window
    {
        private IMemberRepository memberRepository;
        private bool isUpdate;
        public MemberPopup(MemberObject memberObject, IMemberRepository memberRepository, bool isUpdate)
        {
            InitializeComponent();
            this.memberRepository = memberRepository;
            this.isUpdate = isUpdate;
            if(memberObject != null)
            {
                txtId.Text = memberObject.MemberId.ToString();
                txtEmail.Text = memberObject.Email;
                txtPassword.Text = memberObject.Password;
                txtCompany.Text = memberObject.CompanyName;
                txtCity.Text = memberObject.City;
                txtCountry.Text = memberObject.Country;
            }
            btnAction.Content = isUpdate ? "Update" : "Insert";
            txtId.IsReadOnly = isUpdate;
        }

        private MemberObject GetMember()
        {
            MemberObject memberObject = null;
            try
            {
                memberObject = new MemberObject
                {
                    MemberId = int.Parse(txtId.Text),
                    Email = txtEmail.Text,
                    CompanyName = txtCompany.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Getting Member");
            }
            return memberObject;
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            var memberObject = GetMember();
            if(memberObject != null)
            {
                try
                {
                    if (isUpdate)
                    {
                        memberRepository.UpdateMember(memberObject);
                        Close();
                        MessageBox.Show("Information updated successfully", "Update member");
                    }
                    else
                    {
                        memberRepository.CreateMember(memberObject);
                        Close();
                        MessageBox.Show("Member created successfully", "Create member");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, isUpdate ? "Update member" : "Create member");
                }
            }
            else
            {
                MessageBox.Show("Unknown error, please retry.", isUpdate ? "Update member" : "Create member");
            }
        }
    }
}
