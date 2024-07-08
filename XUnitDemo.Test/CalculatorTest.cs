using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using XUnitDemo.App;

namespace XUnitDemo.Test
{
    public class CalculatorTest
    {
        public Calculator calc;

        public CalculatorTest()
        {
            calc = new Calculator();
        }

        /*
         -->If test method doesn't take any parameter, [Fact] attribute is used.
         
         -->If test method take parameter(s), [Theory] attribute is used.
         And to specify value(s) of parameter(s), [InlineData] attribute is used.
        */

        [Fact]
        public void Add_ReturnTrueResult_Test()
        {
            //Arrange
            int firstNumber = 8, secondNumber = 5;

            //Act
            var result = calc.Add(firstNumber, secondNumber);

            //Assert
            Assert.Equal(firstNumber + secondNumber, result);

            /*              --Equal--
                //Arrange
                int num1 = 6;
                int num2 = 10;

                //Act
                var total = calc.Add(num1, num2);

                //Assert
                Assert.Equal<int>(num1 + num2, total);
            */

            /*              --Contains--
                //Arrange
                var names = new List<string>{ "Dixon","Bogdanovic","Datome","Vesely","Udoh"};
                
                //Assert
                Assert.Contains("Dragic", "Goran Dragic");
                Assert.DoesNotContain("Bogdan", "Marko Guduric");
                Assert.Contains<string>("Datome", names);
            */

            /*              --True/False--
                //Assert          
                Assert.True(5 > 2);
                Assert.False("".GetType() == typeof(int));
            */

            /*              --Match--
                //Arrange
                var regex = "^dog";

                //Assert
                Assert.Matches(regex, "dogfight");
             */

            /*              --StartsWith/EndsWith--
                //Assert
                Assert.StartsWith("Fenerbahce", "Fenerbahce Basketball");
                Assert.EndsWith("Efes", "Anadolu Efes");
             */

            /*              --Empty--
                //Arrange
                var list = new List<string>();
                
                //Assert
                Assert.Empty(list);
             */

            /*              --InRange/NotInRange--
                //It checks the number is whether in range or not
                
                //Assert
                Assert.InRange(8, 4, 10);
                Assert.NotInRange(8, 4, 7);
             */

            /*              --Single--
                //If the parameter has single value it returns true else it returns false.
                
                //Arrange
                var list = new List<string>() { "Obradovic" };
                
                //Assert
                Assert.Single(list);
             */

            /*              --IsType--
                //Assert
                Assert.IsType(typeof(int), 10); or
                Assert.IsType<string>("Euroleague");
             */

            /*              --IsAssignableFrom--
                //Assert
                Assert.IsAssignableFrom<IEnumerable<string>>(new List<string> { "a", "b" });
             */

            /*              --Null--
                //Arrange
                string variable = null;
                
                //Assert
                Assert.Null(variable);
                Assert.NotNull(new List<string>() { "Sloukas", "Kalinic" });
             */
        }

        [Theory]
        [InlineData(10, 15, 25)]
        [InlineData(11, 5, 16)]
        public void Add_ExceptZeroValues_Test(int num1, int num2, int total)
        {
            //This test method works twice because "InlineData" attribute written twice. So that the method test their values.

            Assert.Equal(total, calc.Add(num1, num2));
        }

        [Theory]
        [InlineData(0, 45, 0)]
        [InlineData(42, 0, 0)]
        public void Add_ForZeroValues_Test(int num1, int num2, int total)
        {
            //This test method works twice because "InlineData" attribute written twice. So that the method test their values.

            Assert.Equal(total, calc.Add(num1, num2));
        }
    }
}
