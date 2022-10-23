using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ProgramReverse
{
    public class MyPoint
    {
        public float X { get; set; }
        public float Y { get; set; }

        public float Length
        {
            get { return (float)Math.Sqrt(X * X + Y * Y); }
            set
            {
                float tempLenght = Length;
                X = X * value / tempLenght;
                Y = Y * value / tempLenght;
            }
        }
        public MyPoint(float x, float y)
        {
            X = x;
            Y = y;
        }
        public MyPoint Clone()
        {
            return new MyPoint(X, Y);
        }
        public void Normalize()
        {
            float tempLenght = Length;
            X = X / tempLenght;
            Y = Y / tempLenght;
        }

        public void RotateClockWise()
        {
            float temp = Y;
            Y = X;
            X = -temp;
        }
        public void RotateCounterClockWise()
        {
            float temp = X;
            X = Y;
            Y = -temp;
        }

        public bool Compare(MyPoint comparePoint)
        {
            if (X <= comparePoint.X + 2 && X >= comparePoint.X - 2 && Y <= comparePoint.Y + 2 && Y >= comparePoint.Y - 2)
                return true;
            else
                return false;
        }
        public static MyPoint operator +(MyPoint point1, MyPoint point2)
        {
            return new MyPoint(point1.X + point2.X, point1.Y + point2.Y);
        }
        public static MyPoint operator -(MyPoint point1, MyPoint point2)
        {
            return new MyPoint(point1.X - point2.X, point1.Y - point2.Y);
        }
    }
}
