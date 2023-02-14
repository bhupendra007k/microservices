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
        }

        public static void SeedData(DataContext context)
        {
            ProductCategory category1 = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Clothing",
                Description = "cscsdc"
                
            };
            ProductCategory category2 = new ProductCategory
            {
                Id = Guid.NewGuid(),
                Name = "Footwear",
                Description = "cscsdc"
            };
            context.ProductCategory.Add(category1);
            context.ProductCategory.Add(category2);
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
