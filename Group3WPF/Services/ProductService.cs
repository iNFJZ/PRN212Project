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
    public class ProductService
    {
        public  Product GetProductByIdAsync(int productId)
        {
            MyContext myContext = new MyContext();
            ProductRepository _productRepository = new ProductRepository(myContext);
            return  _productRepository.GetProductByIdAsync(productId);
        }

        public  List<Product> GetAllProductsAsync()
        {
            MyContext myContext = new MyContext();
            ProductRepository _productRepository = new ProductRepository(myContext);
            return  _productRepository.GetAllProductsAsync();
        }

        public void CreateProductAsync(Product product)
        {
            MyContext myContext = new MyContext();
            ProductRepository _productRepository = new ProductRepository(myContext);
            _productRepository.CreateProductAsync(product);
        }

        public void UpdateProductAsync(Product product)
        {
            MyContext myContext = new MyContext();
            ProductRepository _productRepository = new ProductRepository(myContext);
            _productRepository.UpdateProductAsync(product);
        }

        public void DeleteProductAsync(int productId)
        {
            MyContext myContext = new MyContext();
            ProductRepository _productRepository = new ProductRepository(myContext);
            _productRepository.DeleteProductAsync(productId);
        }
    }
}
