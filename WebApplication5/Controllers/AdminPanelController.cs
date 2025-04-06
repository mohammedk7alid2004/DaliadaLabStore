using Microsoft.AspNetCore.Mvc;
using WebApplication4.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WebApplication4.ViewModels;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize(Roles= "admin")]
    public class AdminPanelController : Controller  // ✅ تغيير الاسم هنا
    {
        private readonly DalidaAppDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminPanelController(DalidaAppDbContext context , UserManager<User> userManager, RoleManager<IdentityRole> roleManager) 
            // ✅ تغيير الاسم هنا
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetUsersCount()
        {
            int usersCount = _context.Users.Count();
            return Json(usersCount);
        }
        public async Task< IActionResult> GetUser()
        {
            var users= _context.Users.ToList();
            var modellist = new List<UserViewModel>();
            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                UserViewModel model = new UserViewModel()
                {
                    id = user.Id,
                    fullname = user.UserName,
                    phone = user.PhoneNumber,
                    Email = user.Email,
                    Roles = roles,
                };
                modellist.Add(model);
            }
            return View(modellist);
        }
        [HttpGet]
        public IActionResult addrole_forUser(string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return Content("UserId is required.");
            }

            ViewBag.userid = userid;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addrole_forUser(string userid, string rolename)
        {

            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return Content("User not found.");
            }


            var findrole = roleManager.Roles.Any(r => r.Name == rolename);
            if (!findrole)
            {
                return Content("Role not found.");
            }


            var result = await userManager.AddToRoleAsync(user, rolename);
            if (result.Succeeded)
            {
                return RedirectToAction("GetUser", "DashBoard");
            }

            return Content("Failed to add role.");
        }
        public IActionResult GetProductsCount()
        {
            int productsCount = _context.Products.Count();
            return Json(productsCount);
        }

        public IActionResult GetBrandsCount()
        {
            int brandsCount = _context.Brands.Count();
            return Json(brandsCount);
        }

        public IActionResult GetProfit()
        {
            var profit = _context.OrderDetails.Sum(e => e.LineTotal);
            return Json(profit);
        }
    }
}
