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
    public class SupplierService 
    {
    

        public List<Supplier> GetAllSuppliersAsync()
        {
            MyContext myContext = new MyContext();
            SupplierRepository _productRepository = new SupplierRepository(myContext);
            return _productRepository.GetAllSuppliersAsync();
        }

        public Supplier GetSupplierByIdAsync(int supplierId)
        {
            MyContext myContext = new MyContext();
            SupplierRepository _productRepository = new SupplierRepository(myContext);
            return _productRepository.GetSupplierByIdAsync(supplierId);
        }

        public void CreateSupplierAsync(Supplier supplier)
        {
            MyContext myContext = new MyContext();
            SupplierRepository _productRepository = new SupplierRepository(myContext);
            _productRepository.CreateSupplierAsync(supplier);
        }

        public void UpdateSupplierAsync(Supplier supplier)
        {
            MyContext myContext = new MyContext();
            SupplierRepository _productRepository = new SupplierRepository(myContext);
            _productRepository.UpdateSupplierAsync(supplier);
        }

        public void DeleteSupplierAsync(int supplierId)
        {
            MyContext myContext = new MyContext();
            SupplierRepository _productRepository = new SupplierRepository(myContext);
            _productRepository.DeleteSupplierAsync(supplierId);
        }
    }
}
