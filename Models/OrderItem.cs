using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWE30003_Group5_Koala.Models
{
    public class OrderItem
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int MenuItemID { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be positive.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal SubTotal { get; set; }
        [Required]
        public int OrderID { get; set; }
        [ForeignKey("MenuItemID")]
        public MenuItem MenuItem { get; set; }
        [ForeignKey("OrderID")]
        public Order Order { get; set; }
    }
}
