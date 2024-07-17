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
    /// Interaction logic for AddSupplier.xaml
    /// </summary>
    public partial class AddSupplier : Window
    {
        private readonly SupplierViewModel _viewModel;
        public AddSupplier(SupplierViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            Supplier newSupplier = new Supplier
            {
                SupplierName = "New Supplier" // Example initial data
            };
            _viewModel.AddSupplierCommand.Execute(newSupplier);
        }
        private void Button_Save(object sender, RoutedEventArgs e)
        {

            string txt_supplier_name = this.txt_supplier_name.Text;
            string txt_method = this.txt_method.Text;
            string txt_city = this.txt_city.Text;
            string txt_refernce = this.txt_refernce.Text;
            string txt_phone = this.txt_phone.Text;
            string txt_url = this.txt_url.Text;
            Supplier newSupplier = new Supplier
            {
                SupplierId = _viewModel.getMaxId() + 1,//chỗ này lấy ra id lớn nhất rồi +1 để thêm mới nhé
                SupplierName = txt_supplier_name,
                SupplierCategoryId = 2,
                DeliveryMethod = txt_method,
                DeliveryCity = txt_city,
                SupplierReference = txt_refernce,
                BankAccountName = "Contoso Ltd",
                BankAccountBranch = "Woodgrove Bank Zionsville",
                BankAccountCode = "356981",
                BankAccountNumber = "8575824136",
                BankInternationalCode = "25986",
                PaymentDays = 14,
                PhoneNumber = txt_phone,
                FaxNumber = "(847) 555-0101",
                WebsiteUrl = txt_url,
                DeliveryAddressLine1 = "Suite 10"

            };
            _viewModel.AddSupplierCommand.Execute(newSupplier);//đây là hàm thêm mới
            MessageBox.Show("Thêm mới thành công");
            this.Close();
            _viewModel.LoadSuppliersAsync();
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
