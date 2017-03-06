using Xunit;
using Xunit.Abstractions;

namespace CSharpVersion
{
    public class UnitTest
    {
        private readonly ITestOutputHelper _outputHelper;

        public UnitTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void TestData1_Successed()
        {
            var solver = new MinScalarVectorProduct(new Vector(new [] { 1, 3, - 5 }), new Vector(new [] { -2, 4, 1 }), 3);
            Assert.Equal(solver.Answer, -25);
        }

        [Fact]
        public void TestData2_Successed()
        {
            var solver = new MinScalarVectorProduct(new Vector(new[] { 1, 2, 3, 4, 5 }), new Vector(new[] { 1, 0, 1, 0, 1 }), 5);
            Assert.Equal(solver.Answer, 6);
        }
    }
}