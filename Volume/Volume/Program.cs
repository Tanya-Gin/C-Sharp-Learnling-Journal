using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Volume
{
    internal class Program
    {
        public const double PI = 3.1415926;
        static void Main(string[] args)
        {
            double r;
            Console.Write("请输入半径;");
            r= double.Parse(Console.ReadLine());
            double h;
            Console.Write("请输入高:");
            h= double.Parse(Console.ReadLine());
            double SSercal = r * r * PI;
            Console.WriteLine($"您的底面积为;{SSercal}");
            double Volume = SSercal * h;
            Console.WriteLine($"您的体积为;{Volume}");
            Console.ReadLine();

        }
    }
}
