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
    public class PurchaseViewModel : INotifyPropertyChanged
    {
        private readonly PurchaseSevice _purchase;
        private ObservableCollection<PurchaseOrder> _purchaseOrder;
        private PurchaseSevice _purchaseSevice;



        public ObservableCollection<PurchaseOrder> PurchaseOrders
        {
            get { return _purchaseOrder; }
            set
            {
                _purchaseOrder = value;
                OnPropertyChanged(nameof(PurchaseOrders));
            }
        }

        public PurchaseViewModel(PurchaseSevice purchaseSevice)
        {
            _purchaseSevice = purchaseSevice;
        }

        public void LoadAsync()
        {
            var purchaseOrders = _purchaseSevice.GetAllPurchaseOrdersAsync();
            PurchaseOrders = new ObservableCollection<PurchaseOrder>(purchaseOrders);
        }

        public ICommand AddPurchaseOrderCommand => new RelayCommand<PurchaseOrder>(async (purchaseOrder) =>
        {
            _purchaseSevice.CreatePurchaseOrderAsync(purchaseOrder);
            LoadAsync();
        });

        public ICommand UpdateCommand => new RelayCommand<PurchaseOrder>(async (purchaseOrder) =>
        {
            _purchaseSevice.UpdatePurchaseOrderAsync(purchaseOrder);
            LoadAsync();
        });

        public ICommand DeleteCommand => new RelayCommand<int>(async (Id) =>
        {
            _purchaseSevice.DeletePurchaseOrderAsync(Id);
            LoadAsync();
        });

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
