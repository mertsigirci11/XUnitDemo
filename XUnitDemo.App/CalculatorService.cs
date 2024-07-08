﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnitDemo.App
{
    public class CalculatorService : ICalculatorService
    {
        public int Add(int x, int y)
        {
            if (x == 0 || y == 0)
            {
                return 0;
            }
            return x + y;
        }
    }
}