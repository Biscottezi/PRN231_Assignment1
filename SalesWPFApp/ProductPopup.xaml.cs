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

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for ProductPopup.xaml
    /// </summary>
    public partial class ProductPopup : Window
    {
        private IProductRepository productRepository;
        private bool isUpdate;

        public ProductPopup(ProductObject productObject, IProductRepository productRepository, bool isUpdate)
        {
            InitializeComponent();
            this.productRepository = productRepository;
            this.isUpdate = isUpdate;
            if(productObject != null)
            {
                txtProductId.Text = productObject.ProductId.ToString();
                txtCategoryId.Text = productObject.CategoryId.ToString();
                txtName.Text = productObject.ProductName;
                txtWeight.Text = productObject.Weight;
                txtPrice.Text = productObject.UnitPrice.ToString();
                txtStock.Text = productObject.UnitslnStock.ToString();
            }
            btnAction.Content = isUpdate ? "Update" : "Insert";
            txtProductId.IsReadOnly = isUpdate;
        }

        private ProductObject GetProduct()
        {
            ProductObject productObject = null;
            try
            {
                productObject = new ProductObject
                {
                    ProductId = int.Parse(txtProductId.Text),
                    CategoryId = int.Parse(txtCategoryId.Text),
                    ProductName = txtName.Text,
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtPrice.Text),
                    UnitslnStock = int.Parse(txtStock.Text),
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Getting Product");
            }
            return productObject;
        }

        private void btnAction_Click(object sender, RoutedEventArgs e)
        {
            var productObject = GetProduct();
            if (productObject != null)
            {
                try
                {
                    if (isUpdate)
                    {
                        productRepository.UpdateProduct(productObject);
                        Close();
                        MessageBox.Show("Information updated successfully", "Update Product");
                    }
                    else
                    {
                        productRepository.CreateProduct(productObject);
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
