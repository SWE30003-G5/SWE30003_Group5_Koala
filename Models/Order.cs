using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE30003_Group5_Koala.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        [StringLength(20)]
        public string Type { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue, ErrorMessage = "The price must be positive.")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }
        [StringLength(20)]
        [DefaultValue("In Process")]
        public string Status { get; set; }
        [Required]
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
