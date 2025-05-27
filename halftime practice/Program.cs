using static System.Console;
using System.Text.Json;
using System.Text.Json.Serialization;

User user = new User
{
    Name = "Иван",
    Age = 25,
    Favourites = new List<string> { "яблоко", "груша", "апельсин" }

};


var options = new JsonSerializerOptions { WriteIndented = true };
options.Converters.Add(new StringToListConverter());

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
    User loadedUsers = JsonSerializer.Deserialize<User>(loadedJson, options);

    WriteLine($"Имя: {loadedUsers.Name}, Возраст: {loadedUsers.Age}");

    WriteLine($"Любимые товары: ");

    foreach (var item in loadedUsers.Favourites)
    {
        WriteLine($"- {item}");
    }

}

catch (JsonException)
{
    WriteLine("Ошибка: файл содержит некорректный JSON");
}

catch (Exception ex)
{
    WriteLine($"Ошибка: {ex.Message}");
}

public class StringToListConverter : JsonConverter<List<string>>
{
    public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            string value = reader.GetString();
            return value?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

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

public class User
{
    [JsonPropertyName("username")]
    public string Name { get; set; }

    [JsonPropertyName("user_age")]
    public int Age { get; set; }
    //public Profile Profile { get; set; }

    public List<string> Favourites { get; set; }
}

public class Profile
{
    [JsonPropertyName("location")]
    public string City { get; set; }

    [JsonPropertyName("contact_email")]
    public string Email { get; set; }
}