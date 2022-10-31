using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgramReverse
{
    public partial class VarForm : Form
    {
        List<lattice> varList;
        public bool IsOpened { get; set; } = false;
        public VarForm(List<lattice> varList)
        {
            InitializeComponent();
            this.varList = varList;
        }
        private void VarForm_Load(object sender, EventArgs e)
        {
            foreach (var item in varList)
                listBox1.Items.Add(item.getString());
            IsOpened = true;
        }

        private void VarForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsOpened = false;
        }
    }
}
