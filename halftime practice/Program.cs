using static System.Console;

string cart = "Яблоки\nКлубника\nБананы\nГрейпфрут\n";

string path = "shopping.txt";

await WriteToFileAsync(path, cart, false);

WriteLine("Содержимое корзины:");
await ReadFromFileAsync(path);

Write("Добавить: ");
string newItem = ReadLine();

if (string.IsNullOrWhiteSpace(newItem))
{
    WriteLine("Ошибка: нельзя добавить пустой элемент");
}
else
{
    Write("Перезаписать файл (да/нет)? ");

    string answer = ReadLine().ToLower();

    bool append = answer == "да" ? false : true;

    await WriteToFileAsync(path, newItem + "\n", append);
    
    WriteLine($"\n{newItem} добавлен в корзину\n");
}


WriteLine("Обновлённое содержимое корзины:");
await ReadFromFileAsync(path);


async Task WriteToFileAsync(string path, string content, bool append = true)
{
    using (var writer = new StreamWriter(path, append))
    {
        await writer.WriteAsync(content);
    }
}

async Task ReadFromFileAsync(string path)
{
    using (var reader = new StreamReader(path))
    {
        string line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            WriteLine(line);
        }
    }
}