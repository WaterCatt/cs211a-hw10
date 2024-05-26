using static System.Console;
using Homework10.task1;


namespace Homework10.Task1
{
    public class Task1
    {
        public Task1 () { }


        public void circle_task1() {}

        public static void work()
        {
            Rect Rect_1 = new Rect(new Point(10, 0), new Point(10, 1), new Point(1, 0), new Point(1, 1));
            Rect Rect_2 = new Rect(new Point(-10, -5), new Point(-5, -5), new Point(-5, 5), new Point(-10, 5));
            Rect Rect_3 = new Rect(new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(1, 1));
            Rect Rect_4 = new Rect(new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, 1));
            Rect Rect_5 = new Rect(new Point(0, 0), new Point(1, 1), new Point(1, 0), new Point(0, 1));
            Rect Rect_6 = new Rect(new Point(0, 0), new Point(2, 1), new Point(0, 1), new Point(2, 0));

            Ellipse Ellipse_1 = new Ellipse(new Point(0, 0), 10, 5);
            Ellipse Ellipse_2 = new Ellipse(new Point(10, 0), 5, 5);
            Ellipse Ellipse_3 = new Ellipse(new Point(0, 10), 5, 5);

            Simulator simulator_1 = new Simulator(Rect_1, Rect_2, Rect_3, Rect_4, Rect_5, Rect_6, Ellipse_1, Ellipse_2);
            simulator_1.AddShape(Ellipse_3);

            WriteLine("Модуль геометрия. Доступные команды (вызов по номеру):\n" +
                "1. Вывести все фигуры плоскости\n" +
                "2. Вывести наиболее удаленную от центра координат фигуру:\n" +
                "3. Поворот на 45 rect_3\n" +
                "4. Поворот на -45 Ellipse_1\n" +
                "5. Сдвиг на (4, -3) rect_4\n" +
                "6. Сдвиг на (0, -3) Ellipse_2\n" +
                "7. Увеличение на коэф (4, 2) rect_5\n" +
                "8. Увеличение на коэф (4, 2) Ellipse_3\n" +
                "9. Фигуры по предикату 'square > 3':\n" +
                "10. Прямоугольник с минимальным периметром:\n" +
                "11. Количество эллипсов, являющихся окружностями:\n" +
                "12. Выйти из модуля\n");

            while (true)
            {
                Write("Введите команду: ");
                var command = ReadLine();
                if (string.IsNullOrWhiteSpace(command))
                {
                    WriteLine("Введена пустая строку, повторите попытку\n\n");
                    continue;
                }
                switch (command)
                {
                    case "1": simulator_1.Write_All_Shapes(); break;
                    case "2": WriteLine("Наиболее удаленная от центра координат фигура:\n" + simulator_1.Most_Remote()); break;
                    case "3": WriteLine($"rect_3: {Rect_3}"); WriteLine("Поворот на 45 rect_3"); Rect_3.Rotate(45); WriteLine(Rect_3); break;
                    case "4": WriteLine($"Ellipse_1: {Ellipse_1}"); WriteLine("Поворот на 45 Ellipse_1"); Ellipse_1.Rotate(45); WriteLine(Ellipse_1); break;

                    case "5": WriteLine($"rect_4: {Rect_4}"); WriteLine("Сдвиг на (4, -3) rect_4"); Rect_4.Shift(4, -3); WriteLine(Rect_4); break;
                    case "6": WriteLine($"Ellipse_2: {Ellipse_2}"); WriteLine("Сдвиг на (0, -3) Ellipse_2"); Ellipse_2.Shift(0, -3); WriteLine(Ellipse_2); break;

                    case "7": WriteLine($"rect_5: {Rect_5}"); WriteLine("Увеличение на коэф (4, 2) rect_5"); Rect_5.Ratio(4, 2); WriteLine(Rect_5); break;
                    case "8": WriteLine($"Ellipse_3: {Ellipse_3}"); WriteLine("Увеличение на коэф (4, 2) Ellipse_3"); Ellipse_3.Ratio(4, 2); WriteLine(Ellipse_3); break;

                    case "9":
                        {
                            WriteLine("Фигуры по предикату 'square > 3':");
                            foreach (var r in simulator_1.Shapes_on_predicate(x => x.Square > 3))
                                WriteLine(r);
                            break;
                        }

                    case "10": WriteLine($"Прямоугольник с минимальным периметром:\n{simulator_1.Rect_with_min_P()}"); break;
                    case "11": WriteLine($"Количество эллипсов, являющихся окружностями:\n{simulator_1.Count_Circle()}"); break;
                    case "12": return;
                    default: WriteLine("Введена неверная команда"); break;
                }
                WriteLine("\n");
            }            
        }
    }
}
