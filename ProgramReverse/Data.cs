using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeEditor
{
    internal class Data
    {
        public List<Coordinate> Coordinates { get; set; }

        //Для хренения максимальных и минимальных значений
        public float maxX { get; set; }
        public float maxY { get; set; }
        public float minX { get; set; }
        public float minY { get; set; }
        public float shiftX { get; set; }
        public float shiftY { get; set; }

        public float MyScale { get; set; }

        PictureBox pictureBox;

        public Data(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            Coordinates = new List<Coordinate>();
        }

        public void AddingCoordinate(string str, int number)
        {
            if ((str.IndexOf("G0") != -1 || str.IndexOf("G1") != -1 || str.IndexOf("G2") != -1 || str.IndexOf("G3") != -1) && str.IndexOf('Z') == -1)
            {
                var newCoordinate = new Coordinate(str);
                newCoordinate.NumLine = number;
                Coordinates.Add(newCoordinate);
            }
        }

        public void SetMaxMin()
        {
            for (int i = 0; i < Coordinates.Count; i++)
            {
                if (i == 0)
                {
                    minY = Coordinates[i].Y;
                    maxY = Coordinates[i].Y;
                    minX = Coordinates[i].X;
                    maxX = Coordinates[i].X;
                }
                if (maxX < Coordinates[i].X)
                    maxX = Coordinates[i].X;
                if (minX > Coordinates[i].X)
                    minX = Coordinates[i].X;
                if (maxY < Coordinates[i].Y)
                    maxY = Coordinates[i].Y;
                if (minY > Coordinates[i].Y)
                    minY = Coordinates[i].Y;
            }
        }
        public void SetShiftAndScale()
        {
            shiftX = 0 - minX;
            shiftY = 0 - maxY;

            MyScale = Math.Abs((pictureBox.Width - MyCanvas.Padding * 2) / (maxX - minX));
            if (pictureBox.Height - MyCanvas.Padding * 2 < (maxY - minY) * MyScale)
                MyScale = Math.Abs((pictureBox.Height - MyCanvas.Padding * 2) / (maxY - minY));
        }

        public string GetLimitX()
        {
            return "X " + minX.ToString() + "; " + maxX.ToString() + " (" + (maxX - minX).ToString() + ")";
        }

        public string GetLimitY()
        {
            return "Y " + minY.ToString() + "; " + maxY.ToString() + " (" + (maxY - minY).ToString() + ")";
        }

        public void SetNextPrev()
        {
            for (int i = 0; i < Coordinates.Count; i++)
            {
                if (i != 0)
                    Coordinates[i].Prev = Coordinates[i - 1];
                if (i < Coordinates.Count - 1)
                    Coordinates[i].Next = Coordinates[i + 1];
            }
        }

    }
}
