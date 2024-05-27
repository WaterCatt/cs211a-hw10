using System.Xml.Linq;

namespace Homework10
{
    /// <summary>
    /// Класс теорема
    /// </summary>
    class Theorem : Question
    {
        /// <summary>
        /// Условие теоремы
        /// </summary>
        private string condition;

        /// <summary>
        /// Условие теоремы
        /// </summary>
        public string Condition { get => condition; set => condition = string.IsNullOrWhiteSpace(value) ? "Empty" : value; }

        /// <summary>
        /// Заключение теоремы
        /// </summary>
        private string conclusion;

        /// <summary>
        /// Заключение теоремы
        /// </summary>
        public string Conclusion { get => conclusion; set => conclusion = string.IsNullOrWhiteSpace(value) ? "Empty" : value; }

        /// <summary>
        /// Доказательство теоремы
        /// </summary>
        private string proof;

        /// <summary>
        /// Доказательство теоремы
        /// </summary>
        public string Proof { get => proof; set => proof = string.IsNullOrWhiteSpace(value) ? "Empty" : value; }


        /// <summary>
        /// Конструктор теоремы
        /// </summary>
        /// <param name="condition">Условие теоремы</param>
        /// <param name="conclusion">Заключение теоремы</param>
        /// <param name="proof">Доказательство теоремы</param>
        /// <param name="topic">Тема</param>
        public Theorem(string topic, string condition, string conclusion, string proof) : base(topic)
        {
            Condition = condition;
            Conclusion = conclusion;
            Proof = proof;
        }

        /// <summary>
        /// Возвращает строку с темой, условием, заключением и доказательством теоремы
        /// </summary>
        public override string ToString() => $"Тема: {Topic}, условие и заключение: " + Condition + Conclusion + "\nДок-во: " + Proof;
    }
}
