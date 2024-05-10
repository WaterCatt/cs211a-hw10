using static System.Console;
namespace Homework10
{
    internal class Tasks
    {
        static void Main()
        {
            Task2();
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
