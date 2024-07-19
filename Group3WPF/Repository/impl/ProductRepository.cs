
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
    internal class ProductRepository
    {
        private readonly MyContext _context;

        public ProductRepository(MyContext context)
        {
            _context = context;
        }

        public  Product GetProductByIdAsync(int productId)
        {
            return  _context.Products.Find(productId);
        }

        public  List<Product> GetAllProductsAsync()
        {
            return  _context.Products.ToList();
        }

        public void CreateProductAsync(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChangesAsync(); //hàm save dữ liệu vào đb 
        }

        public void UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }

        public void DeleteProductAsync(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChangesAsync();
            }
        }
    }
}
