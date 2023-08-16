using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using consoleShop.Models;
using consoleShop.Services;

namespace consoleShop.Controllers
{
    public class Controller
    {
        ProductService newService = new ProductService();
        public static async Task Init(){
            Console.WriteLine("Welcome to consoleSho, what interests you?");

            Console.WriteLine("============================");

            Console.WriteLine("1. Add product");
            Console.WriteLine("2. View all products");
            Console.WriteLine("3. Update a product");
            Console.WriteLine("4. Delete a product");

            await new Controller().Options();

        }

        public async Task Options(){

            System.Console.WriteLine("select way to Proceed");
            var action = Console.ReadLine();
            switch(action){
                case "1":
                    Console.WriteLine("Add product here");
                    await AddItem();
                    break;
                 case "2":
                    Console.WriteLine("View all product here");
                    await ViewProducts();
                    break;
                 case "3":
                    Console.WriteLine("Select a product to Update");
                    break;
                 case "4":
                    Console.WriteLine("Select a product to Delete");
                    break;
            }
        }
        public async Task AddItem(){
                Console.WriteLine("Enter Product Name");
                var name = Console.ReadLine();
                Console.WriteLine("Enter Product Description");
                var description = Console.ReadLine();
                Console.WriteLine("Enter Product Quantity");
                var quantity= Console.ReadLine();
                Console.WriteLine("Enter Product Price");
                var price = Console.ReadLine();

                var item =  new AddProduct(){
                    ProductName = name,
                    Description = description,
                    Quantity = quantity,
                    Price = price
                };
                try
                {
                    var response = await newService.CreateProductAsync(item);
                    Console.WriteLine(response.message);
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            }

            public async Task ViewProducts (){
                try
                {
                    var products = await newService.GetProductsAsync();
                    foreach(var product in products){
                        await Console.Out.WriteLineAsync($"{product.ID}. {product.ProductName} {product.Description} @{product.Price}");
                    }

                    System.Console.WriteLine("======");
                    System.Console.WriteLine("Select one product to proceed");
                    var id = Console.ReadLine();
                    
                    await ViewOne(id);
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
            public async Task ViewOne(string id){
                try
                {
                    var product = await newService.GetOneProductAsync(id);

                    System.Console.WriteLine($"{product.ID}. {product.ProductName} {product.Description} @{product.Price}");
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
            }
    }
}