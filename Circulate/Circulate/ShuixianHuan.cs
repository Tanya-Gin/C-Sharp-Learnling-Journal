using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circulate
{
    internal class ShuixianHuan
    {
        public static void shuixianhua()
        {
            int conter = 1;
            int baiWei, shiWei, geWei;
            int answer;
            for (; conter < 1000; conter++)
            {
               baiWei = conter / 100;
               shiWei = (conter % 100) / 10;
               geWei = conter % 10;
                //153=1^3+5^3+3^3
                answer = (int)(Math.Pow(baiWei, 3) + Math.Pow(shiWei, 3) + Math.Pow(geWei, 3));
                if (conter == answer)
                {
                    Console.Write($"{conter} ");
                }
            }
        }
    }
}

