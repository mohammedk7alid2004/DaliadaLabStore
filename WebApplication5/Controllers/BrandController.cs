using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4.Controllers;

public class BrandController : Controller
{
    private readonly IBrands _brand;
    private readonly DalidaAppDbContext context;

    public BrandController(IBrands brand, DalidaAppDbContext context)
    {
        _brand = brand;
        this.context = context;
    }

    [HttpGet]
    public IActionResult AddBrand()
    {
        var brands = context.Brands.ToList();
        return View(brands);
    }

    [HttpPost]
    public IActionResult AddBrand(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            _brand.Add(name);
        }

        return RedirectToAction("AddBrand"); // Redirect to GET
    }

    [HttpPost]
    public IActionResult DeleteBrand(int id)
    {
        var brand = context.Brands.FirstOrDefault(b => b.Id == id);

        if (brand == null)
        {
            return NotFound("Brand not found");
        }

        context.Brands.Remove(brand);
        context.SaveChanges();

        return RedirectToAction("AddBrand"); // Refresh the list
    }
}
