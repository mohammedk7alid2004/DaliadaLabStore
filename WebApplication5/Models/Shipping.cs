using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models
{
    public class Shipping
    {
        [Key]
        public int ShippingId { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? BaseCost { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? CostPerUnit { get; set; }

        public int? EstimatedDeliveryTime { get; set; }

        public int OrderId { get; set; }  // مفتاح أجنبي يربط الشحنة بالطلب

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }  // العلاقة مع `Order`
    }
}
