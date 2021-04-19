using ProductAPI.Controllers.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductApp.Models;

namespace ProductAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductCategory>> GetProductCategories();
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(int id, Product product);
        void DeleteProduct(int id);
        Task<Product> GetProductbyId(int id);
        Task<List<Product>> GetAllProducts();

    }
}
