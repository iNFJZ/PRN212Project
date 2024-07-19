using Group3WPF.Context;
using Group3WPF.Models;
using Group3WPF.Repository.impl;
using Group3WPF.Services;
using Group3WPF.VieModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            _viewModel.LoadPurchaseOrdersAsync();
        }

        private void AddPurchaseOrder_Click(object sender, RoutedEventArgs e)
        {
            AddPurchaseOrder addPurchaseOrder = new AddPurchaseOrder(_viewModel);
            addPurchaseOrder.Show();
        }

        private void UpdatePurchaseOrder_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to update the selected supplier
            PurchaseOrder selectedPurchaseOrder = (PurchaseOrder)dataGrid.SelectedItem;
            if (selectedPurchaseOrder != null)
            {
                UpdatePurchaseOrder updatePurchaseOrder = new UpdatePurchaseOrder(_viewModel);
                updatePurchaseOrder.DataContext = selectedPurchaseOrder;
                updatePurchaseOrder.ShowDialog();

                //_viewModel.UpdateSupplierCommand.Execute(selectedSupplier);
            }
        }

        private void OrderDetail_Click(object sender, RoutedEventArgs e)
        {
            PurchaseOrder selectedOrder = (PurchaseOrder)dataGrid.SelectedItem;
            if (selectedOrder != null)
            {
                int id = selectedOrder.PurchaseOrderId;
                PurchaseViewModel viewModel = new PurchaseViewModel(new PurchaseSevice());
                PurchaseOrderDetail viewDetail = new PurchaseOrderDetail(viewModel); // Pass ID as argument
                viewDetail.PurchaseOrderId = id;
                viewDetail.Show();
            }
        }

        private void DeletePurchaseOrder_Click(object sender, RoutedEventArgs e)
        {
            //// Implement logic to delete the selected supplier
            PurchaseOrder selectedPurchaseOrder = (PurchaseOrder)dataGrid.SelectedItem;
            if (selectedPurchaseOrder != null)
            {
                _viewModel.DeletePurchaseOrderCommand.Execute(selectedPurchaseOrder.SupplierId);
            }
        }
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

            dataGrid.ItemsSource = query;
        }
        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|JSON Files (*.json)|*.json|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                string selectedExtension = System.IO.Path.GetExtension(saveFileDialog.FileName).ToLower();

                if (selectedExtension == ".txt")
                {
                    // Save as TXT
                    List<PurchaseOrder> purchaseOrders = _viewModel.PurchaseOrders.ToList();
                    List<string> lines = new List<string>();
                    foreach (var order in purchaseOrders)
                    {
                        string[] fields = new string[]
                        {
                    order.PurchaseOrderId.ToString(),
                    order.SupplierId?.ToString() ?? "",
                    order.OrderDate.ToString("o"), // Using round-trip format
                    order.DeliveryMethod,
                    order.ExpectedDeliveryDate?.ToString("o") ?? "",
                    order.SupplierReference,
                    order.IsOrderFinalized.ToString()
                        };

                        // Escape any fields containing commas
                        for (int i = 0; i < fields.Length; i++)
                        {
                            if (fields[i].Contains(","))
                            {
                                fields[i] = $"\"{fields[i]}\"";
                            }
                        }

                        string line = string.Join(",", fields);
                        lines.Add(line);
                    }
                    File.WriteAllLines(saveFileDialog.FileName, lines);
                    MessageBox.Show("Save as TXT successfully", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (selectedExtension == ".json")
                {
                    // Save as JSON
                    List<PurchaseOrder> purchaseOrders = _viewModel.PurchaseOrders.ToList();

                    var jsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                    };

                    string jsonContent = JsonSerializer.Serialize(purchaseOrders, jsonOptions);
                    File.WriteAllText(saveFileDialog.FileName, jsonContent);
                    MessageBox.Show("Save as JSON successfully", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Unsupported file format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }





        private void btnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|JSON Files (*.json)|*.json|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedExtension = System.IO.Path.GetExtension(openFileDialog.FileName).ToLower();

                if (selectedExtension == ".txt")
                {
                    // Load from TXT
                    string[] lines = File.ReadAllLines(openFileDialog.FileName);
                    List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();
                    foreach (var line in lines)
                    {
                        string[] parts = ParseCsvLine(line);
                        if (parts.Length != 7)
                        {
                            MessageBox.Show("Invalid line format in the file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        PurchaseOrder order = new PurchaseOrder
                        {
                            PurchaseOrderId = int.Parse(parts[0]),
                            SupplierId = string.IsNullOrEmpty(parts[1]) ? (int?)null : int.Parse(parts[1]),
                            OrderDate = DateTime.Parse(parts[2]),
                            DeliveryMethod = parts[3],
                            ExpectedDeliveryDate = string.IsNullOrEmpty(parts[4]) ? (DateTime?)null : DateTime.Parse(parts[4]),
                            SupplierReference = parts[5],
                            IsOrderFinalized = bool.Parse(parts[6])
                        };

                        purchaseOrders.Add(order);
                    }

                    dataGrid.ItemsSource = purchaseOrders;
                }
                else if (selectedExtension == ".json")
                {
                    // Load from JSON
                    string jsonContent = File.ReadAllText(openFileDialog.FileName);
                    List<PurchaseOrder> purchaseOrders = JsonSerializer.Deserialize<List<PurchaseOrder>>(jsonContent);
                    dataGrid.ItemsSource = purchaseOrders;
                }
                else
                {
                    MessageBox.Show("Unsupported file format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
    }
}
