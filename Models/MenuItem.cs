using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE30003_Group5_Koala.Models
{
    public class MenuItem
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue, ErrorMessage = "The price must be positive.")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        [DefaultValue(true)]
        public bool IsAvailable { get; set; }
        [StringLength(50)]
        [DefaultValue("Blank")]
        public string ImageLocation { get; set; }
    }
}
