using Homework10;
using static System.Console;
namespace Homework10
{
    internal class Tasks
    {
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

        static void Main()
        {
            Task3();
        }
    }
}