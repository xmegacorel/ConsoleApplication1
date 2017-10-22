using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CruiseControlHorse
{
    class Program
    {
        static void Main(string[] args)
        {
            AnneHorseSolver solver;
            var fileOutput = File.OpenWrite(@".\output\result1.text");
            var file = File.OpenText(@".\tests\A-small-practice.in-1.txt");
            var amountTest = int.Parse(file.ReadLine());
            for (int i = 0; i < amountTest; i++)
            {
                solver = ReadData(file);
                byte[] answer;
                if (Math.Round(solver.Result) == solver.Result)
                {
                    answer = Encoding.ASCII.GetBytes($"Case #{i + 1}: {solver.Result}\n".Replace(",", "."));
                }
                else
                {
                    answer = Encoding.ASCII.GetBytes($"Case #{i + 1}: {solver.Result:0.000000}\n".Replace(",", "."));
                }
                    
                fileOutput.Write(answer, 0, answer.Length);
            }
            fileOutput.Close();
        }

        private static AnneHorseSolver ReadData(StreamReader file)
        {
            var firstLineArray = file.ReadLine().Split(" ");
            double overallDistanse = double.Parse(firstLineArray[0]);
            int horses = int.Parse(firstLineArray[1]);

            var result = new List<HorseInfo>();
            for (int i = 0; i < horses; i++)
            {
                var itemArray = file.ReadLine().Split(" ");
                result.Add(new HorseInfo()
                {
                    Distanse = double.Parse(itemArray[0]),
                    Speed = double.Parse(itemArray[1])
                });
            }

            return new AnneHorseSolver(overallDistanse, result);
        }
    }
}
