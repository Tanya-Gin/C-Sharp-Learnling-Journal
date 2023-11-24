using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForroF
{
    internal class LeiCheng
    {
        public static void Leicheng() 
        {
            long total=Convert.ToInt64(Console.ReadLine());
            for (long i=1,j=1;i<=total;i++) 
            {
                j *= i;
                Console.WriteLine(j);
            }
            Console.ReadLine();
        }
      
    }
}
