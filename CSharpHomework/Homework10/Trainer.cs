﻿using System.ComponentModel;
using static System.Console;
namespace Homework10
{
    /// <summary>
    /// Класс тренажёр
    /// </summary>
    class Trainer
    {
        /// <summary>
        /// Банк формул
        /// </summary>
        private Dictionary<string, List<Formula>> FormulaBank = new Dictionary<string, List<Formula>>();

        /// <summary>
        /// Статистика ответов
        /// </summary>
        private List<string> Statistics = new List<string>();

        /// <summary>
        /// Конструктор тренажёра
        /// </summary>
        public Trainer() { }

        /// <summary>
        /// Добавляет одну формулу в банк формул в указанную тему
        /// </summary>
        /// <param name="topic">Тема</param>
        /// <param name="formula">Формула</param>
        public void AddFormula(string topic, Formula formula)
        {
            if (FormulaBank.ContainsKey(topic))
                FormulaBank[topic].Add(formula);
            else
                FormulaBank.Add(topic, new List<Formula> { formula });
        }


        /// <summary>
        /// Выбор тем для опроса
        /// </summary>
        private string[] TopicChoice()
        {
            Write("Доступные темы: ");
            foreach (var item in FormulaBank.Keys)
                Write($"{item} ");
            Write("\nВведите темы, формулы из которой нужно справшивать, через пробел (регистр важен!!!): ");
            string[] toparr = ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
            if ((toparr.Any(s => !FormulaBank.ContainsKey(s))) || (toparr.Length == 0))
            {
                WriteLine("Вы ввели некорректные темы. Попробуйте ещё раз...");
                return TopicChoice();
            }
            return toparr;
        }

        /// <summary>
        /// Начинает тренировку
        /// </summary>
        public void StartTraining()
        {
            WriteLine("Здравствуйте!\n");
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
                WriteLine("Следующая формула через 5 секунд. Готовьтесь!");
                Thread.Sleep(5000);
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
                    q.Pop();
                    WriteLine("Молодец!");
                }
                else
                    WriteLine("Жаль!");
                WriteLine();
            }
            WriteLine("Вы - большой молодец!");
        }
    }
}
