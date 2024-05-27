using System.Security.AccessControl;

namespace Homework10
{
    class Exercise
    {
        /// <summary>
        /// Автосвойство условия задачи
        /// </summary>
        public string Task {  get; }
        /// <summary>
        /// Поле вариантов ответов
        /// </summary>
        private List<string> answ_Opt = new List<string>();
        /// <summary>
        /// Свойство для вариатнов ответов
        /// </summary>
        public List<string> Answ_Opt
        {
            get => answ_Opt;
            private set
            {
                if (value.Count != 4 && value.Count != 0)
                    throw new ArgumentException("Количество вариантов ответа может быть только 4");
                answ_Opt = value;
            }
        }
        /// <summary>
        /// Автосвойство ответа на задачу
        /// </summary>
        public string Answer { get; }
        /// <summary>
        /// Поле посказки
        /// </summary>
        private string hint;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="task">Задание</param>
        /// <param name="answ_opt">Варианты ответов</param>
        /// <param name="answer">Ответ</param>
        /// <param name="hint">Подсказка</param>
        public Exercise(string task, List<string> answ_opt, string answer, string hint)
        {
            Task = string.IsNullOrWhiteSpace(task) ? "Default task" : task;
            Answ_Opt = answ_opt;
            Answer = string.IsNullOrWhiteSpace(answer) ? "Default answer" : answer;
            this.hint = string.IsNullOrWhiteSpace(hint) ? "Default hint" : hint;
        }
        /// <summary>
        /// Метод, возвращающий подсказку к заданию
        /// </summary>
        /// <returns>Подсказка</returns>
        public string Get_Hint()
        {
            return hint;
        }
        /// <summary>
        /// Метод перетасовки вариантов ответов
        /// </summary>
        public void Shuffle()
        {
            var temp = new List<string>();
            var rand = new Random();
            while (temp.Count < Answ_Opt.Count)
            {
                var i = rand.Next(Answ_Opt.Count);
                if (temp.Contains(Answ_Opt[i]))
                    continue;
                temp.Add(Answ_Opt[i]);
            }
            Answ_Opt = temp;
        }
    }
}
