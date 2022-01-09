using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopOnline.Business;
using ShopOnline.Business.Customer;
using ShopOnline.Business.Logic;
using ShopOnline.Business.Logic.Customer;
using ShopOnline.Business.Logic.Staff;
using ShopOnline.Business.Order;
using ShopOnline.Business.Staff;
using ShopOnline.Core;
using ShopOnline.Core.Filters;
using ShopOnline.Core.Validators.Account;
using ShopOnline.Data.Repositories.Product;
using ShopOnline.Models;
using System;

namespace ShopOnline
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<MyDbContext>(option => option.UseSqlServer(_configuration.GetConnectionString("HuyConnectionString")));

            // Use AppSetting by DI
            var appSetting = _configuration.GetSection("AppSetting");
            services.Configure<AppSetting>(appSetting);

            services.AddMvc(option =>
            {
                option.Filters.Add(typeof(ExceptionFilter));
                option.Filters.Add(typeof(ModelStateAjaxFilter));
            })
            .AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<AccountLoginValidator>();
            });
            //services.AddScoped<ModelStateAjaxFilter>();

            services.AddDistributedMemoryCache();
            services.AddSession(opt =>
            {
                opt.Cookie.Name = "ShopOnline";
                opt.IdleTimeout = new TimeSpan(0, 30, 0);
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/Account/Login";
                    opt.AccessDeniedPath = "/Login";
                    opt.ReturnUrlParameter = "returnUrl";
                    opt.LogoutPath = "/Logout";
                    opt.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IClientBusiness, ClientBusiness>();
            services.AddScoped<IStaffBusiness, StaffBusiness>();
            services.AddScoped<ICartBusiness, CartBusiness>();
            services.AddScoped<IProductBusiness, ProductBusiness>();
            services.AddScoped<IReviewBusiness, ReviewBusiness>();
            services.AddScoped<ICustomerBusiness, CustomerBusiness>();
            services.AddScoped<IOrderBusiness, OrderBusiness>();
            services.AddScoped<IProductDetailRepository, ProductDetailRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Client}/{action=Home}/{id?}");
            });
        }
    }
}
