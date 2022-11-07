namespace CodeEditor {
    partial class Settings {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.butEditorText = new System.Windows.Forms.Button();
            this.butListBack = new System.Windows.Forms.Button();
            this.butSelectedItem = new System.Windows.Forms.Button();
            this.butArc = new System.Windows.Forms.Button();
            this.butLine = new System.Windows.Forms.Button();
            this.butListText = new System.Windows.Forms.Button();
            this.butGraphicBack = new System.Windows.Forms.Button();
            this.butEditorBack = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tBoxWidthGraphic = new System.Windows.Forms.TextBox();
            this.butAcceptWidthGraphic = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.colorSelectedItem = new CodeEditor.ColorExample();
            this.colorArc = new CodeEditor.ColorExample();
            this.colorEditorText = new CodeEditor.ColorExample();
            this.colorLine = new CodeEditor.ColorExample();
            this.colorListText = new CodeEditor.ColorExample();
            this.colorListBack = new CodeEditor.ColorExample();
            this.colorGraphicBack = new CodeEditor.ColorExample();
            this.colorEditorBack = new CodeEditor.ColorExample();
            this.label6 = new System.Windows.Forms.Label();
            this.tBoxHeightGraphic = new System.Windows.Forms.TextBox();
            this.butAcceptHeightGraphic = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // colorDialog1
            // 
            this.colorDialog1.FullOpen = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(290, 9);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox2.Location = new System.Drawing.Point(290, 39);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Проставлять точки координат";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 73);
            this.panel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(5, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(252, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Проставлять точки центров дуг";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.colorSelectedItem);
            this.panel2.Controls.Add(this.colorArc);
            this.panel2.Controls.Add(this.colorEditorText);
            this.panel2.Controls.Add(this.colorLine);
            this.panel2.Controls.Add(this.butEditorText);
            this.panel2.Controls.Add(this.colorListText);
            this.panel2.Controls.Add(this.butListBack);
            this.panel2.Controls.Add(this.colorListBack);
            this.panel2.Controls.Add(this.colorGraphicBack);
            this.panel2.Controls.Add(this.butSelectedItem);
            this.panel2.Controls.Add(this.butArc);
            this.panel2.Controls.Add(this.butLine);
            this.panel2.Controls.Add(this.butListText);
            this.panel2.Controls.Add(this.butGraphicBack);
            this.panel2.Controls.Add(this.colorEditorBack);
            this.panel2.Controls.Add(this.butEditorBack);
            this.panel2.Location = new System.Drawing.Point(345, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(288, 322);
            this.panel2.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 304);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(227, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Изменения сохранятся после перезапуска";
            // 
            // butEditorText
            // 
            this.butEditorText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butEditorText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butEditorText.Location = new System.Drawing.Point(14, 117);
            this.butEditorText.Name = "butEditorText";
            this.butEditorText.Size = new System.Drawing.Size(210, 30);
            this.butEditorText.TabIndex = 3;
            this.butEditorText.Text = "Текст редактора";
            this.butEditorText.UseVisualStyleBackColor = true;
            this.butEditorText.Click += new System.EventHandler(this.butEditorText_Click);
            // 
            // butListBack
            // 
            this.butListBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butListBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butListBack.Location = new System.Drawing.Point(14, 81);
            this.butListBack.Name = "butListBack";
            this.butListBack.Size = new System.Drawing.Size(210, 30);
            this.butListBack.TabIndex = 8;
            this.butListBack.Text = "Фон списка координат";
            this.butListBack.UseVisualStyleBackColor = true;
            this.butListBack.Click += new System.EventHandler(this.butListBack_Click);
            // 
            // butSelectedItem
            // 
            this.butSelectedItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butSelectedItem.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butSelectedItem.Location = new System.Drawing.Point(14, 261);
            this.butSelectedItem.Name = "butSelectedItem";
            this.butSelectedItem.Size = new System.Drawing.Size(210, 30);
            this.butSelectedItem.TabIndex = 12;
            this.butSelectedItem.Text = "Выбранный элемент";
            this.butSelectedItem.UseVisualStyleBackColor = true;
            // 
            // butArc
            // 
            this.butArc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butArc.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butArc.Location = new System.Drawing.Point(14, 225);
            this.butArc.Name = "butArc";
            this.butArc.Size = new System.Drawing.Size(210, 30);
            this.butArc.TabIndex = 11;
            this.butArc.Text = "Дуги";
            this.butArc.UseVisualStyleBackColor = true;
            // 
            // butLine
            // 
            this.butLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butLine.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butLine.Location = new System.Drawing.Point(14, 189);
            this.butLine.Name = "butLine";
            this.butLine.Size = new System.Drawing.Size(210, 30);
            this.butLine.TabIndex = 10;
            this.butLine.Text = "Линии";
            this.butLine.UseVisualStyleBackColor = true;
            // 
            // butListText
            // 
            this.butListText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butListText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butListText.Location = new System.Drawing.Point(14, 153);
            this.butListText.Name = "butListText";
            this.butListText.Size = new System.Drawing.Size(210, 30);
            this.butListText.TabIndex = 9;
            this.butListText.Text = "Текст списка координат";
            this.butListText.UseVisualStyleBackColor = true;
            this.butListText.Click += new System.EventHandler(this.butListText_Click);
            // 
            // butGraphicBack
            // 
            this.butGraphicBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butGraphicBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butGraphicBack.Location = new System.Drawing.Point(14, 45);
            this.butGraphicBack.Name = "butGraphicBack";
            this.butGraphicBack.Size = new System.Drawing.Size(210, 30);
            this.butGraphicBack.TabIndex = 7;
            this.butGraphicBack.Text = "Фон графической части";
            this.butGraphicBack.UseVisualStyleBackColor = true;
            this.butGraphicBack.Click += new System.EventHandler(this.butGraphicBack_Click);
            // 
            // butEditorBack
            // 
            this.butEditorBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butEditorBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butEditorBack.Location = new System.Drawing.Point(14, 9);
            this.butEditorBack.Name = "butEditorBack";
            this.butEditorBack.Size = new System.Drawing.Size(210, 30);
            this.butEditorBack.TabIndex = 0;
            this.butEditorBack.Text = "Фон редактора";
            this.butEditorBack.UseVisualStyleBackColor = true;
            this.butEditorBack.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(427, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Цвета программы";
            // 
            // tBoxWidthGraphic
            // 
            this.tBoxWidthGraphic.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tBoxWidthGraphic.Location = new System.Drawing.Point(53, 149);
            this.tBoxWidthGraphic.Name = "tBoxWidthGraphic";
            this.tBoxWidthGraphic.Size = new System.Drawing.Size(100, 26);
            this.tBoxWidthGraphic.TabIndex = 5;
            // 
            // butAcceptWidthGraphic
            // 
            this.butAcceptWidthGraphic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butAcceptWidthGraphic.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butAcceptWidthGraphic.Location = new System.Drawing.Point(159, 149);
            this.butAcceptWidthGraphic.Name = "butAcceptWidthGraphic";
            this.butAcceptWidthGraphic.Size = new System.Drawing.Size(111, 26);
            this.butAcceptWidthGraphic.TabIndex = 6;
            this.butAcceptWidthGraphic.Text = "Подтвердить";
            this.butAcceptWidthGraphic.UseVisualStyleBackColor = true;
            this.butAcceptWidthGraphic.Click += new System.EventHandler(this.butAcceptWidthEditor_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(37, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(256, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "Ширина графической части (%)";
            // 
            // colorSelectedItem
            // 
            this.colorSelectedItem.Color = System.Drawing.Color.White;
            this.colorSelectedItem.Location = new System.Drawing.Point(246, 261);
            this.colorSelectedItem.Name = "colorSelectedItem";
            this.colorSelectedItem.Size = new System.Drawing.Size(30, 30);
            this.colorSelectedItem.TabIndex = 18;
            // 
            // colorArc
            // 
            this.colorArc.Color = System.Drawing.Color.White;
            this.colorArc.Location = new System.Drawing.Point(246, 225);
            this.colorArc.Name = "colorArc";
            this.colorArc.Size = new System.Drawing.Size(30, 30);
            this.colorArc.TabIndex = 17;
            // 
            // colorEditorText
            // 
            this.colorEditorText.Color = System.Drawing.Color.White;
            this.colorEditorText.Location = new System.Drawing.Point(246, 117);
            this.colorEditorText.Name = "colorEditorText";
            this.colorEditorText.Size = new System.Drawing.Size(30, 30);
            this.colorEditorText.TabIndex = 6;
            // 
            // colorLine
            // 
            this.colorLine.Color = System.Drawing.Color.White;
            this.colorLine.Location = new System.Drawing.Point(246, 189);
            this.colorLine.Name = "colorLine";
            this.colorLine.Size = new System.Drawing.Size(30, 30);
            this.colorLine.TabIndex = 16;
            // 
            // colorListText
            // 
            this.colorListText.Color = System.Drawing.Color.White;
            this.colorListText.Location = new System.Drawing.Point(246, 153);
            this.colorListText.Name = "colorListText";
            this.colorListText.Size = new System.Drawing.Size(30, 30);
            this.colorListText.TabIndex = 15;
            // 
            // colorListBack
            // 
            this.colorListBack.Color = System.Drawing.Color.White;
            this.colorListBack.Location = new System.Drawing.Point(246, 81);
            this.colorListBack.Name = "colorListBack";
            this.colorListBack.Size = new System.Drawing.Size(30, 30);
            this.colorListBack.TabIndex = 14;
            // 
            // colorGraphicBack
            // 
            this.colorGraphicBack.Color = System.Drawing.Color.White;
            this.colorGraphicBack.Location = new System.Drawing.Point(246, 45);
            this.colorGraphicBack.Name = "colorGraphicBack";
            this.colorGraphicBack.Size = new System.Drawing.Size(30, 30);
            this.colorGraphicBack.TabIndex = 13;
            // 
            // colorEditorBack
            // 
            this.colorEditorBack.Color = System.Drawing.Color.White;
            this.colorEditorBack.Location = new System.Drawing.Point(246, 9);
            this.colorEditorBack.Name = "colorEditorBack";
            this.colorEditorBack.Size = new System.Drawing.Size(30, 30);
            this.colorEditorBack.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(37, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(252, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "Высота графической части (%)";
            // 
            // tBoxHeightGraphic
            // 
            this.tBoxHeightGraphic.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tBoxHeightGraphic.Location = new System.Drawing.Point(53, 207);
            this.tBoxHeightGraphic.Name = "tBoxHeightGraphic";
            this.tBoxHeightGraphic.Size = new System.Drawing.Size(100, 26);
            this.tBoxHeightGraphic.TabIndex = 9;
            // 
            // butAcceptHeightGraphic
            // 
            this.butAcceptHeightGraphic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butAcceptHeightGraphic.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butAcceptHeightGraphic.Location = new System.Drawing.Point(159, 207);
            this.butAcceptHeightGraphic.Name = "butAcceptHeightGraphic";
            this.butAcceptHeightGraphic.Size = new System.Drawing.Size(111, 26);
            this.butAcceptHeightGraphic.TabIndex = 10;
            this.butAcceptHeightGraphic.Text = "Подтвердить";
            this.butAcceptHeightGraphic.UseVisualStyleBackColor = true;
            this.butAcceptHeightGraphic.Click += new System.EventHandler(this.butAcceptHeightGraphic_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 364);
            this.Controls.Add(this.butAcceptHeightGraphic);
            this.Controls.Add(this.tBoxHeightGraphic);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.butAcceptWidthGraphic);
            this.Controls.Add(this.tBoxWidthGraphic);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(450, 350);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butEditorBack;
        private System.Windows.Forms.Button butEditorText;
        private ColorExample colorEditorBack;
        private ColorExample colorSelectedItem;
        private ColorExample colorArc;
        private ColorExample colorLine;
        private ColorExample colorListText;
        private ColorExample colorListBack;
        private ColorExample colorGraphicBack;
        private System.Windows.Forms.Button butSelectedItem;
        private System.Windows.Forms.Button butArc;
        private System.Windows.Forms.Button butLine;
        private System.Windows.Forms.Button butListText;
        private System.Windows.Forms.Button butListBack;
        private System.Windows.Forms.Button butGraphicBack;
        private ColorExample colorEditorText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tBoxWidthGraphic;
        private System.Windows.Forms.Button butAcceptWidthGraphic;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tBoxHeightGraphic;
        private System.Windows.Forms.Button butAcceptHeightGraphic;
    }
}