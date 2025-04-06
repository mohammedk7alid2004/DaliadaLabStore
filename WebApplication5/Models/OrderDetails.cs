using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }

        public int? OrderId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? UnitPrice { get; set; }

        [Column(TypeName = "decimal(21, 2)")]
        public decimal? LineTotal { get; set; }

     
        public virtual Order? Order { get; set; }

      
        public virtual Product? Product { get; set; }
    }
}
