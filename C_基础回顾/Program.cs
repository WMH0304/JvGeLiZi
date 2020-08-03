using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Console;


#region 蓝屏警告


//using System.Runtime.InteropServices;
//namespace ConsoleApplication49
//{
//    class Program
//    {
//        [DllImport("ntdll.dll")]
//        public static extern void RtlSetProcessIsCritical(int i, int j, int k);
//        static void Main(string[] args)
//        {
//            RtlSetProcessIsCritical(1, 0, 0);
//        }
//    }
//}
#endregion














//.net framwork 和.net code 区别
namespace 小常识c_
{
    #region 类和名称空间
    /*
     * 啥是类呢？
     * 以前一直以为类就是构成程序的主体，是一种数据结构，是对对象的抽象啥的
     * 最近在听刘铁猛老师的课，普及了以前很多不知道的小常识，做个记录
     * 
     * 什么是类呢？
     * A: 类是一个数据结构，将状态（字段）和操作（方法和其它函数成员）组合在一个单元里面，
     * B: 类为动态创建的类实例（instance）提供了定义，其实类的实例就是对象了（object） 也可以理解成各种数据类型也是一种对象，
     * C: 类支持 继承（【 inhcritance 】当然，是单继承，而接口支持多继承）和多态性（[ polymorphism ] ）这个是派生类（ derivcd class ） 可以用来
     * 扩展和专用化基类（ basc class 就是子类了 ）的机制
     * 
     * C1: 什么是多态性呢？
     * C2: 多态性有 编译时的多态性 和 运行时的多态性 两种，
     * C3: 编译时的多态性编译时的多态性是通过重载来实现的。对于非虚的成员来说，系统在编译时，根据传递的参数、
     * 返回的类型等信息决定实现何种操作。（说白了，就是数据类型的多样性）
     * 
     * C4: 运行时的多态性就是指直到系统运行时，才根据实际情况决定实现何种操作。C#中运行时的多态性是通过覆写虚成员实现。 (可以理解成是程序运行时的状态的多样性)
     * 
     * D: 类的实例的过程就是 ： 给类分配内存后就成了实例，然后调用构造函数初始化实例，并返回对实例的引用。
     * 
     * 
     * namespace （名称空间） 的作用是什么呢?
     * A: 相当于是一个树形的节点，当你要找到某一个特定的函数的时候可以名称空间进行检索。
     * B: 通过 using 引用,当然也可以从函数里面通过一个访问权限引用 （或者说是一个标记）
     * C: 为什么要用到访问这个访问表记呢？ 
     * D: 因为在不同的命名空间里面可能会存在有相同的类名，当你同时引用两个命名空间是可能会发现应用冲突
     * E: 这个就是访问标记存在的意义
     * 
     * 
     */
    //class Program 
    //{
    //    static void Main(string[] args)
    //    {
    //        Console.WriteLine("Hello World!");
    //        System.Console.WriteLine("Hello World!");//权限命名，给一个访问名称空间的权限

    //    }
    //}
    #endregion

    #region 类，对象，类成员
    /*
     * 啥是类呢？
     * 类是对现实世界事物进行抽象的模型
     * 事物包括"物质（就是实体了）" 和 "运动(逻辑)"
     * 就是把现实中相对有意义的一些属性抽象到程序里面
     * 有点唯物主义的味道，去伪存真，由表及里的过程
     * 
     * 类和对象的关系
     * 按照我的理解就是 类是对对象的抽象，只一个概念，而对象就是对类是实例化，是一个实体
     * 而在现实世界中 类是对某一事物的描述，而这个被描述的东西就是对象也叫实例
     * 而在程序中，所谓的对象就是类经过实例化之后得到的内存中的实体，（或者说，实例化就是给类分配内存，之后他就是一个实例（对象了））
     */
    /*
     * 类成员：
     * 
     * 属性（property）：存储数据，组合起来表示类或对象当前的状态
     * 
     * 方法（method）：由 c 语言中的函数 function 进化而来的，表示类或对象能做什么 实现想法 和构成逻辑的成员
     * 有人喜欢叫他是算法 也没错 逻辑算法
     * 
     * 事件 ：类或对象通知其他类或对象的机制，c# 特有的 就比如委托 delegate 就是一个事件类
     * 
     * 
     */
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        // Form form;//引用变量
    //        // form = new Form();//实例化这类，new 是关键字 相当于引用实例
    //        // form.Text = "沙县小吃";


    //        // form.ShowDialog();
    //        // form.Close();

