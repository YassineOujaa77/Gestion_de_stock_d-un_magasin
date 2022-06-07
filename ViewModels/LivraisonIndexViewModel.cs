using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GestionDeStockMagasin.Models;

namespace GestionDeStockMagasin.ViewModels
{
    public class LivraisonIndexViewModel
    {
        public int ClientId { get; set; }

        public int ProductId { get; set; }
        public int Id { get; set; }

        public int IdFacture { get; set; }
        public int idFact {get; set;}
        public List<Client> Clients { get; set; }

        public List<Product> Products { get; set; }
        public List<Personnel> Personnels { get; set; }
        public Facture Factures { get; set; }

        public List<int> idFactureList {get; set ;}

        
    }
}
