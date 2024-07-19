using Group3WPF.Models;
using Group3WPF.VieModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            int? supplierId = null;
            decimal? taxRate = null;
            decimal? unitPrice = null;
            decimal? recommendedRetailPrice = null;
            decimal? typicalWeightPerUnit = null;

            // TryParse cho các giá trị số
            if (int.TryParse(txt_supplier_id, out int parsedSupplierId))
            {
                supplierId = parsedSupplierId;
            }

            if (decimal.TryParse(txt_tax_rate, out decimal parsedTaxRate))
            {
                taxRate = parsedTaxRate;
            }

            if (decimal.TryParse(txt_unit_price, out decimal parsedUnitPrice))
            {
                unitPrice = parsedUnitPrice;
            }

            if (decimal.TryParse(txt_recommended_retail_price, out decimal parsedRecommendedRetailPrice))
            {
                recommendedRetailPrice = parsedRecommendedRetailPrice;
            }

            if (decimal.TryParse(txt_typical_weight_per_unit, out decimal parsedTypicalWeightPerUnit))
            {
                typicalWeightPerUnit = parsedTypicalWeightPerUnit;
            }

            Product newProduct = new Product
            {
                ProductId = _viewModel.getMaxId() + 1, // Get the max ID and increment it
                ProductName = txt_product_name,
                SupplierId = supplierId,
                Color = txt_color,
                PackageType = txt_package_type,
                Size = txt_size,
                TaxRate = taxRate,
                UnitPrice = unitPrice,
                RecommendedRetailPrice = recommendedRetailPrice,
                TypicalWeightPerUnit = typicalWeightPerUnit
            };

            _viewModel.AddProductCommand.Execute(newProduct); // Execute the command to add the product
            MessageBox.Show("New Product added successfully");
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
