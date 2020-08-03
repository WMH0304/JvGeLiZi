using System;

namespace 垃圾回收器
{
    class Program
    {
        /*
        * 垃圾回收机制：作用是回收不愿再被引用的对象所占用的内存。
        * 垃圾回收只回收内存，不处理其他资源，比如数据库链接，句柄，文件窗口等。。
        * 垃圾回收器根据是否存在任何引用来决定要清理什么，
        * 这暗示垃圾回收处理的是引用对象，只回收堆上的内存
        * 另外，如果要维持对一个对象的引用，就要阻止垃圾回收器重用对象所用的内存 这个就牵扯到了弱引用这个概念了。
        * 
        * 

    系统具有较低的物理内存；

    由托管堆上已分配的对象使用的内存超出了可接受的范围；

    手动调用GC.Collect方法，但几乎所有的情况下，我们都不必调用，因为垃圾回收器会自动调用它，但在上面的例子中，为了体验一下不及时回收垃圾带来的危害，所以手动调用了GC.Collect，大家也可以仔细体会一下运行这个方法带来的不同。

        * 
        * 
        * 
        */
        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with System.GC ****");
            // 输出堆上估计的字节数量
            Console.WriteLine("Estimated bytes on heap: {0}",GC.GetTotalMemory(false));

            Console.WriteLine("最大代数:{0}",GC.MaxGeneration);
            // MaxGeneration是由0开始，因此为了显示的目的加上了1
            Console.WriteLine("This OS has {0} object generations.\n",(GC.MaxGeneration + 1));
            Car refToMyCar = new Car("Zippy", 100);
            Console.WriteLine(refToMyCar.ToString());

            // 输出refToMyCar对象的代
            Console.WriteLine("输出refToMyCar对象的代: {0}",
                GC.GetGeneration(refToMyCar));
            // 只检查第0代对象
            GC.Collect(0);
            //挂起当前线程，直到正在处理的队列的线程终结器清空了队列。
            GC.WaitForPendingFinalizers();

            Console.ReadLine();
        }
    }
    class Car
    {
        // Car的"状态"
        public string petName;
        public int currSpeed;

        // 自定义的默认构造函数
        public Car()
        {
            petName = "Chuck";
            currSpeed = 10;
        }

        // 在这里，currSpeed会获取int的默认值0
        public Car(string pn, int cs)
        {
            petName = pn;
            currSpeed = cs;
        }
    }
}
