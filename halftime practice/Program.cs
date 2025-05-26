using static System.Console;
using System.Text.Json;
using System.Text.Json.Serialization;
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

}

catch (JsonException)
{
    WriteLine("Ошибка: файл содержит некорректный JSON");
}

catch (Exception ex)
{
    WriteLine($"Ошибка: {ex.Message}");
}

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
    public Profile Profile { get; set; }
}

public class Profile
{
    [JsonPropertyName("location")]
    public string City { get; set; }
    [JsonPropertyName("contact_email")]
    public string Email { get; set; }
}