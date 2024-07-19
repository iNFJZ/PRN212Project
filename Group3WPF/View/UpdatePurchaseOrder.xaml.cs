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
    public partial class UpdatePurchaseOrder : Window
    {
        private readonly PurchaseViewModel _viewModel;

        public UpdatePurchaseOrder(PurchaseViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txt_purchase_order_id.Text);
            PurchaseOrder selectedOrder = _viewModel.FindById(id);

            int supplierId = int.Parse(txt_supplier_id.Text);
            DateTime? dp_order_date = this.dp_order_date.SelectedDate;
            string txt_method = this.txt_method.Text;
            DateTime? dp_expected_delivery = this.dp_expected_delivery.SelectedDate;
            string txt_reference = this.txt_reference.Text;
            bool chk_is_finalized = this.chk_is_finalized.IsChecked ?? false;
            DateTime orderDate = dp_order_date.HasValue ? dp_order_date.Value : DateTime.Now;
            DateTime expectedDeliveryDate = dp_expected_delivery.HasValue ? dp_expected_delivery.Value : DateTime.Now;
            if (selectedOrder != null)
            {
                selectedOrder.SupplierId = int.Parse(txt_supplier_id.Text);
                selectedOrder.OrderDate = orderDate;
                selectedOrder.DeliveryMethod = txt_method;
                selectedOrder.ExpectedDeliveryDate = expectedDeliveryDate;
                selectedOrder.SupplierReference = txt_reference;
                selectedOrder.IsOrderFinalized = chk_is_finalized;

                _viewModel.UpdatePurchaseOrderCommand.Execute(selectedOrder);
                MessageBox.Show("Purchase Order updated successfully.");
                this.Close();
                _viewModel.LoadPurchaseOrdersAsync();
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
