using static System.Console;

namespace Homework10
{
    internal class Tasks
    {
        static void Main()
        {
            //Protect_tasks.Protected_on();

            while (true)
            {
                WriteLine("\nМодуль математика. Доступные команды (вызов по номеру):\n" +
                        "1. Геометрия\n" +
                        "2. Проверка знаний\n" +
                        "3. Зазубривание теории\n\n");
                Write("Введите команду: ");
                var s = ReadLine();
                if (string.IsNullOrWhiteSpace(s))
                {
                    WriteLine("Введена пустая строку, повторите попытку\n");
                    Console.Clear();
                    continue;
                }
                if (s == "1")
                {
                    Console.Clear();
                    Task1.Task1.work();
                }
                else if (s == "2")
                {
                    Console.Clear();
                    //task2
                }
                else if (s == "3")
                {
                    Console.Clear();
                    //task3
                }
                else
                {
                    WriteLine("Введена неверная команда");
                    continue;
                }
                Console.Clear();
            }
        }
    }
}
