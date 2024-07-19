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
        private readonly PurchaseSevice _purchaseService;
        private ObservableCollection<PurchaseOrder> _purchaseOrder;
        private ObservableCollection<PurchaseOrderLine> _purchaseOrderLine;
        //private PurchaseSevice _purchase;
        public ObservableCollection<PurchaseOrder> PurchaseOrders
        {
            get { return _purchaseOrder; }
            set
            {
                _purchaseOrder = value;
                OnPropertyChanged(nameof(PurchaseOrders));
            }
        }

        public ObservableCollection<PurchaseOrderLine> PurchaseOrderLines
        {
            get { return _purchaseOrderLine; }
            set
            {
                _purchaseOrderLine = value;
                OnPropertyChanged(nameof(PurchaseOrderLines));
            }
        }

        public PurchaseViewModel(PurchaseSevice purchaseSevice)
        {
            _purchaseService = purchaseSevice;
        }

        public int getMaxId()
        {
            return _purchaseService.GetAllPurchaseOrdersAsync().Last().PurchaseOrderId;
        }
        public PurchaseOrder FindById(int id)
        {
            return _purchaseService.GetPurchaseOrderByIdAsync(id);
        }

        public void LoadPurchaseOrdersAsync()
        {
            var purchaseOrders = _purchaseService.GetAllPurchaseOrdersAsync();
            PurchaseOrders = new ObservableCollection<PurchaseOrder>(purchaseOrders);
        }
        
        public void LoadPurchaseLineAsync()
        {
            var purchaseLineOrders = _purchaseService.GetAllPurchaseOrderLinesAsync();
            PurchaseOrderLines = new ObservableCollection<PurchaseOrderLine>(purchaseLineOrders);
        }

        public void LoadPurchaseLineByOrderIdAsync(int purchaseId)
        {
            var purchaseLineOrders = _purchaseService.GetPurchaseOrderLineByPurchaseOrderIdAsync(purchaseId);
            PurchaseOrderLines = new ObservableCollection<PurchaseOrderLine>(purchaseLineOrders);
        }


        public ICommand AddPurchaseOrderCommand => new RelayCommand<PurchaseOrder>(async (purchaseOrder) =>
        {
            _purchaseService.CreatePurchaseOrderAsync(purchaseOrder);
            LoadPurchaseOrdersAsync();
        });

        public ICommand UpdatePurchaseOrderCommand => new RelayCommand<PurchaseOrder>(async (purchaseOrder) =>
        {
            _purchaseService.UpdatePurchaseOrderAsync(purchaseOrder);
            LoadPurchaseOrdersAsync();
        });

        public ICommand DeletePurchaseOrderCommand => new RelayCommand<int>(async (purchaseOrderId) =>
        {
            _purchaseService.DeletePurchaseOrderAsync(purchaseOrderId);
            LoadPurchaseOrdersAsync();
        });

        // Implement INotifyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
