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
        private Dictionary<string, Formula[]> FormulaBank = new Dictionary<string, Formula[]>();

        /// <summary>
        /// Статистика ответов
        /// </summary>
        private List<string> Statistics = new List<string>(); 
    }
}
