using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GestionDeStockMagasin.Models;


namespace GestionDeStockMagasin.ViewModels
{
    public class ProductCreateViewModel
    {
        
        public int IdCategory { get; set; }

        public int ProductId { get; set; }
        public Product product { get; set; } 
        public List<Category> category { get; set; } 
        
    }
}
