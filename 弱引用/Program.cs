using System;

namespace 弱引用
{
    class Program
    {

        /*
         * 啥是弱引用？
         * 他的目的是什么？
         * 背景是什么？
         *https://blog.csdn.net/leonwei/article/details/52471326
         * 
         *  我们平时用到的多是对象的强引用，如果强引用存在 GC 就不会回收对象。我们能不能同时保持对象的引用
         *  而有可以让GC 需要时回收这个对象呢？
          
          
          
         
         *  NET中提供了WeakReference来实现。弱引用可以让您保持对对象的引用，
         *  同时允许GC在必要时释放对象，回收内存。对于那些创建便宜但耗费大量内存的对象，
         *  即希望保持该对象，又要在应用程序需要时使用，同时希望GC必要时回收时，
         *  可以考虑使用弱引用。弱引用使用起来很简单


            WeakReference类的三个属性:

            IsAlive：获取当前 WeakReference 对象引用的对象是否已被垃圾回收的指示。
            Target：获取或设置当前 WeakReference 对象引用的对象（目标）。
            TrackResurrection：获取当前 WeakReference 对象引用的对象在终止后是否会被跟踪的指示。





         */

        static void Main(string[] args)
        {
            //强引用 标识是分配了内存，也就是说这个对象活着
            Object o1 = new Object();
            //弱引用，此处的o1 还是属于强引用 但 weak 属于弱引用

            WeakReference weak = new WeakReference(o1);

            o1 = null;
           
            //
            Console.WriteLine("我是美丽的o1:{0}::",o1);
            Object o2 = weak.IsAlive;
            Console.WriteLine(o2);

            Console.WriteLine(weak);
            //如果对象被当前系统引用，则为空
            o2 = weak.Target;
            if (!o2.Equals(null))
            {
                Console.WriteLine("不被应用");
            }
            else
            {
                Console.WriteLine("被应用");
            }
            //如果为 true，则该弱引用是一个长弱引用
            o2 = weak.TrackResurrection;
            Console.WriteLine(o2);

            //创建长弱引用或者短弱引用;

            Object l = new Object();
            //如果  为 false，则创建短弱引用,反之亦然
            WeakReference lwr = new WeakReference(l,false);






        }
    }


   

}
