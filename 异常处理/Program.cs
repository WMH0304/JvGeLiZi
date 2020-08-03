using System;

namespace 异常处理
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             * unchecked 忽略异常
             * checked 异常时引发
             */
            unchecked
            {
                int a = int.MaxValue;
                int c = a + 1;
                Console.WriteLine(c);

            }

        }
    }
}
