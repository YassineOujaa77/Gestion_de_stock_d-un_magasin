using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GestionDeStockMagasin.Models
{
    public class Product
    {
        [Key ] //data annotation 
        public int ProductId { get; set; }

        [Required]
        public String Nom { get; set; }

        [Required]
        public String Marque { get; set; }

        [Required]
        public int QteStock { get; set; }

        [Required]
        public String Categorie { get; set; }

        [Required]
        public DateTime DateStock { get; set; } = DateTime.Now;

        
        public String? Description { get; set; }

        [Required]
        public int prixUnite{ get; set; } 

        public List<Facture> Facture { get; set; }
    }



}
