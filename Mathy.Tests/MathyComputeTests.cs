using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Mathy.Tests
{
    public class MathyComputeTests
    {
        [Theory]
        [InlineData("-((-25.250))", 25.250)]
        [InlineData("10^20", 1E+20)]
        public void ValidInput_ShouldCompute(string input, double expected)
        {
            double actual = Mathy.Compute(input);

            Assert.Equal(expected, actual);
        }
    }
}
