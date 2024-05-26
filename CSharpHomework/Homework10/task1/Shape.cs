namespace Homework10.task1
{
    public class Shape
    {
        /// <summary>
        /// класс фигуры
        /// </summary>
        public Shape() { }

        /// <summary>
        /// центр
        /// </summary>
        public Point Center { get; protected set; }


        // <summary>
        /// площадь
        /// </summary>
        public virtual double Square { get; protected set; }

        /// <summary>
        /// периметр
        /// </summary>
        public virtual double Perimeter { get; protected set; }


        /// <summary>
        /// поворачивает фигуру на angle градусов
        /// </summary>
        public virtual void Rotate(double angle) { }

        /// <summary>
        /// сдвиг
        /// </summary>
        public virtual void Shift(double x, double y) { }

        /// <summary>
        /// увеличивает ширину и высоту на заданные коэффициенты
        /// </summary>
        public virtual void Ratio(double k_width, double k_height) { }
    }
}
