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
    }
}
