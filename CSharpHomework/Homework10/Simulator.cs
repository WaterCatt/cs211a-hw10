namespace Homework10
{
    class Simulator
    {
        /// <summary>
        /// список всех прямоугольников на плоскости
        /// </summary>
        List<Rect> rects = new List<Rect>();

        /// <summary>
        /// класс симулятора плоскости
        /// </summary>
        public Simulator(params Rect[] rects) 
        {
            this.rects.AddRange(rects);
        }

        /// <summary>
        /// выводит все прямоугольники на плоскости
        /// </summary>
        public void Write_All_Rects()
        {
            Console.WriteLine("Все прямоугольники на плоскости:");
            foreach (Rect rect in rects)
                Console.WriteLine(rect);
        }

        /// <summary>
        /// возвращает наиболее удаленный от центра координат прямоугольник
        /// </summary>
        public Rect? Most_Remote()
        {
            if (rects.Count == 0)
                return null;
            var p_0 = new Point(0, 0);

            double dist = 0;
            Rect res = new Rect(new Point(0, 0), new Point(0, 1), new Point(1, 0), new Point(1, 1));
            foreach (Rect rect in rects)
            {
                double min_dist = rect.p1.DistanceTo(p_0);
                if (rect.p2.DistanceTo(p_0) < min_dist)
                    min_dist = rect.p2.DistanceTo(p_0);
                if (rect.p3.DistanceTo(p_0) < min_dist)
                    min_dist = rect.p3.DistanceTo(p_0);
                if (rect.p4.DistanceTo(p_0) < min_dist)
                    min_dist = rect.p4.DistanceTo(p_0);
                if (dist < min_dist)
                {
                    res = rect;
                    dist = min_dist;
                }
            }
            return res;
        }

        /// <summary>
        /// Получение массива прямоугольников согласно заданному предикату
        /// </summary>
        public Rect[] Rects_on_predicate(Predicate<Rect> pred)
        {
            return rects.Where(x => pred(x)).ToArray();
        }
    }
}
