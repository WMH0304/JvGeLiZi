using System;
using System.Threading.Tasks;

namespace async_await
{
    class Program
    {
        /*
         * 本质就是对多线程的一种简化手段
         * async 标志方法为异步线程
         * await 标志异步方法的执行入口
         * ~~~
         * 就是一个语法糖~~~~~
         * 
         */
        static void Main(string[] args)
        {
            claAsync();
            Console.WriteLine("Hello World!");
            
        }

        public static int t()
        {
            int j = 0;
            for (int i = 0; i < 1000; i++)
            {
              j=  i + j;
            }

            return j;
        }
        async static void claAsync()
        {
            int a = await Task.Run(new Func<int>(t));//异步委托
            int b = await Task.Run(() => t());
            Console.WriteLine(b);
            Console.WriteLine(a);

        }

    }
}
