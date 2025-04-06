using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Services;
using System.Threading.Tasks;

namespace WebApplication4.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderServices order;
        private readonly UserManager<User> user;
        private readonly CartItemServices c;

        public OrderController(OrderServices order, UserManager<User> user ,CartItemServices c)
        {
            this.order = order;
            this.user = user;
            this.c = c;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder()
        {
            var user1 = await user.GetUserAsync(User); 
            if (user1 == null)
            {
                return Unauthorized("User not found or not logged in.");
            }
           var t=  c.GetItems(user1.Id);
            order.Add(user1.Id, user1.UserName,t);
            order.ClearCart(user1.Id) ; 

            return Ok(new { message = "Order added successfully!" }); // أعد استجابة مناسبة
        }
    }
}
