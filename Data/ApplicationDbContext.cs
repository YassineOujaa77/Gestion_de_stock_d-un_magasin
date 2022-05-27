
using Microsoft.EntityFrameworkCore;
using stock_management.Models;

namespace stock_management.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Personnel> personnels { get; set; }
    }
}
