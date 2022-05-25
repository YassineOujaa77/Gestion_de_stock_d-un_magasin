using Microsoft.EntityFrameworkCore;
using GestionDeStockMagasin.Models;
namespace GestionDeStockMagasin.Data;
public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
    {   
    }

    public DbSet<Product> Products { get; set; }
}