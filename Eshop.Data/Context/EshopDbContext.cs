using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Domain.Models.Basket;
using Eshop.Domain.Models.Products;
using Eshop.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data.Context
{
    public class EshopDbContext:DbContext
    {
        public EshopDbContext(DbContextOptions<EshopDbContext> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetailes { get; set; }
    }
}
