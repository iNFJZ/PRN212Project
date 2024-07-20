using Group3WPF.Context;
using Group3WPF.Models;
using Group3WPF.Repository.impl;
using Microsoft.EntityFrameworkCore;
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

        public async Task DeletePurchaseOrderAsync(int purchaseOrderId)
        {
            using (var myContext = new MyContext())
            {
                PurchaseRepository _purchaseOrderRepository = new PurchaseRepository(myContext);
                await _purchaseOrderRepository.DeletePurchaseOrderAsync(purchaseOrderId);
            }
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

        // Static Management
        //Delivery
        public int CountDelivery(string deliveryMethod)
        {
            MyContext myContext = new MyContext();
            PurchaseRepository purchaseRepository = new PurchaseRepository(myContext);
            return purchaseRepository.CountDelivery(deliveryMethod);
        }

        public float CountDeliveryPercentage(string deliveryMethod)
        {
            MyContext myContext = new MyContext();
            PurchaseRepository purchaseRepository = new PurchaseRepository(myContext);
            return purchaseRepository.CountDeliveryPercentage(deliveryMethod);
        }
        public List<string> GetDeliveryMethodLst()
        {
            MyContext myContext = new MyContext();
            PurchaseRepository purchaseRepository = new PurchaseRepository(myContext);
            return purchaseRepository.GetDeliveryMethodLst();
        }

        public int CountAllDelivery()
        {
            MyContext myContext = new MyContext();
            PurchaseRepository purchaseRepository = new PurchaseRepository(myContext);
            return purchaseRepository.CountAllDelivery();
        }
        //Product
        public int GetProductOrderQuantity(string productName)
        {
            MyContext myContext = new MyContext();
            PurchaseLineRepository purchaseLineRepository = new PurchaseLineRepository(myContext);
            return purchaseLineRepository.GetProductOrderQuantity(productName);
        }

        public int GetProductOrderQuantity(int productId)
        {
            MyContext myContext = new MyContext();
            PurchaseLineRepository purchaseLineRepository = new PurchaseLineRepository(myContext);
            return purchaseLineRepository.GetProductOrderQuantity(productId);
        }
        //public int GetProductIdByName(string productName)
        //{
        //    MyContext myContext = new MyContext();
        //    PurchaseLineRepository purchaseLineRepository = new PurchaseLineRepository(myContext);
        //    return purchaseLineRepository.GetProductIdByName(productName);
        //}

        public int GetAllProductOrderQuantity()
        {
            MyContext myContext = new MyContext();
            PurchaseLineRepository purchaseLineRepository = new PurchaseLineRepository(myContext);
            return purchaseLineRepository.GetAllProductOrderQuantity();
        }
        public float GetProductOrderQuantityPercentage(string productName)
        {
            MyContext myContext = new MyContext();
            PurchaseLineRepository purchaseLineRepository = new PurchaseLineRepository(myContext);
            return purchaseLineRepository.GetProductOrderQuantityPercantage(productName);
        }

        public List<string> GetAllProductName()
        {
            MyContext myContext = new MyContext();
            ProductRepository _productRepository = new ProductRepository(myContext);
            return _productRepository.GetAllProductName();
        }
    }
}
