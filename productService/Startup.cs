using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using productservice.Data;
using productservice.Repositories;
using Microsoft.EntityFrameworkCore;
using productservice.Models;
using productservice.Controllers;
using productservice.NewFolder;
using productservice.Client;

namespace productService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("Ecommerce"));
            services.AddControllers();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddHttpClient<IInventoryClient,InventoryClient>();
            services.AddHttpClient<ICartClient, CartClient>();
        }

        public static void SeedData(DataContext context)
        {
            ProductCategory category1 = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Clothing and apparel",
                Description = "Choose from vast variety of clothing and apparel options"

            };
            ProductCategory category2 = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Footwear and Shoes",
                Description = "Introducing standard footwear sizes online,get access to a lot more options in the online mode"
            };
            ProductCategory category3 = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Electronics and Gadgets",
                Description = "Buy electronic appliance, gadget or device, or device online. Great offers, discounts, and deals available online which might be absent in a physical store."
            };
            ProductCategory category4 = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Stationery",
                Description = "Stationery items rule the world. From books to pens to crayons to planners, everything stationery is availabe on one Click"
            };
            ProductCategory category5 = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "TupperWare",
                Description = " Buy Tupperware online because of the durability and lifetime warranty that comes with it."
            };
            Discount sale_1 = new Discount
            {
                Id=Guid.NewGuid(),
                Name= "Big Bachat Dhamaal",
                Description= "Grocey and pantry sale in the begining of every month!",
                DiscountPercent=20,
                Active=true
            };
            Discount sale_2 = new Discount
            {
                Id = Guid.NewGuid(),
                Name = "End Of Season Sale",
                Description = "Clearance sale on clothes!",
                DiscountPercent = 20,
                Active = true
            };
            Discount sale_3 = new Discount
            {
                Id = Guid.NewGuid(),
                Name = "Valentines Day Sale",
                Description = "The valentines day sale will give you unlimited deals on couple gifts!",
                DiscountPercent = 20,
                Active = true
            };
            Discount sale_4= new Discount
            {
                Id = Guid.NewGuid(),
                Name = "Best Of Season Sale",
                Description = "Huge discounts on men and women clothes. 60% OFF on multiple brands!",
                DiscountPercent = 20,
                Active = true
            };

            context.Discount.Add(sale_1);
            context.Discount.Add(sale_2);
            context.Discount.Add(sale_3);
            context.Discount.Add(sale_4);

            context.ProductCategory.Add(category1);
            context.ProductCategory.Add(category2);
            context.ProductCategory.Add(category3);
            context.ProductCategory.Add(category4);
            context.ProductCategory.Add(category5);
            context.SaveChanges();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<DataContext>();
            SeedData(context);
        }

    }
}
