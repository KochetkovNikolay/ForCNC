using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CodeEditor {
    internal static class Program {
        static Mutex mutex = new Mutex(true, "myProgram");
        [STAThread]
        static void Main(string[] args2) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] args = Environment.GetCommandLineArgs();
            SingleInstanceController controller = new SingleInstanceController(args2);
            controller.Run(args);

        }
    }
    public class SingleInstanceController : WindowsFormsApplicationBase {
        string[] args;
        public SingleInstanceController(string[] args) {
            this.args = args;
            IsSingleInstance = true;
            StartupNextInstance += this_StartupNextInstance;
        }
        void this_StartupNextInstance(object sender, StartupNextInstanceEventArgs e) {
            Form1 form = MainForm as Form1;
            if (form.AskingAboutSave())
                form.OpenFile(e.CommandLine[1]);
        }

        protected override void OnCreateMainForm() {
            MainForm = new Form1(args);
        }
    }
}
