using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLogic;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for ProductManagePage.xaml
    /// </summary>
    public partial class ProductManagePage : Page
    {
        private readonly HttpClient apiClient;

        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        public ProductManagePage()
        {
            InitializeComponent();
            this.apiClient = App.serviceProvider.GetService<HttpClient>();
            btnDelete.IsEnabled = false;
            LoadProductList();
        }

        private async void LoadProductList()
        {
            try
            {
                var response = await apiClient.GetAsync("product");
                var dataString = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<IEnumerable<ProductViewModel>>(dataString, jsonOptions);
                lvProducts.ItemsSource = products;
                btnDelete.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Loading Products");
            }
        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProductViewModel product = (ProductViewModel)lvProducts.SelectedItem;
                var response = await apiClient.DeleteAsync($"product/{product.ProductId}");
                LoadProductList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Deleting Product");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            ProductPopup productPopup = new ProductPopup(null, apiClient, false);
            productPopup.ShowDialog();
            LoadProductList();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            String keyword = txtKeyword.Text;
            decimal price = 0;
            if (txtPrice.Text.Length != 0)
            {
                price = decimal.Parse(txtPrice.Text);
            }

            var response = await apiClient.GetAsync($"product?ProductName={keyword}&UnitPrice={price}");
            var dataString = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<IEnumerable<ProductViewModel>>(dataString, jsonOptions);
            lvProducts.ItemsSource = products;
        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = true;
        }

        private void lvProducts_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductViewModel product = (ProductViewModel)lvProducts.SelectedItem;
            ProductPopup productPopup = new ProductPopup(product, apiClient, true);
            productPopup.ShowDialog();
            LoadProductList();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadProductList();
        }
    }
}