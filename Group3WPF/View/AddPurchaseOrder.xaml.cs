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
    /// Interaction logic for AddPurchaseOrder.xaml
    /// </summary>
    public partial class AddPurchaseOrder : Window
    {
        private readonly PurchaseViewModel _viewModel;

        public AddPurchaseOrder(PurchaseViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            int supplierId = int.Parse(txt_supplier_id.Text);
            DateTime? dp_order_date = this.dp_order_date.SelectedDate;
            string txt_method = this.txt_method.Text;
            DateTime? dp_expected_delivery = this.dp_expected_delivery.SelectedDate;
            string txt_reference = this.txt_reference.Text;
            bool chk_is_finalized = this.chk_is_finalized.IsChecked ?? false;

            DateTime orderDate = dp_order_date.HasValue ? dp_order_date.Value : DateTime.Now;
            DateTime expectedDeliveryDate = dp_expected_delivery.HasValue ? dp_expected_delivery.Value : DateTime.Now;
            // Create a new PurchaseOrder instance
            PurchaseOrder newOrder = new PurchaseOrder
            {
                PurchaseOrderId = _viewModel.getMaxId() + 1, // Assuming GetMaxId() fetches the highest current ID
                SupplierId = supplierId,
                OrderDate = orderDate,
                DeliveryMethod = txt_method,
                ExpectedDeliveryDate = expectedDeliveryDate,
                SupplierReference = txt_reference,
                IsOrderFinalized = chk_is_finalized
            };
            _viewModel.AddPurchaseOrderCommand.Execute(newOrder);
            MessageBox.Show("New Purchase Order added successfully.");
            this.Close();
            _viewModel.LoadPurchaseOrdersAsync();
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
