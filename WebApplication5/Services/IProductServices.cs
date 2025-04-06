using WebApplication4.Models;
using WebApplication4.ViewModels;

namespace WebApplication4.Services
{
    public interface IProductServices
    {
        IEnumerable<Product> GetALL();
        Product? GetBy_Id(int id);
        Task Create(CreateProdectFromVM vm);
        Task <Product> GetById(int id);
        Task<Product> Update(UpdateForm v);
        bool Delete(int id);
        IEnumerable<Product>GetByName(string name);
        IEnumerable<Product> GetByBrand(int id);
        IEnumerable<Product> GetAllR(int brand);
        IEnumerable<Brand> GetAllBrands();



    }
}