    //        // 通过引用用他的类库 可以再控制器显示窗口的操作
    //        //entityframwork 是用来读写数据库的一个 Nuget包


    //    }
    //}
    #endregion




    class Program
    {
       // delegate int weituo(int a, int b); //定义委托
        static void Main(string[] args)
        {
            #region  round-trip  格式化转换
            /*
             *  为了更准确地支持 double 值的准确输出
             *  我们可以将double值转换成字符串的形式输出 
             *  用到的 round-trip
             *  用 R 或r 转换，例如
             *  string.Format("{0:r}",3.14159286575);
             *  这个可以弥补精度问题
             */
            //const double num = 3.1415928657578987987789;

            //double result;
            //string text,t;

            //text = $"{num}";
            //result = double.Parse(text);//Parse 将 数字字符串转化为等效的双精度浮点数
            ////未进行转换 输出false
            //Console.WriteLine($"{result ==num}: result == num");

            //text = string.Format("{0:r}", num);


            //result = double.Parse(text);
            //Console.WriteLine($"{result ==num}: result == num");
            //t = string.Format(" {0}{1}", num, num);//只是返回结果，并不输出
            //Console.WriteLine(t);
            //Console.WriteLine($@"我是谁？ 我是{result}");
            //WriteLine(@"


            //                __.'              ~.   .~              `.__
            //              .'//                  \./                  \\`.
            //            .'//                     |                     \\`.
            //                      ");

            //int a = string.Compare(text,t);
            //Console.WriteLine(a);

            /***************/

            /*
             * 字符串不是变量
             * 要想改变原字符串内容
             * 只能创建一个新的字符串，然后再赋值到原字符串
             */
            //string ta, b;
            //System.Console.Write("uyg");
            //ta = System.Console.ReadLine();
            //b = ta.ToUpper();
            //System.Console.WriteLine(b);
            /*********************/
            /*
             * 
             * trypase 转换 忽略转换异常
             * 
             */
            //double a;
            //Write("soadjfk");
            //string input = ReadLine();
            /////TryParse  忽略转换异常
            //if (double.TryParse(input,out a))
            //{
            //    WriteLine("sidfj");
            //}
            //else
            //{
            //    WriteLine("sdkajf sadfg");
            //}
            /**/
            /*
             * checked  溢出检查
             *  unchecked 忽略溢出简称，系统默认
             */
            //checked
            //{
            //    int a = int.MaxValue;
            //    a = a + 1;
            //    WriteLine(a);
            //}
            //unchecked
            //{
            //    int a = int.MaxValue;
            //    a = a + 1;
            //    WriteLine(a);
            //}
            /**/

            /*********************/
            // Console.ReadKey();

            #region 数组
            /* 是一种数据结构
             * 数组的索引时从0开始的，所以我们说数组是基于0的,由于数组是基于0 的 所以数组的最后一个元素的索引值要比数组元素的总数小1
             * 可以用数组声明相同类型的多个数据项的集合，所以就是这个集合的唯一标识符
             * 
             * int[] a;  一维数组
             * int[,] a; 二维数组 维数 = ，+1
             * 
             * 声明数组并赋值
             *  int[] a = (1, 2, 3,);
             *  string [] a ={"1","2","3"};
             *  声明后赋值
             *  string [] a;
             *  a = new string[]{"1","2","3"};
             *  声明的同时并用new 进行数组赋值 
             *   使用 new 关键字会让 运行内存（ram）分配内存 实例化数据类型
             *  string [] a = new string[]{"1","2","3"};
             *  使用 new 关键字给数组赋值时可以知道数组的大小
             *  在初始化语句时数组的大小（索引值 maxindex）
             *  string [] a = new string[3]{"1","2","3"};
             *  分配数组但不指定初始值
             *  string [] a = new string[3];
             *  运行时 设置数组的大小
             *  int a = int.Parse(ReadLine());
             *  string[]  b = new string[a];
             *  声明二维数组 相当于声明一个横向有三个元素，纵向有三个元素的数字矩阵
             *  int [,] a = int[3,3]
             *  int[,] a = {{ 1, 2 }};
             *  WriteLine(a);
             *  访问数组
             *   string[] a = new string[] { "1", "2", "3" };
             *   string aa = a[1];
             *   Console.WriteLine(aa);
             *   
             *   交错数组 比二维数组灵活
             *   int [][] a ;
             *   
             *   
             */
            /**/
            #region 
            /*利用数组反转字符串*/

            #endregion




            #endregion
            //string a, b;
            //char[] c;
            //Write("helloword");
            //b = ReadLine();
            //a = b.Replace(" ","");
            //a = a.ToLower();
            //c = a.ToCharArray();
            //Array.Reverse(c);

            //if (a==new string(c))
            //{
            //    WriteLine($"\"{b}\"你好世界");
            //}
            //else
            //{
            //    WriteLine($"\"{b}\"I'm fine");
            //} 




            #endregion

            #region 委托
            //    weituo weituo = new weituo(dgg);///实例委托，委托对象要和委托有相同的方法签名，
            //    int sre = weituo(10, 20);//
            //    dgg(10, 20);
            //    Console.WriteLine(sre);
            //    weituo weituo1 = new weituo(t);
            //    int g = weituo1(20, 1);
            //    Console.WriteLine(g);
            //    Console.ReadLine();


            //}
            //static int dgg(int a,int b)
            //{
            //    return a + b;
            //}
            //static int t(int a,int b)
            //{
            //    return a * b;
            //}

            #endregion

            #region lambda
            /*
            Func<int, string> gwl = p => p + 10 + "\0\0\0\0\0\0\0\0--返回类型为string";
            Console.WriteLine(gwl(10) + "");  //打印‘20--返回类型为string，z对应参数b，p对应参数a
            Console.ReadKey();
            */
            //ParameterExpression a = Expression.Parameter(typeof(int), "i");   //创建一个表达式树中的参数，作为一个节点，这里是最下层的节点
            //ParameterExpression b = Expression.Parameter(typeof(int), "j");
            //BinaryExpression be = Expression.Multiply(a, b);    //这里i*j,生成表达式树中的一个节点，比上面节点高一级

            //ParameterExpression c = Expression.Parameter(typeof(int), "w");
            //ParameterExpression d = Expression.Parameter(typeof(int), "x");
            //BinaryExpression be1 = Expression.Multiply(c, d);

            //BinaryExpression su = Expression.Add(be, be1);   //运算两个中级节点，产生终结点

            //Expression<Func<int, int, int, int, int>> lambda = Expression.Lambda<Func<int, int, int, int, int>>(su, a, b, c, d);

            //Console.WriteLine(lambda + "");   //打印‘(i,j,w,x)=>((i*j)+(w*x))’，z对应参数b，p对应参数a

            //Func<int, int, int, int, int> f = lambda.Compile();  //将表达式树描述的lambda表达式，编译为可执行代码，并生成该lambda表达式的委托；

            //Console.WriteLine(f(1, 2,3, 4) + "");  //打印2
            //Console.ReadKey();
            #endregion

            #region 队列初探

            Queue q = new Queue();//表示一个先进先出得集合
            q.Enqueue("A");//添加到队末
            q.Enqueue("B");
            q.Enqueue("C");
            q.Enqueue("D");

            Console.WriteLine("当前所有队列： ");
            foreach (string c in q)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();

            q.Enqueue("K");
            q.Enqueue("P");
            Console.WriteLine("当前所有队列： ");
            foreach (string c in q)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();
            Console.WriteLine("开始移除部分元素： ");
            string str = (string)q.Dequeue();//由于队列有先进先出得属性，所以删除数据时是删除队列得头数据
            Console.WriteLine("当前移除的元素为：{0}", str);
            str = (string)q.Dequeue();
            Console.WriteLine("当前移除的元素为：{0}", str);

            object[] obj = q.ToArray();//

            q.Clear();

            Console.ReadKey();
            #endregion

            #region 堆栈初探
            Stack st = new Stack();
            st.Push('A');
            st.Push('B');
            st.Push('C');
            st.Push('D');

            Console.WriteLine("当前所有堆栈： ");
            foreach (char c in st)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();
            st.Push('V');//在顶部插入一条数据
            st.Push('H');
            Console.WriteLine("堆栈中下一个可能执行的值： {0}", st.Peek());//返回顶部元素
            Console.WriteLine("当前所有堆栈： ");
            foreach (char c in st)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();
            Console.WriteLine("移除堆栈中顶部的值：{0}", st.Pop());//删除顶部元素
            //判断是否包含 ‘V’
            bool isContain = st.Contains('V');//Contains 查看堆栈中是否含有对象
            Console.WriteLine("当前所有堆栈： ");
            foreach (char c in st)
            {
                Console.Write(c + " ");
            }
            st.Clear();
            Console.ReadKey();
            #endregion

        }
    }
}
