using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circulate
{
    internal class Xuefei
    {
        public static void XueFei() 
        {
            int i = 1;
            double totalMoney = 10000.0;
            int Year = 10;
            while (i <= Year)
            {
                totalMoney *= 1 + 0.05;
                i++;
            }
            Console.WriteLine(totalMoney);
        }
    }
}
