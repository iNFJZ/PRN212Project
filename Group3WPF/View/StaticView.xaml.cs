using Group3WPF.Models;
using Group3WPF.Services;
using Group3WPF.VieModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for StaticView.xaml
    /// </summary>
    public partial class StaticView : Window
    {
        private readonly StaticViewModel _viewModel;
        public StaticView(StaticViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += PurchaseView_Loaded;
            loadProduct();
            loadDelivery();
        }



        private void PurchaseView_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadPurchaseOrdersAsync();
        }

        private void loadProduct()
        {
            var pList = _viewModel.GetAllProductName();
            pList.ForEach(name => cboProduct.Items.Add(name));
            cboProduct.SelectedIndex = 0;
            prePro();
        }

        private void loadDelivery()
        {
            var dList = _viewModel.GetDeliveryMethodLst();
            dList.ForEach(method => cboDelivary.Items.Add(method));
            cboDelivary.SelectedIndex = 0;
            preDev();
        }

        private string[] ParseCsvLine(string line)
        {
            // Implement CSV line parsing with support for fields containing commas
            // This is a simple implementation; consider using a library for robust CSV parsing
            var sb = new StringBuilder();
            bool insideQuotes = false;
            List<string> fields = new List<string>();

            foreach (char c in line)
            {
                if (c == '"')
                {
                    insideQuotes = !insideQuotes;
                }
                else if (c == ',' && !insideQuotes)
                {
                    fields.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(c);
                }
            }
            fields.Add(sb.ToString());

            return fields.ToArray();
        }

        private void cboProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            prePro();
        }

        private void cboDelivary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            preDev();
        }

        private void preDev()
        {
            string method = cboDelivary.SelectedValue.ToString();
            txtNumber.Text = _viewModel.CountDelivery(method).ToString();
            txtDPercentage.Text = ((float)(_viewModel.CountDelivery(method) / (float)_viewModel.CountAllDelivery())*100).ToString();
        }

        private void prePro()
        {
            string pName = cboProduct.SelectedValue.ToString();
            txtQuantity.Text = _viewModel.GetProductOrderQuantity(pName).ToString();
            txtSPercentage.Text = ((float)_viewModel.GetProductOrderQuantity(pName)/(float)_viewModel.GetAllProductOrderQuantity()*100).ToString();
        }
    }
}
