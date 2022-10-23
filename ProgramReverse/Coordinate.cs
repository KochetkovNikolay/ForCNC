using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramReverse
{
    public enum GType
    {
        G1,
        G2,
        G3
    }
    public class Coordinate
    {


        public float Radius { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float I { get; set; }
        public float J { get; set; }
        public float scaleX { get; set; }
        public float scaleY { get; set; }
        public float scaleI { get; set; }
        public float scaleJ { get; set; }


        public GType Type { get; set; }
        public Coordinate Next { get; set; }
        public Coordinate Prev { get; set; }
        public MyPoint Point { 
            get {
                return new MyPoint(scaleX, -scaleY);
            } }
        public MyPoint Center
        {
            get
            {
                //return new MyPoint(float.Parse(I.Replace('.', ',')), -float.Parse(J.Replace('.', ',')));
                return new MyPoint(scaleI, -scaleJ);
            }
        }
        public Coordinate() { }
        public Coordinate(string line)
        {
            line = line.Replace('[', '(');
            line = line.Replace(']', ')');

            //Определяем линия это или дуга
            if (line.IndexOf("G1") != -1)
                this.Type = GType.G1;
            else if (line.IndexOf("G2") != -1)
                this.Type = GType.G2;
            else
                this.Type = GType.G3;

            //Вычисляем индекс каждого элемента координаты
            int indexX = line.IndexOf('X');
            int indexY = line.IndexOf('Y');
            int indexR = line.LastIndexOf('R');
            int indexI = line.LastIndexOf('I');
            int indexJ = line.LastIndexOf('J');

            if (indexX == -1 | indexY == -1)
                return;

            //Находим X
            string x = line.Substring(indexX, indexY - indexX);
            x = x.Remove(x.IndexOf('X'), 1); //Удалить сам X

            X = MyMethods.Calculate(x);




            //Находим Y
            //В зависимости от того, есть ли радиус и как записан
            string y;
            if (Type == GType.G1)                               //Если это прямая
                y = line.Substring(indexY, line.Length - indexY); 
            else if (indexR != -1)                              //Если есть радиус
                y = line.Substring(indexY, indexR - indexY);
            else if (indexI != -1 & indexI > indexJ)            //Если есть центр дуги и J впереди
                y = line.Substring(indexY, indexJ - indexY);
            else                                                //Если есть центр дуги и I впереди
                y = line.Substring(indexY, indexI - indexY);

            y = y.Remove(y.IndexOf('Y'), 1); //Удалить сам Y

            Y = MyMethods.Calculate(y);



            if (Type == GType.G2 | Type == GType.G3)
            {
                if (indexR != -1)
                {
                    string r;
                    r = line.Substring(indexR, line.Length - indexR);
                    r = r.Replace("R", "");
                    Radius = MyMethods.Calculate(r);
                }

                else
                {
                    string i, j;
                    if (indexJ > indexI) { 
                        i = line.Substring(indexI, indexJ - indexI);
                        j = line.Substring(indexJ, line.Length - indexJ);
                    }
                    else
                    {
                        i = line.Substring(indexI, line.Length - indexI);
                        j = line.Substring(indexJ, indexI - indexJ);
                    }
                    i = i.Remove(i.IndexOf('I'), 1);
                    I = MyMethods.Calculate(i);

                    j = j.Remove(j.IndexOf('J'), 1);
                    J = MyMethods.Calculate(j);

                }
            }
        }

        private float CheckMathSymbols(string str)
        {
            string[] multSymbols = str.Split('*');


            return 0;
        }


        public string GetString()
        {
            string rad = "";
            if (Radius != null)
                rad = " R[" + Radius + "]";
            if (I != null)
                rad = " I[" + I + "] J[" + J + "]";
            return " " + Type.ToString() + " X[" + X + "] Y[" + Y + "]" + rad; 
        }

        public object Clone()
        {
            Coordinate coor = new Coordinate();
            coor.I = this.I;
            coor.J = this.J;
            coor.X = this.X;
            coor.Y = this.Y;
            coor.Radius = this.Radius;
            Type = this.Type;
            if (Next != null)
            {
                Coordinate coorNext = new Coordinate();
                coorNext.I = Next.I;
                coorNext.J = Next.J;
                coorNext.X = Next.X;
                coorNext.Y = Next.Y;
                coorNext.Radius = Next.Radius;
                coorNext.Type = Next.Type;
                coor.Next = coorNext;
            }
            return coor;
        }
    }
}
