using AoCHelper;
using System;
using Xunit;

namespace AoC_2015.Test
{
    public class SolutionTests
    {
        [Theory]
        [InlineData(typeof(Day_01), "232", "1783")]
        [InlineData(typeof(Day_02), "1586300", "3737498")]
        [InlineData(typeof(Day_03), "2565", "2639")]
        [InlineData(typeof(Day_04), "346386", "9958218")]
        [InlineData(typeof(Day_05), "236", "51")]
        public void Test(Type type, string sol1, string sol2)
        {
            var instance = Activator.CreateInstance(type) as BaseDay;

            Assert.Equal(sol1, instance.Solve_1());
            Assert.Equal(sol2, instance.Solve_2());
        }
    }
}
