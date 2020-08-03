using System;
using System.Collections;

namespace 泛型
{
    class Program 
    {
        /**
         * 什么是泛型？
         * 为什么创造他？
         * 通过泛型促进对代码的重用
         * 实用与什么场景
         * 对代码的重用
         * 他的优点和缺点是什么？
         * 优点
         * 提高代码重用
         * 提高性能，避免了装箱拆箱
         * 较安全
         * 它实现的原理是什么？
         * 忽略数据的类型，减少不必要的类型转换。
         * 简述他的概念。
         * 实现参数化类型实现一份代码可以操作多种数据类型
         * 
         * 
         * 
         * 在我们定义泛型类的时候，语法和我们定义泛型方法的格式相同，约束也是相同，
         * 如果我们的泛型类中是属性，属性使用的是泛型类型泛型可以定义，方法，类，接口，属性
         * 
         * 约束：
         * 强迫作为类型实参提供的类型遵守各种规则
         *  概念：指定泛型 T 可植入的数据类型 可以是 
         *  https://www.cnblogs.com/chenxizhang/archive/2008/09/18/1293680.html
         *   where T: struct 
         *   类型参数必须是值类型
         *   
         *   where T:class  引用类型
         *   值得注意的是
         *   1.这里的class 约束并不是将类型实参限制未类类型，
         *   而是限制未引用类型，所以任何类，接口，委托，和数组类型都符合要求
         *   2.类类型约束要求指定特定的类，所以类类型约束和struct 约束一起使用会相互矛盾
         *   因此struct 约束和class 约束不能一起使用
         *   
         *   
         *   
         *   where T : new()
         *   类型参数必须有一个公有的，无参的构造函数。当与其他约束联合使用时，new()约束必须放在最后
         *   
         *   where T:<base class name >
         *   
         *  类型参数必须是指定的基类型或是派生自指定的基类型。
         *  
         *  where T : <interface name>
         *  类型参数必须是指定的接口或是指定接口的实现。
         *  可以指定多个接口约束。接口约束也可以是泛型的。
         * 
         * 
         * 
         **/

        /*
         * 
         * 表明一个约束，指定T 只能是 Type  或type的派生类
         * 
         * 
         */
        //定义接口约束
        class my<T> where T:System.IComparable<T>
        {
            public T Item { get; set; }

           
        }
        //值约束
        class myy <T> where T : struct
        {
            public T Item { get; set; }
        }
        //构造函数约束
        class myyy<T> where T: new()
        {

        }
     
        static Tuple<string, string, string> tuple;
        /// <summary>
        /// 起始方法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

          



            //声明栈类 存储历史位置
            Stack path = new Stack();
            // Cell cell;
            // ConsoleKeyInfo 描述按下的控制台键，包括控制台键表示的字符以及 Shift、Alt 和 Ctrl 修改键的状态。

            //string vs = "sadasfasdf";
            //foreach (var item in vs)
            //{
            //    path.Push(item);
            //}
            //path.Pop();
            //foreach (var item in path)
            //{

            //    Console.Write(item);


            //}
            #region Tuples
            //元组/元数 是一种特定类型的数据结构，既然书数据结构当然可以存储数据
           
            tuple = Tuple.Create("sd", "asdf", "adf");
            tuple = new Tuple<string, string, string>("dd", "dddf", "sdsf");
            var d = ValueTuple.Create<string, string, string>("sda","sadf","fgh") ;
        
            Console.WriteLine(tuple.Item1);
            Console.WriteLine(d);
            Console.WriteLine(d.Item3);
            string a = "\u4f18\u79c0";
            string aa = "";
            Console.WriteLine(a);
            Console.WriteLine(Max<int>(100,200));
            Console.WriteLine(Min<int>(100, 200));
            Console.WriteLine(Max<string>("a", "d"));

            #region 协变逆变
            //资料参考 https://www.sohu.com/a/337520881_120050810

