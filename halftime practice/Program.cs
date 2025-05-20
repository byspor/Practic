using static System.Console;

using (var writer = new StreamWriter("shopping.txt"))
{
    writer.WriteLine("Яблоки");
    writer.WriteLine("Клубника");
    writer.WriteLine("Бананы");
    writer.WriteLine("Грейпфрут");
}

using (var reader = new StreamReader("shopping.txt"))
{
    WriteLine("Содержимое корзины:");
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        WriteLine(line);
    }
}

using (var writer = new StreamWriter("shopping.txt", append: true))
{
    WriteLine("Добавить: ");
    string newItem = ReadLine();

    if (string.IsNullOrWhiteSpace(newItem))
    {
        WriteLine("Ошибка: нельзя добавить пустой элемент");
    }
    else
    {
        writer.WriteLine(newItem);

        WriteLine($"\n{newItem} добавлен в корзину\n");
    }
}

using (var reader = new StreamReader("shopping.txt"))
{
    WriteLine("Обновлённое содержимое корзины:");
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        WriteLine(line);
    }
}