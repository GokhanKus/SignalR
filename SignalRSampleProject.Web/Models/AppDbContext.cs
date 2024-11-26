using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SignalRSampleProject.Web.Models;
public class AppDbContext(DbContextOptions<AppDbContext> context) :IdentityDbContext<IdentityUser,IdentityRole,string>(context) //primary ctor
{
    public DbSet<Product> Products { get; set; }
}
