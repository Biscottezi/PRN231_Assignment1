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
    /// Interaction logic for ProductManagePage.xaml
    /// </summary>
    public partial class ProductManagePage : Page
    {
        private IProductRepository productRepository;
        public ProductManagePage(IProductRepository productRepository)
        {
            InitializeComponent();
            this.productRepository = productRepository;
            btnDelete.IsEnabled = false;
            LoadProductList();
        }

        private void LoadProductList()
        {
            try
            {
                lvProducts.ItemsSource = productRepository.GetProductList();
                btnDelete.IsEnabled = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Loading Products");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProductObject product = (ProductObject)lvProducts.SelectedItem;
                productRepository.DeleteProduct(product);
                LoadProductList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Deleting Product");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            ProductPopup productPopup = new ProductPopup(null, this.productRepository, false);
            productPopup.ShowDialog();
            LoadProductList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string? keyword = null;
            int? id = null;
            decimal? price = null;
            int? stock = null;

            if(txtId.Text.Length != 0)
            {
                id = int.Parse(txtId.Text);
            }
            if (txtKeyword.Text.Length != 0)
            {
                keyword = txtKeyword.Text;
            }
            if (txtPrice.Text.Length != 0)
            {
                price = decimal.Parse(txtPrice.Text);
            }
            if (txtStock.Text.Length != 0)
            {
                stock = int.Parse(txtStock.Text);
            }

            lvProducts.ItemsSource = productRepository.SearchProduct(keyword, id, price, stock);
        }

        private void lvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = true;
        }

        private void lvProducts_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ProductObject productObject = (ProductObject)lvProducts.SelectedItem;
            ProductPopup productPopup = new ProductPopup(productObject, this.productRepository, true);
            productPopup.ShowDialog();
            LoadProductList();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadProductList();
        }
    }
}
