using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using consoleShop.Models;

namespace consoleShop.Services.Iservice
{
    public interface IService
    {
        Task<Success> CreateProductAsync(AddProduct product);
        Task<List<GetProducts>> GetProductsAsync();
        Task<Success> UpdateProductAsync(GetProducts products);
        Task<GetProducts> GetOneProductAsync(string id);
        Task<Success> DeleteProductAsync(string id);
    }
}