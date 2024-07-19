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
    internal class SupplierRepository
    {
        private readonly MyContext _context;

        public SupplierRepository(MyContext context)
        {
            _context = context;
        }

        public  List<Supplier> GetAllSuppliersAsync()
        {
            return  _context.Suppliers.ToList();
        }

        public Supplier GetSupplierByIdAsync(int supplierId)
        {
            return _context.Suppliers.Find(supplierId);
        }

        public void CreateSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
             _context.SaveChangesAsync();
        }

        public  void UpdateSupplierAsync(Supplier supplier)
        {
            _context.Entry(supplier).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }

        public async Task DeleteSupplierAsync(int supplierId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var supplier = await _context.Suppliers.FindAsync(supplierId);
                if (supplier != null)
                {
                    var supplierTransactions = await _context.SupplierTransactions.Where(st => st.SupplierId == supplierId).ToListAsync();
                    _context.SupplierTransactions.RemoveRange(supplierTransactions);

                    // Cập nhật SupplierId cho tất cả các sản phẩm liên quan đến nhà cung cấp này
                    var products = await _context.Products.Where(p => p.SupplierId == supplierId).ToListAsync();
                    products.ForEach(p => p.SupplierId = null);

                    // Cập nhật SupplierId cho tất cả các đơn đặt hàng liên quan đến nhà cung cấp này
                    var purchaseOrders = await _context.PurchaseOrders.Where(po => po.SupplierId == supplierId).ToListAsync();
                    purchaseOrders.ForEach(po => po.SupplierId = null);

                    await _context.SaveChangesAsync();

                    // Xóa nhà cung cấp
                    _context.Suppliers.Remove(supplier);

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
