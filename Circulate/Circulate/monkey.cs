using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circulate
{
    internal class monkey
    {
        public static void Monkey() 
        {
            int i = 1;
            for (int conter = 1; conter <= 10; conter++) 
            {
                int ans = (i + 1) * 2;
                i = (i + 1) * 2;
                Console.WriteLine(ans);
            }
        }
    }
}
