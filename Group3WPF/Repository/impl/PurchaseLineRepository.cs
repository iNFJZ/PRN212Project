using Group3WPF.Context;
using Group3WPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3WPF.Repository.impl
{
    public class PurchaseLineRepository
    {
        private readonly MyContext _context;

        public PurchaseLineRepository(MyContext context)
        {
            _context = context;
        }

        public List<PurchaseOrderLine> GetAllPurchaseOrderLinesAsync()
        {
            return _context.PurchaseOrderLines.ToList();
        }

        public PurchaseOrderLine GetPurchaseOrderLineByIdAsync(int purchaseOrderLineId)
        {
            return _context.PurchaseOrderLines.Find(purchaseOrderLineId);
        }

        public List<PurchaseOrderLine> GetPurchaseOrderLineByPurchaseOrderIdAsync(int purchaseOrderId)
        {
            return _context.PurchaseOrderLines.Where(s => s.PurchaseOrderId == purchaseOrderId).ToList();
        }

        public void CreatePurchaseOrderLineAsync(PurchaseOrderLine purchaseOrderLine)
        {
            _context.PurchaseOrderLines.Add(purchaseOrderLine);
            _context.SaveChangesAsync();
        }

        public void UpdatePurchaseOrderLineAsync(PurchaseOrderLine purchaseOrderLine)
        {
            _context.Entry(purchaseOrderLine).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }

        public void DeletePurchaseOrderLineAsync(int purchaseOrderLineId)
        {
            var purchaseOrderLine = _context.PurchaseOrderLines.Find(purchaseOrderLineId);
            if (purchaseOrderLine != null)
            {
                _context.PurchaseOrderLines.Remove(purchaseOrderLine);
                _context.SaveChangesAsync();
            }
        }

        public int GetProductOrderQuantity(string productName)
        {
            List<Product> list = _context.Products.Where(p => p.ProductName == productName).ToList();
            int quantity = 0;
            foreach (var product in list)
            {
                int id= product.ProductId;
                quantity += (int)_context.PurchaseOrderLines.Where(pol => pol.ProductId == id).Sum(pol => pol.OrderedQuantity);
            }
            return quantity;
        }
        public int GetProductOrderQuantity(int productId)
        {
            return (int)_context.PurchaseOrderLines.Where(pol => pol.ProductId == productId).Sum(pol => pol.OrderedQuantity) ;
        }


        //public List<Product> GetProductIdByName(string productName)
        //{
        //    return _context.Products.Where(p => p.ProductName == productName).ToList();
        //}

        public int GetAllProductOrderQuantity()
        {
            return (int)_context.PurchaseOrderLines.Sum(pol => pol.OrderedQuantity);
        }

        public float GetProductOrderQuantityPercantage(string productName)
        {
            return (float)(GetProductOrderQuantity(productName) / GetAllProductOrderQuantity() * 100);
        }
    }
}
