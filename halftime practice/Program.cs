using static System.Console;

ExchangeStudent exchangeStudent = new ExchangeStudent("Мария", 22, "Франция");
exchangeStudent.DisplayInfo();
AthleteStudent athleteStudent = new AthleteStudent("Петр", 18, "Футбол");
athleteStudent.DisplayInfo();

public class Student
{
    protected string _name; // protected — доступно в классе и наследниках
    protected int _age;

    public Student(string name, int age)
    {
        _name = name;
        _age = age;
    }

    public virtual void DisplayInfo() // virtual — метод можно переопределить
    {
        WriteLine($"Имя: {_name}, Возраст: {_age}");
    }
}

public class ExchangeStudent : Student
{
    private string _country;

    public ExchangeStudent(string name, int age, string country) : base(name, age)
    {
        _country = country;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        WriteLine($"Страна: {_country}");
    }

}

public class AthleteStudent : Student
{
    private string _sport;

    public AthleteStudent(string name, int age, string sport) : base(name, age)
    {
        _sport = sport;
    }

    public override void DisplayInfo() 
    {
        base.DisplayInfo();
        WriteLine($"Вид спорта: {_sport}");
    }
}