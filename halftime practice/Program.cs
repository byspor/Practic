using static System.Console;

File.WriteAllText("shopping.txt", "Яблоки\nКлубника\nБананы\nГрейпфрут\n");

WriteLine("Содержимое корзины:");
string readContent = File.ReadAllText("shopping.txt");
WriteLine(readContent);

WriteLine("Добавить: ");
string newItem = ReadLine();

if (string.IsNullOrWhiteSpace(newItem))
{
    WriteLine("Ошибка: нельзя добавить пустой элемент");
}
else
{
    File.AppendAllText("shopping.txt", newItem + "\n");

    WriteLine($"{newItem} добавлен в корзину");

    WriteLine("Обновлённое содержимое корзины:");
    readContent = File.ReadAllText("shopping.txt");
    WriteLine(readContent);
}
