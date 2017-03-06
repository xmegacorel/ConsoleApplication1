using Xunit;
using VersionCSharp;
using System.Collections.Generic;
using Should;
using System.Text.RegularExpressions;

namespace UnitTestCSharp
{
    public class UnitTest
    {


        [Fact]
        public void Test()
        {
            var dictWords = new List<string>() { "abc", "bca", "dac", "dbc", "cba" };
            var patterns = new List<string>() { "(ab)(bc)(ca)", "abc", "(abc)(abc)(abc)", "(zyx)bc" };
            var solver = new PatternSolver(dictWords, 3, patterns);

            var answer = solver.Answers;

            answer.Count.ShouldEqual(patterns.Count);
            answer[0].ShouldEqual(2);
            answer[1].ShouldEqual(1);
            answer[2].ShouldEqual(3);
            answer[3].ShouldEqual(0);
        }
            
        [Fact]
        public void MyTestMethod()
        {
            var regexpExpression = "([(][a-z]*[)])|[a-z]*";

            var regex = new Regex(regexpExpression);
            var match = regex.Match("aaa(asd)dasd(s)s(f)");

            do
            {
                if (match.Success)
                {

                }
                else
                {
                    break;
                }

                match = match.NextMatch();
            }
            while (true);
        }

    }
}
