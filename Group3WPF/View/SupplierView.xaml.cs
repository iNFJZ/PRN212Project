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

        private void SupplierView_Loaded(object sender, RoutedEventArgs e)
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

        private void txtFilterBy_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var searchText = txtFilterBy.Text.ToLower();
            var query = _viewModel.Suppliers.Where(s =>
                (s.SupplierName != null && s.SupplierName.ToLower().Contains(searchText)) ||
                (s.DeliveryMethod != null && s.DeliveryMethod.ToLower().Contains(searchText)) ||
                (s.DeliveryCity != null && s.DeliveryCity.ToLower().Contains(searchText)) ||
                (s.SupplierReference != null && s.SupplierReference.ToLower().Contains(searchText)) ||
                (s.BankAccountName != null && s.BankAccountName.ToLower().Contains(searchText)) ||
                (s.BankAccountBranch != null && s.BankAccountBranch.ToLower().Contains(searchText)) ||
                (s.BankAccountCode != null && s.BankAccountCode.ToLower().Contains(searchText)) ||
                (s.BankAccountNumber != null && s.BankAccountNumber.ToLower().Contains(searchText)) ||
                (s.BankInternationalCode != null && s.BankInternationalCode.ToLower().Contains(searchText)) ||
                (s.PhoneNumber != null && s.PhoneNumber.ToLower().Contains(searchText)) ||
                (s.FaxNumber != null && s.FaxNumber.ToLower().Contains(searchText)) ||
                (s.WebsiteUrl != null && s.WebsiteUrl.ToLower().Contains(searchText)) ||
                (s.DeliveryAddressLine1 != null && s.DeliveryAddressLine1.ToLower().Contains(searchText)) ||
                (s.DeliveryAddressLine2 != null && s.DeliveryAddressLine2.ToLower().Contains(searchText)) ||
                (s.DeliveryPostalCode != null && s.DeliveryPostalCode.ToLower().Contains(searchText))
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
                    List<Supplier> suppliers = _viewModel.Suppliers.ToList();
                    suppliers.ForEach(x => x.SupplierCategory = null);
                    List<string> lines = new List<string>();
                    foreach (var supplier in suppliers)
                    {
                        string line = $"{supplier.SupplierId},{supplier.SupplierName},{supplier.SupplierCategoryId},{supplier.DeliveryMethod},{supplier.DeliveryCity},{supplier.SupplierReference},{supplier.BankAccountName},{supplier.BankAccountBranch},{supplier.BankAccountCode},{supplier.BankAccountNumber},{supplier.BankInternationalCode},{supplier.PaymentDays},{supplier.PhoneNumber},{supplier.FaxNumber},{supplier.WebsiteUrl},{supplier.DeliveryAddressLine1},{supplier.DeliveryAddressLine2},{supplier.DeliveryPostalCode}";
                        lines.Add(line);
                    }
                    File.WriteAllLines(saveFileDialog.FileName, lines);
                    MessageBox.Show("Save as TXT successfully", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (selectedExtension == ".json")
                {
                    // Save as JSON
                    List<Supplier> suppliers = _viewModel.Suppliers.ToList();
                    suppliers.ForEach(x => x.SupplierCategory = null);

                    var jsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                    };

                    string jsonContent = JsonSerializer.Serialize(suppliers, jsonOptions);
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
                    List<Supplier> suppliers = new List<Supplier>();
                    foreach (var line in lines)
                    {
                        string[] parts = line.Split(',');
                        Supplier supplier = new Supplier
                        {
                            SupplierId = int.Parse(parts[0]),
                            SupplierName = parts[1],
                            SupplierCategoryId = string.IsNullOrEmpty(parts[2]) ? (int?)null : int.Parse(parts[2]),
                            DeliveryMethod = parts[3],
                            DeliveryCity = parts[4],
                            SupplierReference = parts[5],
                            BankAccountName = parts[6],
                            BankAccountBranch = parts[7],
                            BankAccountCode = parts[8],
                            BankAccountNumber = parts[9],
                            BankInternationalCode = parts[10],
                            PaymentDays = string.IsNullOrEmpty(parts[11]) ? (int?)null : int.Parse(parts[11]),
                            PhoneNumber = parts[12],
                            FaxNumber = parts[13],
                            WebsiteUrl = parts[14],
                            DeliveryAddressLine1 = parts[15],
                            DeliveryAddressLine2 = parts[16],
                            DeliveryPostalCode = parts[17]
                        };

                        suppliers.Add(supplier);
                    }

                    dataGrid.ItemsSource = suppliers;
                }
                else if (selectedExtension == ".json")
                {
                    // Load from JSON
                    string jsonContent = File.ReadAllText(openFileDialog.FileName);
                    List<Supplier> suppliers = JsonSerializer.Deserialize<List<Supplier>>(jsonContent);
                    dataGrid.ItemsSource = suppliers;
                }
                else
                {
                    MessageBox.Show("Unsupported file format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
