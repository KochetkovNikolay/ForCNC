using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;

namespace ProgramReverse
{
    public static class MyMethods
    {
        /// <summary>
        /// Получает на входе путь к файлу, выдает имя файла
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static string ExtractName(string fullName)
        {
            string[] str = fullName.Split(new char[] { '\\' });
            return str[str.Length - 1];
        }
        static string temp = "";
        static string tempTrig = "";


        static private Queue<string> firstList = new Queue<string>(); //Сюда заносятся все значения и операторы по очереди
        static private Queue<string> input_string = new Queue<string>(); //Для обратной польской записи, дабы избавиться от скобок
        static private Stack<string> operators_stack = new Stack<string>(); //Промежуточное хранение операторов
        static private Stack<string> output_string = new Stack<string>(); //Для результата




        static public float Calculate(string expression) {
            if (expression.IndexOf('+') == - 1 && expression.IndexOf('-') == -1 &&
                expression.IndexOf('/') == -1 && expression.IndexOf('*') == -1 &&
                expression.IndexOf('(') == -1 && expression.IndexOf(')') == -1)
                return float.Parse(expression);


            string tempD = "";
            string tempL = "";

            expression = expression.Replace(" ", "");

            for (int i = 0; i < expression.Length; i++) {
                if (char.IsDigit(expression[i]) || expression[i] == ',' || (IsOperator(expression[i].ToString()) && (i == 0 || (IsOperator(expression[i - 1].ToString()) || expression[i - 1] == '(')))) {
                    tempD += expression[i];
                    if (tempL != "") {
                        firstList.Enqueue(tempL);
                        tempL = "";
                    }
                } else if (char.IsLetter(expression[i]))
                    tempL += expression[i];
                else if (IsOperator(expression[i].ToString()) || expression[i] == '(' || expression[i] == ')') {
                    if (tempD != "") {
                        if (tempD != "") {
                            firstList.Enqueue(tempD);
                            tempD = "";
                        }
                        if (tempL != "") {
                            firstList.Enqueue(tempL);
                            tempL = "";
                        }
                    }
                    firstList.Enqueue(expression[i].ToString());
                }
            }
            if (tempD != "") {
                firstList.Enqueue(tempD);
                tempD = "";
            }
            if (tempL != "") {
                firstList.Enqueue(tempL);
                tempL = "";
            }

            TranslationInPolish(firstList);

            while (input_string.Any()) {
                if (IsDigit(input_string.Peek())) {
                    output_string.Push(input_string.Dequeue());
                } else {
                    if (input_string.Peek() == "COS") {
                        input_string.Dequeue();
                        output_string.Push(Math.Cos(double.Parse(output_string.Pop())).ToString());
                        continue;
                    }
                    if (input_string.Peek() == "SIN") {
                        input_string.Dequeue();
                        output_string.Push(Math.Sin(double.Parse(output_string.Pop())).ToString());
                        continue;
                    }
                    if (output_string.Count == 1) {
                        string temp = output_string.Pop();
                        output_string.Push("0");
                        output_string.Push(temp);
                    }
                    output_string.Push(Calc(output_string.Pop(), output_string.Pop(), input_string.Dequeue()));
                }
            }
            return float.Parse(output_string.Pop());
        }

        static private string Calc(string operand1, string operand2, string type) {
            switch (type) {
                case "+":
                    return (double.Parse(operand2) + double.Parse(operand1)).ToString();
                case "-":
                    return (double.Parse(operand2) - double.Parse(operand1)).ToString();
                case "*":
                    return (double.Parse(operand2) * double.Parse(operand1)).ToString();
                case "/":
                    return (double.Parse(operand2) / double.Parse(operand1)).ToString();
                case "COS":
                    return Math.Cos(double.Parse(operand2)).ToString();
                case "SIN":
                    return Math.Sin(double.Parse(operand2)).ToString();
                default:
                    break;
            }
            return "";
        }
        static private bool IsDigit(string str) {
            try {
                double.Parse(str);
                return true;
            } catch (Exception) {
                return false;
            }
        }


        static private void TranslationInPolish(Queue<string> expression) {
            input_string.Clear();
            operators_stack.Clear();

            while (firstList.Any()) {  //Если символ это цифра или точка
                if (IsDigit(firstList.Peek())) {
                    input_string.Enqueue(firstList.Dequeue());
                    continue;
                }
                if (IsOperator(firstList.Peek())) {  //Если символ это оператор

                    if (operators_stack.Count == 0) {  //Если это первый встреченный оператор
                        operators_stack.Push(firstList.Dequeue());  //то положим его в стек
                        continue;
                    } else {                                                        //Если операторы в стеке уже присутствуют,
                        if (GetPriority(operators_stack.Peek()) < GetPriority(firstList.Peek())) {  //то помещаем символ в стек, если его приоритет выше последнего в стеке
                            operators_stack.Push(firstList.Dequeue());
                            continue;
                        }
                    }
                    if (operators_stack.Count != 0) {
                        try {
                            while (GetPriority(operators_stack.Peek()) >= GetPriority(firstList.Peek()))  //Пока приоритет операторов в стеке выше проверяемого
                                input_string.Enqueue(operators_stack.Pop());  //помещаем их в выходной стек
                        } catch { }
                        if (operators_stack.Count == 0) {  //Если стек операторов опустел
                            operators_stack.Push(firstList.Dequeue());  //помещаем проверяемый оператор в стек
                            continue;
                        } else if (operators_stack.Count != 0) {
                            if (GetPriority(operators_stack.Peek()) < GetPriority(firstList.Peek())) {
                                operators_stack.Push(firstList.Dequeue());
                                continue;
                            }
                        }
                    }
                }
                if (firstList.Peek() == "(") {  //Если символ это открывающая скобка, то всегда помещаем ее в стек

                    operators_stack.Push(firstList.Dequeue());
                    continue;
                }

                if (firstList.Peek() == ")") {  //Если символ это закрывающая скобка

                    while (operators_stack.Peek() != "(") {   //то заносим операторы в выходной стек, пока не обнаружится открывающая скобка
                        input_string.Enqueue(operators_stack.Pop());
                    }
                    operators_stack.Pop();  //Удаляем открывающую скобку
                    firstList.Dequeue();
                    continue;
                }
            }
            while (operators_stack.Count != 0)
                input_string.Enqueue(operators_stack.Pop());



        }



        static public bool IsOperator(string op) {
            if (op == "+" || op == "-" || op == "*" || op == "/" || op == "COS" || op == "SIN")
                return true;
            return false;
        }

        static private int GetPriority(string op) {
            switch (op) {
                case "+":
                    return 2;
                case "-":
                    return 2;
                case "*":
                    return 3;
                case "/":
                    return 3;
                case "(":
                    return 1;
                case "COS":
                    return 4;
                case "SIN":
                    return 4;
                default:
                    return 0;
            }
        }
    }
}
