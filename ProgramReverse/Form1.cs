using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgramReverse
{
    enum Sign{
        Plus,
        Sub,
        Mult,
        Div
    }

    public partial class Form1 : Form
    {

        VarForm varForm;
        public bool FormIsOpen { get; set; } = false;

        private bool fileIsOpen = false;
        public bool FileIsOpen { 
            get { return fileIsOpen; }
            set { 
            if(value == true)
                {
                    fileIsOpen = true;
                    button2.Enabled = true;
                    SaveButton.Enabled = true;
                    RegenButton.Enabled = true;
                }
            } }
        public float Padd { get; set; } = 20;
        public int LenghtOfAxes { get; set; } = 100;
        public PointF shiftOfMouse;

        public PointF MouseLocation { get; set; }

        //Для хренения максимальных и минимальных значений
        public float maxX { get; set; }
        public float maxY { get; set; }
        public float minX { get; set; }
        public float minY { get; set; }
        public float shiftX { get; set; }
        public float shiftY { get; set; }
        public PointF Zero { get; set; }

        /// <summary>
        /// Отступ рисунка от краев
        /// </summary>
        

        public float Scale { get; set; }


        public List<lattice> lattices = new List<lattice>();
        public List<tempLattice> lat = new List<tempLattice>(); //Для хранения решеток которые еще не получили значения

        List<double> numbers = new List<double>();
        public List<Coordinate> coordinates;
        List<Coordinate> ReverseCoordinate = new List<Coordinate>();
        List<string> AllCode = new List<string>();

        int timerNum = 0;

        MyCanvas canvas;
        public Form1()
        {
            InitializeComponent();
            coordinates = new List<Coordinate>();
            canvas = new MyCanvas(pictureBox1);
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            varForm = new VarForm(lattices);
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                Scale += 0.1f;
            else
                Scale -= 0.1f;
                    
            DrawProgram(coordinates);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            labelNameFile.Text = String.Empty;
            foreach (string file in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                labelNameFile.Text += file + "\n";
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e) {
            
        }
        private void richTextBox1_KeyDown(object sender, KeyEventArgs e) {
            
        }
        private void FullReset() {
            coordinates.Clear();
            AllCode.Clear();
            lattices.Clear();
            lat.Clear();
            numbers.Clear();
            ReverseCoordinate.Clear();
        }
        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e) {
            timerNum = 0;
            timer1.Start();
        }
        //Для выделения целой строки
        //int startFromIndex = richTextBox1.GetFirstCharIndexFromLine(3);
        //int linelength = richTextBox1.Lines[3].Length;
        //richTextBox1.Select(startFromIndex, linelength);
       
        private void timer1_Tick(object sender, EventArgs e) {
            timerNum++;
            if (timerNum == 2) {
                if (!richTextBox1.Focused)
                    return;
                try
                {
                    FullReset(); //Сброс всей информации

                    AddingLattices(richTextBox1.Lines); // Заносим в список все решетки

                    LatticesReplace(); // Замена всех решеток на значения в исходном файле

                    AddingCoordinate(); //Парсим и добавляем координаты

                    LatticeSorting(); // Сортировка решеток по возрастанию

                    SetNextPrev(coordinates); //Задаем указываем каждой координате ссылку на предыдущую

                    DrawProgram(coordinates); // Рисуем
                    label2.Visible = false;
                }
                catch (Exception)
                {
                    label2.Visible = true;
                }
                
               
                timerNum = 0;
                timer1.Stop();
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
                System.IO.File.WriteAllText(labelNameFile.Text, richTextBox1.Text, Encoding.Default);
        }

        private void panel1_Click(object sender, EventArgs e) {
            using (OpenFileDialog file = new OpenFileDialog()) {
                file.Title = "Выбери файл";
                file.InitialDirectory = @"C:\Users\Nikolay\Desktop";
                if (file.ShowDialog() == DialogResult.OK) {

                    FullReset(); //Сброс всей информации

                    labelNameFile.Text = file.FileName;  //Выводим имя файла в label

                    AddingLattices(File.ReadAllLines(file.FileName, Encoding.Default)); // Заносим в список все решетки

                    LatticesReplace(); // Замена всех решеток на значения в исходном файле

                    AddingCoordinate(); //Парсим и добавляем координаты

                    MaxMinDisplay();

                    LatticeSorting(); // Сортировка решеток по возрастанию

                    SetNextPrev(coordinates); //Задаем указываем каждой координате ссылку на предыдущую

                    richTextBox1.Text = File.ReadAllText(file.FileName, Encoding.Default);
                    richTextBox1.ScrollToCaret(); 

                    DrawProgram(coordinates); // Рисуем
                    FileIsOpen = true;
                }
            }
        }

        private void MaxMinDisplay()
        {
            labelLimitX.Text = "X " + minX.ToString() + "; " + maxX.ToString();
            labelLimitY.Text = "Y " + minY.ToString() + "; " + maxY.ToString();
        }

        private void LatticeSorting() {
            for (int i = 0; i < lattices.Count; i++)
                for (int j = 0; j < lattices.Count - 1; j++) {
                    if (lattices[j].Number > lattices[j + 1].Number) {
                        var temp = lattices[j];
                        lattices[j] = lattices[j + 1];
                        lattices[j + 1] = temp;
                    }
                }
        }

        private void AddingLattices(string[] source) {
            foreach (var item in source) {
                CheckVar(item);
                AllCode.Add(item.Replace(" ", ""));
            }
            for (int i = 0; i < lat.Count; i++)
                for (int j = 0; j < lattices.Count; j++)
                {
                    if (lat[i].Value.Contains(lattices[j].getLattice()))
                        lattices.Add(new lattice(lat[i].Number, MyMethods.Calculate(lat[i].Value.Replace(lattices[j].getLattice(), lattices[j].Value.ToString()))));
                }
        }
        private void LatticesReplace() {
            for (int i = 0; i < AllCode.Count; i++)
                for (int j = 0; j < lattices.Count; j++) {
                    AllCode[i] = AllCode[i].Replace(lattices[j].getLattice(), lattices[j].Value.ToString());
                }
        }

        private void AddingCoordinate() {
            foreach (var item in AllCode) {
                if ((item.IndexOf("G1") != -1 || item.IndexOf("G2") != -1 || item.IndexOf("G3") != -1) && item.IndexOf('Z') == -1) {
                    coordinates.Add(new Coordinate(item));
                }
            }
            for (int i = 0; i < coordinates.Count; i++)
            {
                if (i == 0)
                {
                    minY = coordinates[i].Y;
                    maxY = coordinates[i].Y;
                    minX = coordinates[i].X;
                    maxX = coordinates[i].X;
                }
                if (maxX < coordinates[i].X)
                    maxX = coordinates[i].X;
                if (minX > coordinates[i].X)
                    minX = coordinates[i].X;
                if (maxY < coordinates[i].Y)
                    maxY = coordinates[i].Y;
                if (minY > coordinates[i].Y)
                    minY = coordinates[i].Y;
            }
            SetShiftAndScale();
        }
        private void SetShiftAndScale()
        {
            shiftX = 0 - minX;
            shiftY = 0 - maxY;

            Scale = Math.Abs((pictureBox1.Width - Padd * 2) / (maxX - minX));
            if (pictureBox1.Height - Padd * 2 < (maxY - minY) * Scale)
                Scale = Math.Abs((pictureBox1.Height - Padd * 2) / (maxY - minY));
        }

        private void SetNextPrev(List<Coordinate> coordinates)
        {
            for (int i = 0; i < coordinates.Count; i++)
            {
                if (i != 0)
                    coordinates[i].Prev = coordinates[i - 1];
                if (i < coordinates.Count - 1)
                    coordinates[i].Next = coordinates[i + 1];
            }
        }

        private void DrawProgram(List<Coordinate> coordinates)
        {
            foreach (var item in coordinates)
            {
                if(item.Radius != 0)
                    item.ScaleR = item.Radius * Scale;
                else if (item.Type != GType.G1)
                {
                    item.scaleI = (item.I + shiftX) * Scale + Padd;
                    item.scaleJ = (item.J + shiftY) * Scale - Padd;
                }
                    item.scaleX = (item.X + shiftX) * Scale + Padd;
                    item.scaleY = (item.Y + shiftY) * Scale - Padd;
            }
            canvas.Clear();
            for (int i = 0; i < coordinates.Count; i++)
            {
                if (coordinates[i].Prev != null)
                {
                    if (coordinates[i].Type == GType.G1)
                        canvas.DrawLine(coordinates[i - 1].Point, coordinates[i].Point);
                    if (coordinates[i].Type == GType.G2)
                    {
                        if (coordinates[i].Radius != 0)
                            canvas.DrawArcByRadius(coordinates[i - 1].Point, coordinates[i].Point, coordinates[i].ScaleR, direction.clockwise);
                        else
                            canvas.DrawArcByCenter(coordinates[i - 1].Point, coordinates[i].Point, coordinates[i].Center, direction.clockwise);
                    }
                    if (coordinates[i].Type == GType.G3)
                    {
                        if (coordinates[i].Radius != 0)
                            canvas.DrawArcByRadius(coordinates[i - 1].Point, coordinates[i].Point, coordinates[i].ScaleR, direction.counter);
                        else
                            canvas.DrawArcByCenter(coordinates[i - 1].Point, coordinates[i].Point, coordinates[i].Center, direction.counter);
                    }
                }

            }
            //Нарисовать оси
            canvas.DrawAxes(new MyPoint(shiftX * Scale + Padd, -shiftY * Scale + Padd), new MyPoint(shiftX * Scale + Padd, -shiftY * Scale - LenghtOfAxes + Padd));
            canvas.DrawAxes(new MyPoint(shiftX * Scale + Padd, -shiftY * Scale + Padd), new MyPoint(shiftX * Scale + LenghtOfAxes + Padd, -shiftY * Scale + Padd));
            canvas.DrawAxes(new MyPoint(shiftX * Scale + Padd, -shiftY * Scale + Padd), new MyPoint(shiftX * Scale + Padd, -shiftY * Scale + LenghtOfAxes + Padd));
            canvas.DrawAxes(new MyPoint(shiftX * Scale + Padd, -shiftY * Scale + Padd), new MyPoint(shiftX * Scale - LenghtOfAxes + Padd, -shiftY * Scale + Padd));
            canvas.Refresh();
        }
        //private void Reverse()
        //{

        //    for (int i = 0, j = coordinates.Count - 1; i < coordinates.Count; i++, j--)
        //    {
        //        Coordinate newCoordinate = (Coordinate)coordinates[j].Clone();
        //        if (newCoordinate.Next != null)
        //        {
        //            if (newCoordinate.Next.Type == GType.G2)
        //                newCoordinate.Type = GType.G3;
        //            if (newCoordinate.Next.Type == GType.G3)
        //                newCoordinate.Type = GType.G2;
        //            if (newCoordinate.Next.Type == GType.G1)
        //                newCoordinate.Type = GType.G1;
        //        }
        //        if (newCoordinate.Type != GType.G1)
        //        {
        //            newCoordinate.Radius = newCoordinate.Next.Radius;
        //            newCoordinate.I = newCoordinate.Next.I;
        //            newCoordinate.J = newCoordinate.Next.J;
        //        }
        //        else
        //        {
        //            newCoordinate.Radius = null;
        //            newCoordinate.I = null;
        //            newCoordinate.J = null;
        //        }

        //        ReverseCoordinate.Add(newCoordinate);
        //    }
        //    SetNextPrev(ReverseCoordinate);
        //}



        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "X: " + ((e.Location.X - Padd) / Scale - shiftX).ToString("0.00") + "; Y: " + ((e.Location.Y - Padd) / Scale + shiftY).ToString("0.00");
            if(e.Button == MouseButtons.Left)
            {
                shiftOfMouse = new PointF(MouseLocation.X - e.Location.X, MouseLocation.Y - e.Location.Y);

                shiftX -= shiftOfMouse.X / Scale;
                shiftY += shiftOfMouse.Y / Scale;

                MouseLocation = e.Location;
                DrawProgram(coordinates);
            }
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseLocation = e.Location;
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //Reverse();
            richTextBox1.Clear();
            for (int i = 0; i < ReverseCoordinate.Count; i++)
            {
                richTextBox1.AppendText('\n' + ReverseCoordinate[i].GetString());
            }
            DrawProgram(ReverseCoordinate);
        }

        /// <summary>
        /// Парсер решеток
        /// </summary>
        public void CheckVar(string str) {
            if (str.IndexOf('#') == -1)
                return;
            str = str.Replace(" ", "");
            str = str.Replace(".", ",");
            List<int> nums = new List<int>();
            int num = -1;
            while (str.IndexOf('#', num + 1) != -1) {
                num = str.IndexOf('#', num + 1);
                nums.Add(num);
            }
            foreach (var lattice in nums) {
                string temp = "";
                for (int i = lattice + 1; i < str.Length; i++) {
                    if (char.IsDigit(str[i]) || str[i] == ',' || str[i] == '=' || str[i] == ']' || str[i] == '[' || str[i] == '#' || MyMethods.IsOperator(str[i].ToString())) {
                        temp += str[i];
                    } else {
                        if (temp.IndexOf('=') == -1)
                            break;
                        
                        int number = int.Parse(temp.Substring(0, temp.IndexOf('=')));
                        if (temp.IndexOf('#') != -1) {
                            lat.Add(new tempLattice(number, temp.Substring(temp.IndexOf('=') + 1, temp.Length - 1 - temp.IndexOf('='))));
                            break;
                        }
                        temp = temp.Replace("[", "");
                        temp = temp.Replace("]", "");
                        double value = MyMethods.Calculate(temp.Substring(temp.IndexOf('=') + 1, temp.Length - 1 - temp.IndexOf('=')));
                        lattices.Add(new lattice(number, value));
                        temp = "";
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if(!varForm.Created)
                varForm = new VarForm(lattices);
            if (!varForm.IsOpened)
            {
                varForm.Show();
            }
            if (this.Location.X < 130)
                varForm.Location = new Point(0, this.Location.Y + 20);
            else
                varForm.Location = new Point(this.Location.X - 130, this.Location.Y + 20);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void RegenButton_Click(object sender, EventArgs e)
        {
            SetShiftAndScale();
            DrawProgram(coordinates);
        }


    }
}
