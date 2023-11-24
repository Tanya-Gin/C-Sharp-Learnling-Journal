using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForroF
{
    internal class NineOne
    {
        public static void Nineone()
        {
            for (int H = 1; H <= 9; H++)
            {
                for (int L = 1; L <= H; L++)
                {
                    Console.Write($"{L}*{H}={L * H}\t");
                }
                Console.WriteLine();
            } 
            Console.ReadLine();
        }
    }
}
