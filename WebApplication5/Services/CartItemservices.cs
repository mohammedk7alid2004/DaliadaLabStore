using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Services
{
    public class CartItemServices : ICartItem
    {
        private readonly DalidaAppDbContext context;

        public CartItemServices(DalidaAppDbContext context)
        {
            this.context = context;
        }
     

        public void Add(int productId , string UserId)
        {
            var product = context.Products.FirstOrDefault(x => x.Id == productId);

            if (product == null || product.StockQuantity <= 0)
            {
                throw new Exception("Product not available or out of stock.");
            }

            var existingCartItem = context.ShoppingCart.FirstOrDefault(c => c.ProductId == productId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
            }
            else
            {
                var cartItem = new ShoppingCart()
                {
                    ProductId = product.Id,
                    UnitPrice = product.Price,
                    Quantity = 1,
                    UserId = UserId,
                };
                context.ShoppingCart.Add(cartItem);
            }

            product.StockQuantity -= 1;
            context.SaveChanges();
        }
       
        public int Counter(string User)
        {
            var count= context.ShoppingCart.Where(e=>e.UserId== User).Count();
           if(count > 0)
            {
                return count;
            }
           else
                { return 0; }
        }

        public IEnumerable<ShoppingCart> GetItems(string userId)
        {
            return context.ShoppingCart.Include(c => c.Product).Include(x=>x.Product.Brand).Where(c => c.UserId == userId).ToList();
        }

        public void Remove(int ID, string UserId)
        {
            var cart = context.ShoppingCart.FirstOrDefault(e=>e.ProductId == ID);
            if(cart != null)
            {
                if (cart.Quantity > 1)
                {
                    cart.Quantity -= 1;
                }
                else
                {
                    context.ShoppingCart.Remove(cart);
                }
                var product = context.Products.FirstOrDefault(x=>x.Id == ID);
                if(product != null)
                {
                    product.StockQuantity += 1;

                }
                context.SaveChanges();

            }
        }
    }

}
