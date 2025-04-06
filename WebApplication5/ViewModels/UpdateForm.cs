using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.ViewModels
{
    public class UpdateForm
    {
        public string ProductName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Price { get; set; } = string.Empty;
        public int StockQuantity { get; set; }
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; } = Enumerable.Empty<SelectListItem>();
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        [AllowedExtensions(".jpg,.png,.jpeg")]
        public IFormFile? Cover { get; set; }
        public int id { get; set; }
        public string ?CurrentCover { get; set; }
    }
}
