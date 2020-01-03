using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportsStore.Models;

namespace SportsStore
{
    /// <summary>
    /// Is responsible for configuring the application
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
//            services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlite("Filename=SportsStore.db"));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration["Data:SportsStore:ConnectionString"]));

            services.AddDbContext<AppIdentityDbContext>();
            
            //add Identity services
            services.AddIdentity<IdentityUser, IdentityRole>(opts =>
                {
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            
            //use FakeProductRepository which returns Products
            //or use storage that is connected to the SQLite DB with the help of Entity Framework Core
            services.AddTransient<IProductRepository, EFProductRepository>();
            
            services.AddScoped<Cart>(serv => SessionCart.GetCart(serv));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddTransient<IOrderRepository, EFOrderRepository>();

            services.AddMvc(options => options.EnableEndpointRouting = false);
            //NEW feature - support for sessions - a data which is associated with actions of a particular user
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
//            if (env.IsDevelopment())
//            {
//                
//            }
//            else
//            {
//                app.UseExceptionHandler("/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            
//            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
//            app.UseRouting();

            //Install components for authentication processes
            app.UseAuthentication();

//            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "{Category}/Page{Productpage:int}",
                    defaults: new { Controller = "Product", action = "List"}
                );
                routes.MapRoute(
                    name: null,
                    template: "Page{productPage:int}",
                    defaults: new { Controller = "Product", action = "List", productPage = 1 }
                );
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { Controller = "Product", action = "List", productPage = 1}
                );
                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { Controller = "Product", action = "List", productPage = 1}
                );
                
                //URL templates with description
                // {/} - open first page of all products
                // {/Page2} - open page number 2 
                // {/Chess} - open 1st page of goods in category Chess
                // {/Soccer/Page2} - open 2nd page of goods of category Soccer
                
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
//                routes.MapRoute(
//                    name: "pagination",
//                    template: "Products/Page{productPage}",
//                    defaults: new {Controller = "Product", Action = "List"});
//                routes.MapRoute(
//                    name: "default",
//                    template: "{controller=Product}/{action=List}/{id?}");
            });
            
            //Fill in database with Products
            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}