using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly IProductServices _productServices;

        public HomeController( IProductServices productServices)
        {
            
            _productServices = productServices;
        }

        public IActionResult Index()
        {
            var product = _productServices.GetALL();
            return View(product);
        }

    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
