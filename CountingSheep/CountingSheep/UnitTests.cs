using System;
using System.Runtime.Remoting.Messaging;
using Xunit;
using Xunit.Abstractions;

namespace CountingSheep
{
    public class UnitTests
    {
        private readonly ITestOutputHelper _outputHelper;

        public UnitTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        //https://code.google.com/codejam/contest/6254486/dashboard

        [Theory]
        [InlineData(11, 99)]
        [InlineData(1692, 5076)]
        [InlineData(2, 90)]
        public void aaa(int n, int answer)
        {
            BleatrixTrotterSheepAlgo a = new BleatrixTrotterSheepAlgo(n);
            Assert.Equal(a.Result, answer);
        }

       
            
    }
}