using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clemm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double a, b, c, d, e, f;
            Console.Write("请输入a的值:");
            a = double.Parse(Console.ReadLine());
            Console.Write("请输入b的值:");
            b = double.Parse(Console.ReadLine());
            Console.Write("请输入c的值:");
            c = double.Parse(Console.ReadLine());
            Console.Write("请输入d的值:");
            d = double.Parse(Console.ReadLine());
            Console.Write("请输入e的值:");
            e = double.Parse(Console.ReadLine());
            Console.Write("请输入f的值:");
            f = double.Parse(Console.ReadLine());
            double X, Y;
            double Fenmu;
            Fenmu = (a * d - b * c);
            if (Fenmu == 0)
            {
                Console.Write("无解");
            }
            else
            {
                X = (e * d - b * f) / Fenmu;
                Y = (a * f - e * c) / Fenmu;
                Console.WriteLine($"您的X为:{X}");
                Console.WriteLine($"您的Y为:{Y}");
                Console.ReadLine();
            }
        }
    }
}
