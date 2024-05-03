namespace Homework10
{
    internal class Tasks
    {
        static void Main()
        {
            Protect_tasks.Protected_on();
            Console.WriteLine("\n");
            var Rect_1 = new Rect(new Point(10, 0), new Point(10, 1), new Point(1, 0), new Point(1, 1));
            var Rect_2 = new Rect(new Point(-10, -5), new Point(-5, -5), new Point(-5, 5), new Point(-10, 5));
            var Rect_3 = new Rect(new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(1, 1));
            var Rect_4 = new Rect(new Point(0, 0), new Point(1, 0), new Point(1, 1), new Point(0, 1));
            var Rect_5 = new Rect(new Point(0, 0), new Point(1, 1), new Point(1, 0), new Point(0, 1));
            var Rect_6 = new Rect(new Point(0, 0), new Point(2, 1), new Point(0, 1), new Point(2, 0));

            var simulator_1 = new Simulator(Rect_1, Rect_2, Rect_3, Rect_4, Rect_5, Rect_6);
            simulator_1.Write_All_Rects();

            Console.WriteLine("\nНаиболее удаленный от центра координат прямоугольник:\n" + simulator_1.Most_Remote());

            Console.WriteLine("\nПоворот на 45 rect_3");
            Rect_3.Rotate(45);
            Console.WriteLine(Rect_3);

            Console.WriteLine("\nСдвиг на (4, -3) rect_4");
            Rect_4.Shift(4, -3);
            Console.WriteLine(Rect_4);

            Console.WriteLine("\nУвеличение на коэф (4, 2) rect_5");
            Rect_5.Ratio(4, 2);
            Console.WriteLine(Rect_5);

            Console.WriteLine("\nПрямоугольники по предикату 'square > 3':");
            foreach (var r in simulator_1.Rects_on_predicate(x => x.Square() > 3))
                Console.WriteLine(r);
        }
    }
}
