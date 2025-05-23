using static System.Console;
using System.Text.Json;

Product product = new Product {Name = "Яблоко", Price = 50, InStock = true };

var options = new JsonSerializerOptions {WriteIndented = true };
string json = JsonSerializer.Serialize(product, options);
await File.WriteAllTextAsync("product.json", json);

string loadedJson = await File.ReadAllTextAsync("product.json");
Product loadedProduct = JsonSerializer.Deserialize<Product>(loadedJson);

WriteLine($"Продукт - {loadedProduct.Name, -10} | Цена - {loadedProduct.Price, -3} | Наличие - {(loadedProduct.InStock ? "Да" : "Нет"), -3}|");
public class Product
{
    public string Name { get; set; }
    public int Price { get; set; }
    public bool InStock { get; set; }
}