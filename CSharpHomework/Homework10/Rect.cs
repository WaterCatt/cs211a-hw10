using static System.Math;

namespace Homework10
{
    /// <summary>
    /// класс точки
    /// </summary>
    public class Point
    {
        public double x;
        public double y;
        public Point(double x, double y)
        {
            this.x = x; this.y = y;
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        /// <summary>
        /// возвращает расcтояние до точки
        /// </summary>
        public double DistanceTo(Point p)
        {
            return Sqrt(Pow(x - p.x, 2) + Pow(y - p.y, 2));
        }
    }

    /// <summary>
    /// класс прямоугольника
    /// </summary>
    public class Rect
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
        /// точка центр
        /// </summary>
        private Point p_center;

        /// <summary>
        /// проверяет, образуют ли точки прямоугольник
        /// </summary>
        /// <param name="arr_points"></param>
        /// <returns></returns>
        private bool Is_Rect(params Point[] arr_points)
        {
            double eps = 0.001;

            bool allSidesAreStraight = true;
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
                        throw new Exception("неверные координаты вершин");
                    }
                    else
                        (p3, p2) = (p2, p3);
                } 
                else
                    (p1, p2) = (p2, p1);
            }

            /*
            p_center = new Point((p3.x + p1.x) / 2, (p3.y + p1.y) / 2);
            
            Point p_center_2 = new Point((p4.x + p2.x) / 2, (p4.y + p2.y) / 2);

            if ((((p_center.x - p1.x) < eps) && ((p_center.y - p1.y) < eps)) ||
                (((p_center_2.x - p2.x) < eps) && ((p_center_2.y - p2.y) < eps)) ||
                (Abs(p_center.x - p_center_2.x) > eps) ||
                (Abs(p_center.y - p_center_2.y) > eps) ||
                ((Abs(p1.x - p2.x) > eps) && (Abs(p1.y - p2.y) > eps)) ||
                ((Abs(p3.x - p2.x) > eps) && (Abs(p3.y - p2.y) > eps)) ||
                ((Abs(p1.x - p4.x) > eps) && (Abs(p1.y - p4.y) > eps)) ||
                ((Abs(p3.x - p4.x) > eps) && (Abs(p3.y - p4.y) > eps)))
                throw new Exception("неверные координаты вершин");
            */
            p_center = new Point((p3.x + p1.x) / 2, (p3.y + p1.y) / 2);
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.p4 = p4;
        }
        
        public override string ToString()
        {
            return $"Rect[{p1}, {p2}, {p3}, {p4}]   Square={Square()}, Perimeter={Perimeter()}";
        }

        /// <summary>
        /// вычисляет площадь
        /// </summary>
        public double Square()
        {
            return p1.DistanceTo(p2) * p2.DistanceTo(p3);
        }

        /// <summary>
        /// вычисляет периметр
        /// </summary>
        public double Perimeter()
        {
            return (p1.DistanceTo(p2) + p2.DistanceTo(p3)) * 2;
        }

        /// <summary>
        /// поворачивает прямоугольник на angle градусов
        /// </summary>
        public void Rotate(double angle)
        {
            double scos = Cos(angle * PI / 180);
            double ssin = Sin(angle * PI / 180);
            var arr_p = new Point[] { p1, p2, p3, p4 };
            foreach (var p in arr_p)
            {
                (p.x, p.y) = (p_center.x + (p.x - p_center.x) * scos - (p.y - p_center.y) * ssin, p_center.y + (p.y - p_center.y) * scos - (p.x - p_center.x) * ssin);
            }
        }

        /// <summary>
        /// сдвиг прямоугольника
        /// </summary>
        public void Shift(double x, double y)
        {
            var arr_p = new Point[] { p1,  p2, p3, p4, p_center };
            foreach (var p in arr_p)
            {
                p.x += x;
                p.y += y;
            }
        }

        /// <summary>
        /// увеличивает ширину и высоту прямоугольника на заданные коэффициенты
        /// </summary>
        public void Ratio(double k_width, double k_height)
        {
            if ((k_width <= 0) || (k_height <= 0))
                throw new Exception("error of ratio");
            var arr_p = new Point[] { p1, p2, p3, p4, p_center };
            foreach (var p in arr_p)
            {
                p.x = p_center.x + (p.x - p_center.x) * k_width;
                p.y = p_center.y + (p.y - p_center.y) * k_height;
            }
        }
    }
}
