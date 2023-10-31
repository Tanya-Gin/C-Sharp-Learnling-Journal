using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrithdayYear
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入你的出生年份");
            int brithdayyear = int.Parse(Console.ReadLine());
            Console.WriteLine("请输入你的第n个生日");
            int n = int.Parse(Console.ReadLine());
            int c;
                c = brithdayyear + n;
            Console.WriteLine(c);
            Console.ReadLine();
        }
    }
}
