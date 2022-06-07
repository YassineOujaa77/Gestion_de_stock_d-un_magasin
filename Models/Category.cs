using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestionDeStockMagasin.Models
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        [Required]
        public string Name { get; set; }
        
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}
