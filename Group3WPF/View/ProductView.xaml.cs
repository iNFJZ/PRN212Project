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
            Loaded += ProductView_Loaded;
        }

        private void ProductView_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadProductsAsync();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct(_viewModel);
            addProduct.Show();
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to update the selected product
            Product selectedProduct = (Product)grdProduct.SelectedItem;
            if (selectedProduct != null)
            {
                UpdateProduct updateProduct = new UpdateProduct(_viewModel);
                updateProduct.DataContext = selectedProduct;
                updateProduct.ShowDialog();

                //_viewModel.UpdateProductCommand.Execute(selectedProduct);
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            // Implement logic to delete the selected product
            Product selectedProduct = (Product)grdProduct.SelectedItem;
            if (selectedProduct != null)
            {
                _viewModel.DeleteProductCommand.Execute(selectedProduct.ProductId);
            }
        }

        private void txtFilterBy_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var searchText = txtFilterBy.Text.ToLower();
            var query = _viewModel.Products.Where(p =>
                (p.ProductName != null && p.ProductName.ToLower().Contains(searchText)) ||
                (p.Color != null && p.Color.ToLower().Contains(searchText)) ||
                (p.PackageType != null && p.PackageType.ToLower().Contains(searchText)) ||
                (p.Size != null && p.Size.ToLower().Contains(searchText)) ||
                (p.UnitPrice.HasValue && p.UnitPrice.Value.ToString().Contains(searchText)) ||
                (p.RecommendedRetailPrice.HasValue && p.RecommendedRetailPrice.Value.ToString().Contains(searchText)) ||
                (p.TaxRate.HasValue && p.TaxRate.Value.ToString().Contains(searchText)) ||
                (p.TypicalWeightPerUnit.HasValue && p.TypicalWeightPerUnit.Value.ToString().Contains(searchText))
            ).ToList();

            grdProduct.ItemsSource = query;
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
                    List<Product> products = _viewModel.Products.ToList();
                    List<string> lines = new List<string>();
                    foreach (var product in products)
                    {
                        string[] fields = new string[]
                        {
                    product.ProductId.ToString(),
                    product.ProductName.Contains(",") ? $"\"{product.ProductName}\"" : product.ProductName,
                    product.SupplierId?.ToString() ?? "",
                    product.Color,
                    product.PackageType,
                    product.Size,
                    product.TaxRate?.ToString() ?? "",
                    product.UnitPrice?.ToString() ?? "",
                    product.RecommendedRetailPrice?.ToString() ?? "",
                    product.TypicalWeightPerUnit?.ToString() ?? ""
                        };

                        string line = string.Join(",", fields);
                        lines.Add(line);
                    }
                    File.WriteAllLines(saveFileDialog.FileName, lines);
                    MessageBox.Show("Save as TXT successfully", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (selectedExtension == ".json")
                {
                    // Save as JSON
                    List<Product> products = _viewModel.Products.ToList();
                    products.ForEach(x => x.Supplier = null);

                    var jsonOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                    };

                    string jsonContent = JsonSerializer.Serialize(products, jsonOptions);
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
                    List<Product> products = new List<Product>();
                    foreach (var line in lines)
                    {
                        string[] parts = ParseCsvLine(line);
                        if (parts.Length != 10)
                        {
                            MessageBox.Show("Invalid line format in the file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        Product product = new Product
                        {
                            ProductId = int.Parse(parts[0]),
                            ProductName = parts[1],
                            SupplierId = string.IsNullOrEmpty(parts[2]) ? (int?)null : int.Parse(parts[2]),
                            Color = parts[3],
                            PackageType = parts[4],
                            Size = parts[5],
                            TaxRate = string.IsNullOrEmpty(parts[6]) ? (decimal?)null : decimal.Parse(parts[6]),
                            UnitPrice = string.IsNullOrEmpty(parts[7]) ? (decimal?)null : decimal.Parse(parts[7]),
                            RecommendedRetailPrice = string.IsNullOrEmpty(parts[8]) ? (decimal?)null : decimal.Parse(parts[8]),
                            TypicalWeightPerUnit = string.IsNullOrEmpty(parts[9]) ? (decimal?)null : decimal.Parse(parts[9])
                        };

                        products.Add(product);
                    }

                    grdProduct.ItemsSource = products;
                }
                else if (selectedExtension == ".json")
                {
                    // Load from JSON
                    string jsonContent = File.ReadAllText(openFileDialog.FileName);
                    List<Product> products = JsonSerializer.Deserialize<List<Product>>(jsonContent);
                    grdProduct.ItemsSource = products;
                }
                else
                {
                    MessageBox.Show("Unsupported file format", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private string[] ParseCsvLine(string line)
        {
            List<string> fields = new List<string>();
            StringBuilder field = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char currentChar = line[i];

                if (inQuotes)
                {
                    if (currentChar == '\"')
                    {
                        // Check if it's a double quote
                        if (i < line.Length - 1 && line[i + 1] == '\"')
                        {
                            field.Append('\"');
                            i++; // Skip the next character
                        }
                        else
                        {
                            inQuotes = false;
                        }
                    }
                    else
                    {
                        field.Append(currentChar);
                    }
                }
                else
                {
                    if (currentChar == '\"')
                    {
                        inQuotes = true;
                    }
                    else if (currentChar == ',')
                    {
                        fields.Add(field.ToString());
                        field.Clear();
                    }
                    else
                    {
                        field.Append(currentChar);
                    }
                }
            }

            // Add the last field
            fields.Add(field.ToString());

            return fields.ToArray();
        }
    }
}
