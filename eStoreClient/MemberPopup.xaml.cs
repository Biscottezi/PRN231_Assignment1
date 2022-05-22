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
    /// Interaction logic for MemberPopup.xaml
    /// </summary>
    public partial class MemberPopup : Window
    {
        private readonly HttpClient apiClient;
        private bool isUpdate;

        public MemberPopup(MemberViewModel MemberViewModel, HttpClient apiClient, bool isUpdate)
        {
            InitializeComponent();
            this.apiClient = apiClient;
            this.isUpdate = isUpdate;
            if (MemberViewModel != null)
            {
                txtId.Text = MemberViewModel.MemberId.ToString();
                txtEmail.Text = MemberViewModel.Email;
                txtPassword.Text = MemberViewModel.Password;
                txtCompany.Text = MemberViewModel.CompanyName;
                txtCity.Text = MemberViewModel.City;
                txtCountry.Text = MemberViewModel.Country;
            }

            btnAction.Content = isUpdate ? "Update" : "Insert";
            txtId.IsReadOnly = isUpdate;
        }

        private MemberViewModel GetMember()
        {
            MemberViewModel member = null;
            try
            {
                member = new MemberViewModel
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

            return member;
        }

        private async void btnAction_Click(object sender, RoutedEventArgs e)
        {
            var member = GetMember();
            if (member != null)
            {
                try
                {
                    if (isUpdate)
                    {
                        var response = await apiClient.PutAsJsonAsync($"member/{member.MemberId}",
                            new MemberCreateModel()
                            {
                                MemberId = member.MemberId,
                                Email = member.Email,
                                Password = member.Password,
                                City = member.City,
                                Country = member.Country,
                                CompanyName = member.CompanyName,
                            });
                        if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            throw new Exception("Internal server error. Please retry.");
                        }

                        Close();
                        MessageBox.Show("Information updated successfully", "Update member");
                    }
                    else
                    {
                        var response = await apiClient.PostAsJsonAsync($"member", new MemberCreateModel()
                        {
                            MemberId = member.MemberId,
                            Email = member.Email,
                            Password = member.Password,
                            City = member.City,
                            Country = member.Country,
                            CompanyName = member.CompanyName,
                        });
                        if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            throw new Exception("Internal server error. Please retry.");
                        }

                        Close();
                        MessageBox.Show("Member created successfully", "Create member");
                    }
                }
                catch (Exception ex)
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