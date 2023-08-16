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
            Console.WriteLine("Welcome to consoleShop, what interests you?");

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
                    await UpdateProduct();
                    break;
                 case "4":
                    Console.WriteLine("Select a product to Delete");
                    await DeleteProduct();
                    break;
                default:
                    Controller.Init();
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
                    Console.ForegroundColor = ConsoleColor.Blue;
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
             public async Task UpdateProduct()
        {
            try
            {
                var products = await newService.GetProductsAsync();
                foreach (var product in products)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    await Console.Out.WriteLineAsync($"{product.ID}. {product.ProductName}, {product.Description} @ {product.Price}");
                    Console.ResetColor();
                }
                Console.Write("Enter product id: ");
                var id = Console.ReadLine();


                var current_product = products.FirstOrDefault(x => x.ID == id);

                Console.Write($"Enter product name: {current_product.ProductName}: New name: ");
                var productName = Console.ReadLine();
                if (productName == "")
                {
                    productName = current_product.ProductName;
                }

                Console.Write($"Enter product description: {current_product.Description}: New description: ");
                var productDescription = Console.ReadLine();
                if (productDescription == "")
                {
                    productDescription = current_product.Description;
                }

                Console.Write($"Enter product price: {current_product.Price}: New price: ");
                var productPrice = Console.ReadLine();
                if (productPrice == "")
                {
                    productPrice = current_product.Price;
                }

                var newProduct = new GetProducts()
                {
                    ID = id,
                    ProductName = productName,
                    Description = productDescription,
                    Price = productPrice,
                };

                try
                {
                    var res = await newService.UpdateProductAsync(newProduct);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(res.message);
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
         public async Task DeleteProduct()
        {
            try
            {
                var products = await newService.GetProductsAsync();
                foreach (var product in products)
                {
                    await Console.Out.WriteLineAsync($"{product.ID}. {product.ProductName}, {product.Description} @ {product.Price}");
                    Console.ResetColor();
                }
                Console.Write("Enter product id to delete: ");
                var id = Console.ReadLine();

                try
                {
                    var res = await newService.DeleteProductAsync(id);
                    Console.WriteLine(res.message);
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}