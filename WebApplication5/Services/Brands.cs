using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Services;

public class Brands : IBrands
{
    private DalidaAppDbContext _context;

    public Brands(DalidaAppDbContext context)
    {
        _context = context;
    }

    public void Add(string Name)
    {
        var brand = new Brand()
        {
            Name = Name
        };
        _context.Brands.Add(brand);
        _context.SaveChanges();
    }

    public  async  Task Create(CreateBrandForm vm)
    {
        Brand p = new()
        {
            Name = vm.Name,
        };
        _context.Brands.Add(p); 
        _context.SaveChanges();
    }

    public IEnumerable<SelectListItem> GetBrands()
    {
        return _context.Brands.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).OrderBy(c => c.Text).ToList();
    }

    public void Remove(int id)
    {
        var item = _context.Brands.FirstOrDefault(b => b.Id == id);
        if (item != null)
        {
            _context.Brands.Remove(item);
        }
        _context.SaveChanges();
    }
}
