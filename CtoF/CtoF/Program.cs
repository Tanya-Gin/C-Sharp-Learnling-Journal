using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtoF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double Celsius;
            Console.Write("请输入温度(℃):");
            Celsius = double.Parse(Console.ReadLine());
            double Fahrenheit = (9 / 5) * Celsius + 32;
            Console.WriteLine($"您输入的℃温度为:{Celsius}");
            Console.WriteLine($"您输入的℃对应的℉为:{Fahrenheit}");
            Console.ReadLine();
        }
       
    }

}
