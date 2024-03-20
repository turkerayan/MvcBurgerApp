using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCGrup2.Data;
namespace MVCGrup2
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
   var connectionString = builder.Configuration.GetConnectionString("MVCGrup2ContextConnection") ?? throw new InvalidOperationException("Connection string 'MVCGrup2ContextConnection' not found.");

   builder.Services.AddDbContext<MVCGrup2Context>(options => options.UseSqlServer(connectionString));

   builder.Services.AddDefaultIdentity<MVCGrup2User>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddRoles<IdentityRole>()
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<MVCGrup2Context>();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

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

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapRazorPages();


			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
