using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace GestionDeStockMagasin.Models
{
    public class Facture
    {
        [Key]
        public int IdFacture { get; set; }
        [Required]
        
        public int Qte { get; set; }
        
        public float Prix_total { get; set; } 

        public bool livrer {get; set;}

        public int CommandId { get; set; }

	[Required]
        public DateTime DateStock { get; set; } = DateTime.Now;

        // relationships 
            // with table client  one facture can have one client 
        public int ClientId { get; set; }

        public Client client { get; set; }

            // with table Product 
        public int ProductId { get; set; }

        public Product product { get; set; }
    }
}
