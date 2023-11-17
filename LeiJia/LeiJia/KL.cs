using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeiJia
{
    internal class KL
    {
        static public void KLS()
        {
            while (true)
            {
                Console.WriteLine("累乘\n请输入整数:");
                int n = Convert.ToInt32(Console.ReadLine());
                int i = 1, j = 1;
                while (i <= n)
                {
                    j *= i;
                    i++;
                }
                Console.WriteLine(j);
                Console.WriteLine("请按任意键继续;按ESC退出");
                var key= Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                break;
            }
            while (true) 
            {
                Console.WriteLine("累加\n请输入整数:");
                int n= Convert.ToInt32(Console.ReadLine());
                int i = 1, j = 0;
                while (i <= n) 
                {
                    j = j + i;
                    i++;
                }
                Console.WriteLine(j);
                Console.WriteLine("请按任意键继续;按ESC退出");
                var key= Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    break;
            }
            while (true)
            {
                Console.WriteLine("累乘2\n请输入整数:");
                double n = Convert.ToInt32(Console.ReadLine());
                double i = 1, j = 1;
                while (i <= n)
                {
                    j *= n;
                    i++;
                }
                Console.WriteLine(j);
                Console.WriteLine("请按任意键继续;按ESC退出");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    break;
            }
            Console.WriteLine("开始进行第二阶段...");
            while (true)
            {
                Console.WriteLine("累乘\n请输入整数:");
                int n = Convert.ToInt32(Console.ReadLine());
                int i = 1, j = 1;
                do 
                {
                    j *= i;
                    i++;
                }
                while (i <= n);
                Console.WriteLine(j);
                Console.WriteLine("请按任意键继续;按ESC退出");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    break;
            }
            while (true)
            {
                Console.WriteLine("累加\n请输入整数:");
                int n = Convert.ToInt32(Console.ReadLine());
                int i = 1, j = 0;
                do
                {
                    j = j + i;
                    i++;
                }
                while (i <= n);
                Console.WriteLine(j);
                Console.WriteLine("请按任意键继续;按ESC退出");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    break;
            }
            while (true)
            {
                Console.WriteLine("累乘2\n请输入整数:");
                double n = Convert.ToInt32(Console.ReadLine());
                double i = 1, j = 1;
                do
                {
                    j *= n;
                    i++;
                }
                while (i <= n);
                Console.WriteLine(j);
                Console.WriteLine("请按任意键继续;按ESC退出");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                    break;
            }
        }
    }
}
