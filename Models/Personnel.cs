using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GestionDeStockMagasin.Models
{
    public class Personnel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        public string Prenom { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "Minimum eight characters, at least one letter and one number.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Minimum eight characters, at least one letter and one number.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Adresse { get; set; }
        public string Role { get; set; }

        [Required(ErrorMessage = "Mobile is required")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\d{10}$",ErrorMessage ="Phone must be 10 digits long")]
        public int Phone { get; set; }

    }
}
