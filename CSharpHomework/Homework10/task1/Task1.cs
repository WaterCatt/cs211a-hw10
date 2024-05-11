using static System.Console;


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
            Simulator simulator_1 = new Simulator(Rect_1, Rect_2, Rect_3, Rect_4, Rect_5, Rect_6);


            WriteLine("Модуль геометрия. Доступные команды (вызов по номеру):\n" +
                "1. Вывести все прямоугольники плоскости\n" +
                "2. Вывести Наиболее удаленный от центра координат прямоугольник:\n" +
                "3. Поворот на 45 rect_3\n" +
                "4. Сдвиг на (4, -3) rect_4\n" +
                "5. Увеличение на коэф (4, 2) rect_5\n" +
                "6. Прямоугольники по предикату 'square > 3':\n" +
                "7. Выйти из модуля\n");

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
                    case "1": simulator_1.Write_All_Rects(); break;
                    case "2": WriteLine("Наиболее удаленный от центра координат прямоугольник:\n" + simulator_1.Most_Remote()); break;
                    case "3": WriteLine($"rect_3: {Rect_3}"); WriteLine("Поворот на 45 rect_3"); Rect_3.Rotate(45); WriteLine(Rect_3); break;
                    case "4": WriteLine($"rect_4: {Rect_4}"); WriteLine("Сдвиг на (4, -3) rect_4"); Rect_4.Shift(4, -3); WriteLine(Rect_4); break;
                    case "5": WriteLine($"rect_5: {Rect_5}"); WriteLine("Увеличение на коэф (4, 2) rect_5"); Rect_5.Ratio(4, 2); WriteLine(Rect_5); break;
                    case "6":
                        {
                            WriteLine("Прямоугольники по предикату 'square > 3':");
                            foreach (var r in simulator_1.Rects_on_predicate(x => x.Square() > 3))
                                WriteLine(r);
                            break;
                        }
                    case "7": return;
                    default: WriteLine("Введена неверная команда"); break;
                }
                WriteLine("\n");
            }            
        }
    }
}
