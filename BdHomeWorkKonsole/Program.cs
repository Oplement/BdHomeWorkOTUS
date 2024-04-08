using BanksDB;
using BanksDB.DataBase;
using BanksDB.DataBase.Entities;
using System.Globalization;


class Program
{
    static void Main()
    {
        Repository _repository = new Repository();

        bool isRunning = true;

        while (isRunning)
        {
            //Console.Clear(); 
            DisplayMenu();   

            // Обработка выбора пользователя
            char key = Console.ReadKey().KeyChar;

            Console.WriteLine();

            switch (key)
            {
                case '1':
                    ReadAllDate(ref _repository);
                    break;
                case '2':
                    AddClient(ref _repository);
                    break;
                case '3':
                    AddNewTypeCredit(ref _repository);
                    break;
                case '4':
                    AddNewCreditClients(ref _repository);
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

    private static void AddNewCreditClients(ref Repository repository)
    {
        Console.WriteLine("Введите ID типа кредита: ");
        var creditId = Console.ReadLine();

        Console.WriteLine("Введите ID заемщика: ");
        var userCreditId = Console.ReadLine();

        Console.WriteLine("Введите сумму заема: ");
        var creditDebt = Console.ReadLine();

        var credit = new Credit();
        credit.debt = Convert.ToInt32(creditDebt);
        credit.userId = Convert.ToInt32(userCreditId);
        credit.typeId = Convert.ToInt32(creditId);

        try
        {
            repository.AddNewCredit(credit);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Возникла ошибка при добавлении нового кредита: {ex.Message}");
            return;
        }

        Console.WriteLine("Кредит успешно создан!");
    }

    static void AddNewTypeCredit(ref Repository repository)
    {
        Console.WriteLine("Введите название типа кредита: ");
        var title = Console.ReadLine();

        var creditType = new CreditType();
        creditType.type = title;


        try
        {
            repository.AddNewCreditType(creditType);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Возникла ошибка при добавлении нового типа кредита: {ex.Message}");
            return;
        }

        Console.WriteLine("Тип кредита успешно создан!");
    }

    static void AddClient(ref Repository repository)
    {
        Console.WriteLine("Введите имя пользователя: ");
        var fname = Console.ReadLine();

        Console.WriteLine("Введите фамилию пользователя: ");
        var lname = Console.ReadLine();

        Console.WriteLine("Введите дату рождения пользователя в формате dd.MM.yyyy (напр. 14.01.2002): ");
        var dob = Console.ReadLine();


        var user = new User();
        user.firstName = fname;
        user.lastName = lname;
        var local = DateTime.ParseExact(dob, "dd.MM.yyyy", CultureInfo.InvariantCulture);
        user.dob = DateTime.SpecifyKind(local, DateTimeKind.Utc);

        user.creditScore = 100;

        try
        {
            repository.AddNewUser(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Возникла ошибка при добавлении нового пользователя: {ex.Message}");
            return;
        }

        Console.WriteLine("Пользователь успешно создан!");
    }

    static void ReadAllDate(ref Repository repository)
    {
        var users = repository.GetAllUsers();
        var credits = repository.GetAllCredits();
        var creditTypes = repository.GetAllCreditTypes();


        Console.WriteLine();
        Console.WriteLine("▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀");
        Console.WriteLine(); 

        Console.WriteLine("Пользователи системы:");

        foreach (var user in users)
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"ID: {user.Id}");
            Console.WriteLine($"Имя: {user.firstName}");
            Console.WriteLine($"Фамилия: {user.lastName}");
            Console.WriteLine($"Дата рождения: {user.dob}");
            Console.WriteLine($"Кредитный рейтинг: {user.creditScore}");
            Console.WriteLine("--------------------------------------");
        }

        Console.WriteLine();
        Console.WriteLine("▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒");
        Console.WriteLine();

        Console.WriteLine("Типы кредитов в системе:");
        foreach (var creditType in creditTypes)
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"ID : {creditType.id}");
            Console.WriteLine($"Название : {creditType.type}");
            Console.WriteLine("--------------------------------------");
        }

        Console.WriteLine();
        Console.WriteLine("▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒");
        Console.WriteLine();

        Console.WriteLine("Все кредиты в системе:");
        foreach (var credit in credits)
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Тип кредита: {creditTypes.FirstOrDefault(m => m.id == credit.typeId).type}");
            var user = users.FirstOrDefault(m => m.Id == credit.userId);
            Console.WriteLine($"ID заёмщика: {user.Id}");
            Console.WriteLine($"Имя заёмщика: {user.firstName}");
            Console.WriteLine($"Фамилия заёмщика: {user.lastName}");
            Console.WriteLine($"Сумма заема: {credit.debt}");
            Console.WriteLine("--------------------------------------");
        }
        Console.WriteLine();
        Console.WriteLine("▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀");
        Console.WriteLine();

        Console.ReadKey();

    }

    static void DisplayMenu()
    {
        Console.WriteLine("Меню управления программой:");
        Console.WriteLine("1. Получить все данные банка");
        Console.WriteLine("2. Добавить клиента");
        Console.WriteLine("3. Добавить новый тип кредита клиента");
        Console.WriteLine("4. Добавить новый кредит клиента");
        Console.WriteLine("0. Выйти из программы");
        Console.WriteLine();
        Console.Write("Выберите действие: ");
    }




    #region MenuAction

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

    #endregion
}
