using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperAlg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the value of i:");
            int i = Convert.ToInt32(Console.ReadLine());
            double result = Factorial(i);
            double result1 = Fibonacci(i);
            double result2 = CalculateSum(i);
            double result3 = CalculateSeries(i);
            Console.WriteLine($"The factorial is: {result}");
            Console.WriteLine($"The fibonacci is: {result1}");
            Console.WriteLine($"The sum is: {result2}");
            Console.WriteLine($"The result of the series for i = {i} is {result3}");
        }
        static int Factorial(int i)
        {
            if (i == 0) return 1;else return i*Factorial(i-1);
        }
        static int Fibonacci(int i)
        {
            if (i <= 1) return i;else return Fibonacci(i - 1) + Fibonacci(i - 2);
        }
        static double CalculateSum(int i)
        {
            if (i == 0) return 1;
            else return 1 / i + CalculateSum(i - 1);
        }
        static double CalculateSeries(int i)
        {
            if (i == 1)
            {
                return 1.0 / 3;
            }
            else
            {
                return i / (2.0 * i + 1) + CalculateSeries(i - 1);
            }
        }
    }
}
