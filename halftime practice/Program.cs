using static System.Console;
using System.Text.Json;

//Product product = new Product {Name = "Яблоко", Price = 50, InStock = true };
List<Product> products = new List<Product>
{
    new Product { Name = "Яблоко", Price = 50, InStock = true},
    new Product { Name = "Молоко", Price = 70, InStock = false},
    new Product { Name = "Хлеб", Price = 30, InStock = true}
};

var options = new JsonSerializerOptions {WriteIndented = true };
string json = JsonSerializer.Serialize(products, options);
await File.WriteAllTextAsync("product.json", json);

string loadedJson = await File.ReadAllTextAsync("product.json");
List<Product> loadedProduct = JsonSerializer.Deserialize<List<Product>>(loadedJson);

foreach (var product in loadedProduct)
{
    WriteLine($"Продукт - {product.Name,-10} | Цена - {product.Price,-3} | Наличие - {(product.InStock ? "Да" : "Нет"),-3}|");
}
public class Product
{
    public string Name { get; set; }
    public int Price { get; set; }
    public bool InStock { get; set; }
}