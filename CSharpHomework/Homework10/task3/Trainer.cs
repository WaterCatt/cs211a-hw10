using System.ComponentModel;
using System.Xml.Linq;
using static System.Console;
namespace Homework10
{
    /// <summary>
    /// Класс тренажёр
    /// </summary>
    class Trainer
    {
        /// <summary>
        /// Номер тренировки
        /// </summary>
        private static int Trainingnum = 0;

        /// <summary>
        /// Банк формул
        /// </summary>
        private Dictionary<string, List<Formula>> FormulaBank = new Dictionary<string, List<Formula>>();

        /// <summary>
        /// Статистика ответов
        /// </summary>
        private Dictionary<int, List<(string, Formula)>> Statistics = new Dictionary<int, List<(string, Formula)>>();

        /// <summary>
        /// Конструктор тренажёра
        /// </summary>
        public Trainer() { }

        /// <summary>
        /// Добавляет одну формулу в банк формул в указанную тему
        /// </summary>
        /// <param name="formula">Формула</param>
        public void AddFormula(Formula formula)
        {
            var top = formula.Topic.ToLower();
            if (FormulaBank.ContainsKey(top))
                FormulaBank[top].Add(formula);
            else
                FormulaBank.Add(top, new List<Formula> { formula });
        }

        /// <summary>
        /// Выбор тем для опроса
        /// </summary>
        private string[] TopicChoice()
        {
            WriteLine("\nДоступные темы: ");
            foreach (var item in FormulaBank.Keys)
                WriteLine($"{item} ");
            Write("\nВведите темы, формулы из которой нужно справшивать, через запятые: ");
            string[] toparr = ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower().Trim()).ToArray();
            if ((toparr.Any(s => !FormulaBank.ContainsKey(s))) || (toparr.Length == 0))
            {
                WriteLine("Вы ввели некорректные темы. Попробуйте ещё раз...");
                return TopicChoice();
            }
            return toparr;
        }

        /// <summary>
        /// Загружает формулы в банк формул из файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        public void LoadFromFile(string filename)
        {
            foreach (var s in File.ReadLines(filename).Skip(1))
            {
                var form = s.Split(',', StringSplitOptions.RemoveEmptyEntries);
                AddFormula(new Formula(form[1], form[2], form[0]));
            }
        }

        /// <summary>
        /// Выводит статистику неправильных ответов по темам
        /// </summary>
        /// <param name="cnt">Количество учитываемых тренировок</param>
        public void WrongAnswersStatistic(int cnt)
        {
            if ((cnt < 0) || (cnt > Trainingnum))
                throw new ArgumentException("Некорректное число учитываемых тренировок.");
            WriteLine($"Статистика неправильных ответов по темам в {cnt} последних(-ней) тренировках(-ке):");
            var cort = Statistics.TakeLast(cnt).SelectMany(kv => kv.Value).Where(sf => sf.Item1 == "Неправильный ответ");
            foreach (var k in FormulaBank.Keys)
                WriteLine($"Тема: {k}, кол-во неправильных ответов = {cort.Where(sf => sf.Item2.Topic.ToLower() == k).Count()}");
        }
        /// <summary>
        /// Начинает тренировку
        /// </summary>
        public void StartTraining()
        {
            Trainingnum++;
            Statistics.Add(Trainingnum, new List<(string, Formula)>());
            var r = new Random();
            var q = new Stack<Formula>();
            var tc = TopicChoice();
            var z = FormulaBank.Where(kv => tc.Contains(kv.Key)).SelectMany(kv => kv.Value).ToArray();
            Write("Сколько формул справшивать? ");

            var n = 0;
            void amount(string s)
            {
                if (!int.TryParse(s, out n))
                {
                    Write("Введите корректное число! ");
                    amount(ReadLine());
                }
                if (n <= 0)
                {
                    Write("Введите корректное число! ");
                    amount(ReadLine());
                }
            }
            amount(ReadLine());
            for (var i = 0; i < n; i++)
                q.Push(z[r.Next(0, z.Length)]);

            WriteLine();
            WriteLine("Тренировка началась!");
            while (q.Count > 0)
            {
                WriteLine("Следующая формула через 3 секунды. Готовьтесь!");
                Thread.Sleep(3000);
                WriteLine();
                WriteLine($"Формула: {q.Peek().Name};");
                Write("Осталось времени: ");
                for (int i = 5; i > 0; i--)
                {
                    Write($"{i} ");
                    Thread.Sleep(1000);
                }
                WriteLine();
                Write($"Ответ: {q.Peek().Answer}; Дали ли Вы правильный ответ? (Введите да или что-то другое, если нет): ");
                if (ReadLine().ToLower() == "да")
                {
                    Statistics[Trainingnum].Add(($"Правильный ответ", q.Peek()));
                    q.Pop();
                    WriteLine("Молодец!");
                }
                else
                {
                    Statistics[Trainingnum].Add(($"Неправильный ответ", q.Peek()));
                    WriteLine("Жаль!");
                }
                WriteLine();
            }
            WriteLine("Вы - большой молодец!");
        }
    }
}
