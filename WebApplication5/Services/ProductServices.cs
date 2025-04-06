using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Services
{

    public class ProductServices : IProductServices
    {
        private DalidaAppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string images_path;

        public ProductServices(DalidaAppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            images_path = $"{_webHostEnvironment.WebRootPath}/asset/images";
        }
        public IEnumerable<Product> GetALL()
        {
           
            return _context.Products.Include(g => g.Brand).ToList();
        }
        public Product? GetBy_Id(int id)
        {
            return _context.Products.Include(g => g.Brand).AsNoTracking().SingleOrDefault(p => p.Id == id);
        }

        public async Task Create(CreateProdectFromVM vm)
        {
            var covername = $"{Guid.NewGuid()}{Path.GetExtension(vm.Cover.FileName)}";
            var path = Path.Combine(images_path, covername);
            using var Stream = File.Create(path);
            await vm.Cover.CopyToAsync(Stream);
            Product p1 = new()
            {
                ProductName = vm.ProductName,
                Description = vm.Description,
                Price = vm.Price,
                BrandId = vm.BrandId,
                Cover = covername,
                StockQuantity = vm.StockQuantity,

            };
            _context.Products.Add(p1);
            _context.SaveChanges();
        }

        public async Task<Product> GetById(int id)
        {
            return _context.Products.FirstOrDefault(e => e.Id == id);
        }

        public async Task<Product?> Update(UpdateForm v)
        {
            var pp = _context.Products.Find(v.id);
            var old = pp.Cover;
            if (pp is null)
                return null;
            else
            pp.ProductName = v.ProductName;
            pp.Description = v.Description;
            pp.Price = v.Price;
            pp.BrandId = v.BrandId;
            var hasnewcover = v.Cover is not null;
            if(hasnewcover)
            {
                var covername = $"{Guid.NewGuid()}{Path.GetExtension(v.Cover.FileName)}";
                var path = Path.Combine(images_path, covername);
                using var Stream = File.Create(path);
                await v.Cover.CopyToAsync(Stream);
                pp.Cover = covername;
            }
            var ef_row = _context.SaveChanges();
            if(ef_row>0)
            {
                if(hasnewcover)
                {
                    var cover = Path.Combine(images_path, old);
                    File.Delete(cover);

                }
                return pp;

            }
            else
            {
                var cover = Path.Combine(images_path, pp.Cover);
                File.Delete(cover);

                return null;

            }
        }

        public bool Delete(int id)
        {
            bool IsDeleted = false;
            var pp = _context.Products.Find(id);
            if(pp is null)
            {
                return false;   
            }
            _context.Products.Remove(pp);
            var efrow= _context.SaveChanges();
            if(efrow>0)
            {
                IsDeleted = true;
                var cover1=Path.Combine(images_path, pp.Cover);
                File.Delete(cover1 );

            }
            return IsDeleted;
        }

        public IEnumerable<Product> GetByName(string name)
        {
            var products = _context.Products.Include(p => p.Brand)
                                   .Where(p => p.ProductName.Contains(name))
                                   .ToList();
            return products;
        }

        public IEnumerable<Product> GetByBrand(int id)
        {
            var product = _context.Products.Include(p => p.Brand).Where(p=>p.BrandId == id).ToList();
            return product;
        }

        public IEnumerable<Product> GetAllR(int brand)
        {

            var products = GetByBrand(brand).Take(7);
            return products;
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            var brands = _context.Brands.ToList();
            return brands;
        }
    }
}
