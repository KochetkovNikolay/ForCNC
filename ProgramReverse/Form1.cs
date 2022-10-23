using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

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

                FullReset(); //Сброс всей информации

                AddingLattices(richTextBox1.Lines); // Заносим в список все решетки

                LatticesReplace(); // Замена всех решеток на значения в исходном файле

                AddingCoordinate(); //Парсим и добавляем координаты

                LatticeSorting(); // Сортировка решеток по возрастанию


                SetNextPrev(coordinates); //Задаем указываем каждой координате ссылку на предыдущую

                DrawProgram(coordinates); // Рисуем
               
                timerNum = 0;
                timer1.Stop();
            }
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

                    LatticeSorting(); // Сортировка решеток по возрастанию


                    SetNextPrev(coordinates); //Задаем указываем каждой координате ссылку на предыдущую

                    richTextBox1.Text = File.ReadAllText(file.FileName, Encoding.Default);

                    DrawProgram(coordinates); // Рисуем
                }
            }
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
                            canvas.DrawArcByRadius(coordinates[i - 1].Point, coordinates[i].Point, coordinates[i].Radius, direction.clockwise);
                        else
                            canvas.DrawArcByCenter(coordinates[i - 1].Point, coordinates[i].Point, coordinates[i].Center, direction.clockwise);
                    }
                    if (coordinates[i].Type == GType.G3)
                    {
                        if (coordinates[i].Radius != 0)
                            canvas.DrawArcByRadius(coordinates[i - 1].Point, coordinates[i].Point, coordinates[i].Radius, direction.counter);
                        else
                            canvas.DrawArcByCenter(coordinates[i - 1].Point, coordinates[i].Point, coordinates[i].Center, direction.counter);
                    }
                }

            }
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
            label1.Text = e.Location.ToString();
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
                    if (char.IsDigit(str[i]) || str[i] == ',' || str[i] == '=' || str[i] == ']' || str[i] == '[' || str[i] == '#' || str[i] == '-') {
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
            VarForm varForm = new VarForm(lattices);
            
            varForm.Show();
            if (this.Location.X < 130)
                varForm.Location = new Point(0, this.Location.Y + 20);
            else
                varForm.Location = new Point(this.Location.X - 130, this.Location.Y + 20);
        }
    }
}
