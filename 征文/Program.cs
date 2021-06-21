using System;
using System.Threading;

namespace 征文
{
    class Program
    {
        static void Main(string[] args)
        {
            float w, x, y, z;
            for (y = 1.5f; y > -1.5f; y -= 0.1f)
            {
                for (x = -1.5f; x < 1.5f; x += 0.05f)
                {
                    z = x * x + y * y - 1;
                    w = z * z * z - x * x * y * y * y;
                    Console.Write(w <= 0.0f ? ".;-=+*#%@"[(int)(w * -8.0f)] : ' ');
                }
                Thread.Sleep(100);
                Console.WriteLine();
            }
           
            string[] str = { "喜", "欢", "你", "，", "是", "我", "独", "家", "的", "记", "忆", "。", "", "" };



            Console.Write("               ");
            for (int i = 0; i < str.Length; i++)
            {
                Console.Write(str[i]);
                Thread.Sleep(700);
            }
            Console.ReadKey();
            /*float x, y, a;
            for (y = 1.5f; y > -1.5f; y -= 0.1f)
            {
                for (x = -1.5f; x < 1.5f; x += 0.05f)
                {
                    a = x * x + y * y - 1;
                    Console.Write(a * a * a - x * x * y * y * y <= 0.0f ? "*" : " "); ;
                }
                Console.WriteLine();
            }
            Console.ReadKey();
            */
        }
    }
}
