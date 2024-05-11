namespace Homework10
{
    class Exercise
    {
        /// <summary>
        /// Автосвойство условия задачи
        /// </summary>
        public string Task {  get; }
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
        /// <param name="answer">Ответ</param>
        public Exercise(string task, string answer, string hint)
        {
            Task = string.IsNullOrWhiteSpace(task) ? "Default task" : task;
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
    }
}
