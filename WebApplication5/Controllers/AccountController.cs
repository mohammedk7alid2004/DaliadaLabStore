using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.InteropServices;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> ss;

        public AccountController(UserManager<User> userManager, SignInManager<User> ss)
        {
            this.userManager = userManager;
            this.ss = ss;
        }
        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveRegister(RegisterUserViewModel vm)
        {
            if (ModelState.IsValid)
            {
                User S = new User();
                S.Email = vm.Email;
                S.UserName = vm.UserName;
                S.PasswordHash = vm.Password;
                
                S.PhoneNumber = vm.PhoneNumber;
                S.Address = vm.Address;
                IdentityResult ss11 = await userManager.CreateAsync(S,vm.Password);
                if (ss11.Succeeded)
                {
                    await ss.SignInAsync(S, false);
                    await userManager.AddToRoleAsync(S, "user");

                    return RedirectToAction("Index", "Home");
                }

                foreach (var item in ss11.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View("Register", vm);
        }
        public IActionResult Login()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel uservm)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByEmailAsync(uservm.Email);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, uservm.Password);
                    if (found)
                    {
                        await ss.SignInAsync(user, uservm.RememberMe);
                        TempData["LoginSuccess"] = "Login successful! Welcome to MASHROEE.";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Password Wrong");
                    }
                }
                ModelState.AddModelError("", "Username  Wrong");
            }

            return View(uservm);
        }
        public IActionResult Logout()
        {
            ss.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}




