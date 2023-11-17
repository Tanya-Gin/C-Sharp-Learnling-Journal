using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeiJia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            while (i <= 100) 
            {
                Console.WriteLine("我爱C#");
                i++;
            }
            Console.WriteLine("执行完毕..."+i);
            Console.ReadLine();
            KL.KLS();
        }
        
    }
}
