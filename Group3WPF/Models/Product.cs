using System;
using System.Collections.Generic;

namespace Group3WPF.Models
{
    public partial class Product
    {
        public Product()
        {
            PurchaseOrderLines = new HashSet<PurchaseOrderLine>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public string? Color { get; set; }
        public string? PackageType { get; set; }
        public string? Size { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? RecommendedRetailPrice { get; set; }
        public decimal? TypicalWeightPerUnit { get; set; }

        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<PurchaseOrderLine> PurchaseOrderLines { get; set; }
    }
}
