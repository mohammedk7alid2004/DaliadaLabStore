using System.ComponentModel.DataAnnotations .Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartItemId { get; set; }

        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public  User? User { get; set; }

        public int Quantity { get; set; }
         
        public string ? UnitPrice { get; set; }

        public decimal? LineTotal { get; set; }



        [ForeignKey("Product")]
        public int? ProductId { get; set; }

        public virtual Product? Product { get; set; }
    }
}
