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
    /// Interaction logic for PurchaseOrderView.xaml
    /// </summary>
    public partial class PurchaseOrderView : Window
    {
        private readonly PurchaseViewModel _viewModel;
        public PurchaseOrderView(PurchaseViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += PurchaseView_Loaded; //
        }



        private void PurchaseView_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadAsync();
        }

        //private void AddSupplier_Click(object sender, RoutedEventArgs e)
        //{
        //    // Implement logic to add a new supplier
        //    Supplier newSupplier = new Supplier
        //    {
        //        SupplierName = "New Supplier" // Example initial data
        //    };
        //    _viewModel.AddPurchaseOrderCommand.Execute(newSupplier);
        //}

        //private void UpdateSupplier_Click(object sender, RoutedEventArgs e)
        //{
        //    // Implement logic to update the selected supplier
        //    Supplier selectedSupplier = (Supplier)dataGrid.SelectedItem;
        //    if (selectedSupplier != null)
        //    {
        //        // Update properties of selectedSupplier as needed
        //        _viewModel.UpdateSupplierCommand.Execute(selectedSupplier);
        //    }
        //}

        //private void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        //{
        //    // Implement logic to delete the selected supplier
        //    Supplier selectedSupplier = (Supplier)dataGrid.SelectedItem;
        //    if (selectedSupplier != null)
        //    {
        //        _viewModel.DeleteSupplierCommand.Execute(selectedSupplier.SupplierId);
        //    }
        //}
        private void txtFilterBy_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var searchText = txtFilterBy.Text.ToLower();
            var query = _viewModel.PurchaseOrders.Where(p =>
                (p.Supplier != null && p.Supplier.SupplierName != null && p.Supplier.SupplierName.ToLower().Contains(searchText)) ||
                (p.DeliveryMethod != null && p.DeliveryMethod.ToLower().Contains(searchText)) ||
                (p.SupplierReference != null && p.SupplierReference.ToLower().Contains(searchText)) ||
                (p.OrderDate.ToString().ToLower().Contains(searchText)) ||
                (p.ExpectedDeliveryDate != null && p.ExpectedDeliveryDate.Value.ToString().ToLower().Contains(searchText)) ||
                (p.IsOrderFinalized.ToString().ToLower().Contains(searchText))
            ).ToList();

            grdPurchaseOrder.ItemsSource = query;
        }

    }
}
