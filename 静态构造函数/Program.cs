using System;

namespace 静态构造函数
{
   
    /*
     * 从输出结果可以看出，他的执行循序并不影响静态方法的执行
     * 也就是说，静态构造函数是在所有实例方法之前执行的，在编译的过程，
     * 编译器会对所有的静态函数进行一个预处理的操作。
     * 优先执行。
     * 
     */
    class static_structure
    {
       
        protected int num{ get; set; }//一个存储a 的属性
       /*
        * 静态构造函数，在实例引用前执行
        * 
        */

        public void test(int b)
        {
            num = b + 1;
            Console.Out.WriteLine("引用执行{0}", num * 7);
        }


        /*
         * 静态构造函数不使用访问修饰符 public private protected ,
         * 没有参数且一个类只有一个只有一个且只在程序引用方法（实例方法）前执行且只执行一次，
         * 就意味这他不能被调用，为什么只执行一次呢？由于他是静态方法，那就意味着他是一个全局方法
         * 
         * 
         */

        static static_structure()
        {
            Console.WriteLine("还没实例化之前执行。");
        }
     
        public static_structure(int a)
        {
            num = a;
            Console.Out.WriteLine("实例引用输出 {0}", num);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            static_structure static_ = new static_structure(7);
            static_.test(2);
            // static_structure static_1 = new static_structure();
            /*
             * 需要有 和a 对应的参数，为什么不能访问 那个静态构造函数呢？
             * 
             */
        }
    }


}
