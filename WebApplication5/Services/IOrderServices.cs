using WebApplication4.Models;

namespace WebApplication4.Services
{
    public interface IOrderServices
    {
        public void ClearCart(string userId);

        public void Add(string userid ,string UserName ,IEnumerable <ShoppingCart> cart );
    }
}
