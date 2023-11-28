using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入列表长度");
            int arrLong=Convert.ToInt32(Console.ReadLine());
            while(true) 
            {
                if (arrLong < 0)
                {
                    Console.WriteLine($"您输入的列表长度为{arrLong}不合法");
                    arrLong = Convert.ToInt32(Console.ReadLine());
                }
                if (arrLong > 0)
                {
                    Console.WriteLine($"您输入的长度{arrLong}合法");
                    break;
                }
                else { Console.WriteLine("你输你M呢？"); }
            }
            int[] arr = new int[arrLong];
            for (int i=0;i<arr.Length;i++) 
            {
                Console.WriteLine($"请依次输入{arrLong}个值的第{i+1}个值:");
                arr[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("您的值已被保存");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
            int lower = arr[0], index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (lower > arr[i]) 
                {
                    lower= arr[i];
                    index = i;
                }
            }
            Console.WriteLine($"最小值为{lower},最小值索引为{index}");
            Console.ReadLine();
        }
    }
}
