using Microsoft.EntityFrameworkCore;
using shoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shoppingCart.Data
{
    public class cartContext:DbContext
    {

        public cartContext(DbContextOptions<cartContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("UserDb");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
