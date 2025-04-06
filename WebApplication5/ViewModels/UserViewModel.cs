using WebApplication4.Models;

namespace WebApplication4.ViewModels
{
    public class UserViewModel
    {
        public string id { get; set; }
        public string fullname { get; set; }
        public string Email { get; set; }
        public string phone { get; set; }
        public IList<string>? Roles { get; set; }

    }
}
