using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace 委托和lambda
{
    class Program
    {
        public enum Simp
        {
            升序,
            降序
        }
        public static void Bt(int[] items, Simp simp)
        {
            int i, j, temp;
            if (items == null)
            {
                return;
            }
            for (i = items.Length; i >= 0; i--)
            {
                for (j = 1; j <= i; j++)
                {
                    bool swap = false;
                    switch (simp)
                    {
                        case Simp.升序:
                            try
                            {
                                swap = items[j - 1] > items[j];
                            }
                            catch (Exception)
                            {

                                break;
                            }

                            break;
                        case Simp.降序:
                            try
                            {
                                swap = items[j - 1] < items[j];
                            }
                            catch (Exception)
                            {

                                break;
                            }
                            break;
                        default:
                            break;
                    }
                    if (swap)
                    {
                        temp = items[j - 1];
                        items[j - 1] = items[j];
                        items[j] = temp;

                    }
                }
            }
            Console.WriteLine(simp + ":");
            foreach (var item in items)
            {

                Console.Write(item + ",");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            #region 傻逼排序
            /*
            int[] tem = { 1, 3, 2, 5, 4, 7, 6, 9, 8 };
            Bt(tem, Simp.升序);
            Bt(tem, Simp.降序);

            */
            #region  傻逼排序
            /*   
            int t;
            for (int i = tem.Length-1; i >=0 ; i--)
            {
                for (int j = 1; j<=i; j++)
                {
                    if (tem[j-1]>tem[j])
                    {// 1 2 3
                        t = tem[j];
                        tem[j] = tem[j - 1];
                        tem[j - 1] = t; 
                    }
                }
            }
            Console.ReadKey();
    */
            #endregion
            #endregion

            #region 委托概述
            /*
            Delegates delegates = new Delegates();
            delegates.DeTs();
            //忽略目标函数的类型（静态或实例）
            Delegates.bS(tem, Delegates.gT);
         
            int[] items = { 1,3,5,7,9,2,4,6,8 };
            Delegates.bS(items, Delegates.gT);
            Console.WriteLine("1***********************1");
            
            foreach (var item in items)
            {
                Console.Write(item);
            }*/

            #endregion

            #region 匿名方法

            // TestDelegate.Min();

            #endregion
            /***/
            //Console.WriteLine("asdfasdf");
            //泛型委托();
            //Console.ReadKey();

            #region 表达式树

            #endregion

            #region 闭包
            Closure();

            #endregion

            Console.ReadKey();
        }


        #region 1.委托概述
        /*
         * 委托的定义：
         *  delegate 关键字
         * 将对方法的引用作为实参传给另一个方法，
         * 在c#中委托允许捕捉对方法的引用。
         * 并像传递其他对象一样传递这个引用 （类似于现实中的委托的定义，他的思路有点像线程，
         * 开启第三方，然后去执行新的方法，但他有不是异步的。。）
         * 
         * 特点：
         * 1.可以在任何地方定义， 因为定义委托就是定义一个新的类(可以把委托看作是一个类)
         * 2.定义委托时，必须给出他所表示的方法的签名和返回类型的指定名称（通俗地来说就是委托名和委托的类型）
         * 3.理解委托的一种好方式就是把委托视为给方法的签名和返回类型指定名称
         * 4.和c/c++ 的函数指针类似，与 C 中的函数指针不同，委托是面向对象的、类型安全的和保险的。 委托的类型由委托的名称定义。
         * 5.类型安全啊和泛型一样，那么问题来了，他为什么类型安全呢？因为他可以确保被调用的方法的签名是对的，
         * 更有趣的是，他们并不关心什么类型的对象调用这个方法，甚至不考虑该方法是静态方法还是实例方法
         * 唯一的约束条件就是-------只要方法的签名匹配委托的签名一样即可
         * 6.他派生自 System。delegate 而在.net 中 他又是派生自system.multicastdelegate  而 system.multicastdelegate 又派生自 System。delegate 
         * 所以委托总是直接或间接的派生自  System.delegate
         * 7.所有委托都是不可变的，委托一旦创建好就无法更改，如果变量包含了委托的引用，还想引用其他不同的方法，那就必须创建一个新的委托，并把它指派给这个变量
         */
        class Delegates
        {
            private delegate string toString();

            public delegate bool comparisonH(int first, int second);
             public void DeTs()
            {
                int x = 10;
                int y = 7;
                //x.ToString 指向 ToString 方法
                toString to = new toString(x.ToString);
                //委托推断, 这是一个语法糖 为了减少代码量，可以在委托示例中只传递地址的名称

                 /*
                  * 哎呀，调用方法时他的括号呢?括号呢？
                  * 这个又牵扯到了委托的本质了
                  * 1.什么是委托，委托就是吧方法封装成参数然后传递到另一个方法里面，所以在委托中的委托方法是一个参数
                  * 2.既然是参数，那么肯是空或者不空了，这样说来，他又是一个‘变量’了。
                  * 3.因为委托是带有指向性的，所以委托传递的‘变量’实际上是一个地址。
                  */
                toString to1 = y.ToString;
                //最终在il中都会变成这个逼样
                /*
             IL_0018:  callvirt   instance string '委托和lambda'.Program/Delegates/toString::Invoke()
             IL_001d:  call       void [System.Console]System.Console::WriteLine(string)
               */
                //打印委托
                Console.WriteLine(to());
                //Invoke 回调函数
                /*
                 * 事实上 C#编译器 最终还是会将 to() 转换成 to.Invoke() 
                 * 
                 * 原因 声明的委托是委托类型的一个变量
                 * 
                 */

                Console.WriteLine(to.Invoke());
            }

            public static void bS(int []tem,comparisonH comparisonH)
            {
                int i, j, temp;
                for (i = tem.Length; i >= 0; i--)
                {
                    for ( j = 1; j <=i; j++)
                    {
                        try
                        {
                            if (comparisonH(tem[j - 1], tem[j]))
                            {
                                temp = tem[j - 1];
                                tem[j - 1] = tem[j];
                                tem[j] = temp;
                            }
                        }
                        catch (Exception)
                        {

                            break;
                        }
                        
                    }
                }
            }
            
            public static bool gT(int first,int second)
            {
                int con;
                con = (first.ToString().CompareTo(second.ToString()));
                return con > 0;
            }
        }



        #endregion

        #region 2.匿名函数

        /*
        为了能更精简的创建委托。 C#3.0 引用了更精简的语法 Lambda表达式
        在此之前 这种特性叫做匿名方法他们统称为 lambda 表达式
        */

        #region 2.匿名方法
        /*
         * 匿名方法：顾名思义就是没有名字的方法
         * 匿名方法是没有名称只有主体的方法。在匿名方法中您不需要指定返回类型，它是从方法主体内的 return 语句推断的。
         * 匿名方法是通过使用 delegate 关键字创建委托实例来声明的（一般情况下和delegate一起使用）
         * 
         * 实现过程
         * 通过 deldgate 关键字创建
         * 然后通过已经声明的委托调用其它方法
         * 
         * 特点：
         * 1.必须显式指定每个参数类类型，而且必须有一个语句块
         * 2.参数列表之间不能用 lambda 操作符 =>,而是在参数列表前添加关键字 delegate，强调匿名方法转换成一个委托类型
         * 3.无参数的匿名方法，匿名方法运行彻底省略参数列表，前提是主体中不使用任何参数，而且委托类型只要求“值”（不要求将参数标记为out 或ref）
         * 粒子：  delegate { return Console.ReadLine() != ""; } 它可以转换成任意要求返回 bool 的委托类型，而不管为u他需要多少个参数。
         * 4.匿名方法在某些情况下可以彻底省略参数列表。
         */

        delegate void NumberChanger(int n);

        class TestDelegate
        {
            static int num = 10;
            public static void AddNum(int p)
            {
                num += p;
                Console.WriteLine("AddNum Named Method: {0}", num);
            }

            public static void MultNum(int q)
            {
               
                num *= q;
                Console.WriteLine("MultNum Named Method: {0}", num);
            }

            public static void Min()
            {                // 使用匿名方法创建委托实例


                // delegate (int x) 匿名方法 强调匿名方法转换成一个委托类型
                NumberChanger nc = delegate (int x)
                {
                    Console.WriteLine("Anonymous Method: {0}", x);
                };

                // 使用匿名方法调用委托
                nc(10);

                // 使用命名方法实例化委托
                nc = new NumberChanger(AddNum);

                // 使用命名方法调用委托
                nc(5);

                // 使用另一个命名方法实例化委托
                nc = new NumberChanger(MultNum);

                // 使用命名方法调用委托
                nc(2);
                Console.ReadKey();
            }
        }

        #endregion

        #endregion

        #region 3.Lambda 
        /*
         * 他的实质就是一个语法糖
         * 
         * 特点：
         * 1.所有lambda 表达式都不需要显示声明参数类型，但如果指定类型能使代码更易读，
         * 在不能托段类型的情况下 C#  会要求显式指定 lambda 参数类型
         * 
         */

        /*
         * 语句lambda 比完整方法声明要简单的多，可以不指定方法名，可访问性，返回类型，不指定参数类型
         * 事实上在这 在lambda块中只需要准备返回（return）的表的是，其它的都可以忽略 
         */
        public void 语句lambda()
        {
            //  IEnumerable 集合中的基接口  公开枚举数，该枚举数支持在指定类型的集合上进行简单迭代。
            //  Process 提供对本地和远程进程的访问权限并使你能够启动和停止本地系统进程。 
            //  GetProcesses 为本地计算机上的每个进程资源创建一个新的 Process 组件
            //  WorkingSet64 获取为关联进程分配的物理内存量。
            /* 
             * 对本地句柄超过 100000 的进程进行查询
             */
            IEnumerable<Process> processes = Process.GetProcesses().Where(p => { return p.WorkingSet64 > 100000; });

            /*
             * Func 泛型委托
             * 读取输入行并返回输入行内容
             */
            Func<string> func = () => { string input; do { input = Console.ReadLine(); } while (input.Trim().Length == 0); return input; };
        }
        /*
         * 1.
         * ∵lambda 表达式本身没有类型
         * ∴没有直接从表达式中访问的成员
         * 2.
         * ∵lambda 表达式本身没有类型
         * ∴ 不能出现在 is 操作符左边
         * 3.
         * ∵lambda 只能转换成兼容的委托类型，
         * ∴在例子中 返回int 的lambda 不能转换成返回bool方法的委托类型（委托的实质就是只看 方法名和参数是否对应）
         * 4.
         * ∵lambda 表达式本身没有类型
         * ∴ 不能用于推断局部变量类型
         * 5.
         * 如果目的地在lambda 外部，c#不允许在匿名方法内部使用跳转语句（break，goto，continue）
         * 6.
         * lambda 中引用的参数和局部变量只是用于lambda 主体
         * 7.
         * 编译器的 确定性赋值分析机制 在lambda 表达式内部检测不到对外部局部变量进行初始化的情况
         * 但是能访问外部变量 两者概念不要混淆喽,
         * 值得一提是的，局部变量的生命期一般都和他的作用域绑定，但是如果lambda 表达式 捕捉到的变量，会根据
         * 表达式创建的委托可能会具有在一般情况下更长或更短的生存期
         */
        public void 表达式lambda()
        {
            //1. 无法访问成员
            // string s = ((int x) =>  x).ToString();
            //2. 无法作为源比较
            // bool b = ((int x) => x) is Func<int, int>;
            //3. 无法转换类型
            //Func<int, bool> func = (int x) => x;
            //4.无法推断变量类型
            //var v = x => x;
            //5. 无法用跳转语句跳出当前表达式
            //string[] str;
            //Func<string> f;
            //switch (str[0])
            //{
            //    case "/File":
            //        f = () =>
            //          {
            //              if (File.Exists(str[1])) break;
            //              return str[1];
            //          }; 
            //}
            //6.主体参数无法在外部使用
            // int f=1, s=2;
            // Func<int, int, bool> func = (f, s) => f > s;
            //7.外部值改变不影响主题内参数值的变化
            //8.当表达式外部参数值改变时，表达式内部无法检测（值类型，复制值）
            //int mun ;
            //Func<string, bool> func = text => int.TryParse(text, out mun);
            //if (func("1"))
            //{

            //    // 确定性赋值分析机制 
            //    //使用了未赋值的局部变量
            //    Console.WriteLine(mun);
            //}
            ///*********************************/
            //int n1;
            //Func<int, bool> func1 = i => 22 == (n1 = i);
            //if (func1(22))
            //{
            //    Console.WriteLine(n1);
            //}




        }





        #endregion

        #region 4.泛型委托
        /*
         * 在..net 中 委托类型不具备结构相等性
         * 他只考虑 方法名和方法签名
         * 
         * 由于不具备结构相等性，所以两个委托之间不能相互转换
         * 即使两个委托类型的形参和返回类型完全一致
         * 
         * 
         * 
         * system.func     系列委托代表有 返回值 的方法
         * func 委托左后一个类型参数总是委托的返回类型。
         * 特点：
         * 1.可以不必自定义委托类型
         * 2.由此而来的是代码的可读性下降
         * 3.最后一个类型参数总是委托的返回类型
         * 
         * 
         * system.Action   系列委托代表返回 void 的方法
         */

       static public void 泛型委托()
        {
            Action<object> action = (object data) => { Console.WriteLine(data); };


            Action<string> action1 = action;


            Func<string> func = () => Console.ReadLine();
            Func<object, string> func1 = (object data) => data.ToString();
            Func<string, object> func2 = func1;

        }

        #endregion




        #region 5.表达式树

        #endregion

        #region C#中的闭包
        /*
         * 闭包：
         * 1.闭包（Closure）是词法闭包（Lexical Closure）的简称，是引用了自由变量的函数。
         * 2.这个被引用的自由变量将和这个函数一同存在，即使已经离开了创造它的环境也不例外。
         * 3.有另一种说法认为闭包是由函数和与其相关的引用环境组合而成的实体。
         * 
         * 当我们执行 action 时，它输出了我们预期的结果。请注意，
         * 当我们执行时，原始的“x”此时已经脱离了它当初的变量环境，但它仍然能用。
         * 
         * lambda委托 改变变量的值时，lambda外部可以访问已改动值
         * 
         * 
         * 
         */
        private int some;
        public Program(int some)
        {
            this.some = some;
        }
        public int AM(int x) => x + some;
     
        public static void Closure()
        {
            //var x = 1;
            //Action action = () =>
            //{
            //    var y = 2;
            //    var result = x + y;
            //    Console.Out.WriteLine("result = {0}", result);
            //};
            // action();

            //委托数组
            //Action[] actions = new Action[10];

            //for (var i = 0; i < actions.Length; i++)
            //{
            //    actions[i] = () =>
            //    {
            //        Console.WriteLine(i);
            //    };
            //}

            //foreach (var item in actions)
            //{
            //    item();
            //}

            //C# 5.0之前捕捉循环变量的解决方案
            //应避免在匿名函数中循环捕捉变量
            var it = new string[] {"A","B","v" };
            var ac = new List<Action>();
            foreach (string item in it)
            {
                string _it = item;
                ac.Add(() => { Console.WriteLine(_it); });

                foreach (Action em in ac)
                {
                    em();
                }
            }

        }
        #endregion
    }
}
