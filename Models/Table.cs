using System.ComponentModel.DataAnnotations;

namespace SWE30003_Group5_Koala.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Availability")]
        public bool IsAvailable { get; set; }
    }
}
