using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CDGBulgaria.Data;
using CDGBulgaria.Data.Models;
using CDGBulgaria.Services;
using CDGBulgaria.Services.Contracts;
using System.Globalization;
using CloudinaryDotNet;
using AspNetCoreTemplate.Services.Mapping;
using CDGBulgaria.Web.InputModels.Event;
using System.Reflection;
using CDGBulgaria.Services.Models;
using CDGBulgaria.Web.ViewModels.Event;
using CDGBulgaria.Web.ViewModels.Question;
using CDGBulgaria.Web.InputModels.Question;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI;
using System;

namespace CDGBulgaria.Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

	
		public void ConfigureServices(IServiceCollection services)
		{
		

			services.AddDbContext<CDGBulgariaDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<CDGUser, IdentityRole>()
				//.AddDefaultUI(UIFramework.Bootstrap4)
				.AddEntityFrameworkStores<CDGBulgariaDbContext>()
				.AddDefaultTokenProviders();

			Account cloudinaryCredentials = new Account(
				this.Configuration["Cloudinary:CloudName"],
				this.Configuration["Cloudinary:ApiKey"],
				this.Configuration["Cloudinary:ApiSecret"]);

			Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

			services.AddSingleton(cloudinaryUtility);

			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.Configure<IdentityOptions>(options =>
			{
				// Default Password settings.
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 3;
				options.Password.RequiredUniqueChars = 0;
				options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(1, 0, 0);
				options.Lockout.MaxFailedAccessAttempts = 5;

				options.User.RequireUniqueEmail = true;
			});

			services.AddTransient<IEventsService, EventsService>();
			services.AddTransient<ICloudinaryService, CloudinaryService>();
			services.AddTransient<IQuestionsService, QuestionsService>();
			services.AddTransient<IArticlesService, ArticlesService>();
			services.AddTransient<IFactsService, FactsService>();
			services.AddTransient<IAnswersService, AnswersService>();
			services.AddTransient<IDiseasesService, DiseasesService>();
			services.AddTransient<IUsersService, UsersService>();

			services.AddMvc(
				options=> 
				{
					options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
				}
				).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
		   AutoMapperConfig.RegisterMappings(
		     typeof(EventCreateInputModel).GetTypeInfo().Assembly,
		         typeof(EventViewModel).GetTypeInfo().Assembly,
			          typeof(EventServiceModel).GetTypeInfo().Assembly);
			

			CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
			CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

			using (var serviceScope = app.ApplicationServices.CreateScope())
			{
				using (var context=serviceScope.ServiceProvider.GetRequiredService<CDGBulgariaDbContext>())
				{
					context.Database.EnsureCreated();

					if (!context.Roles.Any())
					{
						context.Roles.Add(new IdentityRole
						{
							Name="Admin",
							NormalizedName="ADMIN"
						});
						context.Roles.Add(new IdentityRole
						{
							Name = "User",
							NormalizedName = "USER"
						});
						context.Roles.Add(new IdentityRole
						{
							Name ="Doctor",
							NormalizedName = "DOCTOR"
						});

						context.SaveChanges();
					}
				}
			}
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			
				app.UseHsts();
			

			app.UseHttpsRedirection();
			app.UseStaticFiles();


			app.UseAuthentication();

			app.UseMvc(routes =>
			{
				routes.MapRoute( 
					name: "areas",
					template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");

				
			});
		}
	}
}
