using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Lab9T1
{
    public void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Console.WriteLine("--- Завдання 1 (Stack) ---");
        string input = "abc#d##c";
        string result = ProcessBackspace(input);
        Console.WriteLine($"Вхідний рядок: {input}");
        Console.WriteLine($"Результат: {result}");
    }

    private string ProcessBackspace(string text)
    {
        Stack<char> stack = new Stack<char>();
        foreach (char ch in text)
        {
            if (ch == '#')
            {
                if (stack.Count > 0) stack.Pop();
            }
            else
            {
                stack.Push(ch);
            }
        }
        char[] resultArray = stack.ToArray();
        Array.Reverse(resultArray);
        return new string(resultArray);
    }
}

public class Student
{
    public string FullName { get; set; }
    public string Group { get; set; }
    public int[] Grades { get; set; }
    public bool IsSuccessful => Grades.All(g => g >= 4);

    public override string ToString() => $"{FullName} (Група: {Group}), Оцінки: {string.Join(", ", Grades)}";
}

public class Lab9T2
{
    public void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Console.WriteLine("\n--- Завдання 2 (Queue) ---");
        // Симуляція читання з файлу (створення тестових даних)
        List<Student> students = new List<Student>
        {
            new Student { FullName = "Іванов І.І.", Group = "КН-1", Grades = new[] { 5, 4, 5 } },
            new Student { FullName = "Петров П.П.", Group = "КН-2", Grades = new[] { 3, 4, 5 } },
            new Student { FullName = "Сидоров С.С.", Group = "КН-1", Grades = new[] { 5, 5, 5 } },
            new Student { FullName = "Коваленко О.О.", Group = "КН-2", Grades = new[] { 2, 3, 4 } }
        };

        Queue<Student> highAchievers = new Queue<Student>();
        Queue<Student> others = new Queue<Student>();

        foreach (var s in students)
        {
            if (s.IsSuccessful) highAchievers.Enqueue(s);
            else others.Enqueue(s);
        }

        Console.WriteLine("Успішні студенти (4-5):");
        while (highAchievers.Count > 0) Console.WriteLine(highAchievers.Dequeue());

        Console.WriteLine("\nІнші студенти:");
        while (others.Count > 0) Console.WriteLine(others.Dequeue());
    }
}

public class Lab9T3
{
    public void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Console.WriteLine("\n--- Завдання 3 (ArrayList) ---");

        // Переробка Завдання 1
        string input = "abc#d##c";
        ArrayList list = new ArrayList();
        foreach (char ch in input)
        {
            if (ch == '#') { if (list.Count > 0) list.RemoveAt(list.Count - 1); }
            else { list.Add(ch); }
        }
        Console.Write("Результат Backspace: ");
        foreach (var item in list) Console.Write(item);
        Console.WriteLine();
    }
}

public class Lab9T4
{
    private Hashtable catalog = new Hashtable();

    public void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Console.WriteLine("\n--- Завдання 4 (Hashtable) ---");
        AddDisk("The Wall", "Pink Floyd");
        AddSong("The Wall", "Another Brick in the Wall");
        AddSong("The Wall", "Hey You");

        AddDisk("Discovery", "Daft Punk");
        AddSong("Discovery", "One More Time");

        ViewCatalog();
        SearchArtist("Pink Floyd");
    }

    public void AddDisk(string title, string artist) => catalog[title] = new List<string> { artist }; // Перший елемент - виконавець

    public void AddSong(string diskTitle, string song)
    {
        if (catalog.ContainsKey(diskTitle)) ((List<string>)catalog[diskTitle]).Add(song);
    }

    public void ViewCatalog()
    {
        Console.WriteLine("Весь каталог:");
        foreach (DictionaryEntry entry in catalog)
        {
            var content = (List<string>)entry.Value;
            Console.WriteLine($"Диск: {entry.Key} (Виконавець: {content[0]})");
            for (int i = 1; i < content.Count; i++) Console.WriteLine($"  - {content[i]}");
        }
    }

    public void SearchArtist(string artist)
    {
        Console.WriteLine($"\nПошук пісень виконавця {artist}:");
        foreach (DictionaryEntry entry in catalog)
        {
            var content = (List<string>)entry.Value;
            if (content[0].Equals(artist, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"На диску '{entry.Key}':");
                for (int i = 1; i < content.Count; i++) Console.WriteLine($"  - {content[i]}");
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Lab9T1 task1 = new Lab9T1();
        task1.Run();

        Lab9T2 task2 = new Lab9T2();
        task2.Run();

        Lab9T3 task3 = new Lab9T3();
        task3.Run();

        Lab9T4 task4 = new Lab9T4();
        task4.Run();
    }
}