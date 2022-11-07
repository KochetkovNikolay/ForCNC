using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeEditor {
    public partial class Settings : Form {
        Form1 mainForm;
        public Settings(Form1 mainForm) {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void Settings_Load(object sender, EventArgs e) {
            checkBox1.Checked = Properties.Settings.Default.IsDotsNeed;
            checkBox2.Checked = Properties.Settings.Default.IsDotsCentreNeed;

            colorEditorBack.Color = Properties.Settings.Default.ClEditorBackground;
            colorEditorText.Color = Properties.Settings.Default.ClEditorText;
            colorGraphicBack.Color = Properties.Settings.Default.ClGraphicBackground;
            colorListBack.Color = Properties.Settings.Default.ClListBackground;
            colorListText.Color = Properties.Settings.Default.ClListText;
            colorLine.Color = Properties.Settings.Default.ClLine;
            colorArc.Color = Properties.Settings.Default.ClArc;
            colorSelectedItem.Color = Properties.Settings.Default.ClSelectedItem;

            tBoxWidthGraphic.Text = Properties.Settings.Default.GraphicWidth.ToString();
            tBoxHeightGraphic.Text = Properties.Settings.Default.GraphicHeigth.ToString();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.IsDotsNeed = checkBox1.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.IsDotsCentreNeed = checkBox2.Checked;
            Properties.Settings.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e) {
            colorDialog1.Color = Properties.Settings.Default.ClEditorBackground;
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                colorEditorBack.Color = colorDialog1.Color;
                Properties.Settings.Default.ClEditorBackground = colorDialog1.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void butEditorText_Click(object sender, EventArgs e) {
            colorDialog1.Color = Properties.Settings.Default.ClEditorText;
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                colorEditorText.Color = colorDialog1.Color;
                Properties.Settings.Default.ClEditorText = colorDialog1.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void butGraphicBack_Click(object sender, EventArgs e) {
            colorDialog1.Color = Properties.Settings.Default.ClGraphicBackground;
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                colorGraphicBack.Color = colorDialog1.Color;
                Properties.Settings.Default.ClGraphicBackground = colorDialog1.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void butListBack_Click(object sender, EventArgs e) {
            colorDialog1.Color = Properties.Settings.Default.ClListBackground;
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                colorListBack.Color = colorDialog1.Color;
                Properties.Settings.Default.ClListBackground = colorDialog1.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void butListText_Click(object sender, EventArgs e) {
            colorDialog1.Color = Properties.Settings.Default.ClListText;
            if (colorDialog1.ShowDialog() == DialogResult.OK) {
                colorListText.Color = colorDialog1.Color;
                Properties.Settings.Default.ClListText = colorDialog1.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void butAcceptWidthEditor_Click(object sender, EventArgs e) {
            float width = float.Parse(tBoxWidthGraphic.Text);
            if (width < 0 || width > 100)
                return;
            float editorWidth = 100 - width;
            mainForm.tableLayoutPanel1.ColumnStyles[1].Width = width;
            mainForm.tableLayoutPanel1.ColumnStyles[0].Width = editorWidth;
            Properties.Settings.Default.GraphicWidth = width;
            Properties.Settings.Default.Save();
        }

        private void butAcceptHeightGraphic_Click(object sender, EventArgs e) {
            float height = float.Parse(tBoxHeightGraphic.Text);
            if (height < 0 || height > 100)
                return;
            float downHeight = 100 - height;
            mainForm.tableLayoutPanel1.RowStyles[0].Height = height;
            mainForm.tableLayoutPanel1.RowStyles[1].Height = downHeight;
            Properties.Settings.Default.GraphicHeigth = height;
            Properties.Settings.Default.Save();
        }
    }
}
