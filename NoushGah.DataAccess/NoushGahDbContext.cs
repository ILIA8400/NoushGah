using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoushGah.DataAccess.IdentityModel;
using NoushGah.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoushGah.DataAccess
{
    public class NoushGahDbContext : IdentityDbContext<SystemUser>
    {
        #region DbSets
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        #endregion

        #region Constructor
        public NoushGahDbContext(DbContextOptions<NoushGahDbContext> options) : base(options)
        {
        } 
        #endregion
    }
}
