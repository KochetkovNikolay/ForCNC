using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramReverse
{
    public class Variable
    {
        public List<lattice> Lattices { get; set; } = new List<lattice>();
        public List<tempLattice> TempLattices { get; set; } = new List<tempLattice>(); //Для хранения решеток которые еще не получили значения

        Code code;

        public Variable(Code code)
        {
            this.code = code;
        }
        public void Clear()
        {
            Lattices.Clear();
            TempLattices.Clear();
        }

        public void AddingVar(string str)
        {
            int index;
            if ((index = str.IndexOf('#')) == -1) //Если решетки в строке нет, выйти
                return;
            str = str.Replace(".", ",");
            str = str.Replace("[", "(");
            str = str.Replace("]", ")");
            string temp = "";
            string letter = "";
            for (int i = index + 1; i < str.Length; i++)
            {
                if (char.IsLetter(str[i]))
                {
                    letter += str[i];
                    continue;
                }

                if (char.IsDigit(str[i]) || str[i] == ',' || str[i] == '=' || str[i] == ')' || str[i] == '(' || str[i] == '#' || MyMethods.IsOperator(str[i].ToString()))
                {
                    if (letter == "COS" || letter == "SIN" || letter == "SQRT")
                    {
                        temp += letter;
                        letter = "";
                    }
                    temp += str[i];
                }
                if (i == str.Length - 1)
                {
                    if (temp.IndexOf('=') == -1)
                        break;

                    int number = int.Parse(temp.Substring(0, temp.IndexOf('=')));
                    double value = MyMethods.Calculate(temp.Substring(temp.IndexOf('=') + 1, temp.Length - 1 - temp.IndexOf('=')));
                    Lattices.Add(new lattice(number, value));
                }
            }
        }
        public void AddingLattices(string[] source)
        {
            foreach (var item in source)
                CheckVar(item);

            for (int x = 0; x < 5; x++)
                for (int i = 0; i < TempLattices.Count; i++)
                    for (int j = 0; j < Lattices.Count; j++)
                    {
                        if (TempLattices[i].Value.Contains(Lattices[j].getLattice()))
                            Lattices.Add(new lattice(TempLattices[i].Number, MyMethods.Calculate(TempLattices[i].Value.Replace(Lattices[j].getLattice(), Lattices[j].Value.ToString()))));
                    }
        }

        public void VarSorting()
        {
            for (int i = 0; i < Lattices.Count; i++)
                for (int j = 0; j < Lattices.Count - 1; j++)
                {
                    if (Lattices[j].Number > Lattices[j + 1].Number)
                    {
                        var temp = Lattices[j];
                        Lattices[j] = Lattices[j + 1];
                        Lattices[j + 1] = temp;
                    }
                }
        }

        public string VarReplace(string str)
        {
            if (str.IndexOf('#') == -1)
                return str;
            string newStr = str;
            if (Lattices.Count > 0)
                for (int i = Lattices.Count - 1; i >= 0; i--)
                {
                    string latt = Lattices[i].getLattice();
                    int num = newStr.LastIndexOf(latt) + latt.Length;
                    if (newStr.IndexOf(latt) != -1) {
                        if (newStr.IndexOf('=') != -1)
                        {
                            string[] temp = newStr.Split('=');
                            temp[1] = temp[1].Replace(Lattices[i].getLattice(), Lattices[i].Value.ToString());
                            newStr = temp[0] + '=' + temp[1];
                        }
                        else
                        {
                            newStr = newStr.Replace(latt, Lattices[i].Value.ToString());
                        }
                    }
                }
            return newStr;
        }

        public void LatticesReplace()
        {
            for (int i = 0; i < code.TrimCode.Count(); i++)
                for (int j = 0; j < Lattices.Count; j++)
                {
                    code.TrimCode[i] = code.TrimCode[i].Replace(Lattices[j].getLattice(), Lattices[j].Value.ToString());
                }
        }

        public void LatticeSorting()
        {
            for (int i = 0; i < Lattices.Count; i++)
                for (int j = 0; j < Lattices.Count - 1; j++)
                {
                    if (Lattices[j].Number > Lattices[j + 1].Number)
                    {
                        var temp = Lattices[j];
                        Lattices[j] = Lattices[j + 1];
                        Lattices[j + 1] = temp;
                    }
                }
        }


        /// <summary>
        /// Парсер решеток
        /// </summary>
        private void CheckVar(string str)
        {
            int index;
            if ((index = str.IndexOf('#')) == -1)
                return;
            str = str.Replace(".", ",");
            str = str.Replace("[", "(");
            str = str.Replace("]", ")");
            string temp = "";
            for (int i = index + 1; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]) || str[i] == ',' || str[i] == '=' || str[i] == ')' || str[i] == '(' || str[i] == '#' || MyMethods.IsOperator(str[i].ToString()))
                {
                    temp += str[i];
                }
                else
                {
                    if (temp.IndexOf('=') == -1)
                        break;

                    int number = int.Parse(temp.Substring(0, temp.IndexOf('=')));
                    if (temp.IndexOf('#') != -1)
                    {
                        TempLattices.Add(new tempLattice(number, temp.Substring(temp.IndexOf('=') + 1, temp.Length - 1 - temp.IndexOf('='))));
                        break;
                    }
                    double value = MyMethods.Calculate(temp.Substring(temp.IndexOf('=') + 1, temp.Length - 1 - temp.IndexOf('=')));
                    Lattices.Add(new lattice(number, value));
                    temp = "";
                }
            }
        }
    }
}

