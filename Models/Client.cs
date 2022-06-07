using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestionDeStockMagasin.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        [Required]
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresses { get; set; }
        
        
        public DateTime CreateDateTime { get; set; } = DateTime.Now;

        public List<Facture> Facture { get; set; }
    }
}
