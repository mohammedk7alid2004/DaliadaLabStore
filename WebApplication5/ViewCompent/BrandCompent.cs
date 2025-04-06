using Microsoft.AspNetCore.Mvc;
using WebApplication4.Services;

namespace WebApplication4.ViewCompent;

public class BrandCompent: ViewComponent
{
    private readonly IProductServices productServices;

    public BrandCompent(IProductServices productServices)
    {
        this.productServices = productServices;
    }
    public IViewComponentResult Invoke()
    {
        var brand=productServices.GetAllBrands();
        return View(brand);

    }
}
