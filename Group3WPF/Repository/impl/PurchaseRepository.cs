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
    public class PurchaseRepository
    {
        private readonly MyContext _context;

        public PurchaseRepository(MyContext context)
        {
            _context = context;
        }

        public List<PurchaseOrder> GetAllPurchaseOrdersAsync()
        {
            return _context.PurchaseOrders.ToList();
        }

        public PurchaseOrder GetPurchaseOrderByIdAsync(int supplierId)
        {
            return _context.PurchaseOrders.Find(supplierId);
        }

        public void CreatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            _context.PurchaseOrders.Add(purchaseOrder);
            _context.SaveChangesAsync();
        }

        public void UpdatePurchaseOrderAsync(PurchaseOrder purchaseOrder)
        {
            _context.Entry(purchaseOrder).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }

        public void DeletePurchaseOrderAsync(int purchaseOrderId)
        {
            var purchaseOrder = _context.PurchaseOrders.Find(purchaseOrderId);
            if (purchaseOrder != null)
            {
                _context.PurchaseOrders.Remove(purchaseOrder);
                _context.SaveChangesAsync();
            }
        }
    }
}
