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

        public async Task<Success>DeleteProductAsync(string id)
        {
             var response = await _httpClient.DeleteAsync(_url + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                return new Success { message = "Product Deleted Successfully " };
            }
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

       public async Task<Success> UpdateProductAsync(GetProducts products)
        {
            var content = JsonConvert.SerializeObject(products);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_url + "/" + products.ID, bodyContent);

             if (response.IsSuccessStatusCode)
            {
                return new Success { message = "The Product was updated successfully " };
            }
            throw new NotImplementedException();
        }
    }
}