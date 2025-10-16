using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SpaceTravellast.Models;
using System.Globalization;
using System.Reflection;

internal class Program
{
	[Obsolete]

	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		//Dil sistemii//
		builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
		builder.Services.AddMvc().AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
		builder.Services.Configure<RequestLocalizationOptions>(
			opt =>
			{
				var supportedCultures = new List<CultureInfo>
				{
					new CultureInfo("en"),
					new CultureInfo("az"),
					new CultureInfo("ru"),
				};
				opt.DefaultRequestCulture = new RequestCulture("en");
				opt.SupportedCultures = supportedCultures;
				opt.SupportedUICultures = supportedCultures;
			});


		builder.Services.AddControllersWithViews().AddJsonOptions(x =>
			x.JsonSerializerOptions.PropertyNamingPolicy = null
);
		// Add services to the container.
		builder.Services.AddControllersWithViews().AddFluentValidation(options =>
		{
			options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			  .AddCookie(options =>
			  {
				  options.Cookie.Name = "User";
				  options.LoginPath = "/User/Login";
				  options.AccessDeniedPath = "/User/Login";
				  options.ExpireTimeSpan = TimeSpan.FromDays(7);
				  options.SlidingExpiration = false;
			  });
		});
		builder.Services.AddDbContext<RmlubecoSpaceContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
		builder.Services.AddScoped<RmlubecoSpaceContext>();

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();
		var options = ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>();
		app.UseRequestLocalization(options.Value);
		//app.UseRequestLocalization(app.applicationservices.getrequiredservice<ýoptions<requestlocalizationoptions>>().value);
		app.UseRouting();
		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}/{id?}");

		app.Run();
	}

}


