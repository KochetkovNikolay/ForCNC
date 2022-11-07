using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeEditor
{
    enum direction
    {
        clockwise,
        counter
    }
    enum type
    {
        normal,
        click
    }
    internal class MyCanvas
    {
        PictureBox picBox;
        Graphics g;
        Bitmap bitmap;
        type Type = type.normal;
        public int LenghtOfAxes { get; set; } = 100;
        public Color BackColor { get; set; } = Color.FromArgb(30, 30, 30);
        public Pen ForLineG0
        {
            get
            {
                if (Type == type.normal)
                {
                    Pen newPen = new Pen(Color.White, 1);
                    newPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    return newPen;
                }
                else
                {
                    return new Pen(new SolidBrush(Color.Blue), 1);
                }
            }
        }
        public Pen ForLine
        {
            get
            {
                if (Type == type.normal)
                {
                    return new Pen(new SolidBrush(Color.White), 2);
                }
                else
                {
                    return new Pen(new SolidBrush(Color.Blue), 2);
                }
            }
        }
        public Pen ThinSilver { get; set; } = new Pen(new SolidBrush(Color.Silver), 1);
        public Pen ForArcCenter { get; set; } = new Pen(new SolidBrush(Color.DarkOrange), 1);
        public Pen ForArc
        {
            get
            {
                if (Type == type.normal)
                {
                    return new Pen(new SolidBrush(Color.DarkOrange), 2);
                }
                else
                {
                    return new Pen(new SolidBrush(Color.Blue), 2);
                }
            }
        }
        public Pen ForAxes { get; set; } = new Pen(new SolidBrush(Color.Orange), 1);
        public static int Padding { get; set; } = 20;
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


            if (Properties.Settings.Default.IsDotsNeed) {
                DrawPoint(point1, ThinSilver); // Нарисовать точку 1
                DrawPoint(point2, ThinSilver); // Нарисовать точку 1
            }
            if (Properties.Settings.Default.IsDotsCentreNeed) {
                DrawPoint(center, ForArcCenter); // Нарисовать центр дуги
            }

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


            if (Properties.Settings.Default.IsDotsNeed) {
                DrawPoint(point1, ThinSilver); // Нарисовать точку 1
                DrawPoint(point2, ThinSilver); // Нарисовать точку 1
            }
            if (Properties.Settings.Default.IsDotsCentreNeed) {
                DrawPoint(center, ForArcCenter); // Нарисовать центр дуги
            }

            DrawArc(center, radius, angle1, angle2);
        }

        public void DrawLine(MyPoint point1, MyPoint point2)
        {
            g.DrawLine(ForLine, point1.X, point1.Y, point2.X, point2.Y);
            if (Properties.Settings.Default.IsDotsNeed) {
                DrawPoint(point1, ThinSilver); // Нарисовать точку 1
                DrawPoint(point2, ThinSilver); // Нарисовать точку 1
            }
        }
        public void DrawLineG0(MyPoint point1, MyPoint point2)
        {
            g.DrawLine(ForLineG0, point1.X, point1.Y, point2.X, point2.Y);
            if (Properties.Settings.Default.IsDotsNeed) {
                DrawPoint(point1, ThinSilver); // Нарисовать точку 1
                DrawPoint(point2, ThinSilver); // Нарисовать точку 1
            }
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
        public void DrawOneLine(Data data, int number, Label label)
        {
            Type = type.normal;
            List<Coordinate> twoCoordinate = new List<Coordinate>();

            //Установка значений в масштабе
            foreach (var item in data.Coordinates)
            {
                if (item.Radius != 0)
                    item.ScaleR = item.Radius * data.MyScale;
                else if (item.Type != GType.G1 && item.Type != GType.G0)
                {
                    item.scaleI = (item.I + data.shiftX) * data.MyScale + Padding;
                    item.scaleJ = (item.J + data.shiftY) * data.MyScale - Padding;
                }
                item.scaleX = (item.X + data.shiftX) * data.MyScale + Padding;
                item.scaleY = (item.Y + data.shiftY) * data.MyScale - Padding;
               
                if (item.NumLine == number && item.Prev != null)
                {
                    twoCoordinate.Add(item.Prev);
                    twoCoordinate.Add(item);
                    label.Text = "X: " + Math.Round(item.X, 2).ToString() + " Y: " + Math.Round(item.Y, 2).ToString();
                }
            }
            

            Clear();

            Draw(data.Coordinates);

            Type = type.click;

            Draw(twoCoordinate);

            //Нарисовать оси
            DrawAxes(new MyPoint(data.shiftX * data.MyScale + Padding, -data.shiftY * data.MyScale + Padding),
                new MyPoint(data.shiftX * data.MyScale + Padding, -data.shiftY * data.MyScale - LenghtOfAxes + Padding));

            DrawAxes(new MyPoint(data.shiftX * data.MyScale + Padding, -data.shiftY * data.MyScale + Padding),
                new MyPoint(data.shiftX * data.MyScale + LenghtOfAxes + Padding, -data.shiftY * data.MyScale + Padding));

            DrawAxes(new MyPoint(data.shiftX * data.MyScale + Padding, -data.shiftY * data.MyScale + Padding),
                new MyPoint(data.shiftX * data.MyScale + Padding, -data.shiftY * data.MyScale + LenghtOfAxes + Padding));

            DrawAxes(new MyPoint(data.shiftX * data.MyScale + Padding, -data.shiftY * data.MyScale + Padding),
                new MyPoint(data.shiftX * data.MyScale - LenghtOfAxes + Padding, -data.shiftY * data.MyScale + Padding));



            Refresh();

        }

        private void Draw(List<Coordinate> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].Type == GType.G0)
                    if (i == 0)
                    {
                        DrawLineG0(new Coordinate { X = 0, Y = 0, Type = GType.G0 }.Point, list[i].Point);
                    }
                    else
                    {
                        DrawLineG0(list[i - 1].Point, list[i].Point);
                    }
                else if (list[i].Type == GType.G1)
                    DrawLine(list[i - 1].Point, list[i].Point);
                else if (list[i].Type == GType.G2)
                {
                    if (list[i].Radius != 0)
                        DrawArcByRadius(list[i - 1].Point, list[i].Point, list[i].ScaleR, direction.clockwise);
                    else
                        DrawArcByCenter(list[i - 1].Point, list[i].Point, list[i].Center, direction.clockwise);
                }
                else if (list[i].Type == GType.G3)
                {
                    if (list[i].Radius != 0)
                        DrawArcByRadius(list[i - 1].Point, list[i].Point, list[i].ScaleR, direction.counter);
                    else
                        DrawArcByCenter(list[i - 1].Point, list[i].Point, list[i].Center, direction.counter);
                }
            }
        }

        public void DrawProgram(Data data)
        {
            Type = type.normal;
            //Установка значений в масштабе
            foreach (var item in data.Coordinates)
            {
                if (item.Radius != 0)
                    item.ScaleR = item.Radius * data.MyScale;
                else if (item.Type != GType.G1 && item.Type != GType.G0)
                {
                    item.scaleI = (item.I + data.shiftX) * data.MyScale + Padding;
                    item.scaleJ = (item.J + data.shiftY) * data.MyScale - Padding;
                }
                item.scaleX = (item.X + data.shiftX) * data.MyScale + Padding;
                item.scaleY = (item.Y + data.shiftY) * data.MyScale - Padding;
            }


            Clear();

            Draw(data.Coordinates);

            //Нарисовать оси
            DrawAxes(new MyPoint(data.shiftX * data.MyScale + Padding, -data.shiftY * data.MyScale + Padding),
                new MyPoint(data.shiftX * data.MyScale + Padding, -data.shiftY * data.MyScale - LenghtOfAxes + Padding));

            DrawAxes(new MyPoint(data.shiftX * data.MyScale + Padding, -data.shiftY * data.MyScale + Padding),
                new MyPoint(data.shiftX * data.MyScale + LenghtOfAxes + Padding, -data.shiftY * data.MyScale + Padding));

            DrawAxes(new MyPoint(data.shiftX * data.MyScale + Padding, -data.shiftY * data.MyScale + Padding),
                new MyPoint(data.shiftX * data.MyScale + Padding, -data.shiftY * data.MyScale + LenghtOfAxes + Padding));

            DrawAxes(new MyPoint(data.shiftX * data.MyScale + Padding, -data.shiftY * data.MyScale + Padding),
                new MyPoint(data.shiftX * data.MyScale - LenghtOfAxes + Padding, -data.shiftY * data.MyScale + Padding));
            Refresh();
        }

    }
}
