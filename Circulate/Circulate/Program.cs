using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circulate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("水仙花数");
            ShuixianHuan.shuixianhua();
            Console.WriteLine("\n学费");
            Xuefei.XueFei();
            Console.WriteLine("猴子");
            monkey.Monkey();
            Console.ReadLine();
        }
    }
}
