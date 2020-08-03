using System;

namespace 向上转型和向下转型
{
    /*
     * 向上转型 ：在继承链中将子类转换成父类对象，使该对象具有父类的属性，并且可以自由直接转换
     * 
     * 向下转型 ：在继承链中将父类转换成子类对象，使该对象具有子类的属性，需要强制转换
     * 
     */
    #region 继承自非抽象类 

   
    class A
    {
        public void a()
        {
            Console.WriteLine("我是父类");
        }
    }
    class B :A
    {
        public void a()
        {
            Console.WriteLine("我是子类");
        }
        public void b()
        {
            Console.WriteLine("我也是子类");
        }
    }
    #endregion


    #region 继承自抽象类
    abstract class A_a
    {
        public abstract void a();
    }
    class B_b:A_a
    {
        public override void a() { Console.WriteLine("重写抽象类方法"); }
        public void b() { Console.WriteLine("扩展方法"); }
    }
    /*
     * 刚刚看了java 的资料对这个概念又有的新的理解
     * 
     * 我们可以想象一下，每个类在继承链中的关系，你会发现 他居然是一个树状图 类似于
     * 
     *       B
     *        \
     *          A
     *           \ /
     *           obj
     * 在关系图中， obj 是整个继承链中的基类， 所以存在关系  b:a:obj
     * 
     * 
     * 
     *向上转型： 父类引用指向子类对象   B b = new A();  
     * 这一个操作不用强制转换， 但是他为什么可以不用强制转换呢？
     * 个人理解：
     * 1.在继承链中子类对象和父类对象有了继承关系，所以子类可以访问父类对象
     * 2.子类对象是父类对象的扩张，所以可以包含父类对象。
     * 3.从内存方面来说，子类对象的分配的内存比父类对象内存要大，所以可以直接放置，
     * 理由：
     * 3.1 假设父类对象是个接口，或者其他调用时需要实现器内部成员的对象
     * 3.2 在向下转型中的编码格式类似于平常数据类型的隐式转换，让我更确信了这一点。
     * 3.3 有待补充。
     * 
     * 向下转型： 子类对象指向父类引用为向下转型，  B b = (b)A;
     * 
     *  
     * 总结： 向上转型和向下转型 就是针对同一个继承链中的两个类型的相互转换，类似于化学实验中的互逆反应。 
     *  
     */




    #endregion

    class Program 
    {
        static void Main(string[] args)
        {
            #region 非抽象类
            A a = new B();//向上转型, new的作用是隐藏父类的同名方法
            //A a = new A();
            a.a();//然后会输出父类 这时的A 已经是一个父对象发
            /****/
            B b = (B)a;//向下转型,相当于用父类代替子类，但是子类有扩展方法，所以需要转型
            b.a();
            b.b();

            /*
             * A a = new B()   相当于给父类申请类一块子类的大小的内存，
             * 用于存放子类。当然 小一些的父类放进去也是完全没问题的。
             */
            #endregion

            #region 抽象类
            A_a a_A = new B_b();
            a_A.a();

          // B_b b_ = (B_b)a_A;
            B_b b_ = a_A as B_b;//也可以使用 as 操作符进行转换  使用as 操作符时 会将对象转换成特定数据类型，如果不能转换将返回null

            b_.a();
            b_.b();
            #endregion
        }

    }
}
