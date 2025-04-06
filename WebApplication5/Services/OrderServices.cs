using WebApplication4.Data;
using WebApplication4.Models;

namespace WebApplication4.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly DalidaAppDbContext context;

        public OrderServices(DalidaAppDbContext context)
        {
            this.context = context;
        }
        public void Add(string userid,string UserName, IEnumerable<ShoppingCart> cart)
        {
            var order = new Order()
            {
                UserId = userid,
                OrderDate = DateTime.Now,
                OrderStatus = "Shipping",
                Name=UserName,
            };
            foreach (var item in cart)
            {
                var orderDetail = new OrderDetails
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = Convert.ToDecimal(item.UnitPrice),
                    LineTotal = Convert.ToDecimal( item.UnitPrice) * Convert.ToDecimal( item.Quantity),
                    OrderId = order.Id
                };
                order.OrderDetails.Add(orderDetail);
            }
            context.Orders.Add(order);
            context.SaveChanges();

            
        }
        public void ClearCart(string userId)
        {
            var items = context.ShoppingCart.Where(c => c.UserId == userId);
            context.ShoppingCart.RemoveRange(items);
            context.SaveChanges();
        }
    }
}
