using Group3WPF.Context;
using Group3WPF.Models;
using Group3WPF.Repository.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3WPF.Services
{
    public class PurchaseSevice
    {
        public List<PurchaseOrder> GetAllPurchaseOrdersAsync()
        {
            MyContext myContext = new MyContext();
            PurchaseRepository purchaseRepository  = new PurchaseRepository(myContext);
            return purchaseRepository.GetAllPurchaseOrdersAsync();
        }

        public PurchaseOrder GetPurchaseOrderByIdAsync(int purchaseId)
        {
            MyContext myContext = new MyContext();
            PurchaseRepository purchaseRepository = new PurchaseRepository(myContext);
            return purchaseRepository.GetPurchaseOrderByIdAsync(purchaseId);
        }

        public void CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            MyContext myContext = new MyContext();
            PurchaseRepository purchaseRepository = new PurchaseRepository(myContext);
            purchaseRepository.CreatePurchaseOrderAsync(purchaseOrder);
        }

        public void UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            MyContext myContext = new MyContext();
            PurchaseRepository purchaseRepository = new PurchaseRepository(myContext);
            purchaseRepository.UpdatePurchaseOrderAsync(purchaseOrder);
        }

        public void DeletePurchaseOrderAsync(int purchaseOrderId)
        {
            MyContext myContext = new MyContext();
            PurchaseRepository purchaseRepository = new PurchaseRepository(myContext);
            purchaseRepository.DeletePurchaseOrderAsync(purchaseOrderId);
        }

        //Purchase Order Line -- Detail
        public PurchaseOrderLine GetPurchaseOrderLineByIdAsync(int purchaseOrderLineId)
        {
            MyContext myContext = new MyContext();
            PurchaseLineRepository purchaseLineRepository = new PurchaseLineRepository(myContext);
            return purchaseLineRepository.GetPurchaseOrderLineByIdAsync(purchaseOrderLineId);
        }

        public List<PurchaseOrderLine> GetAllPurchaseOrderLinesAsync()
        {
            MyContext myContext = new MyContext();
            PurchaseLineRepository purchaseLineRepository = new PurchaseLineRepository(myContext);
            return purchaseLineRepository.GetAllPurchaseOrderLinesAsync();
        }

        public List<PurchaseOrderLine> GetPurchaseOrderLineByPurchaseOrderIdAsync(int purchaseId)
        {
            MyContext myContext = new MyContext();
            PurchaseLineRepository purchaseLineRepository = new PurchaseLineRepository(myContext);
            return purchaseLineRepository.GetPurchaseOrderLineByPurchaseOrderIdAsync(purchaseId);
        }
    }
}
