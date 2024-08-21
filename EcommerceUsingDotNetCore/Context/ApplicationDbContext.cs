using EcommerceUsingDotNetCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EcommerceUsingDotNetCore.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
            {

            }

        public DbSet<User> Users { get; set; }

    }
}
