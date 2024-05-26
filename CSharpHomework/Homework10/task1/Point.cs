using static System.Math;

namespace Homework10.task1
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
            return $"({Round(x, 2)}, {Round(y, 2)})";
        }

        /// <summary>
        /// возвращает расcтояние до точки
        /// </summary>
        public double DistanceTo(Point p)
        {
            return Sqrt(Pow(x - p.x, 2) + Pow(y - p.y, 2));
        }
    }
}
