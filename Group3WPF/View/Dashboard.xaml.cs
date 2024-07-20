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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            PurchaseViewModel viewModel = new PurchaseViewModel(new PurchaseSevice());
            PurchaseOrderView view = new PurchaseOrderView(viewModel);
            view.Show();
           
        }

        private void Product_Click(object sender, RoutedEventArgs e)
        {
            ProductViewModel productViewModel = new ProductViewModel(new ProductService());
            ProductView productView = new ProductView(productViewModel);
            productView.Show();
           
        }

        private void Category_Click(object sender, RoutedEventArgs e)
        {
            SupplierCategoryService categoryService = new SupplierCategoryService();

            // Create an instance of the ViewModel and pass the service to it
            SupplierCategoryViewModel viewModel = new SupplierCategoryViewModel(categoryService);

            // Create an instance of the SupplierCategoriesView and pass the ViewModel to it
            SupplierCategoriesView view = new SupplierCategoriesView(viewModel);

            // Show the SupplierCategoriesView
            view.Show();

        }

        private void Supplies_Click(object sender, RoutedEventArgs e)
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel(new SupplierService());
            SupplierView supplierView  = new SupplierView(supplierViewModel);
            supplierView.Show();
           
        }

        private void Static_Click(object sender, RoutedEventArgs e)
        {
            StaticViewModel staticViewModel = new StaticViewModel(new PurchaseSevice());
            StaticView view = new StaticView(staticViewModel);
            view.Show();
        }
    }
}
