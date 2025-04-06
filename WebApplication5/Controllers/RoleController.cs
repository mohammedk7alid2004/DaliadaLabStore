using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers;

public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly UserManager<User> userManager;

    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        this.roleManager = roleManager;
        this.userManager = userManager;
    }

    public IActionResult Index()
    {
        var listrole = roleManager.Roles.ToList();
        List<RolesViewModel> roleViewModellist = new List<RolesViewModel>();
        foreach (var role in listrole)
        {
            RolesViewModel model = new RolesViewModel();
            model.Name = role.Name;
            roleViewModellist.Add(model);
        }


        return View(roleViewModellist);
    }
    [HttpGet]
    public IActionResult AddRole()
    {
        return View();
    }
    [HttpPost]
    public async Task<  IActionResult> SaveRole(RolesViewModel newrole)
    {
        if (ModelState.IsValid)
        {
            IdentityRole role = new IdentityRole();
            role.Name = newrole.Name;
            var checkrol = await roleManager.FindByNameAsync(role.Name);
            if (checkrol == null)
            {
                IdentityResult result = await roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return Content("Role is added before");
            }

        }
        return Content("Error");

    }
}
