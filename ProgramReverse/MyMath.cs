using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CodeEditor
{
    internal static class MyMath
    {
        /// <summary>
        /// Найти гиппотенузу по двум катетам
        /// </summary>
        public static float Hypotenuse(float leg1, float leg2)
        {
            return (float)Math.Sqrt(leg1 * leg1 + leg2 * leg2);
        }

        /// <summary>
        /// Найти угол равнобедренного треугольника
        /// </summary>
        public static float AngleOfIsoscelesTriangle(float equalSides, float anotherSide)
        {
            float tempAngle = (float)(Math.Acos(anotherSide / (equalSides * 2)));
            tempAngle /= 0.0175f;
            return 180 - tempAngle * 2;
        }

        /// <summary>
        /// Найти катет через гипотенузу и катет
        /// </summary>
        public static float FindKatet(float hypo, float side)
        {
            return (float)Math.Sqrt(hypo * hypo - side * side);
        }

        /// <summary>
        /// Поиск точки по середине
        /// </summary>
        /// <returns>Возвращает точку находящуюся между двумя другими</returns>
        public static MyPoint FindMidpoint(MyPoint point1, MyPoint point2)
        {
            return new MyPoint((point1.X + point2.X) / 2, (point1.Y + point2.Y) / 2);
        }

        /// <summary>
        /// Расстояние между двумя точками
        /// </summary>
        public static float Distance(MyPoint point1, MyPoint point2)
        {
            return Hypotenuse(Math.Abs(point1.X - point2.X), Math.Abs(point1.Y - point2.Y));
        }


    }
}
