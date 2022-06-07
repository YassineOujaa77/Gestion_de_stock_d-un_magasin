using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GestionDeStockMagasin.Models;


namespace GestionDeStockMagasin.ViewModels
{
    public class AchatIndexViewModel
    {
        
        
        public int Qte { get; set; }
        
        public float Prix_total { get; set; } 

        public bool livrer {get; set;}

        public int CommandId { get; set; }

        public int ClientId {get; set;}

        public int ProductId {get; set;}

        public List<Client> clients{get; set;}

        public List<Product> products{get; set;}

        public List<Facture> facturesTemp {get; set;}
        
    }
}
