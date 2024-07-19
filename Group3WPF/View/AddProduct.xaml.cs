using Group3WPF.Models;
using Group3WPF.VieModel;
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

namespace Group3WPF.View
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        private readonly ProductViewModel _viewModel;

        public AddProduct(ProductViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Product newProduct = new Product
            {
                ProductName = "New Product" // Example initial data
            };
            _viewModel.AddProductCommand.Execute(newProduct);
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            string txt_product_name = this.txt_product_name.Text;
            string txt_supplier_id = this.txt_supplier_id.Text;
            string txt_color = this.txt_color.Text;
            string txt_package_type = this.txt_package_type.Text;
            string txt_size = this.txt_size.Text;
            string txt_tax_rate = this.txt_tax_rate.Text;
            string txt_unit_price = this.txt_unit_price.Text;
            string txt_recommended_retail_price = this.txt_recommended_retail_price.Text;
            string txt_typical_weight_per_unit = this.txt_typical_weight_per_unit.Text;

            Product newProduct = new Product
            {
                ProductId = _viewModel.getMaxId() + 1, // Get the max ID and increment it
                ProductName = txt_product_name,
                SupplierId = string.IsNullOrEmpty(txt_supplier_id) ? (int?)null : int.Parse(txt_supplier_id),
                Color = txt_color,
                PackageType = txt_package_type,
                Size = txt_size,
                TaxRate = string.IsNullOrEmpty(txt_tax_rate) ? (decimal?)null : decimal.Parse(txt_tax_rate),
                UnitPrice = string.IsNullOrEmpty(txt_unit_price) ? (decimal?)null : decimal.Parse(txt_unit_price),
                RecommendedRetailPrice = string.IsNullOrEmpty(txt_recommended_retail_price) ? (decimal?)null : decimal.Parse(txt_recommended_retail_price),
                TypicalWeightPerUnit = string.IsNullOrEmpty(txt_typical_weight_per_unit) ? (decimal?)null : decimal.Parse(txt_typical_weight_per_unit)
            };

            _viewModel.AddProductCommand.Execute(newProduct); // Execute the command to add the product
            MessageBox.Show("Product added successfully");
            this.Close();
            _viewModel.LoadProductsAsync();
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumberOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }

        private static bool IsTextNumeric(string text)
        {
            return text.All(char.IsDigit);
        }
    }
}
