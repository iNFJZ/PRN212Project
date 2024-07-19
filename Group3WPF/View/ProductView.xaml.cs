using Group3WPF.Models;
using Group3WPF.Services;
using Group3WPF.VieModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : Window
    {
        private readonly ProductViewModel _viewModel;
        public ProductView(ProductViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += ProductView_Loaded; // Subscribe to Loaded event
        }

        private  void ProductView_Loaded(object sender, RoutedEventArgs e)
        {
             _viewModel.LoadProductsAsync();
        }

        private void txtFilterBy_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var searchText = txtFilterBy.Text.ToLower();
            var query = _viewModel.Products.Where(p =>
                (p.ProductName != null && p.ProductName.ToLower().Contains(searchText)) ||
                (p.Color != null && p.Color.ToLower().Contains(searchText)) ||
                (p.PackageType != null && p.PackageType.ToLower().Contains(searchText)) ||
                (p.Size != null && p.Size.ToLower().Contains(searchText)) ||
                (p.UnitPrice != null && p.UnitPrice.ToString().ToLower().Contains(searchText)) ||
                (p.RecommendedRetailPrice != null && p.RecommendedRetailPrice.ToString().ToLower().Contains(searchText)) ||
                (p.TypicalWeightPerUnit != null && p.TypicalWeightPerUnit.ToString().ToLower().Contains(searchText)) ||
                (p.Supplier != null && p.Supplier.SupplierName != null && p.Supplier.SupplierName.ToLower().Contains(searchText))
            // Add more fields if needed
            ).ToList();

            grdProduct.ItemsSource = query;
        }


    }
}
