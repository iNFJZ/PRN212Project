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
    /// Interaction logic for PurchaseOrderDetail.xaml
    /// </summary>
    public partial class PurchaseOrderDetail : Window
    {
        private readonly PurchaseViewModel _viewModel;
        public int PurchaseOrderId { get; set; }
        public PurchaseOrderDetail(PurchaseViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += PurchaseDetailView_Loaded;
        }

        private void PurchaseDetailView_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadPurchaseLineByOrderIdAsync(PurchaseOrderId);
        }
    }
}
