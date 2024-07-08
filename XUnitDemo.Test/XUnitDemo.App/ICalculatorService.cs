using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitDemo.App
{
    public interface ICalculatorService
    {
        public int Add(int x, int y);

        public int Multiple(int x, int y);

        public int Divide(int numberToBeDivided, int dividerNumber);
    }
}
