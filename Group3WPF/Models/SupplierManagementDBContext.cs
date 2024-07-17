using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Group3WPF.Models
{
    public partial class SupplierManagementDBContext : DbContext
    {
        public SupplierManagementDBContext()
        {
        }

        public SupplierManagementDBContext(DbContextOptions<SupplierManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountMember> AccountMembers { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; } = null!;
        public virtual DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<SupplierCategory> SupplierCategories { get; set; } = null!;
        public virtual DbSet<SupplierTransaction> SupplierTransactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=SupplierManagementDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountMember>(entity =>
            {
                entity.ToTable("AccountMember");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password")
                    .IsFixedLength();

                entity.Property(e => e.Role)
                    .HasMaxLength(10)
                    .HasColumnName("role")
                    .IsFixedLength();

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductID");

                entity.Property(e => e.Color).HasMaxLength(20);

                entity.Property(e => e.PackageType).HasMaxLength(50);

                entity.Property(e => e.ProductName).HasMaxLength(100);

                entity.Property(e => e.RecommendedRetailPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Size).HasMaxLength(20);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.TaxRate).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.TypicalWeightPerUnit).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK__Products__Suppli__412EB0B6");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.Property(e => e.PurchaseOrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("PurchaseOrderID");

                entity.Property(e => e.DeliveryMethod).HasMaxLength(50);

                entity.Property(e => e.ExpectedDeliveryDate).HasColumnType("date");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.SupplierReference).HasMaxLength(20);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PurchaseOrders)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK__PurchaseO__Suppl__440B1D61");
            });

            modelBuilder.Entity<PurchaseOrderLine>(entity =>
            {
                entity.Property(e => e.PurchaseOrderLineId)
                    .ValueGeneratedNever()
                    .HasColumnName("PurchaseOrderLineID");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.LastReceiptDate).HasColumnType("date");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.PurchaseOrderId).HasColumnName("PurchaseOrderID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PurchaseOrderLines)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__PurchaseO__Produ__4222D4EF");

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.PurchaseOrderLines)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .HasConstraintName("FK__PurchaseO__Purch__4316F928");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.SupplierId)
                    .ValueGeneratedNever()
                    .HasColumnName("SupplierID");

                entity.Property(e => e.BankAccountBranch).HasMaxLength(50);

                entity.Property(e => e.BankAccountCode).HasMaxLength(20);

                entity.Property(e => e.BankAccountName).HasMaxLength(50);

                entity.Property(e => e.BankAccountNumber).HasMaxLength(20);

                entity.Property(e => e.BankInternationalCode).HasMaxLength(20);

                entity.Property(e => e.DeliveryAddressLine1).HasMaxLength(60);

                entity.Property(e => e.DeliveryAddressLine2).HasMaxLength(60);

                entity.Property(e => e.DeliveryCity).HasMaxLength(50);

                entity.Property(e => e.DeliveryMethod).HasMaxLength(50);

                entity.Property(e => e.DeliveryPostalCode).HasMaxLength(10);

                entity.Property(e => e.FaxNumber).HasMaxLength(20);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.SupplierCategoryId).HasColumnName("SupplierCategoryID");

                entity.Property(e => e.SupplierName).HasMaxLength(100);

                entity.Property(e => e.SupplierReference).HasMaxLength(20);

                entity.Property(e => e.WebsiteUrl)
                    .HasMaxLength(256)
                    .HasColumnName("WebsiteURL");

                entity.HasOne(d => d.SupplierCategory)
                    .WithMany(p => p.Suppliers)
                    .HasForeignKey(d => d.SupplierCategoryId)
                    .HasConstraintName("FK__Suppliers__Suppl__44FF419A");
            });

            modelBuilder.Entity<SupplierCategory>(entity =>
            {
                entity.Property(e => e.SupplierCategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("SupplierCategoryID");

                entity.Property(e => e.SupplierCategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<SupplierTransaction>(entity =>
            {
                entity.Property(e => e.SupplierTransactionId)
                    .ValueGeneratedNever()
                    .HasColumnName("SupplierTransactionID");

                entity.Property(e => e.AmountExcludingTax).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FinalizationDate).HasColumnType("date");

                entity.Property(e => e.PaymentMethod).HasMaxLength(50);

                entity.Property(e => e.PurchaseOrderId).HasColumnName("PurchaseOrderID");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.SupplierInvoiceNumber).HasMaxLength(20);

                entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TransactionAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TransactionDate).HasColumnType("date");

                entity.Property(e => e.TransactionType).HasMaxLength(50);

                entity.HasOne(d => d.PurchaseOrder)
                    .WithMany(p => p.SupplierTransactions)
                    .HasForeignKey(d => d.PurchaseOrderId)
                    .HasConstraintName("FK__SupplierT__Purch__45F365D3");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SupplierTransactions)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK__SupplierT__Suppl__46E78A0C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
