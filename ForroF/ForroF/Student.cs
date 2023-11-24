using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForroF
{
    internal class Student
    {
        public static void Point() 
        {
            int num = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("您输入的学生人数为:"+num);
            while (num <= 0)
            {
                Console.WriteLine("你输你M呢？啊，你输你M呢。");
            }
        }
    }
}
