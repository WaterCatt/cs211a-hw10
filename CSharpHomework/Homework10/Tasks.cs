using static System.Console;

namespace Homework10
{
    internal class Tasks
    {
        static void Main()
        {
            Protect_tasks.Protected_on();

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
                    Task3();
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

        /// <summary>
        /// Модуль "Зазубривание теории"
        /// </summary>
        static void Task3()
        {
            WriteLine("Добро пожаловать в модуль \"Зазубривание теории\"!");
            var trainer = new Trainer();
            trainer.LoadFromFile("task3/Formulas.txt");
            while (true)
            {
                WriteLine("\nДоступные команды:\nНачать тренировку\nСтатистика неправильных ответов\nВыход\n");
                Write("Введите команду: ");
                var s = ReadLine();
                if (s.ToLower() == "начать тренировку")
                    trainer.StartTraining();
                else if (s.ToLower() == "статистика неправильных ответов")
                {
                    Write("Сколько тренировок учитывать? ");
                    var cnt = 0;
                    void amount(string s)
                    {
                        if (!int.TryParse(s, out cnt))
                        {
                            Write("Введите корректное число! ");
                            amount(ReadLine());
                        }
                        if (cnt <= 0)
                        {
                            Write("Введите корректное число! ");
                            amount(ReadLine());
                        }
                    }
                    amount(ReadLine());
                    try
                    {
                        trainer.WrongAnswersStatistic(cnt);
                    }
                    catch (ArgumentException e)
                    {
                        WriteLine($"Ошибка вывода статистики неправильных ответов: {e.Message}");
                    }
                }
                else if (s.ToLower() == "выход")
                    return;
                else
                {
                    WriteLine("Команда введена неверно! Попробуйте заново!");
                }
            }
        }
    }
}