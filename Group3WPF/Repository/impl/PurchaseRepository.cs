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

        public async Task DeletePurchaseOrderAsync(int purchaseOrderId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var purchaseOrder = await _context.PurchaseOrders.FindAsync(purchaseOrderId);

                if (purchaseOrder != null)
                {
                    var purchaseOrderLine = await _context.PurchaseOrderLines.Where(pol => pol.PurchaseOrderId == purchaseOrderId).ToListAsync();
                    _context.PurchaseOrderLines.RemoveRange(purchaseOrderLine);
                    
                    var supplierTransaction = await _context.SupplierTransactions.Where(st => st.PurchaseOrderId == purchaseOrderId).ToListAsync();
                    _context.SupplierTransactions.RemoveRange(supplierTransaction);
                    

                    await _context.SaveChangesAsync();

                    _context.PurchaseOrders.Remove(purchaseOrder);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
