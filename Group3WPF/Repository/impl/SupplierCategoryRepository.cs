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
    public class SupplierCategoryRepository
    {
       
            private readonly MyContext _context;

            public SupplierCategoryRepository(MyContext context)
            {
                _context = context;
            }

            public List<SupplierCategory> GetAllSupplierCategoriesAsync()
            {
                return _context.SupplierCategories.ToList();
            }
        
    }
}
