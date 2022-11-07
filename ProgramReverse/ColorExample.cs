using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeEditor {
    public partial class ColorExample : UserControl {
        Graphics g;
        Bitmap bitmap;
        private Color color = Color.White;
        public Color Color {
            get { return color; }
            set {
                color = value;
                g.FillRectangle(new SolidBrush(value), 0, 0, pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Refresh();
            } }
        public ColorExample() {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
        }
    }
}
