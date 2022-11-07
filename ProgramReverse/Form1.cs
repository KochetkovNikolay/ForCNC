using ProgramReverse;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace CodeEditor {
    enum Sign {
        Plus,
        Sub,
        Mult,
        Div
    }

    public partial class Form1 : Form {

        VarForm varForm;
        FileHistoryForm fileHistoryForm;
        Variable variables;
        Code code;
        Data data;
        public bool FormIsOpen { get; set; } = false;

        bool isChanged = false;
        public bool IsChanged {
            get {
                return isChanged;
            }
            set {
                if (value == false) {
                    isChanged = false;
                    confirmButton.Enabled = false;
                } else {
                    isChanged = true;
                    confirmButton.Enabled = true;
                }
            }

        }
        private bool fileIsOpen = false;
        public bool FileIsOpen {
            get { return fileIsOpen; }
            set {
                if (value == true) {
                    saveButton.Enabled = true;
                    fileIsOpen = true;
                }
            }
        }
        public PointF shiftOfMouse;

        public string FileExtension { get; set; } = "Все файлы|*.*||*.tap||*.nc";
        public string Path { get; set; }

        public PointF MouseLocation { get; set; }



        List<double> numbers = new List<double>();
        List<Coordinate> ReverseCoordinate = new List<Coordinate>();
        public List<string> AllCode { get; set; } = new List<string>();


        MyCanvas canvas;
        public Form1(string[] args2) {
            InitializeComponent();
            SetSettings();


            if (!Properties.Settings.Default.FullScreenForm)
                this.WindowState = FormWindowState.Normal;

            canvas = new MyCanvas(pictureBox1);
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            code = new Code();
            variables = new Variable(code);
            varForm = new VarForm(variables.Lattices);
            fileHistoryForm = new FileHistoryForm(this);
            data = new Data(pictureBox1);
            if (args2.Length > 0)
                OpenFile(args2[0]);

        }

        private void SetSettings() {
            textBox.BackColor = Properties.Settings.Default.ClEditorBackground;
            textBox.ForeColor = Properties.Settings.Default.ClEditorText;
            pictureBox1.BackColor = Properties.Settings.Default.ClGraphicBackground;
            listBox1.BackColor = Properties.Settings.Default.ClListBackground;
            listBox1.ForeColor = Properties.Settings.Default.ClListText;

            tableLayoutPanel1.ColumnStyles[1].Width = Properties.Settings.Default.GraphicWidth;
            tableLayoutPanel1.ColumnStyles[0].Width = 100 - Properties.Settings.Default.GraphicWidth;
            tableLayoutPanel1.RowStyles[0].Height = Properties.Settings.Default.GraphicHeigth;
            tableLayoutPanel1.RowStyles[1].Height = 100 - Properties.Settings.Default.GraphicHeigth;
        }

        public void OpenFile(string path) {
            FullReset();
            Do(System.IO.File.ReadAllLines(path, Encoding.Default), path);
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
        }

        private void panel1_Click(object sender, EventArgs e) {
            using (OpenFileDialog file = new OpenFileDialog()) {
                file.Filter = "Все файлы|*.*||*.tap||*.nc";
                file.Title = "Выбери файл";
                if (file.ShowDialog() == DialogResult.OK) {
                    Do(System.IO.File.ReadAllLines(file.FileName, Encoding.Default), file.FileName);
                }
            }
        }

        private void panel3_DragDrop(object sender, DragEventArgs e) {
            string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
            Do(System.IO.File.ReadAllLines(file[0], Encoding.Default), file[0]);
        }

        public void Do(string[] lines, string path) {

            FullReset();
            this.Text = "CodeEditor " + path;
            Path = path;
            FileExtension = "*" + path.Substring(path.LastIndexOf('.'), path.Length - path.LastIndexOf('.')) + "||Все файлы|*.*"; ;

            code.SetCode(lines);

            for (int i = 0; i < code.TrimmedCode.Count; i++) {
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

            foreach (var item in data.Coordinates) {
                string lineForList = item.Type.ToString() + " X " + item.X.ToString() + "; Y " + item.Y.ToString();
                if (item.Radius != 0)
                    lineForList += "; R " + item.Radius.ToString();
                else if (item.Type != GType.G1 && item.Type != GType.G0)
                    lineForList += "; I " + item.I.ToString() + "; J " + item.J.ToString();
                listBox1.Items.Add(lineForList);
            }

            canvas.DrawProgram(data); // Рисуем
            FileIsOpen = true;
            IsChanged = false;
            errorLabel.Visible = false;

            string text = "";
            foreach (var item in code.FullCode) {
                text += item + Environment.NewLine;
            }
            textBox.Text = text;
            fileHistoryForm.PutNewFile(path);
        }

        public void Do2(string[] lines, string path) {

            listBox1.Items.Clear();
            code.Clear();
            data.Coordinates.Clear();
            variables.Clear();
            numbers.Clear();
            ReverseCoordinate.Clear();
            MyMethods.Clear();

            this.Text = "CodeEditor" + path;
            Path = path;
            FileExtension = "*" + path.Substring(path.LastIndexOf('.'), path.Length - path.LastIndexOf('.')) + "||Все файлы|*.*"; ;

            code.SetCode(lines);

            for (int i = 0; i < code.TrimmedCode.Count; i++) {
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

            foreach (var item in data.Coordinates) {
                string lineForList = item.Type.ToString() + " X " + item.X.ToString() + "; Y " + item.Y.ToString();
                if (item.Radius != 0)
                    lineForList += "; R " + item.Radius.ToString();
                else if (item.Type != GType.G1 && item.Type != GType.G0)
                    lineForList += "; I " + item.I.ToString() + "; J " + item.J.ToString();
                listBox1.Items.Add(lineForList);
            }

            canvas.DrawProgram(data); // Рисуем
            FileIsOpen = true;
            IsChanged = false;
            errorLabel.Visible = false;
            
            fileHistoryForm.PutNewFile(path);
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e) {
            if (e.Delta > 0)
                data.MyScale += 0.1f;
            else
                data.MyScale -= 0.1f;
            if (data.MyScale < 0)
                data.MyScale = 0.01f;
            canvas.DrawProgram(data);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Link;
        }

        private void FullReset() {
            listBox1.Items.Clear();
            code.Clear();
            data.Coordinates.Clear();
            variables.Clear();
            numbers.Clear();
            ReverseCoordinate.Clear();
            textBox.Clear();
            MyMethods.Clear();
        }




        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            canvas.DrawOneLine(data, data.Coordinates[listBox1.SelectedIndex].NumLine, selectXY_label);
            int startIndex = textBox.GetFirstCharIndexFromLine(data.Coordinates[listBox1.SelectedIndex].NumLine);
            textBox.Select(startIndex, textBox.Lines[data.Coordinates[listBox1.SelectedIndex].NumLine].Length);
            textBox.ScrollToCaret();
        }




        private void SaveButton_Click(object sender, EventArgs e) {
            System.IO.File.WriteAllText(Path, textBox.Text, Encoding.Default);
        }





        private void SetNextPrev(List<Coordinate> coordinates) {
            for (int i = 0; i < coordinates.Count; i++) {
                if (i != 0)
                    coordinates[i].Prev = coordinates[i - 1];
                if (i < coordinates.Count - 1)
                    coordinates[i].Next = coordinates[i + 1];
            }
        }





        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) {
            if (FileIsOpen)
                label1.Text = "X: " + ((e.Location.X - MyCanvas.Padding) / data.MyScale - data.shiftX).ToString("0.00") + "; Y: " + ((e.Location.Y - MyCanvas.Padding) / data.MyScale + data.shiftY).ToString("0.00");
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) {
                shiftOfMouse = new PointF(MouseLocation.X - e.Location.X, MouseLocation.Y - e.Location.Y);

                data.shiftX -= shiftOfMouse.X / data.MyScale;
                data.shiftY += shiftOfMouse.Y / data.MyScale;

                MouseLocation = e.Location;
                canvas.DrawProgram(data);
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) {
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


        private void button2_Click_1(object sender, EventArgs e) {
            if (!varForm.Created)
                varForm = new VarForm(variables.Lattices);
            if (!varForm.IsOpened) {
                varForm.Show();
            }
        }



        private void RegenButton_Click(object sender, EventArgs e) {
            data.SetShiftAndScale();
            canvas.DrawProgram(data);
        }



        private void Form1_DragEnter(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Copy;
        }


        private void pictureBox1_Click(object sender, EventArgs e) {
            pictureBox1.Focus();
        }

        private void button4_Click(object sender, EventArgs e) {
            code.ResetNumbers();
            textBox.Clear();
            listBox1.Items.Clear();
            string text = "";
            foreach (var item in code.FullCode) {
                text += item + Environment.NewLine;
                listBox1.Items.Add(item);
            }
            textBox.Text = text;
        }
        private void ConfirmChanges() {
            try {
                Do2(textBox.Lines, Path);
                errorLabel.Visible = false;

            } catch (Exception) {
                errorLabel.Visible = true;
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e) {
            if (e.Control && e.KeyCode == Keys.Q && IsChanged) {
                ConfirmChanges();
                IsChanged = false;
                e.SuppressKeyPress = true;
            }
            if ((!e.Control || (e.Control && e.KeyCode == Keys.V) || (e.Control && e.KeyCode == Keys.Z)) && !e.Alt && !e.Shift || (e.Shift && e.KeyValue > 20))
                IsChanged = true;

        }

        private void ConfirmButton_Click(object sender, EventArgs e) {
            ConfirmChanges();
            IsChanged = false;

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            //Запомнить состояние окна
            Properties.Settings.Default.FullScreenForm = this.WindowState == FormWindowState.Maximized;
            Properties.Settings.Default.Save();

            if (!AskingAboutSave())
                e.Cancel = true;
        }

        private void настройкиToolStripMenuItem1_Click(object sender, EventArgs e) {
            Settings settings = new Settings(this);
            settings.Show();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e) {
            using (var save = new SaveFileDialog()) {
                save.Filter = FileExtension;
                save.FileName = Path;
                if (save.ShowDialog() == DialogResult.OK) {
                    System.IO.File.WriteAllText(save.FileName, textBox.Text, Encoding.Default);
                }
            }
        }

        public bool AskingAboutSave() {
            if (FileIsOpen && IsChanged) {
                DialogResult = DialogResult.Cancel;
                if ((DialogResult = MessageBox.Show("Сохранить файл?\n" + Path, "Сохранение", MessageBoxButtons.YesNoCancel)) == DialogResult.Yes) {
                    System.IO.File.WriteAllText(Path, textBox.Text, Encoding.Default);
                    return true;
                } else if (DialogResult == DialogResult.Cancel) {
                    return false;
                }
            }
            return true;
        }

        private void fileHistoryButton_Click(object sender, EventArgs e) {
            fileHistoryForm.ShowDialog();
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
    }
}

