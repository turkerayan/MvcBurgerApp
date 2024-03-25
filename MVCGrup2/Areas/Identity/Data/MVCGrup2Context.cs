using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCGrup2.Data;
using MVCGrup2.Entities.Concrete;
using MVCGrup2.Roles;
using System.Reflection.Emit;
using MVCGrup2.Models;

namespace MVCGrup2.Data;

public class MVCGrup2Context : IdentityDbContext<MVCGrup2User>
{
    public MVCGrup2Context(DbContextOptions<MVCGrup2Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
       
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<ExtraMat> ExtraMats { get; set; }
    public DbSet<Role>? Roles { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Menu> Menus { get; set; }

public DbSet<MVCGrup2.Models.OrderViewModel> OrderViewModel { get; set; } = default!;

}
