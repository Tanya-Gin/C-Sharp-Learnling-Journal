using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MoneyChange
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("(1)定义变量int,long,string,bool,float,char");
            int first = 1;
            Console.WriteLine($"整数类型变量{first}");
            long second = 1145141919810;
            Console.WriteLine($"浮点数类型变量{second}");
            string third = "Hello World!";
            Console.WriteLine($"字符串类型变量{third}");
            bool fourthly = true;
            Console.WriteLine($"布尔类型变量{fourthly}");
            float fifth = 114.514f;
            Console.WriteLine($"浮点数类型变量{fifth}");
            char sixth = 'C';
            Console.WriteLine($"字符类型变量{sixth}");
            while (true)
            {
                Console.WriteLine("(2)自适应输入值,将输入值转换为short||int||double");
                Console.WriteLine("请输入数值:");
                string taping = Console.ReadLine();
                if (Int16.TryParse(taping, out short number1))
                {
                    Console.WriteLine($"将输入值转化为Short值为;{number1}");
                }
                else if (Int32.TryParse(taping, out int number2))
                {
                    Console.WriteLine($"将输入的值转化为int值为:{number2}");
                }
                else if (double.TryParse(taping, out double number3))
                {
                    Console.WriteLine($"将输入的值转化为double值为:{number3}");
                }
                else
                {
                    Console.WriteLine("输入值无效！");
                }
                Console.WriteLine("请按下任意键继续;ESC退出进行下一环节");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            Console.WriteLine("(3)整钱兑零钱");
            while (true)
            {
                double rmb;
                Console.Write("请输入想要兑换的金额(元):");
                if (!double.TryParse(Console.ReadLine(), out rmb))
                {
                    Console.WriteLine("输入值无效！");
                    continue;
                }
                double jiao;
                jiao = rmb * 10;
                double rmbZ, rmbY, wjZ, wjZY, ljZ, ljZY, yjZY, yjZ;
                rmbY = jiao % 10;
                rmbZ = (jiao - rmbY) / 10;
                wjZY = rmbY % 5;
                wjZ = (rmbY - wjZY) / 5;
                ljZY = wjZY % 2;
                ljZ = (wjZY - ljZY) / 2;
                yjZY = ljZY % 2;
                yjZ = (ljZY - yjZY) / 2;
                Console.WriteLine($"您所输入的金额对应的1元张数是{rmbZ}");
                Console.WriteLine($"您所输入的金额对应的5角张数是{wjZ}");
                Console.WriteLine($"您所输入的金额对应的2角张数是{ljZ}");
                Console.WriteLine($"您所输入的金额对应的1角张数是{yjZ}");
                Console.WriteLine("请按下任意键继续;ESC退出");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}

