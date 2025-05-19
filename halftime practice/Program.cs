using static System.Console;

string fileName = "notes.txt";

string content = "Привет, хозяин!";

File.WriteAllText(fileName, content);

if (File.Exists(fileName))
{
    string contentRead = File.ReadAllText(fileName);

    WriteLine("Содержимое файла:");

    WriteLine(contentRead);

    File.AppendAllText(fileName, "\nЧто прикажите?");

    contentRead = File.ReadAllText(fileName);

    WriteLine(contentRead);
}

else
{
    WriteLine("Файл не найден");
}
