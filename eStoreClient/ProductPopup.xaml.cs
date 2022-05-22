using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
        private readonly HttpClient apiClient;
        private readonly bool isUpdate;
        private List<CategoryViewModel> categories;

        private readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        public ProductPopup(ProductViewModel product, HttpClient apiClient, bool isUpdate)
        {
            InitializeComponent();
            this.apiClient = apiClient;
            this.isUpdate = isUpdate;
            btnAction.Content = isUpdate ? "Update" : "Insert";
            txtProductId.IsReadOnly = isUpdate;
            GetCategories(product);
        }

        private async void GetCategories(ProductViewModel product)
        {
            var response = await apiClient.GetAsync("category");
            var dataString = await response.Content.ReadAsStringAsync();
            var categories = JsonSerializer.Deserialize<IEnumerable<CategoryViewModel>>(dataString, jsonOptions);
            cbCategoryId.ItemsSource = categories;
            cbCategoryId.DisplayMemberPath = "CategoryName";
            if (product != null)
            {
                txtProductId.Text = product.ProductId.ToString();
                txtName.Text = product.ProductName;
                txtWeight.Text = product.Weight;
                txtPrice.Text = product.UnitPrice.ToString();
                txtStock.Text = product.UnitsInStock.ToString();
                cbCategoryId.SelectedItem = categories.Single(c => c.CategoryId == product.CategoryId);
            }
        }

        private ProductViewModel GetProduct()
        {
            ProductViewModel product = null;
            try
            {
                product = new ProductViewModel
                {
                    ProductId = int.Parse(txtProductId.Text),
                    CategoryId = (cbCategoryId.SelectionBoxItem as CategoryViewModel).CategoryId,
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
                                ProductId = product.ProductId,
                                ProductName = product.ProductName,
                                Weight = product.Weight,
                                CategoryId = product.CategoryId,
                                UnitPrice = product.UnitPrice,
                                UnitsInStock = product.UnitsInStock,
                            });
                        if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            throw new Exception("Internal server error. Please retry.");
                        }

                        Close();
                        MessageBox.Show("Information updated successfully", "Update Product");
                    }
                    else
                    {
                        var response = await apiClient.PostAsJsonAsync("product", new ProductCreateModel()
                        {
                            ProductId = product.ProductId,
                            ProductName = product.ProductName,
                            Weight = product.Weight,
                            CategoryId = product.CategoryId,
                            UnitPrice = product.UnitPrice,
                            UnitsInStock = product.UnitsInStock,
                        });
                        if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            throw new Exception("Internal server error. Please retry.");
                        }

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