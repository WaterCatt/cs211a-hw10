namespace Homework10
{
    class Generator
    {
        /// <summary>
        /// Автосвойство банка вопросов
        /// </summary>
        public List<Exercise> ExerciseList { get; private set; } = new List<Exercise>();
        /// <summary>
        /// Автосвойство Списка вариантов
        /// </summary>
        public List<List<Exercise>> Variant_list { get; private set; } = new List<List<Exercise>>();
        /// <summary>
        /// Конструктор
        /// </summary>
        public Generator() { }
        /// <summary>
        /// Метод генерации вариантов
        /// </summary>
        /// <param name="countQst">Количество заданий</param>
        /// <param name="countVar">Количество вариантов</param>
        /// <returns>Список вариантов</returns>
        public void GenerateVars(int countQst, int countVar)
        {
            var rand = new Random();
            if (countQst > ExerciseList.Count)
                throw new ArgumentException("В банке заданий меньше заданий, чем вы хотите увидеть в варианте");
            for (var i = 0; i < countVar; i++)
            {
                var variant = new List<Exercise>();
                while (variant.Count < countQst)
                {
                    var ind = rand.Next(ExerciseList.Count);
                    if (!variant.Contains(ExerciseList[ind]))
                        variant.Add(ExerciseList[ind]);
                }
                Variant_list.Add(variant);
            }
        }
        /// <summary>
        /// Метод загрузки заданий из файла в банк заданий
        /// </summary>
        /// <param name="fname">Путь к файлу</param>
        public void LoadExercise(string fname = "task2/Bank.txt")
        {
            var lst = File.ReadAllLines(fname).Select(x => x.Split('|', StringSplitOptions.RemoveEmptyEntries)).Where(x => x.Length == 3);
            foreach (var exercise in lst)
            {
                ExerciseList.Add(new Exercise(exercise[0], exercise[1], exercise[2]));
            }
        }
        /// <summary>
        /// Метод сохранения вариантов в разные файлы
        /// </summary>
        /// <param name="fname">Имя файла</param>
        public void SaveVariants(string fname = "Variant")
        {
            for (var i = 0; i < Variant_list.Count; i++)
            {
                using (var sw1 = new StreamWriter(File.Create("task2/" + fname + $" {i + 1}.txt")))
                {
                    using (var sw2 = new StreamWriter(File.Create("task2/" + fname + $" {i + 1} Hints.txt")))
                    {
                        sw1.WriteLine($"{i + 1} вариант.");
                        sw2.WriteLine($"{i + 1} вариант.");
                        var j = 1;
                        foreach (var exercise in Variant_list[i])
                        {
                            sw1.WriteLine($"№{j}. {exercise.Task}\nОтвет: {exercise.Answer}");
                            sw2.WriteLine($"№{j++}. {exercise.Get_Hint()}");
                        }
                    }
                }
            }
        }
    }
}
