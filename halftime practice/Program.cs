using static System.Console;
using System.Text.Json;




//Product product = new Product {Name = "Яблоко", Price = 50, InStock = true };

//List<Product> products = new List<Product>
//{
//    new Product { Name = "Яблоко", Price = 50, InStock = true},
//    new Product { Name = "Молоко", Price = 70, InStock = false},
//    new Product { Name = "Хлеб", Price = 30, InStock = true}
//};


User user = new User
{
    Name = "Иван",
    Age = 25,
    Profile = new Profile 
    {  
        City = "Москва",
        Email = "ivan@example.com"
    }
};

var options = new JsonSerializerOptions { WriteIndented = true };
string json = JsonSerializer.Serialize(user, options);
await File.WriteAllTextAsync("user.json", json);

if (!File.Exists("user.json"))
{
    WriteLine("Файл не найден");
    return;
}

try
{
    string loadedJson = await File.ReadAllTextAsync("user.json");
    User loadedUsers = JsonSerializer.Deserialize<User>(loadedJson);

    WriteLine($"Имя: {loadedUsers.Name}, Возраст: {loadedUsers.Age}");

    WriteLine($"Город: {loadedUsers.Profile.City}");
    WriteLine($"Email: {loadedUsers.Profile.Email}");

    //foreach (var product in loadedProduct)
    //{
    //    WriteLine($"Продукт - {product.Name,-10} | Цена - {product.Price,-3} | Наличие - {(product.InStock ? "Да" : "Нет"),-3}|");
    //}
}

catch (JsonException)
{
    WriteLine("Ошибка: файл содержит некорректный JSON");
}

catch (Exception ex)
{
    WriteLine($"Ошибка: {ex.Message}");
}


//var options = new JsonSerializerOptions {WriteIndented = true };
//string json = JsonSerializer.Serialize(products, options);
//await File.WriteAllTextAsync("product.json", json);

//string loadedJson = await File.ReadAllTextAsync("product.json");
//List<Product> loadedProduct = JsonSerializer.Deserialize<List<Product>>(loadedJson);

//foreach (var product in loadedProduct)
//{
//    WriteLine($"Продукт - {product.Name,-10} | Цена - {product.Price,-3} | Наличие - {(product.InStock ? "Да" : "Нет"),-3}|");
//}

public class Product
{
    public string Name { get; set; }
    public int Price { get; set; }
    public bool InStock { get; set; }
}

public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public Profile Profile { get; set; }
}

public class Profile
{
    public string City { get; set; }
    public string Email { get; set; }
}