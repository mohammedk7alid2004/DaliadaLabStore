using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using WebApplication4.ViewModels;
using WebApplication4.Services;
using WebApplication4.Models;
namespace WebApplication4.Controllers;

public class ProductController : Controller
{
    private readonly DalidaAppDbContext _context;
    private readonly IBrands _brand;
    private readonly IProductServices _productServices;

    public ProductController(DalidaAppDbContext context, IBrands brand, IProductServices productServices)
    {
        _context = context;
        _brand = brand;
        _productServices = productServices;
    }

    public IActionResult Index()
    {
        var product = _productServices.GetALL();
        return View(product);
    }
    public IActionResult Details(int id)
    {
        Product pro= _productServices.GetBy_Id(id);
        List<Product>  ps = _productServices.GetALL().Take(7).ToList();
       
        if(pro is null)
        {
            return NotFound();
        }
        var viewModel = new ProductViewModel
        {
            MainProduct = pro,
            RelatedProducts = ps,
        };
        return View(viewModel);
    }
    [HttpGet]
    public IActionResult Create()
    {
        CreateProdectFromVM viewmoder = new()
        {
            Brands = _brand.GetBrands()

        };
            return View(viewmoder);
    }
    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Create(CreateProdectFromVM model)
    {
        if (!ModelState.IsValid)
        {
            model.Brands = _brand.GetBrands();  
            return View(model);
        }
        await _productServices.Create(model);

        return RedirectToAction(nameof(Index));
    }
    public IActionResult  Update(int id)
    {
        Product pro = _productServices.GetBy_Id(id);
        if (pro is null)
        {
            return NotFound();
        }
        UpdateForm v= new UpdateForm()
        {
            id= id,
           ProductName=pro.ProductName,
           Description=pro.Description,
           Price=pro.Price,
           StockQuantity=pro.StockQuantity,
           BrandId=pro.BrandId,
           Brands=_brand.GetBrands(),
           CurrentCover=pro.Cover,

        };
        return View(v);

    }
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Update(UpdateForm v)
    {
        if (!ModelState.IsValid)
        {
            v.Brands = _brand.GetBrands();
            return View(v);
        }
       var pr= await _productServices.Update(v);
        if (pr is null)
            return BadRequest();
        return RedirectToAction(nameof(Index));


    }
    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var pp= _productServices.Delete(id);
        
        return pp? Ok():BadRequest();

    }
    [HttpGet("GetByName")]
    public ActionResult GetByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return View(new List<Product>());
        }

        var products = _productServices.GetByName(name) ?? new List<Product>();

        return View(products);
    }
    [HttpGet("GetByBrand")]
    public ActionResult GetById(int id)
    {
        if(id == 0)
        { 
            return View(new List<Product>());
        }
        var pp=_productServices.GetByBrand(id);
        return View("GetByName",pp);

    }
    public IActionResult GetBrands()
    {
        var brands = _brand.GetBrands(); // جلب العلامات التجارية
        return Json(brands); // إرجاع البيانات كـ JSON
    }



}
