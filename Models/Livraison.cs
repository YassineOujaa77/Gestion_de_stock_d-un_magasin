

using System.ComponentModel.DataAnnotations;

namespace GestionDeStockMagasin.Models
{
    public class Livraison
    {
        [Key]
        public int idLivraison { get; set; }
  
        public int idFacture { get; set; }
        public Facture facture { get; set; }
        public string nameCL { get; set; }

        public string namePR { get; set; }
        public string livreur { get; set; }

    }
}
