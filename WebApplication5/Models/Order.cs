using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication4.Models;

public class Order
{
    public int Id { get; set; }

    [MaxLength(250)]
    public string Name { get; set; } = string.Empty;

    public string? UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? OrderStatus { get; set; }

    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();


    [ForeignKey("UserId")]
    public virtual User? User { get; set; }

}
