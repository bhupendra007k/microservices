using Microsoft.EntityFrameworkCore;
using productservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace productservice.Data
{
    public class DataContext : DbContext
    {

        public DataContext(
            DbContextOptions<DataContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
       
        public DbSet<Discount> Discount { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName:"Ecommerce");
        }


       /* protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProductCategory>().HasData(
                new ProductCategory {
                    ProductCategoryId=Guid.NewGuid(),
                    Name="Clothing",
                    Description="cscsdc"
                },
                new ProductCategory {
                    ProductCategoryId = Guid.NewGuid(),
                    Name = "Footwear",
                    Description = "cscsdasdasdc"
                },
                new ProductCategory {
                    ProductCategoryId = Guid.NewGuid(),
                    Name = "jewellery",
                    Description = "cscssadasddc"
                }
                );
        }*/



     


    
    }
}
