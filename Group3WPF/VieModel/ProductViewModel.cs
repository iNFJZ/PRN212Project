using Group3WPF.Models;
using Group3WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3WPF.VieModel
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _productService;
        private ObservableCollection<Product> _products;

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ProductViewModel(ProductService productService)
        {
            _productService = productService;
        }

        public  void LoadProductsAsync()
        {
            var products =  _productService.GetAllProductsAsync();
            Products = new ObservableCollection<Product>(products);
        }

        // Implement PropertyChanged event for data binding
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
