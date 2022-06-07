using Microsoft.EntityFrameworkCore;
using GestionDeStockMagasin.Models;
namespace GestionDeStockMagasin.Data;
public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
    {   
    }

    

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Personnel> personnels { get; set; }

    public DbSet<Facture> Facture { get; set; }

    public DbSet<Client> Client { get; set; }

   public DbSet<Statistic> Statistic { get; set; }


   public void ExistMail(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personnel>()
                .HasIndex(x => x.Email)
                .IsUnique();
        }
        public void ExistPhone(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personnel>()
                .HasIndex(x => x.Phone)
                .IsUnique();
        }

    
}