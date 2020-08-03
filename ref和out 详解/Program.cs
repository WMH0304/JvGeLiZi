using System;

namespace ref和out_详解
{
    class Program
    {
        /*
         * 异同
         * 同：
         * 1.使用后都能改变调用函数的值
         * 异：
         * 1.ref 再调用前必须先初始化可以理解成传值，传值参数传递的是调用参数的一份拷贝
         * 2.out 在调用后在目标函数中必须初始化，而传址参数传递的是调用参数的内存地址，
         * 该参数在方法内外指向的是同一个存储位置。 由于公用一个地址，所以目标值改变，调用函数值也相应地发生改变
         */

        /// <summary>
        /// 有进有出
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        static public  int RefValue(int i, ref int j)
        {
            /*
             *  ref j 标志了参数是有进有出， 在调用函数中必须要为 ref j 参数赋值（初始化）
             */
            int k = j;
            j = 222;
            return i + k;
        }

        /// <summary>
        /// 只出不进
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        static public int  OutValue(int i, out int j)
        {
            /*
             * out j 标志了参数只传出 在目标函数例使用时必须要为 out j 赋值
             * 
             */
            j = 222;
            return i + j;
        }

        public  static void Main()
        {
           
          
            cmdRef();
            cmdOut();
            Console.ReadKey();
        }
        private static void cmdRef()
        {
            int m = 0;
            /*
             * ref m 参数在传递前必须先初始化，
             * 否则编译不通过，另外 目标方法中 ref m 给m赋值时，会改变调用函数中的值
             * 
             */
            Console.WriteLine(RefValue(1, ref m).ToString()); ;
            Console.WriteLine(m.ToString());
        }

        private static void cmdOut()
        {
            int m;
            Console.WriteLine(OutValue(1, out m).ToString());
            Console.WriteLine(m.ToString());
        }
    }
}
