using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GestionDeStockMagasin.Models;

namespace GestionDeStockMagasin.ViewModel
{
    public class LivraisonIndexViewModel
    {
        public int ClientId { get; set; }

        public int ProductId { get; set; }
        public int Id { get; set; }

        public int idFacture { get; set; }
        public List<Client> Clients { get; set; }

        public List<Product> Products { get; set; }
        public List<Personnel> Personnels { get; set; }
        public List<Facture> Factures { get; set; }

        
    }
}
