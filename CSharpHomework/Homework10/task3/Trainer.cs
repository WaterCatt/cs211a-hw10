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
        /// Список формул, на которые пользователь ответил неправильно
        /// </summary>
        private static List<Formula> WrongAnswers = new List<Formula>();

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
            Write("\nВведите темы, формулы из которой нужно спрашивать, через запятые: ");
            //массив тем для опроса
            string[] toparr = ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower().Trim()).ToArray();
            //проверка наличия всех тем в массиве среди доступных тем
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
            WriteLine($"\nСтатистика неправильных ответов по темам в {cnt} последних(-ней) тренировках(-ке):");
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
            //Стек формул для опроса
            var stack = new Stack<Formula>();
            //Выбранные темы
            var chosentopics = TopicChoice();
            //Список всех формул в банке тех тем, которые выбрал пользователь
            var formulalist = FormulaBank.Where(kv => chosentopics.Contains(kv.Key)).SelectMany(kv => kv.Value).ToList();
            //Из списка формул, которые пользователь не ответил, формулы добавляются в список формул, пригодных для опроса для увеличения частоты их появления
            foreach (var form in WrongAnswers)
            {
                if (chosentopics.Contains(form.Topic))
                {
                    formulalist.Add(form);
                    formulalist.Add(form);
                    formulalist.Add(form);
                }
            }
            var formulaarr = formulalist.ToArray();

            Write("Сколько формул спрашивать? ");

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
            //Добавляем в стек для опроса случайные формулы из массива пригодных формул(массив из формул тех тем, которые выбрал пользователь + формулы, на которые пользователь до этого ответил неправильно(тех же тем))
            for (var i = 0; i < n; i++)
                stack.Push(formulaarr[r.Next(0, formulaarr.Length)]);

            WriteLine();
            WriteLine("Тренировка началась!");
            while (stack.Count > 0)
            {
                WriteLine("Следующая формула через 3 секунды. Готовьтесь!");
                Thread.Sleep(3000);
                WriteLine();
                WriteLine($"Формула: {stack.Peek().Name};");
                Write("Осталось времени: ");
                for (int i = 5; i > 0; i--)
                {
                    Write($"{i} ");
                    Thread.Sleep(1000);
                }
                WriteLine();
                Write($"Ответ: {stack.Peek().Answer}; Дали ли Вы правильный ответ? (Введите да или что-то другое, если нет): ");
                if (ReadLine().ToLower() == "да")
                {
                    Statistics[Trainingnum].Add(($"Правильный ответ", stack.Peek()));
                    stack.Pop();
                    WriteLine("Молодец!");
                }
                else
                {
                    WrongAnswers.Add(stack.Peek());
                    Statistics[Trainingnum].Add(($"Неправильный ответ", stack.Peek()));
                    WriteLine("Жаль!");
                }
                WriteLine();
            }
            WriteLine("Вы - большой молодец!");
        }
    }
}
