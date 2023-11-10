using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int lottery = new Random().Next(0, 99);
                int AsA = lottery / 10;
                int AsB = lottery % 10;
                Console.WriteLine("老登,告诉我你的彩票码");
                int guess;
                guess = int.Parse(Console.ReadLine());
                int A = guess / 10;
                int B = guess % 10;
                if (A == AsA && B == AsB)
                {
                    Console.WriteLine("哎嘛老登,撞大运了,奖你1000");
                }
                else if (A == AsB && B == AsA)
                {
                    Console.WriteLine("呦,俩数都对了,位置不对,真可惜啊老登,奖你500");
                }
                else if (A == AsB || B == AsA || A == AsA || B == AsB)
                {
                    Console.WriteLine("你也不行啊老登,俩数就猜对一个,给你200吧");
                }
                else
                {
                    Console.WriteLine("真废啊老登,一个都不对,你是纯贵物");
                }
                Console.WriteLine("请按下任意键继续猜;ESC退出");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}
