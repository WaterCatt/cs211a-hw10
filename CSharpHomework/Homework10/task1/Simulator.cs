using Homework10.task1;

namespace Homework10
{
    public class Simulator
    {
        /// <summary>
        /// список всех прямоугольников на плоскости
        /// </summary>
        List<Shape> shapes = new List<Shape>();

        /// <summary>
        /// добавляет прямоугольник на плоскость
        /// </summary>
        public void AddShape(Shape shape)
        {
            shapes.Add(shape);
        }

        /// <summary>
        /// класс симулятора плоскости
        /// </summary>
        public Simulator(params Shape[] shapes) 
        {
            this.shapes.AddRange(shapes);
        }

        /// <summary>
        /// выводит все прямоугольники на плоскости
        /// </summary>
        public void Write_All_Shapes()
        {
            Console.WriteLine("Все фигуры на плоскости:");
            foreach (Shape shape in shapes)
                Console.WriteLine(shape);
        }

        /// <summary>
        /// возвращает наиболее удаленную от центра координат фигуру
        /// </summary>
        public Shape? Most_Remote()
        {
            if (shapes.Count == 0)
                return null;
            var p_0 = new Point(0, 0);

            double dist = 0;
            int ind = 0;
            int i = 0;
            foreach (Shape shape in shapes)
            {
                double dist_now = p_0.DistanceTo(shape.Center);
                if (dist < dist_now)
                {
                    ind = i;
                    dist = dist_now;
                }
                i++;
            }
            return shapes[ind];
        }

        /// <summary>
        /// Получение массива фигур согласно заданному предикату
        /// </summary>
        public Shape[] Shapes_on_predicate(Predicate<Shape> pred)
        {
            return shapes.Where(x => pred(x)).ToArray();
        }

        /// <summary>
        /// количество окружностей
        /// </summary>
        public int Count_Circle()
        {
            int res = 0;
            foreach (Shape shape in shapes)
            {
                if (shape is Ellipse ellipse)
                    if (ellipse.A ==  ellipse.B)
                        res++;
            }
            return res;
        }

        /// <summary>
        /// прямоугольник с наименьшим периметром
        /// </summary>
        public Rect? Rect_with_min_P()
        {
            int res = -1;
            double min_p = double.MaxValue;
            int i = 0;
            foreach (Shape shape in shapes)
            {
                if ((shape is Rect) && (shape.Perimeter < min_p))
                {
                    min_p = shape.Perimeter;
                    res = i;
                }
                i++;
            }
            if (res == -1)
                return null;
            return (Rect)shapes[res];
        }
    }
}
