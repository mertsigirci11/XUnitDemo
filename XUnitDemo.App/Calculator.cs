using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitDemo.App
{
    public class Calculator : ICalculatorService
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
    }
}
