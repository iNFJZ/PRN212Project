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

        public void DeleteSupplierAsync(int supplierId)
        {
            var supplier =  _context.Suppliers.Find(supplierId);
            if (supplier != null)
            {
                _context.Suppliers.Remove(supplier);
                 _context.SaveChangesAsync();
            }
        }
    }
}
