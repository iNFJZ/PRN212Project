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
    /// Interaction logic for SupplierCategoriesView.xaml
    /// </summary>
    public partial class SupplierCategoriesView : Window
    {
        private readonly SupplierCategoryViewModel _viewModel;

        public SupplierCategoriesView(SupplierCategoryViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += CategoryView_Loaded; //
        }

        private void CategoryView_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.CategorysAsync();
        }

    

        private void dataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtFilterBy_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = txtFilterBy.Text.ToLower();
            var filteredCategories = _viewModel.SupplierCategories
                .Where(c => c.SupplierCategoryName.ToLower().Contains(searchText))
                .ToList();
            dataGrid.ItemsSource = filteredCategories;
        }
    }
}
