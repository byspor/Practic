using static System.Console;
using System.Text.Json;

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
    public string Name { get; set; }
    public int Age { get; set; }
    public Profile Profile { get; set; }
}

public class Profile
{
    public string City { get; set; }
    public string Email { get; set; }
}