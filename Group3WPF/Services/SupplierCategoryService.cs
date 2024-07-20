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
    public class SupplierCategoryService
    {
        public List<SupplierCategory> GetAllSupplierCategoriesAsync()
        {
            MyContext myContext = new MyContext();
            SupplierCategoryRepository categoryRepository = new SupplierCategoryRepository(myContext);
            return categoryRepository.GetAllSupplierCategoriesAsync();
        }
    }
}
