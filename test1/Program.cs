using System.IO;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

List<IUser> users = new List<IUser>();

users.Add(new User("Иван", 25));
users.Add(new AdminUser("admin", 101));
users.Add(new Person("Оля", 22));
users.Add(new GuestUser("Светлана", 18));

string filePath = "users.txt";

await SaveUsersToFileAsync(users, filePath);

var loadedUsers = await LoadUsersFromFileAsync(filePath);


WriteLine("Загруженные пользователи: ");
foreach (var user in loadedUsers)
{
    user.PrintInfo();
}

await RemoveUserFromFileAsync(filePath, "admin");
WriteLine("Обновлённый список");
var updateUsers = await LoadUsersFromFileAsync(filePath);
foreach (var user in updateUsers)
{
    user.PrintInfo();
}

await UpdateUserInFileAsync(filePath, "Иван", "Пётр", 35, "Пользователь");
updateUsers = await LoadUsersFromFileAsync(filePath);
foreach (var user in updateUsers)
{
    user.PrintInfo();
}

async Task SaveUsersToFileAsync(List<IUser> users, string filePath)
{
    try
    {
        var lines = users.Select(u => $"{u.Name},{u.Age},{u.GetRole()}");
        await File.WriteAllLinesAsync(filePath, lines);
    }
    catch (Exception ex)
    {
        WriteLine($"Ошибка записи в файл: {ex.Message}");
    }
}

async Task<List<IUser>> LoadUsersFromFileAsync(string filePath)
{
    var loadedUsers = new List<IUser>();

    if (!File.Exists(filePath))
    {
        WriteLine("Файл не найден");
        return loadedUsers;
    }

    try
    {
        var lines = await File.ReadAllLinesAsync(filePath);


        foreach (var line in lines)
        {
            string[] parts = line.Split(',');
            string name = parts[0];
            int age = int.Parse(parts[1]);
            string role = parts[2];

            switch (role)
            {
                case "Пользователь":
                    loadedUsers.Add(new User(name, age));
                    break;
                case "Администратор":
                    loadedUsers.Add(new AdminUser(name, age));
                    break;
                case "Обычный пользователь":
                    loadedUsers.Add(new Person(name, age));
                    break;
                case "Гость":
                    loadedUsers.Add(new GuestUser(name, age));
                    break;
            }
        }


    }

    catch (Exception ex)
    {
        WriteLine($"Ошибка чтения файла: {ex.Message}");
    }

    return loadedUsers;
}

async Task RemoveUserFromFileAsync(string filePath, string nameToRemove)
{
    if (!File.Exists(filePath))
    {
        WriteLine("Файл не найден");
        return;
    }

    try
    {
        var lines = await File.ReadAllLinesAsync(filePath);
        var updateLines = lines.Where(line =>
        {
            string[] parts = line.Split(',');
            return parts[0] != nameToRemove;
        }
            );

        await File.WriteAllLinesAsync(filePath, updateLines);
        WriteLine($"Пользователь {nameToRemove} удален");
    }

    catch (Exception ex)
    {
        WriteLine($"Ошибка удаления: {ex.Message}");
    }

}

async Task UpdateUserInFileAsync(string filePath, string oldName, string newName, int newAge, string newRole)
{
    if (!File.Exists(filePath))
    {
        WriteLine("Файл не найден");
        return;
    }

    try
    {
        var lines = await File.ReadAllLinesAsync(filePath);
        var updateLines = new List<string>();

        foreach (var line in lines)
        {
            string[] parts = line.Split(',');

            if (parts[0] == oldName)
            {
                updateLines.Add($"{newName},{newAge},{newRole}");
            }
            else
            {
                updateLines.Add(line);
            }
        }

        await File.WriteAllLinesAsync(filePath, updateLines);
        WriteLine($"Данные о пользователе {oldName} обновлены");
    }

    catch (Exception ex)
    {
        WriteLine($"Ошибка обновления: {ex.Message}");
    }
}



//bool restart = true;

//do
//{
//    WriteLine("Введите обычного пользователя: ");
//    User user = new User("Иван", 25);
//    user.InputUserData();
//    user.ValidateData();
//    WriteLine($"Роль: {user.GetRole()}");
//    user.LogActivity();
//    users.Add(user);



