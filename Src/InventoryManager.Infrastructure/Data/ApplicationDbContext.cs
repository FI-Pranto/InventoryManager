using InventoryManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManager.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Unit> Units { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<MaterialIssue> MaterialIssues { get; set; }

        public DbSet<MaterialIssueItem> MaterialIssueItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>().HasData(
                
                new Unit { Id=1, Name = "Kilogram", Description = "Unit of mass equal to 1000 grams." }
                );

            modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 4);

            modelBuilder.Entity<Purchase>().Property(p=>p.TotalAmount).HasPrecision(18, 4);
            modelBuilder.Entity<PurchaseItem>().Property(p =>p.UnitPrice).HasPrecision(18, 4);

            modelBuilder.Entity<MaterialIssue>().Property(u=>u.TotalAmount).HasPrecision(18, 4);
            modelBuilder.Entity<MaterialIssueItem>().Property(u=>u.ProductPrice).HasPrecision(18, 4);
        }

        


    }
}
