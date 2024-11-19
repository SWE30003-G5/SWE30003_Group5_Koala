using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE30003_Group5_Koala.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        // Foreign key for User
        [Required]
        public int UserID { get; set; }


        [Required]
        [Range(1, 20, ErrorMessage = "Party size must be between 1 and 20.")]
        [Display(Name = "Party Size")]
        public int PartySize { get; set; }

        // Foreign key for Table
        [Required]
        public int TableID { get; set; }


        [Required]
        [Display(Name = "Reservation Time")]
        public DateTime Time { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Reservation Status")]
        public string Status { get; set; } = "Pending"; // Default value
    }
}
