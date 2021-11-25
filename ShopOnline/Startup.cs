using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using ShopOnline.Business;
using ShopOnline.Business.Customer;
using ShopOnline.Business.Logic;
using ShopOnline.Business.Logic.Customer;
using ShopOnline.Business.Logic.Staff;
using ShopOnline.Business.Staff;
using ShopOnline.Core;
using ShopOnline.Core.Filters;
using ShopOnline.Core.Validators.Account;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            services.AddPaging(options =>
            {
                options.ViewName = "Bootstrap4";
                options.PageParameterName = "page";
            });

            //User AutoMapper
            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new StaffProfile());
            //});
            //mappingConfig.AssertConfigurationIsValid();
            //IMapper mapper = mappingConfig.CreateMapper();
            //services.AddSingleton(mapper);


            services.AddMvc(option =>
            {
                option.Filters.Add(typeof(ModelStateAjaxFilter));
            })
            .AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<AccountLoginValidator>();
            });

            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.LoginPath = "/Account/Login";
                    opt.AccessDeniedPath = "/Login";
                    opt.ReturnUrlParameter = "returnUrl";
                    opt.LogoutPath = "/Logout";
                    opt.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                });

            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IClientBusiness, ClientBusiness>();
            services.AddScoped<IStaffBusiness, StaffBusiness>();
            services.AddScoped<IProductBusiness, ProductBusiness>();

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
