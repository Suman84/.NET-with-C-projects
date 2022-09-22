using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class CalculatorTests
    {
        Calculator _calc;

        public CalculatorTests()
        {
            _calc = new Calculator();
        }

        [Fact]
        public void shouldaddtwonumbers()
        {
            int res = _calc.Add(5, 3);
            Assert.Equal(res, 8);
        }


        [Fact]
        public void shouldaddtwonumbers_2()
        {
            int res = _calc.Add(1, 1);
            Assert.Equal(res, 2);
        }


        [Fact]
        public void shouldsubstracttwonumbers()
        {
            int res = _calc.Sub(5, 3);
            Assert.Equal(res, 2);
        }
    }
}
