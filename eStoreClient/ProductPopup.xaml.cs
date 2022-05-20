using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for ProductPopup.xaml
    /// </summary>
    public partial class ProductPopup : Window
    {
        private HttpClient apiClient;
        private bool isUpdate;

        public ProductPopup(ProductViewModel ProductViewModel, HttpClient apiClient, bool isUpdate)
        {
            InitializeComponent();
            this.apiClient = apiClient;
            this.isUpdate = isUpdate;
            if (ProductViewModel != null)
            {
                txtProductId.Text = ProductViewModel.ProductId.ToString();
                txtCategoryId.Text = ProductViewModel.CategoryId.ToString();
                txtName.Text = ProductViewModel.ProductName;
                txtWeight.Text = ProductViewModel.Weight;
                txtPrice.Text = ProductViewModel.UnitPrice.ToString();
                txtStock.Text = ProductViewModel.UnitsInStock.ToString();
            }

            btnAction.Content = isUpdate ? "Update" : "Insert";
            txtProductId.IsReadOnly = isUpdate;
        }

        private ProductViewModel GetProduct()
        {
            ProductViewModel product = null;
            try
            {
                product = new ProductViewModel
                {
                    ProductId = int.Parse(txtProductId.Text),
                    CategoryId = int.Parse(txtCategoryId.Text),
                    ProductName = txtName.Text,
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtPrice.Text),
                    UnitsInStock = int.Parse(txtStock.Text),
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Getting Product");
            }

            return product;
        }

        private async void btnAction_Click(object sender, RoutedEventArgs e)
        {
            var product = GetProduct();
            if (product != null)
            {
                try
                {
                    if (isUpdate)
                    {
                        var response = await apiClient.PutAsJsonAsync($"product/{product.ProductId}",
                            new ProductCreateModel()
                            {
                                ProductName = product.ProductName,
                                Weight = product.Weight,
                                CategoryId = product.CategoryId,
                                UnitPrice = product.UnitPrice,
                                UnitsInStock = product.UnitsInStock,
                            });
                        Close();
                        MessageBox.Show("Information updated successfully", "Update Product");
                    }
                    else
                    {
                        var response = await apiClient.PostAsJsonAsync("product", new ProductCreateModel()
                        {
                            ProductName = product.ProductName,
                            Weight = product.Weight,
                            CategoryId = product.CategoryId,
                            UnitPrice = product.UnitPrice,
                            UnitsInStock = product.UnitsInStock,
                        });
                        Close();
                        MessageBox.Show("Product created successfully", "Create Product");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, isUpdate ? "Update Product" : "Create Product");
                }
            }
            else
            {
                MessageBox.Show("Unknown error, please retry.", isUpdate ? "Update Product" : "Create Product");
            }
        }
    }
}