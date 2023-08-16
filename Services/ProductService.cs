using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using consoleShop.Models;
using consoleShop.Services.Iservice;
using Newtonsoft.Json;
using System.Text;

namespace consoleShop.Services
{
    public class ProductService : IService
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "http://localhost:3000/Products";

        public ProductService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<Success> CreateProductAsync(AddProduct product)
        {
            var products = JsonConvert.SerializeObject(product);
            var body = new StringContent(products, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url, body);
            if(response.IsSuccessStatusCode){
                return new Success{
                    message = "products added successfully"
                };
            }
            throw new NotImplementedException();
        }

        Task<Success>IService.DeleteProductAsync(string id)
        {
            throw new NotImplementedException();
        }

         public async Task<GetProducts> GetOneProductAsync(string id)
        {
            var response = await _httpClient.GetAsync(_url+"/"+id);
            var product= JsonConvert.DeserializeObject<GetProducts>( await response.Content.ReadAsStringAsync());
            if(response.IsSuccessStatusCode){
                return product;
            }
            throw new NotImplementedException();
        }

        public async Task<List<GetProducts>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync(_url);

            var products = JsonConvert.DeserializeObject<List<GetProducts>>( await response.Content.ReadAsStringAsync());
            if(response.IsSuccessStatusCode){
                return products;
            }
            throw new NotImplementedException("no products found");
        }

        Task<Success> IService.UpdateProductAsync(GetProducts products)
        {
            throw new NotImplementedException();
        }
    }
}