            /*
             * 区别：
             * 协变： 对具体成员 输出参数 进行一次类型转换 
             * 逆变： 对具体成员 输入参数 进行一次类型转换
             * 
             */
           
            /* 
             * 
             * 协变就是对具体成员的输出参数进行一次类型转换，
             * 且类型转换的准则是 “里氏替换原则”
             * 
             * 值类型转换成引用类型
             */
            IFoo<string> foo = new Foo();
            string m = foo.GetName();
            IFoo<object> fooo = foo;
            object me = fooo.GetName();
            Console.WriteLine(m);
            Console.WriteLine(me);
           
            /*
             * 逆变就是对具体成员的输入参数进行一次类型转换，且类型转换的准则是"里氏替换原则"。
             * 引用类型转换成值类型
             */
            IFii<object> fii = new Fii();
            IFii<string> fiii = fii;
            fiii.Print("sdfasd");


            //调用泛型方法 
            int j = 1;
            int i = 5;
            //string j = "大海";
            //string i = "小孩";
            //DateTime i = DateTime.Now;
            //DateTime j = DateTime.Today;

            Swap(ref j, ref i);

            Console.ReadKey();
            /*
             * 自问自答
             * 协变，逆变 为什么只能针对泛型接口或者委托？而不能针对泛型类？
             * 
             * 1.因为他们都只能定义方法成员（接口不能定义字段），
             * 而方法成员在创建对象时是不涉及到对象内存分配的，
             * 所以他们是类型（内存）安全的
             * 2. 为什么不针对泛型？因为泛型是模板类，
             * 而类型成员是包含字段的，不同类型的字段是影响对象内存分配的，
             * 没有派生关系的类型他们是不兼容的，内存也不安全
             * 
             * 协变、逆变 为什么是类型安全的？
             * 
             * 本质上是里氏替换原则，由里氏替换原则可知：
             * 派生程度小的是派生程度大的子集，
             * 所以子类替换父类的位置整个程序功能都不会发生改变。
             * 
             * 为什么 in 、out 只能是单向的（输入或输出）？
             * 因为若类型参数同时为输入参数和输出参数，
             * 则必然会有一种转换不符合里氏替换原则，
             * 即将父类型的变量赋值给子类型的变量，
             * 这是不安全的所以需要明确指定 in 或 out。
             * 
             */


            #endregion
            #endregion
          
        }

        /// <summary>
        /// 泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="y"></param>
    static    void Swap<T>(ref T x,ref T y)
        {
            T temp;
            temp = x;
            y = x;
            y = temp;
            Console.WriteLine("{0}{1}",x,y);
        }

        /// <summary>
        /// 定义带约束的泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T Max<T>(T first,params T [] values) where T: IComparable<T>
        {
            T ma = first;
            foreach (T item in values)
            {
                ma = item;
            }
            return ma;
        }
        /// <summary>
        /// 泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T Min<T>(T first, params T[] values) where T : IComparable<T>
        {
            T ma = first;
            foreach (T item in values)
            {
                if (item.CompareTo(ma)<0)
                {
                    ma = item;
                }
                
            }
            return ma;
        }
    }


    //声明结构体存储位置信息
    public struct Cell
    {
        public int X { get; }
        public int Y { get; }
        public Cell(int x,int y)
        {
            X = x;
            Y = y;
        }
    }



    //声明泛型接口
    interface IPair<T>
    {
        T a { get; set; }
        T b { get; set; }
    }

    //实现泛型接口
    public struct Pair<T> : IPair<T>
    {
        public T a { get; set; }
        public T b { get; set; }
    }



    #region 逆变性和协变性
    //out 指定类型参数可协变
    interface IFoo<out T>
    {
        T GetName();
    }
    class Foo : IFoo<string>
    {
        public string GetName()
        {
            return GetType().Name;
        }
    }
    //in 指定类型参数可逆变
    interface IFii<in T>
    {
        void Print(T c);
    }
    class Fii : IFii<object>
    {
        public void Print(object c)
        {
            Console.WriteLine(c);
        }
    }

    #endregion




}
