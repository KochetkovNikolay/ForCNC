using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeEditor
{
    public class Code {
        public List<string> TrimmedCode { get; set; } = new List<string>();
        public List<string> FullCode { get; set; } = new List<string>();
        List<string> rewrittenNum;
        public void Clear()
        {
            FullCode.Clear();
            TrimmedCode.Clear();
        }
        public List<string> GetTrimmedLine() {
            return TrimmedCode;
        }
        public void SetCode(string[] code)
        {
            foreach (var line in code)
            {
                FullCode.Add(line);
                TrimmedCode.Add(Trim(line));
            }
        }
        private string Trim(string line)
        {
            string newStr = "";
            line = line.Replace(" ", "");
            line = line.Replace("\t", "");
            for (int i = 0; i < line.Length; i++)
            {
                if (i == 0 && line[i] == 'N') //Удаляем номер в начале
                {
                    i++;
                    while (char.IsDigit(line[i]))
                    {
                        if (line.Length == i + 1)
                            return "";
                        i++;
                    }
                }
                if (line[i] == '(') //Избавляемся от комментариев
                    while (line[i] != ')' && i < line.Length) { i++; }
                else
                    newStr += line[i];
            }
            return newStr;
        }
        private string RemoveNumber(string line)
        {
            line = line.Trim();
            string newStr = "";
            for (int i = 0; i < line.Length; i++)
                if (i == 0 && line[i] == 'N') //Удаляем номер в начале
                {
                    i++;
                    while (char.IsDigit(line[i]))
                    {
                        if (line.Length == i + 1)
                            return "";
                        i++;
                    }
                }
                else
                    newStr+=line[i];
            return newStr;
        }

        public void ResetNumbers()
        {
            rewrittenNum = new List<string>();
            for (int i = 0; i < FullCode.Count; i++)
            {
                FullCode[i] = RemoveNumber(FullCode[i]);
                rewrittenNum.Add('N' + i.ToString() + " " + FullCode[i]);
            }
            FullCode = rewrittenNum;
        }
    }

}
