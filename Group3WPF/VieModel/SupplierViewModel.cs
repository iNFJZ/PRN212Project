using GalaSoft.MvvmLight.Command;
using Group3WPF.Models;
using Group3WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Group3WPF.VieModel
{
    public class SupplierViewModel : INotifyPropertyChanged
    {
        private readonly SupplierService _supplierService;
        private ObservableCollection<Supplier> _suppliers;
        public ObservableCollection<Supplier> Suppliers
        {
            get { return _suppliers; }
            set
            {
                _suppliers = value;
                OnPropertyChanged(nameof(Suppliers));
            }
        }

        public SupplierViewModel(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        public int getMaxId()
        {
            return _supplierService.GetAllSuppliersAsync().Last().SupplierId;
        }
        public Supplier FindById(int id)
        {
            return _supplierService.GetSupplierByIdAsync(id);
        }
        public  void LoadSuppliersAsync()
        {
            var suppliers =  _supplierService.GetAllSuppliersAsync();
            Suppliers = new ObservableCollection<Supplier>(suppliers);
        }

        public ICommand AddSupplierCommand => new RelayCommand<Supplier>(async (supplier) =>
        {
             _supplierService.CreateSupplierAsync(supplier);
             LoadSuppliersAsync();
        });

        public ICommand UpdateSupplierCommand => new RelayCommand<Supplier>(async (supplier) =>
        {
             _supplierService.UpdateSupplierAsync(supplier);
             LoadSuppliersAsync();
        });

        public ICommand DeleteSupplierCommand => new RelayCommand<int>(async (supplierId) =>
        {
             _supplierService.DeleteSupplierAsync(supplierId);
             LoadSuppliersAsync();
        });

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
