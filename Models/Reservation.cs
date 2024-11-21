using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace SWE30003_Group5_Koala.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        // Foreign key for User
        [Required]
        [ForeignKey("User")]
        public int UserID { get; set; }
        [ValidateNever]
        public User User { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Party size must be between 1 and 20.")]
        [Display(Name = "Party Size")]
        public int PartySize { get; set; }


        [Required]
        [FutureDate(ErrorMessage = "Reservation time must be in the future.")]
        [Display(Name = "Reservation Time")]
        public DateTime Time { get; set; }
        [ForeignKey("Table")]
        [Display(Name = "Assigned Table")]
        public int TableID { get; set; }
        [ValidateNever]
        public Table Table { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending";
    }
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime <= DateTime.Now)
                {
                    return new ValidationResult(ErrorMessage ?? "Date and time must be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
