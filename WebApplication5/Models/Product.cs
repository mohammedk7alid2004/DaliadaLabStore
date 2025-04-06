using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models;

public class Product
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string ProductName { get; set; } = string.Empty;
    [MaxLength(2500)]
    public string Description{ get; set; } = string.Empty;
    [MaxLength(100)]
    public string Price { get; set; } = string.Empty;
    public string Cover { get; set; } = string.Empty;
    public int StockQuantity { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
  

}
