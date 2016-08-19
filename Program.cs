using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите выражение");
            String newLine = Console.ReadLine();
            if (existLetter(newLine))
                Console.WriteLine(CountThis(Treatment(newLine)));
            else
                Console.WriteLine("Некорректный ввод, строка содержит недопустимое выражение.");
            Console.ReadLine();
        }
        public static bool existLetter(String line)
        {
            bool flag=true;
            for (int i = 0; i < line.Length&&flag; i++)
                if (line[i] == '*' || line[i] == '-' || line[i] == '+' || line[i] == '/' || (line[i] >= '0' && line[i] <= '9')
                    ||line[i]=='.'||line[i]==' '||line[i]==','||line[i]==')'||line[i]=='(')
                    flag = true;
                else
                {
                    flag = false;
                }


            return flag;
        }
        
       public static double CountThis(string partOfLine)
       {
            double value = Number(partOfLine, out partOfLine);
            double variable;
            while (partOfLine.Length > 0)
            {
                    string newPartOfLine=partOfLine;
                    variable = Number(partOfLine.Substring(1, partOfLine.Length - 1), out partOfLine);
                    switch (newPartOfLine[0])
                    {
                        case '-': value -= variable; break;
                        case '+': value += variable; break;
                        case '*': value *= variable; break;
                    }
                    if (variable == 0 && newPartOfLine[0] == '/')
                    { 
                        Console.WriteLine("Делить на ноль нельзя!");
                        return -1;
                    }

                    else
                        if (newPartOfLine[0] == '/')
                            value /= variable;
                

            }                
            return value;
        }

        public static double Number(string str, out string strOut)
        {
            double output = 0.0;
            double discharge = 10.0;
            int sign = 1;
            int index = 0;
            int i = 0;
            while (index == 0)
            {
                bool flag = false;
                if (str[i] == '-')
                {
                    sign = -1;
                    i++;
                }
               
                while (i < str.Length && !flag)
                {
                    if (str[i] >= '0' && str[i] <= '9')
                    {
                        output = output * 10.0 + (str[i] - '0');
                        i++;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                if (i < str.Length&&str[i] == ',')
                {
                    flag = false;
                    i++;
                    while (i < str.Length && !flag)
                    {
                        if (str[i] >= '0' && str[i] <= '9')
                        {
                            output += (str[i] - '0') / discharge;
                            discharge *= 10.0;
                            i++;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                }
                index = i;
            }
            strOut = str.Substring(i);
            return sign * output;


        }
        public static string Treatment(String line)
        {
            line = line.Replace(" ", string.Empty);
            line = line.Replace(".", ",");
            double value;
            while(line.LastIndexOf("(") != -1)
            {
                value = CountThis(line.Substring(line.LastIndexOf("(") + 1, line.Length - line.LastIndexOf("(") -
                        (line.Length - line.IndexOf(")", line.LastIndexOf("("))) - 1));
                Math.Round(value, 2);
                line = line.Replace(line.Substring(line.LastIndexOf("("), line.Length - line.LastIndexOf("(") -
                         (line.Length - line.IndexOf(")", line.LastIndexOf("("))) + 1), value.ToString());

            }
            return line;

        }

    }
                

}
        