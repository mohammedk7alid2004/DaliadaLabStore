using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Services;

[Route("ShoppingCart")]
public class ShoppingCartController : Controller
{
    private readonly CartItemServices cartItem;
    private readonly UserManager<User> user;

    public ShoppingCartController(CartItemServices cartItem, UserManager<User> user)
    {
        this.cartItem = cartItem;
        this.user = user;
    }

    [HttpPost("AddOrder")]
    public async Task<IActionResult> AddOrder([FromForm] int productId)
    {
        try
        {
            var user1 = await user.GetUserAsync(User);
            if (user1 == null)
            {
                return Unauthorized(new { success = false, message = "User not logged in." });
            }

            cartItem.Add(productId, user1.Id);
            return Json(new { success = true, message = "Product added to cart successfully." });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return Json( new { success = false, message = "An error occurred." });
        }
    }

    [HttpGet("GetCount")]
    public async Task<IActionResult> GetCount()
    {
        var user1 = await user.GetUserAsync(User);
        if (user1 == null)
        {
            return NotFound("User not found");
        }

        int itemCount = cartItem.Counter(user1.Id);
        return Ok(new { Count = itemCount });
    }

    [HttpGet("GetItems")]
    public async Task<IActionResult> GetItems()
    {
        var user1 = await user.GetUserAsync(User);
        if (user1 == null)
        {
            return Unauthorized(new { success = false, message = "User not logged in." });
        }

        var ProductInCartItem = cartItem.GetItems(user1.Id);
        return View(ProductInCartItem);
    }

    [HttpPost("Remove")]
    public async Task<IActionResult> Remove([FromForm] int productId)
    {
        try
        {
            var user1 = await user.GetUserAsync(User);
            if (user1 == null)
            {
                return Unauthorized(new { success = false, message = "User not logged in." });
            }

            cartItem.Remove(productId, user1.Id);
            return Json(new { success=true, message = "Item removed from cart successfully." });
        }
        catch (Exception ex)
        {
            return Json(500, new { success = false, message = ex.Message });
        }
    }
}
