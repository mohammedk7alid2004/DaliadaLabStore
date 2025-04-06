using WebApplication4.Models;

namespace WebApplication4.ViewModels
{
    public class ProductViewModel
    {
        public Product MainProduct { get; set; } // Main product details
        public List<Product> RelatedProducts { get; set; } // List of related products
    }
}
