using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace ProgramReverse
{
    enum Sign
    {
        Plus,
        Sub,
        Mult,
        Div
    }

    public partial class Form1 : Form
    {

        VarForm varForm;
        Variable variables;
        Code code;
        Data data;
        public bool FormIsOpen { get; set; } = false;

        private bool fileIsOpen = false;
        public bool FileIsOpen
        {
            get { return fileIsOpen; }
            set
            {
                if (value == true)
                {
                    fileIsOpen = true;
                }
            }
        }
        public PointF shiftOfMouse;

        public PointF MouseLocation { get; set; }


        public List<Coordinate> Coordinates { get; set; }

        List<double> numbers = new List<double>();
        List<Coordinate> ReverseCoordinate = new List<Coordinate>();
        public List<string> AllCode { get; set; } = new List<string>();

        int timerNum = 0;

        MyCanvas canvas;
        public Form1(string[] args)
        {
            InitializeComponent();
            Coordinates = new List<Coordinate>();
            canvas = new MyCanvas(pictureBox1);
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            code = new Code();
            variables = new Variable(code);
            varForm = new VarForm(variables.Lattices);
            data = new Data(pictureBox1);
            if (args.Length > 0)
            {
                errorLabel.Text = args[0];
                using (StreamReader reader = new StreamReader(args[0]))
                {
                    richTextBox1.Clear();
                    tBoxFileName.Text = args[0];  //Выводим имя файла в label
                    Do(System.IO.File.ReadAllLines(args[0], Encoding.Default));
                    foreach (var item in code.FullCode)
                    {
                        richTextBox1.AppendText(item + '\n');
                    }
                    SetTextColor();

                }
            }
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                data.MyScale += 0.1f;
            else
                data.MyScale -= 0.1f;
            if (data.MyScale < 0)
                data.MyScale = 0.01f;
            canvas.DrawProgram(data);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void FullReset()
        {
            listBox1.Items.Clear();
            code.Clear();
            data.Coordinates.Clear();
            variables.Clear();
            numbers.Clear();
            ReverseCoordinate.Clear();
            MyMethods.Clear();
        }
        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //timerNum = 0;
            //timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }


        private void panel3_DragDrop(object sender, DragEventArgs e)
        {
            foreach (string file in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                richTextBox1.Clear();
                tBoxFileName.Text = file;  //Выводим имя файла в label
                Do(System.IO.File.ReadAllLines(file, Encoding.Default));

                string stroka = "";
                foreach (var item in code.FullCode)
                {
                    stroka += item + '\n';
                }
                richTextBox1.Text = stroka;
                SetTextColor();
            }
        }
        private void SetTextColor()
        {
            richTextBox1.Visible = false;
            foreach (Match item in Regex.Matches(richTextBox1.Text, @"\(.*\)", RegexOptions.IgnoreCase))
            {
                richTextBox1.Select(item.Index, item.Length);
                richTextBox1.SelectionColor = Color.FromArgb(205, 187, 141);
            }
            foreach (Match item in Regex.Matches(richTextBox1.Text, @"G1\s+X.*\n|G0\s+X.*", RegexOptions.IgnoreCase))
            {
                richTextBox1.Select(item.Index, item.Length);
                richTextBox1.SelectionColor = Color.FromArgb(156, 217, 239);
            }
            foreach (Match item in Regex.Matches(richTextBox1.Text, @"G2\s+X.*\n|G3\s+X.*\n", RegexOptions.IgnoreCase))
            {
                richTextBox1.Select(item.Index, item.Length);
                richTextBox1.SelectionColor = Color.FromArgb(205, 183, 84);
            }
            richTextBox1.Select(0, 0);
            richTextBox1.Visible = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            canvas.DrawOneLine(data, listBox1.SelectedIndex, selectXY_label);
        }
        private void Do(string[] lines)
        {
            FullReset();

            code.SetCode(lines);

            for (int i = 0; i < code.TrimmedCode.Count; i++)
            {
                if (code.TrimmedCode[i] == "")
                    continue;
                code.TrimmedCode[i] = variables.VarReplace(code.TrimmedCode[i]); //Заменяем решетки при наличии
                variables.AddingVar(code.TrimmedCode[i]); //Проверяем на наличие переменных, добавляем при наличии
                variables.VarSorting();
                data.AddingCoordinate(code.TrimmedCode[i], i);
            }
            data.SetMaxMin();
            data.SetShiftAndScale();
            //labelLimitX.Text = data.GetLimitX();
            //labelLimitY.Text = data.GetLimitY();

            data.SetNextPrev();

            listBox1.Items.AddRange(code.FullCode.ToArray());

            canvas.DrawProgram(data); // Рисуем
            FileIsOpen = true;
            errorLabel.Visible = false;

        }

        private void panel1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "Все файлы|*.*||*.tap||*.nc";
                file.Title = "Выбери файл";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    richTextBox1.Clear();
                    tBoxFileName.Text = file.FileName;  //Выводим имя файла в label
                    Do(System.IO.File.ReadAllLines(file.FileName, Encoding.Default));
                    string stroka = "";
                    foreach (var item in code.FullCode)
                    {
                        stroka += item + '\n';
                    }
                    richTextBox1.Text = stroka;
                    SetTextColor();
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            System.IO.File.WriteAllText(tBoxFileName.Text, richTextBox1.Text, Encoding.Default);
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
            if (FileIsOpen)
                label1.Text = "X: " + ((e.Location.X - MyCanvas.Padding) / data.MyScale - data.shiftX).ToString("0.00") + "; Y: " + ((e.Location.Y - MyCanvas.Padding) / data.MyScale + data.shiftY).ToString("0.00");
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                shiftOfMouse = new PointF(MouseLocation.X - e.Location.X, MouseLocation.Y - e.Location.Y);

                data.shiftX -= shiftOfMouse.X / data.MyScale;
                data.shiftY += shiftOfMouse.Y / data.MyScale;

                MouseLocation = e.Location;
                canvas.DrawProgram(data);
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MouseLocation = e.Location;

        }


        //private void button2_Click(object sender, EventArgs e)
        //{
        //    //Reverse();
        //    richTextBox1.Clear();
        //    for (int i = 0; i < ReverseCoordinate.Count; i++)
        //    {
        //        richTextBox1.AppendText('\n' + ReverseCoordinate[i].GetString());
        //    }
        //    canvas.DrawProgram(data);
        //}


        private void button2_Click_1(object sender, EventArgs e)
        {
            if (!varForm.Created)
                varForm = new VarForm(variables.Lattices);
            if (!varForm.IsOpened)
            {
                varForm.Show();
            }
        }



        private void RegenButton_Click(object sender, EventArgs e)
        {
            data.SetShiftAndScale();
            canvas.DrawProgram(data);


        }



        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            richTextBox1.Focus();
            int line = richTextBox1.GetFirstCharIndexFromLine(listBox1.SelectedIndex);
            richTextBox1.Select(line, richTextBox1.Lines[listBox1.SelectedIndex].Length);
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            int line = richTextBox1.GetFirstCharIndexFromLine(listBox1.SelectedIndex);
            richTextBox1.Select(line, richTextBox1.Lines[listBox1.SelectedIndex].Length);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = false;
            code.ResetNumbers();
            richTextBox1.Clear();
            listBox1.Items.Clear();
            foreach (var item in code.FullCode)
            {
                richTextBox1.AppendText(item + '\n');
                listBox1.Items.Add(item);
            }
            SetTextColor();
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                try
                {
                    int selectedItem = listBox1.SelectedIndex;
                    Do(richTextBox1.Lines);
                    listBox1.SelectedIndex = selectedItem;
                    errorLabel.Visible = false;
                }
                catch (Exception)
                {
                    errorLabel.Visible = true;
                }
            }
        }
    }
}

