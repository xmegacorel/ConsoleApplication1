using System;
using System.Collections.Generic;
using System.IO;

namespace VersionCSharp
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                return;
            }

            var fileNameIn = args[0];
            var fileNameOut = args[1];
            int l = 0, d = 0, n = 0; // input param l - length of dict, d - size of dict, n - size of patters

            var file = OpenFile(fileNameIn);
            ReadConstantFromFile(file, ref l, ref d, ref n);
            var words = ReadWords(file, d);
            var patterns = ReadPatterns(file, n);

            var solver = new PatternSolver(words, l, patterns);
            
            var fileOut = new StreamWriter(fileNameOut);
            for (int i = 0; i < n; i++)
            {
                fileOut.WriteLine($"Case #1: {solver.Answers[i]}");
            }
            fileOut.Close();

        }

        private static List<string> ReadPatterns(StreamReader file, int n)
        {
            return ReadWords(file, n);
        }

        private static List<string> ReadWords(StreamReader file, int n)
        {
            var result = new List<string>();
            for (int i = 0; i < n; i++)
            {
                var line = file.ReadLine();
                if (line == null)
                {
                    throw new Exception("Cant read data from file");
                }
                
                result.Add(line);
            }
            return result;
        }

        private static void ReadConstantFromFile(StreamReader file, ref int l, ref int d, ref int n)
        {
            var line = file.ReadLine();
            var consts = line.Split(' ');
            if (consts.Length != 3)
                throw new ArgumentException("Bad L D N const");
            l = int.Parse(consts[0]);
            d = int.Parse(consts[1]);
            n = int.Parse(consts[2]);
        }

        private static StreamReader OpenFile(string fileNameIn)
        {
            return new StreamReader(fileNameIn);
        }
    }
}
