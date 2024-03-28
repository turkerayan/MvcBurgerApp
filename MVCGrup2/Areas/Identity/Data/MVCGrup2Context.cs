using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using MVCGrup2.Data;
using MVCGrup2.Entities.Concrete;
using MVCGrup2.Roles;
using System.Reflection.Emit;
using MVCGrup2.Models;

namespace MVCGrup2.Data;

public class MVCGrup2Context : IdentityDbContext<MVCGrup2User>
{
    private readonly IServiceProvider _serviceProvider;
    public MVCGrup2Context(DbContextOptions<MVCGrup2Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
       
        base.OnModelCreating(builder);

    }

    public DbSet<ExtraMat> ExtraMats { get; set; }
    public DbSet<Role>? Roles { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Menu> Menus { get; set; }

    //public DbSet<MVCGrup2.Models.OrderViewModel> OrderViewModel { get; set; } = default!;

}
