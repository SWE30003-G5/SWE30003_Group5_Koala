using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE30003_Group5_Koala.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        [DefaultValue("Customer")]
        public string Role { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [DefaultValue("Hunter3")]
        public string Password { get; set; } //password must contain letters and numbers only, no special characters

        [Phone]
        [StringLength(10, ErrorMessage = "Phone number cannot exceed 10 digits.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}