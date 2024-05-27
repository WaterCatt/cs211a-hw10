
namespace Homework10
{
    /// <summary>
    /// Класс вопрос
    /// </summary>
    class Question
    {
        /// <summary>
        /// Тема
        /// </summary>
        private string topic;

        /// <summary>
        /// Тема
        /// </summary>
        public string Topic { get => topic; set => topic = string.IsNullOrWhiteSpace(value) ? "Empty" : value; }

        /// <summary>
        /// Конструктор вопроса
        /// </summary>
        /// <param name="topic">Тема</param>
        public Question(string topic) { Topic = topic; }
    }
}
