using XUnitDemo.Console;

namespace XUnitDemo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("aaa");
            var calc = new Calculator();
            System.Console.WriteLine(calc.Add(20, 30).ToString());
        }
    }
}