using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTranslate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int MilkPrice = 65;
            int PortPrice = 16;
            int TotalPrice;
            TotalPrice = MilkPrice * 2 + PortPrice * 3;
            Console.WriteLine(TotalPrice);
            double Price = TotalPrice * 0.88;
            Console.WriteLine(Price);
            Console.ReadLine();
            
        }
        
    }
}
