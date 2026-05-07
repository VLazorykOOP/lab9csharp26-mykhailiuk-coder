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

    // студент успішний, якщо всі оцінки 4 або 5
    public bool IsSuccessful
    {
        get
        {
            return Grades.All(g => g >= 4);
        }
    }

    public override string ToString()
    {
        return $"{FullName} | Група: {Group} | Оцінки: {string.Join(", ", Grades)}";
    }
}

public class Lab9T2
{
    public void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.WriteLine("\n--- Завдання 2 (Queue + файл) ---");

        string filePath =
            @"D:\cs_tasks\lab9\lab9csharp26-mykhailiuk-coder\Lab9_10CharpT\performance.txt";

        Queue<Student> highAchievers = new Queue<Student>();
        Queue<Student> others = new Queue<Student>();

        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не знайдено!");
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(';');

                if (parts.Length != 3)
                    continue;

                Student student = new Student
                {
                    FullName = parts[0].Trim(),
                    Group = parts[1].Trim(),
                    Grades = parts[2]
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray()
                };

                if (student.IsSuccessful)
                    highAchievers.Enqueue(student);
                else
                    others.Enqueue(student);
            }

            Console.WriteLine("\nСтуденти, що навчаються на 4 і 5:");

            while (highAchievers.Count > 0)
            {
                Console.WriteLine(highAchievers.Dequeue());
            }

            Console.WriteLine("\nІнші студенти:");

            while (others.Count > 0)
            {
                Console.WriteLine(others.Dequeue());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}

public class Lab9T3
{
    public void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        Console.WriteLine("\n--- Завдання 3 (ArrayList) ---");

        string input = "abc#d##c";
        ArrayList list = new ArrayList();
        foreach (char ch in input)
        {
            if (ch == '#') { if (list.Count > 0) list.RemoveAt(list.Count - 1); }
            else { list.Add(ch); }
        }
        Console.WriteLine($"Вхідний рядок: {input}");
        Console.Write("Результат Backspace: ");
        foreach (var item in list) Console.Write(item);
        Console.WriteLine();

            string filePath = @"D:\cs_tasks\lab9\lab9csharp26-mykhailiuk-coder\Lab9_10CharpT\performance.txt";
            ArrayList highAchievers = new ArrayList();
            ArrayList others = new ArrayList();
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Файл не знайдено!");
                    return;
                }
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(';');
                    if (parts.Length != 3)
                        continue;
                    Student student = new Student
                    {
                        FullName = parts[0].Trim(),
                        Group = parts[1].Trim(),
                        Grades = parts[2]
                            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray()
                    };
                    if (student.IsSuccessful)
                        highAchievers.Add(student);
                    else
                        others.Add(student);
                }
                Console.WriteLine("\nСтуденти, що навчаються на 4 і 5:");
                foreach (Student student in highAchievers) Console.WriteLine(student);
                Console.WriteLine("\nІнші студенти:");
                foreach (Student student in others) Console.WriteLine(student);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
        }
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

    public void AddDisk(string title, string artist) => catalog[title] = new List<string> { artist }; 

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
        while (true)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.WriteLine("Виберіть завдання (1-4) або 0 для виходу:");
            string choice = Console.ReadLine()!;
            if (choice == "0") break;
            switch (choice)
            {
                case "1":
                    Lab9T1 task1 = new Lab9T1();
                    task1.Run();
                    break;
                case "2":
                    Lab9T2 task2 = new Lab9T2();
                    task2.Run();
                    break;
                case "3":
                    Lab9T3 task3 = new Lab9T3();
                    task3.Run();
                    break;
                case "4":
                    Lab9T4 task4 = new Lab9T4();
                    task4.Run();
                    break;
                default:
                    Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                    break;
            }
            Console.WriteLine();
        }
    }
}