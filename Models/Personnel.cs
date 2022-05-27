using System.ComponentModel.DataAnnotations;

namespace stock_management.Models
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
        public string Email { get; set; }

        [StringLength(18, ErrorMessage = "Minimum eight characters, at least one letter and one number.", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Adresse { get; set; }
        public string Role { get; set; }
        
        [Required(ErrorMessage = "Mobile is required")]
        [RegularExpression(@"\d{10}", ErrorMessage = "Please enter 10 digit Mobile No.")]
        public int Phone { get; set; }
        
    }
}
