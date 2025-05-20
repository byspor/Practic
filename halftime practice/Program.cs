using static System.Console;

using (var writer = new StreamWriter("shopping.txt"))
{
    writer.WriteLine("Яблоки");
    writer.WriteLine("Клубника");
    writer.WriteLine("Бананы");
    writer.WriteLine("Грейпфрут");
}


WriteLine("Содержимое корзины:");
PrintFileContent();

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

WriteLine("Обновлённое содержимое корзины:");
PrintFileContent();

void PrintFileContent()
{
    using (var reader = new StreamReader("shopping.txt"))
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            WriteLine(line);
        }
    }
}