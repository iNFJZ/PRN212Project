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

    }
}
