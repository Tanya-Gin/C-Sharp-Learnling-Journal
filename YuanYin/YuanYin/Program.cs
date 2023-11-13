using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuanYin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("元音查询器");
            while (true)
            {
                Console.WriteLine("请输入字母:");
                string guree;
                guree = Console.ReadLine();
                switch (guree)
                {
                    case "a":
                    case "e":
                    case "i":
                    case "o":
                    case "u":
                        Console.WriteLine($"对,你输入的{guree}是元音");
                        break;
                    default:
                        Console.WriteLine($"你看看你看看,那{guree}能是元音吗？");
                        break;
                }
                Console.WriteLine("请按下任意键继续;ESC退出");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
                    while (true)
                    {
                        Console.Write("请输入你的百分制分数: ");
                        int score = Convert.ToInt32(Console.ReadLine());

                        if (score >= 90)
                        {
                            Console.WriteLine("等级: A");
                        }
                        else if (score >= 80)
                        {
                            Console.WriteLine("等级: B");
                        }
                        else if (score >= 70)
                        {
                            Console.WriteLine("等级: C");
                        }
                        else if (score >= 60)
                        {
                            Console.WriteLine("等级: D");
                        }
                        else
                        {
                            Console.WriteLine("等级: F");
                        }
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
    