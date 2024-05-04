namespace Homework10
{
    /// <summary>
    /// Класс формула
    /// </summary>
    class Formula
    {
        /// <summary>
        /// Название формулы
        /// </summary>
        private string name = "Default name";

        /// <summary>
        /// Название формулы
        /// </summary>
        public string Name { get => name; set => name = string.IsNullOrWhiteSpace(value) ? "Empty" : value; }

        /// <summary>
        /// Правильный ответ
        /// </summary>
        private string answer = "Default answer";

        /// <summary>
        /// Правильный ответ
        /// </summary>
        public string Answer { get => answer; set => answer = string.IsNullOrWhiteSpace(value) ? "Empty" : value; }

        /// <summary>
        /// Конструктор формулы
        /// </summary>
        /// <param name="name">Название формулы</param>
        /// <param name="answer">Правильный ответ</param>
        public Formula(string name, string answer)
        {
            this.name = name;
            this.answer = answer;
        }

        /// <summary>
        /// Возвращает строку с названием и правильным ответом формулы
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Формула {Name}: {Answer};";
    }
}
