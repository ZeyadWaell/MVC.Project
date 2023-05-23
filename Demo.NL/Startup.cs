using Demo.DAL.Context;
using Demo.DAL.Entites;
using Demo.NL.Mapper;
using Demo.PLL.Interfaces;
using Demo.PLL.Repostoies;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.NL
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
			services.AddControllersWithViews();
			services.AddDbContext<MVCAPPContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("ZeyadConntetion"));
			});
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
			{
				option.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
				option.AccessDeniedPath = new PathString("/Shared/Error");
			});
			services.AddScoped<IDepartmentRepostory, DepRepostory>();
			services.AddScoped<IEmplyeeRepostory,EmplyeeRepostory>();
			services.AddScoped<IUniteofWork, UniteofWork>();
            services.AddAutoMapper(profile=>profile.AddProfile(new EmplyeeProfile()));
            services.AddAutoMapper(profile => profile.AddProfile(new DepartmentProfile()));
			services.AddIdentity<ApplicationUser, IdentityRole>(option =>
			{
				option.Password.RequireUppercase = true;
				option.Password.RequireLowercase = true;
				option.Password.RequireNonAlphanumeric = true;
			}).AddEntityFrameworkStores<MVCAPPContext>().AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
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
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
