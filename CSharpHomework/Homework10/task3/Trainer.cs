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
        /// Банк вопросов
        /// </summary>
        private Dictionary<string, List<Question>> QuestionBank = new Dictionary<string, List<Question>>();

        /// <summary>
        /// Статистика ответов
        /// </summary>
        private Dictionary<int, List<(string, Question)>> Statistics = new Dictionary<int, List<(string, Question)>>();

        /// <summary>
        /// Конструктор тренажёра
        /// </summary>
        public Trainer() { }

        /// <summary>
        /// Список вопросов, на которые пользователь ответил неправильно
        /// </summary>
        private static List<Question> WrongAnswers = new List<Question>();

        /// <summary>
        /// Добавляет один вопрос(формула или теорема) в банк вопросов в указанную тему
        /// </summary>
        /// <param name="question">Вопрос</param>
        public void AddQuestion(Question question)
        {
            var top = question.Topic.ToLower();
            if (QuestionBank.ContainsKey(top))
                QuestionBank[top].Add(question);
            else
                QuestionBank.Add(top, new List<Question> { question });
        }

        /// <summary>
        /// Выбор тем для опроса
        /// </summary>
        private string[] TopicChoice()
        {
            WriteLine("\nДоступные темы: ");
            foreach (var item in QuestionBank.Keys)
                WriteLine($"{item} ");
            Write("\nВведите темы, формулы и теоремы из которой нужно спрашивать, через запятые: ");
            //массив тем для опроса
            string[] toparr = ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToLower().Trim()).ToArray();
            //проверка наличия всех тем в массиве среди доступных тем
            if ((toparr.Any(s => !QuestionBank.ContainsKey(s))) || (toparr.Length == 0))
            {
                WriteLine("Вы ввели некорректные темы. Попробуйте ещё раз...");
                return TopicChoice();
            }
            return toparr;
        }

        /// <summary>
        /// Загружает формулы и теоремы в банк вопросов из файла
        /// </summary>
        /// <param name="filename">Имя файла</param>
        public void LoadFromFile(string filename)
        {
            foreach (var s in File.ReadLines(filename))
            {
                var ques = s.Split('_', StringSplitOptions.RemoveEmptyEntries);
                if (ques[0] == "F")
                    AddQuestion(new Formula(ques[1], ques[2], ques[3]));
                else
                    AddQuestion(new Theorem(ques[1], ques[2], ques[3], ques[4]));
            }
        }

        /// <summary>
        /// Выводит формулу с самой короткой записью
        /// </summary>
        public void ShortestFormula() => WriteLine($"Формула с кратчайшей записью:\n{QuestionBank.Values.SelectMany(x => x.Where(c => (c is Formula))).OrderBy(x => (x as Formula).Answer.Length).First()}");

        /// <summary>
        /// Выводит теорему с самым длинным док-вом
        /// </summary>
        public void LongestProofTheorem() => WriteLine($"Теорема с самым длинным док-вом:\n{QuestionBank.Values.SelectMany(x => x.Where(c => (c is Theorem))).OrderByDescending(x => (x as Theorem).Proof.Length).First()}");

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
            foreach (var k in QuestionBank.Keys)
                WriteLine($"Тема: {k}, кол-во неправильных ответов = {cort.Where(sf => sf.Item2.Topic.ToLower() == k).Count()}");
        }

        /// <summary>
        /// Начинает тренировку
        /// </summary>
        public void StartTraining()
        {
            Trainingnum++;
            Statistics.Add(Trainingnum, new List<(string, Question)>());
            var r = new Random();
            //Стек вопросов для опроса
            var stack = new Stack<Question>();
            //Выбранные темы
            var chosentopics = TopicChoice();
            //Список всех вопросов в банке тех тем, которые выбрал пользователь
            var qlist = QuestionBank.Where(kv => chosentopics.Contains(kv.Key)).SelectMany(kv => kv.Value).ToList();
            //Из списка вопросов, которые пользователь не ответил, вопросы добавляются в список вопросов, пригодных для опроса для увеличения частоты их появления
            foreach (var q in WrongAnswers)
            {
                if (chosentopics.Contains(q.Topic))
                {
                    qlist.Add(q);
                    qlist.Add(q);
                    qlist.Add(q);
                }
            }
            var qarr = qlist.ToArray();

            Write("Сколько вопросов спрашивать? ");

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
            //Добавляем в стек для опроса случайные вопросы из массива пригодных вопросов(массив из вопросов тех тем, которые выбрал пользователь + вопросы, на которые пользователь до этого ответил неправильно(тех же тем))
            for (var i = 0; i < n; i++)
                stack.Push(qarr[r.Next(0, qarr.Length)]);

            WriteLine();
            WriteLine("Тренировка началась!");
            while (stack.Count > 0)
            {
                if (stack.Peek() is Formula)
                {
                    WriteLine("Следующая формула через 3 секунды. Готовьтесь!");
                    Thread.Sleep(3000);
                    WriteLine();
                    WriteLine($"Формула: {(stack.Peek() as Formula).Name};");
                    Write("Осталось времени: ");
                    for (int i = 5; i > 0; i--)
                    {
                        Write($"{i} ");
                        Thread.Sleep(1000);
                    }
                    WriteLine();
                    Write($"Ответ: {(stack.Peek() as Formula).Answer}; Дали ли Вы правильный ответ? (Введите да или что-то другое, если нет): ");
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
                else
                {
                    var t = stack.Peek() as Theorem;
                    WriteLine("Следующая теорема через 3 секунды. Готовьтесь!");
                    Thread.Sleep(3000);
                    WriteLine();
                    WriteLine($"Допишите у себя заключение теоремы:\n{t.Condition}...");
                    Write("Осталось времени: ");
                    for (int i = 10; i > 0; i--)
                    {
                        Write($"{i} ");
                        Thread.Sleep(1000);
                    }
                    WriteLine();
                    Write($"Заключение теоремы: {t.Conclusion}; Дали ли Вы правильный ответ? (Введите да или что-то другое, если нет): ");
                    if (ReadLine().ToLower() == "да")
                        WriteLine("Молодец!");
                    else
                    {
                        WrongAnswers.Add(t);
                        Statistics[Trainingnum].Add(($"Неправильный ответ", t));
                        WriteLine("Жаль!");
                        continue;
                    }
                    WriteLine("\nНапишите у себя доказательство теоремы");
                    Write("Осталось времени: ");
                    for (int i = 20; i > 0; i--)
                    {
                        Write($"{i} ");
                        Thread.Sleep(1000);
                    }
                    WriteLine();
                    WriteLine($"Док-во: {t.Proof}");
                    Write("Дали ли Вы правильный ответ? (Введите да или что-то другое, если нет): ");
                    if (ReadLine().ToLower() == "да")
                    {
                        Statistics[Trainingnum].Add(($"Правильный ответ", t));
                        stack.Pop();
                        WriteLine("Молодец!");
                    }
                    else
                    {
                        WrongAnswers.Add(t);
                        Statistics[Trainingnum].Add(($"Неправильный ответ", t));
                        WriteLine("Жаль!");
                    }
                    WriteLine();
                }
            }
            WriteLine("Вы - большой молодец!");
        }
    }
}
