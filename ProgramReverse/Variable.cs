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

        List<string> AllCode = new List<string>();

        private void AddingLattices(string[] source)
        {
            foreach (var item in source)
            {
                CheckVar(item);
                AllCode.Add(item.Replace(" ", ""));
            }
            for (int i = 0; i < TempLattices.Count; i++)
                for (int j = 0; j < Lattices.Count; j++)
                {
                    if (TempLattices[i].Value.Contains(Lattices[j].getLattice()))
                        Lattices.Add(new lattice(TempLattices[i].Number, MyMethods.Calculate(TempLattices[i].Value.Replace(Lattices[j].getLattice(), Lattices[j].Value.ToString()))));
                }
        }


        /// <summary>
        /// Парсер решеток
        /// </summary>
        private void CheckVar(string str)
        {
            if (str.IndexOf('#') == -1)
                return;
            str = str.Replace(" ", "");
            str = str.Replace(".", ",");
            List<int> nums = new List<int>();
            int num = -1;
            while (str.IndexOf('#', num + 1) != -1)
            {
                num = str.IndexOf('#', num + 1);
                nums.Add(num);
            }
            foreach (var lattice in nums)
            {
                string temp = "";
                for (int i = lattice + 1; i < str.Length; i++)
                {
                    if (char.IsDigit(str[i]) || str[i] == ',' || str[i] == '=' || str[i] == ']' || str[i] == '[' || str[i] == '#' || MyMethods.IsOperator(str[i].ToString()))
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
                        temp = temp.Replace("[", "");
                        temp = temp.Replace("]", "");
                        double value = MyMethods.Calculate(temp.Substring(temp.IndexOf('=') + 1, temp.Length - 1 - temp.IndexOf('=')));
                        Lattices.Add(new lattice(number, value));
                        temp = "";
                    }
                }
            }
        }
    }
}
