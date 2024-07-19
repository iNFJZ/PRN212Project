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
    /// Interaction logic for UpdateSupplier.xaml
    /// </summary>
    public partial class UpdateSupplier : Window
    {
        private readonly SupplierViewModel _viewModel;
        public UpdateSupplier(SupplierViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            int id = int.Parse( this.txt_supplier_id.Text);
            Supplier selectedSupplier = _viewModel.FindById(id);

            string txt_supplier_name = this.txt_supplier_name.Text;
            string txt_method = this.txt_method.Text;
            string txt_city = this.txt_city.Text;
            string txt_refernce = this.txt_refernce.Text;
            string txt_phone = this.txt_phone.Text;
            string txt_url = this.txt_url.Text;
            string txt_addressline1 = this.txt_addressline1.Text;
            string txt_addressline2 = this.txt_addressline2.Text;
            string txt_postalcode = this.txt_postalcode.Text;
            if (selectedSupplier != null)
            {
                selectedSupplier.SupplierName = this.txt_supplier_name.Text;
                selectedSupplier.DeliveryMethod = this.txt_method.Text;
                selectedSupplier.DeliveryCity = txt_city;
                selectedSupplier.SupplierReference = txt_refernce;
                selectedSupplier.PhoneNumber = txt_phone;
                selectedSupplier.WebsiteUrl = txt_url;
                selectedSupplier.DeliveryAddressLine1 = txt_addressline1;
                selectedSupplier.DeliveryAddressLine2 = txt_addressline2;
                selectedSupplier.DeliveryPostalCode = txt_postalcode;
                _viewModel.UpdateSupplierCommand.Execute(selectedSupplier);
                MessageBox.Show("Supplier updated successfully.");
                this.Close();
                _viewModel.LoadSuppliersAsync();
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
