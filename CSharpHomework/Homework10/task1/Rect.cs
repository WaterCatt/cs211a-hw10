using static System.Math;
using Homework10.task1;

namespace Homework10
{
    /// <summary>
    /// класс прямоугольника
    /// </summary>
    public class Rect : Shape
    {
        /// <summary>
        /// точка 1
        /// </summary>
        public Point p1 { get; private set; }

        /// <summary>
        /// точка 2
        /// </summary>
        public Point p2 { get; private set; }

        /// <summary>
        /// точка 3
        /// </summary>
        public Point p3 { get; private set; }

        /// <summary>
        /// точка 4
        /// </summary>
        public Point p4 { get; private set; }

        /// <summary>
        /// проверяет, образуют ли точки прямоугольник
        /// </summary>
        private bool Is_Rect(params Point[] arr_points)
        {
            double eps = 0.001;

            for (int i = 0; i < arr_points.Length; i++)
            {
                int j = (i + 1) % arr_points.Length;
                int k = (i + 2) % arr_points.Length;

                double angle = Angle_Between_Points(arr_points[i], arr_points[j], arr_points[k]);
                if ((Abs(angle - 90) > eps) && (Abs(angle - 270) > eps))
                    return false;
            }

            double Angle_Between_Points(Point A, Point B, Point C)
            {
                double angleR = Atan2(C.y - B.y, C.x - B.x) - Atan2(A.y - B.y, A.x - B.x);
                if (angleR < 0)
                    angleR += 2 * PI;
                return angleR * (180 / PI);
            }
            return true;
        }

        /// <summary>
        /// класс прямоугольника
        /// </summary>
        public Rect(Point p1, Point p2, Point p3, Point p4)
        {
            if (!Is_Rect(p1, p2, p3, p4))
            {
                if (!Is_Rect(p2, p1, p3, p4))
                {
                    if (!Is_Rect(p1, p3, p2, p4))
                    {
                        throw new ArgumentException("неверные координаты вершин");
                    }
                    else
                        (p3, p2) = (p2, p3);
                } 
                else
                    (p1, p2) = (p2, p1);
            }

            Center = new Point((p3.x + p1.x) / 2, (p3.y + p1.y) / 2);
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.p4 = p4;
        }
        
        public override string ToString()
        {
            return $"Rect[{p1}, {p2}, {p3}, {p4}]   Square={Square}, Perimeter={Perimeter}";
        }

        // <summary>
        /// площадь
        /// </summary>
        public override double Square { get => Round(p1.DistanceTo(p2) * p2.DistanceTo(p3), 2); }

        /// <summary>
        /// периметр
        /// </summary>
        public override double Perimeter { get => Round((p1.DistanceTo(p2) + p2.DistanceTo(p3)) * 2, 2); }

        /// <summary>
        /// поворачивает прямоугольник на angle градусов
        /// </summary>
        public override void Rotate(double angle)
        {
            double scos = Cos(angle * PI / 180);
            double ssin = Sin(angle * PI / 180);
            var arr_p = new Point[] { p1, p2, p3, p4 };
            foreach (var p in arr_p)
            {
                (p.x, p.y) = (Center.x + (p.x - Center.x) * scos - (p.y - Center.y) * ssin, Center.y + (p.y - Center.y) * scos + (p.x - Center.x) * ssin);
            }
        }

        /// <summary>
        /// сдвиг прямоугольника
        /// </summary>
        public override void Shift(double x, double y)
        {
            var arr_p = new Point[] { p1,  p2, p3, p4, Center };
            foreach (var p in arr_p)
            {
                p.x += x;
                p.y += y;
            }
        }

        /// <summary>
        /// увеличивает ширину и высоту прямоугольника на заданные коэффициенты
        /// </summary>
        public override void Ratio(double k_width, double k_height)
        {
            if ((k_width <= 0) || (k_height <= 0))
                throw new Exception("error of ratio");
            var arr_p = new Point[] { p1, p2, p3, p4, Center };
            foreach (var p in arr_p)
            {
                p.x = Center.x + (p.x - Center.x) * k_width;
                p.y = Center.y + (p.y - Center.y) * k_height;
            }
        }
    }
}
