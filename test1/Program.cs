using System.Xml.Linq;
using static System.Console;

List<IUser> users = new List<IUser>();

users.Add(new User("Иван", 25));
users.Add(new AdminUser("admin", 101));

bool restart = true;

do
{
    WriteLine("Введите обычного пользователя: ");
    User user = new User("Иван", 25);
    user.InputUserData();
    user.ValidateData();
    WriteLine($"Роль: {user.GetRole()}");
    users.Add(user);

    Write("Повторить ввод? (да/нет): ");
    string answer = ReadLine();
    restart = answer.ToLower() == "да";
}
while (restart);

WriteLine("Все пользователи: ");

foreach (IUser user in users)
{
    user.ValidateData();
    WriteLine($"Роль: {user.GetRole()}");
    user.PrintInfo();
}

public interface IUser
{
    void ValidateData();
    string GetRole();
    void PrintInfo();
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
            WriteLine("Имя не может быть пустым. Установленно имя по умолчанию: 'Гость'");
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
        WriteLine($"Имя: {Name}, Возраст: {Age}");
    }
}

public class Person
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
        WriteLine($"Имя: {Name}, Возраст: {Age}");
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
        WriteLine($"{GetRole()} Имя: {Name}, Возраст: {Age}");
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

