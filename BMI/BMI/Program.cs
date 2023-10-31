using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double height;
            double weight;
            Console.Write("请输入您的身高(米)；");
            height = double.Parse(Console.ReadLine());
            Console.Write("请输入您的体重(千克)：");
            weight = double.Parse(Console.ReadLine());
            double bmi = weight / (height * height);
            Console.WriteLine($"您的身高为；{height}");
            Console.WriteLine($"您的体重为；{weight}");
            Console.WriteLine($"您的bmi指数为；{bmi}");
            if (bmi < 18.5)
                Console.WriteLine("体重过轻");
            else if (bmi >= 18.5 && bmi < 24.9)
                Console.WriteLine("体重正常");
            else if (bmi >= 24.9 && bmi < 29.9)
                Console.WriteLine("体重过重");
            else
                Console.WriteLine("肥胖");
            Console.ReadLine();
        }
    }
}
