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
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Window
    {
        private readonly ProductViewModel _viewModel;
        public UpdateProduct(ProductViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }
        private void Button_Update(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(this.txt_product_id.Text);
            Product selectedProduct = _viewModel.FindById(id);

            string txt_product_name = this.txt_product_name.Text;
            string txt_supplier_id = this.txt_supplier_id.Text;
            string txt_color = this.txt_color.Text;
            string txt_package_type = this.txt_package_type.Text;
            string txt_size = this.txt_size.Text;
            string txt_tax_rate = this.txt_tax_rate.Text;
            string txt_unit_price = this.txt_unit_price.Text;
            string txt_recommended_retail_price = this.txt_recommended_retail_price.Text;
            string txt_typical_weight_per_unit = this.txt_typical_weight_per_unit.Text;
            if (selectedProduct != null)
            {
                selectedProduct.ProductName = this.txt_product_name.Text;
                selectedProduct.SupplierId = string.IsNullOrEmpty(this.txt_supplier_id.Text) ? (int?)null : int.Parse(this.txt_supplier_id.Text);
                selectedProduct.Color = this.txt_color.Text;
                selectedProduct.PackageType = this.txt_package_type.Text;
                selectedProduct.Size = this.txt_size.Text;
                selectedProduct.TaxRate = string.IsNullOrEmpty(this.txt_tax_rate.Text) ? (decimal?)null : decimal.Parse(this.txt_tax_rate.Text);
                selectedProduct.UnitPrice = string.IsNullOrEmpty(this.txt_unit_price.Text) ? (decimal?)null : decimal.Parse(this.txt_unit_price.Text);
                selectedProduct.RecommendedRetailPrice = string.IsNullOrEmpty(this.txt_recommended_retail_price.Text) ? (decimal?)null : decimal.Parse(this.txt_recommended_retail_price.Text);
                selectedProduct.TypicalWeightPerUnit = string.IsNullOrEmpty(this.txt_typical_weight_per_unit.Text) ? (decimal?)null : decimal.Parse(this.txt_typical_weight_per_unit.Text);

                _viewModel.UpdateProductCommand.Execute(selectedProduct);

                MessageBox.Show("Product updated successfully");
                this.Close();
                _viewModel.LoadProductsAsync();
            }
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
