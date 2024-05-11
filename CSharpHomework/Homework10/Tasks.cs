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
                        "3. Зазубривание теории\n" +
                        "4. Выход\n\n");
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
                    Task2();
                }
                else if (s == "3")
                {
                    Console.Clear();
                    //task3
                }
                else if (s == "4")
                {
                    return;
                }
                else
                {
                    WriteLine("Введена неверная команда");
                    continue;
                }
                Console.Clear();
            }

            /// <summary>
            /// Метод вызова второго модуля
            /// </summary>
            static void Task2()
            {
                WriteLine("Добро пожаловать в модуль \"Составитель вариантов\"!\nДоступные команды:\nСоздать\nВыход");
                while (true)
                {
                    Write("Введите команду: ");
                    var s = ReadLine();

                    if (s.ToLower() == "выход")
                        return;

                    if (s.ToLower() == "создать")
                    {
                        WriteLine("Добро пожаловать в создание вариантов!");

                        var kr = new Generator();
                        kr.LoadExercise();

                        Write("Сколько вариантов вы хотите составить: ");
                        var cnt_vars = int.Parse(ReadLine());

                        while (true)
                        {
                            Write("Сколько вопросов в каждом варианте вы хотите видеть: ");
                            var cnt_exc = int.Parse(ReadLine());
                            try
                            {
                                kr.GenerateVars(cnt_exc, cnt_vars);
                                break;
                            }
                            catch (ArgumentException e)
                            {
                                WriteLine($"Ошибка создания варианта: {e}");
                            }
                        }

                        WriteLine("Варианты сгенерированны успешно!");
                        WriteLine();

                        Write("Как вы хотите назварь файлы с вариантами (Просто слово (Можно оставить пустым, тогда будет стандартное имя) без разрешения): ");
                        var fname = ReadLine();
                        kr.SaveVariants((string.IsNullOrWhiteSpace(fname) || fname == "") ? "Variant" : fname);

                        WriteLine("Процесс сохранения вариантов завершён успешно!");
                        WriteLine();
                    }

                    else WriteLine("Команда введена неверно! Попробуйте заново!");
                }
            }
        }
    }
}
