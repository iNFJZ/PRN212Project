
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

        public async Task DeleteProductAsync(int productId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var product = await _context.Products
                    .Include(p => p.PurchaseOrderLines) // Đảm bảo rằng tất cả các liên kết cần thiết được tải
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product != null)
                {
                    // Xóa tất cả các dòng đơn đặt hàng liên quan đến sản phẩm
                    var purchaseOrderLines = await _context.PurchaseOrderLines
                        .Where(pol => pol.ProductId == productId)
                        .ToListAsync();
                    _context.PurchaseOrderLines.RemoveRange(purchaseOrderLines);

                    // Xóa sản phẩm
                    _context.Products.Remove(product);

                    // Lưu các thay đổi vào cơ sở dữ liệu
                    await _context.SaveChangesAsync();

                    // Cam kết giao dịch
                    await transaction.CommitAsync();
                }
                else
                {
                    throw new Exception("Product not found.");
                }
            }
            catch (Exception ex)
            {
                // Rollback giao dịch trong trường hợp có lỗi
                await transaction.RollbackAsync();
                // Ghi lại hoặc xử lý lỗi nếu cần
                throw new Exception("Error occurred while deleting product", ex);
            }
        }

    }
}
