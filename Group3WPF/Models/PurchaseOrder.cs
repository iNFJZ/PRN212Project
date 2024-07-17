using System;
using System.Collections.Generic;

namespace Group3WPF.Models
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            PurchaseOrderLines = new HashSet<PurchaseOrderLine>();
            SupplierTransactions = new HashSet<SupplierTransaction>();
        }

        public int PurchaseOrderId { get; set; }
        public int? SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? DeliveryMethod { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public string? SupplierReference { get; set; }
        public bool IsOrderFinalized { get; set; }

        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<PurchaseOrderLine> PurchaseOrderLines { get; set; }
        public virtual ICollection<SupplierTransaction> SupplierTransactions { get; set; }
    }
}
