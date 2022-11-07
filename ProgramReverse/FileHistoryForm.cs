using CodeEditor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ProgramReverse {
    public partial class FileHistoryForm : Form {
        private Form1 mainForm;
        public int OutputNum { get; set; } = 20;
        public List<string> FileHistory { get; set; } = new List<string>();
        public string FileLocation { get; set; }
        public FileHistoryForm(Form1 mainForm) {
            InitializeComponent();
            this.mainForm = mainForm;
            FileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\fileHistory.niko";
            FileRead();
            Shown += FileHistoryForm_Shown;
        }

        private void FileHistoryForm_Shown(object sender, EventArgs e) {
            FileRead();
            listBox.Items.Clear();
            for (int i = FileHistory.Count - 1; i >= 0 ; i--)
                listBox.Items.Add(FileHistory[i]);
        }

        private void FileRead() {
            try {
                string[] lines = File.ReadAllLines(FileLocation, Encoding.Default);
                FileHistory.Clear();
                foreach (string line in lines)
                    FileHistory.Add(line);
                if (FileHistory.Count > OutputNum)
                    while (FileHistory.Count > OutputNum)
                        FileHistory.RemoveAt(0);
            } catch (Exception) {
            }
            
        }
        public void PutNewFile(string path) {
            bool alreadyExist = false;
            int indexExist = 0;
            for (int i = 0; i < FileHistory.Count; i++) {
                if (FileHistory[i] == path) {
                    alreadyExist = true;
                    indexExist = i;
                }
            }
            if (!alreadyExist)
                FileHistory.Add(path);
            else {
                string temp = FileHistory[indexExist];
                FileHistory.RemoveAt(indexExist);
                FileHistory.Add(temp);
            }
            File.WriteAllText(FileLocation, "", Encoding.Default);
            for (int i = 0; i < FileHistory.Count; i++) {
                File.AppendAllText(FileLocation, FileHistory[i] + Environment.NewLine, Encoding.Default);
            }
            
        }

        private void listBox_DoubleClick(object sender, EventArgs e) {
            if (listBox.SelectedItems.Count > 0) {
                string path = listBox.SelectedItem.ToString();
                mainForm.Do(File.ReadAllLines(path, Encoding.Default), path);
                this.Close();
            }
        }
    }
}
