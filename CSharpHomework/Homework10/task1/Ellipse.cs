namespace Homework10.task1
{
    public class Ellipse : Shape
    {
        /// <summary>
        /// радиус 1 (горизонтальная полуось)
        /// </summary>
        public double A { get; private set; }

        /// <summary>
        /// радиус 2 (вертикальная полуось)
        /// </summary>
        public double B { get; private set; }

        /// <summary>
        /// угол поворота эллипса по часовой стрелке
        /// </summary>
        public double Angle { get; private set; } = 0;

        /// <summary>
        /// класс эллипса
        /// </summary>
        /// <param name="center">центр</param>
        /// <param name="a">радиус 1 (горизонтальная полуось)</param>
        /// <param name="b">радиус 2 (вертикальная полуось)</param>
        /// <param name="angle">угол поворота эллипса по часовой стрелке</param>
        public Ellipse(Point center, double a, double b, double angle = 0)
        {
            Center = center;
            if ((a < 0) || (b < 0))
            {
                throw new ArgumentException("радиус не может быть отрицательным");
            }
            A = a;
            B = b;
        }

        public override string ToString()
        {
            return $"Ellipse[center = {Center}, a = {A}, b = {B}, angle = {Angle}]   Square={Square}, Perimeter={Perimeter}";
        }

        // <summary>
        /// площадь
        /// </summary>
        public override double Square { get => Math.Round(double.Pi * A * B, 2); }

        /// <summary>
        /// периметр
        /// </summary>
        public override double Perimeter { get => Math.Round(double.Pi * Math.Sqrt(2*(A*A + B*B)), 2); }


        /// <summary>
        /// поворачивает эллипс на angle градусов
        /// </summary>
        public override void Rotate(double angle)
        {
            Angle += angle;
        }

        /// <summary>
        /// сдвиг эллипса
        /// </summary>
        public override void Shift(double x, double y)
        {
            Center.x += x;
            Center.y += y;
        }

        /// <summary>
        /// увеличивает ширину и высоту эллипса на заданные коэффициенты
        /// </summary>
        public override void Ratio(double k_width, double k_height)
        {
            if ((k_width <= 0) || (k_height <= 0))
                throw new Exception("error of ratio");
            A *= k_width;
            B *= k_height;
        }
    }
}
