using System.Diagnostics.Metrics;
using WebApplication4.Models;

namespace WebApplication4.Services
{
    public interface ICartItem
    {
        IEnumerable<ShoppingCart> GetItems(string add);
        public void  Add(int ID , string UserId);
        public  int  Counter(String User);

        public void Remove(int ID, string UserId);
    }
}
