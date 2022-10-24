using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgramReverse
{
    enum direction
    {
        clockwise,
        counter
    }
    internal class MyCanvas
    {
        PictureBox picBox;
        Graphics g;
        Bitmap bitmap;
        public Color BackColor { get; set; } = Color.FromArgb(30, 30, 30);
        public Pen DotPen { get; set; } = new Pen(new SolidBrush(Color.White), 2);
        public Pen ThinSilver  { get; set; } = new Pen(new SolidBrush(Color.Silver), 1);
        public Pen ForArcCenter { get; set; } = new Pen(new SolidBrush(Color.DarkOrange), 1);
        public Pen ForArc { get; set; } = new Pen(new SolidBrush(Color.DarkOrange), 2);
        public Pen ForAxes { get; set; } = new Pen(new SolidBrush(Color.Orange), 1);
        public MyCanvas(PictureBox pictureBox)
        {
            picBox = pictureBox;
            bitmap = new Bitmap(1920, 1080);
            g = Graphics.FromImage(bitmap);
            picBox.Image = bitmap;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        }
        public void DrawArcByCenter(MyPoint point1, MyPoint point2, MyPoint center, direction direct)
        {
            MyPoint midPoint = MyMath.FindMidpoint(point1, point2); // точка между теми точками

            float leg1 = Math.Abs(point1.X - point2.X);
            float leg2 = Math.Abs(point1.Y - point2.Y);
            float hypo = MyMath.Hypotenuse(leg1, leg2); //Расстояние между точками


            float radius = MyMath.Distance(center, point1);

            float fromMidToCenter = MyMath.FindKatet(radius, hypo / 2); //Расстояние между midPoint и центром дуги

            //Если направление против час. стрелки, то поменять точки местами
            if (direct == direction.counter)
            {
                MyPoint temp = point1.Clone();
                point1 = point2;
                point2 = temp;
            }

            float angle1 = 0;
            //Расчет начального угла
            if (point1.X > center.X && point1.Y < center.Y)
            {
                angle1 = (float)Math.Asin(Math.Abs(point1.Y - center.Y) / radius);
                angle1 = angle1 / 0.0175f; // Перевод в градусы
            }
            else if (point1.X <= center.X && point1.Y < center.Y)
            {
                angle1 = MyMath.AngleOfIsoscelesTriangle(radius, MyMath.Distance(point1, new MyPoint(center.X + radius, center.Y)));
            }
            else if (point1.X <= center.X && point1.Y > center.Y)
            {
                angle1 = MyMath.AngleOfIsoscelesTriangle(radius, MyMath.Distance(point1, new MyPoint(center.X - radius, center.Y)));
                angle1 += 180;
            }
            else
            {
                angle1 = MyMath.AngleOfIsoscelesTriangle(radius, MyMath.Distance(point1, new MyPoint(center.X, center.Y + radius)));
                angle1 += 270;
            }

            //Расчет общего угла
            float angle2;
            float distBetweenPoints = MyMath.Distance(point1, point2);
            if (distBetweenPoints > radius * 2 - 0.5 && distBetweenPoints < radius * 2 + 0.5)
                angle2 = 180;
            else
                angle2 = MyMath.AngleOfIsoscelesTriangle(radius, MyMath.Distance(point1, point2));

            //Вектор для определения ушел ли центр дуги за линию соединяющую точки (если да, то угол определяется чуть по другому)
            MyPoint newVector = new MyPoint(point2.X - point1.X, point2.Y - point1.Y);
            newVector.RotateClockWise();
            newVector.Normalize();
            newVector.Length += fromMidToCenter;
            newVector += midPoint;
            if (!newVector.Compare(center))
            {
                angle2 = 360 - angle2;
            }



            DrawPoint(point1, ThinSilver); // Нарисовать точку 1
            DrawPoint(point2, ThinSilver); // Нарисовать точку 1
            DrawPoint(center, ForArcCenter); // Нарисовать центр дуги
            //DrawPoint(point2); // Нарисовать точку 2

            //g.DrawLine(DotPen, point1.X, point1.Y, point2.X, point2.Y);
            //g.DrawLine(DotPen, center.X, 0, center.X, 1000);
            //g.DrawLine(DotPen, 0, center.Y, 2000, center.Y);


            DrawArc(center, radius, angle1, angle2);
        }
        public void Clear()
        {
            g.Clear(BackColor);
        }
        public void DrawArcByRadius(MyPoint point1, MyPoint point2, float radius, direction direct)
        {
            MyPoint midPoint = MyMath.FindMidpoint(point1, point2); // точка между теми точками

            float leg1 = Math.Abs(point1.X - point2.X);
            float leg2 = Math.Abs(point1.Y - point2.Y);
            float hypo = MyMath.Hypotenuse(leg1, leg2); //Расстояние между точками

            float fromMidToCenter = MyMath.FindKatet(radius, hypo / 2); //Расстояние между midPoint и центром дуги

            MyPoint center = new MyPoint(point2.X - point1.X, point2.Y - point1.Y);

            if (direct == direction.counter)
            {
                center.RotateCounterClockWise();
                MyPoint temp = point1.Clone();
                point1 = point2;
                point2 = temp;
            }
            else
                center.RotateClockWise();
            center.Normalize();

            center.Length = fromMidToCenter;
            center = midPoint + center;

            float angle1 = 0;
            //Расчет улов
            if (point1.X >= center.X && point1.Y <= center.Y)
            {
                angle1 = (float)Math.Asin(Math.Abs(point1.Y - center.Y) / radius);
                angle1 = angle1 / 0.0175f; // Перевод в градусы
            }
            else if (point1.X <= center.X && point1.Y <= center.Y)
            {
                angle1 = MyMath.AngleOfIsoscelesTriangle(radius, MyMath.Distance(point1, new MyPoint(center.X + radius, center.Y)));
            }
            else if (point1.X <= center.X && point1.Y >= center.Y)
            {
                angle1 = MyMath.AngleOfIsoscelesTriangle(radius, MyMath.Distance(point1, new MyPoint(center.X - radius, center.Y)));
                angle1 += 180;
            }
            else if (point1.X >= center.X && point1.Y >= center.Y)
            {
                angle1 = MyMath.AngleOfIsoscelesTriangle(radius, MyMath.Distance(point1, new MyPoint(center.X, center.Y + radius)));
                angle1 += 270;
            }

            float angle2 = MyMath.AngleOfIsoscelesTriangle(radius, MyMath.Distance(point1, point2));


            DrawPoint(point1, ThinSilver); // Нарисовать точку 1
            DrawPoint(point2, ThinSilver); // Нарисовать точку 2
            DrawPoint(center, ForArcCenter); // Нарисовать центр дуги

            DrawArc(center, radius, angle1, angle2);
        }

        public void DrawLine(MyPoint point1, MyPoint point2)
        {
            g.DrawLine(DotPen, point1.X, point1.Y, point2.X, point2.Y);
            DrawPoint(point1, ThinSilver);
            DrawPoint(point2, ThinSilver);
        }
        public void DrawAxes(MyPoint point1, MyPoint point2)
        {
            g.DrawLine(ForAxes, point1.X, point1.Y, point2.X, point2.Y);
        }

        public void Refresh() => picBox.Refresh();


        private void DrawPoint(MyPoint point, Pen pen)
        {
            try
            {
                g.DrawRectangle(pen, point.X - 3, point.Y - 3, 6, 6); // Нарисовать точку 1
            }
            catch (Exception)
            {
            }

        }
        private void DrawArc(MyPoint vector, float r, float angle1, float angle2)
        {
            try
            {
                g.DrawArc(ForArc, vector.X - r, vector.Y - r, r * 2, r * 2, -angle1, angle2);
            }
            catch (Exception)
            {
            }
        }

    }
}