//    Write("Повторить ввод? (да/нет): ");
//    string answer = ReadLine();
//    restart = answer.ToLower() == "да";
//}
//while (restart);

//WriteLine("Все пользователи: ");

//foreach (IUser user in users)
//{
//    user.ValidateData();
//    WriteLine($"Роль: {user.GetRole()}");
//    user.PrintInfo();
//}

public interface IUser
{
    void ValidateData();
    string GetRole();
    void PrintInfo();

    string Name { get; set; }
    int Age { get; set; }
}

public abstract class BaseUser : IUser
{
    public string Name { get; set; }

    public int Age { get; set; }

    public BaseUser(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public abstract void ValidateData();
    public abstract string GetRole();

    public abstract void PrintInfo();

    public virtual void LogActivity()
    {
        Write("Логи активированны ");
    }
}

public class User : BaseUser
{
    public User(string name, int age) : base(name, age)
    {
    }

    public override void ValidateData()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            Name = "Гость";
        }
        if (Age < 0)
        {
            Age = 0;
        }
    }

    public string GetName()
    {
        Write("Введите имя: ");

        string input = ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            WriteLine("Имя не может быть пустым. Установлено имя по умолчанию: 'Гость'");
            return "Гость";
        }

        if (input.ToLower() == "admin")
        {
            WriteLine("Добрый день хозяин ;)");
        }

        return input;
    }

    public int GetInteger(string prompt)
    {
        bool isValid = false;
        int attemptCount = 0;

        do
        {
            Write(prompt);
            string Input = (ReadLine());

            isValid = int.TryParse(Input, out int tempAge);

            if (!isValid)
            {
                attemptCount++;
                WriteLine("Ошибка: введено не число! Попробуйте снова");
            }
            else
            {
                Age = tempAge;
            }

            if (attemptCount >= 3)
            {
                WriteLine("Слишком много ошибок. Программа завершена.");
                Environment.Exit(0);
            }
        }
        while (!isValid);

        if (Age < 18)
        {
            WriteLine("Вы несовершеннолетний. Доступ запрещён!");
        }
        else
        {
            WriteLine("Поздравляем! Вы совершеннолетний!");
            WriteLine($"Привет, {Name}!\nВаш возраст = {Age}");
        }
        return Age;
    }

    public void InputUserData()
    {
        Name = GetName();
        Age = GetInteger("Введите возраст: ");
    }

    public override string GetRole()
    {
        return "Пользователь";
    }

    public override void PrintInfo()
    {
        WriteLine($"[{GetRole(),-20}] | Имя: {Name,-10} | Возраст: {Age,3} |");
    }

    public override void LogActivity()
    {
        base.LogActivity();
        WriteLine($"для {Name}");
    }
}

public class Person : IUser
{
    public string Name { get; set; }
    private int _age;


    public int Age
    {
        get
        {
            return _age;
        }
        set
        {
            if (value < 0)
                _age = 0;
            else
                _age = value;
        }
    }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public virtual void PrintInfo()
    {
        WriteLine($"[{GetRole(),-20}] | Имя: {Name,-10} | Возраст: {Age,3} |");
    }

    public virtual void ValidateData()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            Name = "Гость";
        }
        if (Age < 0)
        {
            Age = 0;
        }
    }

    public string GetRole()
    {
        return "Обычный пользователь";
    }
}

public class AdminUser : BaseUser
{
    public AdminUser(string name, int age) : base(name, age)
    {
    }

    public override string GetRole()
    {
        return "Администратор";
    }

    public override void PrintInfo()
    {
        WriteLine($"[{GetRole(),-20}] | Имя: {Name,-10} | Возраст: {Age,-3} |");
    }

    public override void ValidateData()
    {
        if (!string.IsNullOrWhiteSpace(Name))
        {
            WriteLine("Имя администратора не может быть пустым. Ошибка входа!");
        }

        if (Age > 100)
        {
            Age = 100;
        }
    }

}

public class GuestUser : IUser
{
    public string Name { get; set; }
    public int Age { get; set; }

    public GuestUser(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string GetRole()
    {
        return "Гость";
    }

    public void PrintInfo()
    {
        WriteLine($"[{GetRole(),-20}] | Имя: {Name,-10} | Возраст: {Age,3} |");
    }

    public void ValidateData()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            Name = "Гость";
        }
        if (Age < 0)
        {
            Age = 0;
        }
    }
}

