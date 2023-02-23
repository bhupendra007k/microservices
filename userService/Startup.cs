using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using userService.Client;
using userService.Data;
using userService.Models;
using userService.Repositories;

namespace userService
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
            services.AddControllers();
            services.AddDbContext<UserContext>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddHttpClient<ICartClient, CartClient>();
            services.AddAuthentication(options =>
            {


                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options => {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                };
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            });
        }

        public static void SeedData(UserContext context)
        {
            User firstUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "bhupendar@gmail.com",
                Username = "Bhupendar",
                UserType = "Admin",
                Password = "Admin@123"
            };

            User secondUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "seconduse@gmail.com",
                Username = "SecondUser",
                UserType = "User",
                Password = "SecoundUser@123"
            };

            User thirdUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "thirduser@gmail.com",
                Username = "ThirdUser",
                UserType = "User",
                Password = "ThirdUser@123"
            };
            User fourthUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "fourth@gmail.com",
                Username = "FourthUser",
                UserType = "User",
                Password = "FourthUser@123"
            };
            context.Users.AddAsync(firstUser);
            context.Users.AddAsync(secondUser);
            context.Users.AddAsync(thirdUser);
            context.Users.AddAsync(fourthUser);

            context.SaveChangesAsync();

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
            var context = scope.ServiceProvider.GetService<UserContext>();
            SeedData(context);
        }
    }
}
