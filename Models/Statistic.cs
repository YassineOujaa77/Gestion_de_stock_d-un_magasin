using System.ComponentModel.DataAnnotations;

namespace GestionDeStockMagasin.Models
{
    public class Statistic
    {
        public Statistic(string nom, int prix_total)
        {
            Product = nom;
            Qte = prix_total;
        }

        public Statistic()
        {
           
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Product { get; set; }
        public int  Qte { get; set; }
       
    }
}
