using System;
using System.Collections.Generic;

namespace Group3WPF.Models
{
    public partial class SupplierCategory
    {
        public SupplierCategory()
        {
            Suppliers = new HashSet<Supplier>();
        }

        public int SupplierCategoryId { get; set; }
        public string? SupplierCategoryName { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
