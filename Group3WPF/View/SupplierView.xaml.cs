using Group3WPF.Context;
using Group3WPF.Models;
using Group3WPF.Repository.impl;
using Group3WPF.Services;
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
    /// Interaction logic for SupplierView.xaml
    /// </summary>
    public partial class SupplierView : Window
    {
        private readonly SupplierViewModel _viewModel;
        public SupplierView(SupplierViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += SupplierView_Loaded; //
        }

        private  void SupplierView_Loaded(object sender, RoutedEventArgs e)
        {
             _viewModel.LoadSuppliersAsync();
        }

        private void AddSupplier_Click(object sender, RoutedEventArgs e)
        {
            AddSupplier addSupplier = new AddSupplier(_viewModel);
            addSupplier.Show();

        }

        private void UpdateSupplier_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to update the selected supplier
            Supplier selectedSupplier = (Supplier)dataGrid.SelectedItem;
            if (selectedSupplier != null)
            {
                UpdateSupplier updateSupplier = new UpdateSupplier(_viewModel);
                updateSupplier.DataContext = selectedSupplier;
                updateSupplier.ShowDialog();

                //_viewModel.UpdateSupplierCommand.Execute(selectedSupplier);
            }
        }

        private void DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            //// Implement logic to delete the selected supplier
            Supplier selectedSupplier = (Supplier)dataGrid.SelectedItem;
            if (selectedSupplier != null)
            {
                _viewModel.DeleteSupplierCommand.Execute(selectedSupplier.SupplierId);
            }
        }
    }
}
