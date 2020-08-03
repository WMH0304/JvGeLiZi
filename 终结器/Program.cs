using System;
using System.IO;
using System.Runtime.InteropServices;

namespace 终结器
{
    class Program
    {



        /*
         * 终结器 (finalizer) 允许编写代码来清理类的资源
         * 和使用new 显式调用构造器不同，终结器不能从代码中显式调用，没有与new 对应的操作符,
         * 也不能在编译时确定执行终结器的时间，（也就是说不能人为的去确定终结器执行的时间）唯一能确定的是，
         * 终结器会在对象最后一次使用之后，并在程序正常关闭之前的某个时间运行。
         * 
         * 终结器声明要求 要在类名之前添加一个 ~ 字符
         * https://www.cnblogs.com/zhangq/p/3922123.html
         * 
         * 1)   垃圾回收过程中执行终结器的准确时间是不确定的。不保证资源在任何特定的时间都能释放，除非调用 Close 方法或 Dispose 方法。
         * 2)   即使一个对象引用另一个对象，也不能保证两个对象的终结器以任何特定的顺序运行。即，如果对象 A 具有对对象 B 的引用，并且两者都有终结器，则当对象 A 的终结器启动时，对象 B 可能已经终结了。
         * 3)   运行终结器的线程是未指定的。
         * 
         */
        static void Main(string[] args)
        {
            #region 终结器


            /*
            ExampleClass ex = new ExampleClass();
            ex.ShowDuration();
            GC.Collect();
            */
            #endregion
        }

    }
    #region 终结器
    public class ExampleClass
    {
        //提供一组可用于精确度量的方法和属性运行时间。
        System.Diagnostics.Stopwatch sw;

        public ExampleClass()
        {
            sw = System.Diagnostics.Stopwatch.StartNew();
            Console.WriteLine("实例化 object");
        }
        public void ShowDuration()
        {
            //一个只读的系统。表示所测量的总运行时间当前实例。 Elapsed
            Console.WriteLine("实例 {0} 存在时间 {1}", this, sw.Elapsed);
        }
        ~ExampleClass()
        {
            Console.WriteLine("销毁 object");
            sw.Stop();
            Console.WriteLine("实例 {0} 存在时间 {1}", this, sw.Elapsed);
        }
    }
    #endregion

    #region IDisposable 资源释放
    /*
     * 
     * https://www.cnblogs.com/wyt007/p/9304564.html
     * 
     * https://www.cnblogs.com/myzony/p/11712689.html
     * 
     * 
     * Dispose 清理与内存无关的资源（文件，图片，音频），这种资源不会被垃圾回收器隐式清理
     * 
     * 托管资源： 由CLR管理分配和释放的资源，也就是我们直接new出来的对象；
     *  
     *  
     * 通过实现 IDisposable 接口去实现释放非托管资源
     * 非托管资源不受CLR控制的资源，也就是不属于.NET本身的功能，
     * 往往是通过调用跨平台程序集(如C++)或者操作系统提供的一些接口，
     * 比如Windows内核对象、文件操作、数据库连接、socket、Win32API、网络等。
     * 
     * IDisposable 实现逻辑
     * 
     * 1. 实现dispose 方法
     * 2.提取一个受保护的dispose 虚方法 ，用来实现具体的释放资源的逻辑
     * 3.添加析构函数
     * 4.添加一个私有的bool 类型字段，作为释放资源的标志
     * 
     */
    public class Myclass : IDisposable
    {
        /// <summary>
        /// 模拟一个非托管资源
        /// </summary>
        // IntPtr 用于表示指针或句柄的平台特定类型
        // Marshal 提供了一个方法集，这些方法用于分配非托管内存、复制非托管内存块、将托管类型转换为非托管类型，此外还提供了在与非托管代码交互时使用的其他杂项方法。
        // AllocHGlobal 通过使用指定的字节数，从进程的非托管内存中分配内存。
        private IntPtr NR { get; set; } = System.Runtime.InteropServices.Marshal.AllocHGlobal(100);

        /// <summary>
        /// 一个托管资源 
        /// </summary>
        public Random random { get; set; } = new Random();

        /// <summary>
        /// 释放标志
        /// </summary>
        private bool disposed;
        /// <summary>
        /// 为了防止忘记显式的调用Dispose方法
        /// </summary>
        ~Myclass()
        {
            Dispose(false);
        }


        /// <summary>
        /// 重写 Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            //清理托管资源
            if (disposing)
            {
                if (!random.Equals(null))
                {
                    random = null;
                }
            }
            //清理非托管资源
            if (NR != IntPtr.Zero)
            {
                //IntPtr.Zero 一个只读字段，代表已初始化为零的指针或句柄。
                // FreeHGlobal 释放以前从进程的非托管内存中分配的内存。
                Marshal.FreeHGlobal(NR);
                NR = IntPtr.Zero;
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            // SuppressFinalize 请求系统不要调用指定对象的终结器。 通知垃圾回收器不再调用终结器
            GC.SuppressFinalize(this);

        }
    }

    public class Files
    {
      public Files()
        {
            string s = @"C:\Users\Administrator\Desktop\VS_Git使用说明";
            byte[] ss = new byte[] { 1, 2, 3, 4 };
            System.IO.FileStream file = new FileStream("abc.text", FileMode.Create);

            file.Write(ss,0,ss.Length);
            File.Delete("abc.text");
            file.Dispose();

            /*
             * 使用suing 相当于在内部放置了一个try 
             * 用完就释放资源，换种方法说，他的声明周期只在这个方法里面
             */
            using (System.IO.FileStream fileStream = new FileStream("asd.text", FileMode.CreateNew))
            {

            } 
        }

        ~Files(){

        }
       
    }



    #endregion
}
