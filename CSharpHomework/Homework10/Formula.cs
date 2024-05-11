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
        private string name;

        /// <summary>
        /// Название формулы
        /// </summary>
        public string Name { get => name; set => name = string.IsNullOrWhiteSpace(value) ? "Empty" : value; }

        /// <summary>
        /// Правильный ответ
        /// </summary>
        private string answer;

        /// <summary>
        /// Правильный ответ
        /// </summary>
        public string Answer { get => answer; set => answer = string.IsNullOrWhiteSpace(value) ? "Empty" : value; }

        /// <summary>
        /// Тема
        /// </summary>
        private string topic;

        /// <summary>
        /// Тема
        /// </summary>
        public string Topic { get => topic; set => topic = string.IsNullOrWhiteSpace(value) ? "Empty" : value; }

        /// <summary>
        /// Конструктор формулы
        /// </summary>
        /// <param name="name">Название формулы</param>
        /// <param name="answer">Правильный ответ</param>
        public Formula(string name, string answer, string topic)
        {
            this.name = name;
            this.answer = answer;
            this.topic = topic;
        }

        /// <summary>
        /// Возвращает строку с темой, названием и правильным ответом формулы
        /// </summary>
        public override string ToString() => $"Тема: {Topic}, название: {Name}, ответ: {Answer}";
    }
}
