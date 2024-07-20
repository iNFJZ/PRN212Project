using Group3WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Group3WPF.Services;

namespace Group3WPF.VieModel
{

    public class SupplierCategoryViewModel : INotifyPropertyChanged
    {
        private readonly SupplierCategoryService _categoryService;
        private ObservableCollection<SupplierCategory> _supplierCategories;

        public ObservableCollection<SupplierCategory> SupplierCategories
        {
            get { return _supplierCategories; }
            set
            {
                _supplierCategories = value;
                OnPropertyChanged(nameof(SupplierCategories));
            }
        }

        public SupplierCategoryViewModel(SupplierCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public void CategorysAsync()
        {
            var categories = _categoryService.GetAllSupplierCategoriesAsync();
            SupplierCategories = new ObservableCollection<SupplierCategory>(categories);
        }

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
