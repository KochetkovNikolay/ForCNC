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
        //Для хренения максимальных и минимальных значений
        static public float maxX { get; set; } 
        static public float maxY { get; set; }
        static public float minX { get; set; }
        static public float minY { get; set; }

        public string X { get; set; }
        public string Y { get; set; }
        public string Radius { get; set; }
        public string I { get; set; }
        public string J { get; set; }

        public float X_value { get {
                return MyMethods.Calculate(X.Replace('.', ',')); ;
            } }
        public float Y_value { get {
                return -MyMethods.Calculate(Y.Replace('.', ','));
            } }
        public float I_value { get {
                return MyMethods.Calculate(I.Replace('.', ','));
            }
        }
        public float J_value { get {
                return MyMethods.Calculate(J.Replace('.', ','));
            } }

        public GType Type { get; set; }
        public Coordinate Next { get; set; }
        public Coordinate Prev { get; set; }
        public MyPoint Point { 
            get {
                return new MyPoint(X_value, Y_value);
            } }
        public MyPoint Center
        {
            get
            {
                //return new MyPoint(float.Parse(I.Replace('.', ',')), -float.Parse(J.Replace('.', ',')));
                return new MyPoint(I_value, -J_value);
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
            X = x.Remove(x.IndexOf('X'), 1); //Удалить сам X
            if (maxX < X_value)
                maxX = X_value;
           

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

            Y = y.Remove(y.IndexOf('Y'), 1); //Удалить сам Y

            if(maxY < Y_value)
                maxY = Y_value;

            if (Type == GType.G2 | Type == GType.G3)
            {
                if (indexR != -1)
                {
                    string r;
                    r = line.Substring(indexR, line.Length - indexR);
                    r = r.Replace("R", "");
                    Radius = r.Trim();
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

                    i = i.Replace("I", "");
                    I = i.Trim();

                    j = j.Replace("J", "");
                    J = j.Trim();

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
