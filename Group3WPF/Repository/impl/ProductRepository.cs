
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

        public void AddProductAsync(Product product)
        {
            _context.Products.Add(product);
             _context.SaveChanges();
        }

        public void  UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
             _context.SaveChanges();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
