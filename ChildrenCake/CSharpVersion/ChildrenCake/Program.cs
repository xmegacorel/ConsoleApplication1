using System;
using System.Collections.Generic;
using System.IO;

namespace ChildrenCake
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = File.OpenText(@"C:\Users\Sergey\AppData\Local\Temp\A-small-practice.in.txt");

            var str = sw.ReadLine();
            int amountTest = int.Parse(str);

            for (int i = 0; i < amountTest; i++)
            {
                var info = ReadTest(sw);
                MatrixSolver ms = new MatrixSolver(info.Row, info.Column, info.Dict);
                Console.WriteLine(info.RawText);
                Console.WriteLine(ms.Result);
                Console.WriteLine("/******************************/");

                if (i == 10)
                    break;
            }
        }

        private static Info ReadTest(StreamReader sw)
        {
            var result = new Info();

            var dimention = sw.ReadLine();
            var t = dimention.Split(' ');
            result.Row = int.Parse(t[0]);
            result.Column = int.Parse(t[1]);
            result.RawText += dimention + "\n";
            /////
            int index = 1;
            for (int i = 0; i < result.Row; i++)
            {
                var rawRow = sw.ReadLine();
                result.RawText += rawRow + "\n";
                foreach (var character in rawRow)
                {
                    if (character != '?')
                    {
                        result.Dict.Add(index, character);
                    }
                    index++;
                }
            }

            return result;
        }

        class Info
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public Dictionary<int, char> Dict { get; set; } = new Dictionary<int, char>();
            public string RawText { get; set; }
        }
    }
}
