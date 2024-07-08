using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitDemo.App
{
    public class Calculator
    {
        private ICalculatorService _calculatorService;

        public Calculator(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }
        public int Add(int x, int y)
        {
            return _calculatorService.Add(x, y);
        }

        public int Divide(int numberToBeDivided, int dividerNumber)
        {
            return _calculatorService.Divide(numberToBeDivided, dividerNumber);
        }

        public int Multiple(int x, int y)
        {
            return _calculatorService.Multiple(x, y);
        }
    }
}
