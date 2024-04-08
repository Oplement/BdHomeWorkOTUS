using System;
using System.Threading;
using HW;
using HW.Storage;
using HW.Entities;


class Program
{
    static void Main()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear(); // Очистка консоли перед каждым обновлением интерфейса
            DisplayMenu();   // Отображение меню

            // Обработка выбора пользователя
            char key = Console.ReadKey().KeyChar;

            Console.WriteLine();

            switch (key)
            {
                case '1':
                    DisplayCurrentDateTime();
                    break;
                case '2':
                    MenuGetAllClients();
                    break;
                case '3':
                    MenuAddClient();
                    break;
                case '4':
                    MenuDeleteClientID();
                    break;
                case '0':
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("\nНекорректный ввод. Попробуйте еще раз.");
                    break;
            }

        }
    }

    static void DisplayMenu()
    {
        Console.WriteLine("Меню управления программой:");
        Console.WriteLine("1. Показать текущее время и дату");
        Console.WriteLine("2. Показать всех клиентов");
        Console.WriteLine("3. Добавить клиента");
        Console.WriteLine("4. Удалить клиента");
        Console.WriteLine("0. Выйти из программы");
        Console.WriteLine();
        Console.Write("Выберите действие: ");
    }

    static void DisplayCurrentDateTime()
    {
        Console.WriteLine($"\nТекущее время и дата: {DateTime.Now}");
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }






    static void PrintTable<T>(List<T> items)
    {
        var columnWidths = CalculateColumnWidths(items);

        var columnHeaders = typeof(T).GetProperties().Select(p => p.Name).ToArray();

        Console.WriteLine(new string('-', columnWidths.Sum() + columnWidths.Count * 3 - 1));
        Console.WriteLine($"| {string.Join(" | ", columnHeaders.Select((header, index) => header.PadRight(columnWidths[index])))} |");
        Console.WriteLine(new string('-', columnWidths.Sum() + columnWidths.Count * 3 - 1));

        foreach (var item in items)
        {
            var values = typeof(T).GetProperties().Select(p => p.GetValue(item)?.ToString() ?? string.Empty).ToArray();
            Console.WriteLine($"| {string.Join(" | ", values.Select((value, index) => value.PadRight(columnWidths[index])))} |");
        }

        Console.WriteLine(new string('-', columnWidths.Sum() + columnWidths.Count * 3 - 1));
    }

    static List<int> CalculateColumnWidths<T>(List<T> items)
    {
        var columnProperties = typeof(T).GetProperties();
        var columnWidths = new List<int>();

        foreach (var property in columnProperties)
        {
            int maxWidth = property.Name.Length;
            foreach (var item in items)
            {
                var value = property.GetValue(item)?.ToString() ?? string.Empty;
                maxWidth = Math.Max(maxWidth, value.Length);
            }
            columnWidths.Add(maxWidth + 2); // Добавляем небольшой отступ
        }

        return columnWidths;
    }



    static void MenuGetAllClients()
    {
        var Clients = ClientStorage.ClientsGetAll();

        PrintTable(Clients);
        Console.ReadKey();

    }

    static void MenuDeleteClientID()
    {
        Console.WriteLine($"\nID клиента для удаления: ");

        var idClient = ReadStringFromConsole();

        string result = ClientStorage.ClientsDeleteID( idClient );
        Console.WriteLine( result );
        Console.ReadKey();

    }


    static string ReadStringFromConsole()
    {
        ConsoleKeyInfo key;
        string input = "";

        do
        {
            key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Backspace)
            {
                if (input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else if (key.Key != ConsoleKey.Enter)
            {
                input += key.KeyChar;
                Console.Write(key.KeyChar);
            }

        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine(); // Переход на новую строку после ввода

        return input;
    }

    static void MenuAddClient()
    {
        Console.WriteLine($"\nИмя клиента: ");
    
        // Убираем символ Enter из введенной строки
        string FirstName = ReadStringFromConsole();
        //string FirstName = Console.ReadKey().ToString().Trim();

        Console.WriteLine($"\nФамилия клиента: ");
     
        // Убираем символ Enter из введенной строки
        string LastName = ReadStringFromConsole();

        //string LastName = Console.ReadKey().ToString().Trim();

        Console.WriteLine($"\nEmail клиента: ");

        // Убираем символ Enter из введенной строки
        string Email = ReadStringFromConsole();

        //string Email = Console.ReadKey().ToString().Trim();

        Console.WriteLine($"\nТелефон клиента: ");

        // Убираем символ Enter из введенной строки
        string PhoneNumber = ReadStringFromConsole();

        //string PhoneNumber = Console.ReadKey().ToString().Trim();

        Thread.Sleep(1000);

        Console.WriteLine($"\nВ процессе....");
        ClientStorage.ClientsInsert(FirstName: FirstName, LastName: LastName, Email: Email, PhoneNumber: PhoneNumber);   
        
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}
