using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Services
{
    public interface IBrands
    {
        IEnumerable<SelectListItem> GetBrands();
        public void Add(string Name);
        public void Remove(int id);
        Task Create(CreateBrandForm vm);
    }
}
