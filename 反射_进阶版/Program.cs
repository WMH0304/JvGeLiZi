using System;
using System.ComponentModel;
using System.Reflection;


namespace 反射_进阶版
{
    /*
     * https://www.zhihu.com/search?type=content&q=c%23%E5%8F%8D%E5%B0%84
     * 
     * 
     *理解前思考
     * 
     * 什么是反射？
     * 
     * 1.官网解答：发射提供昂程序集，模块和类型的对象（Type 类型）。可以使用反射动态的创建类型的实例
     * 将类型绑定到现有对象，或从现有对象中获取类型，然后调用其它方法或访问器字段的属性，如果代码中使用类特性可以利用反射访问他们
     * 
     * 2.说明书解答： 指对程序集中的元数据进行检查的一个过程。
     * 
     * 
     * 3.就是透过现象看本质的一个过程
     * 
     * 4.地球内部结构:地球的内部结构大体可以分为三层:地壳、地幔和地核。
     * 如何在地球表面不用深入地球内部就知道其内部的构造呢?我们可以向地球发射“地震波”,
     * “地震波”分两种一种是“横波”,另一种是“纵波”。“横波”只能穿透固体,而“纵波”既可穿透固体又可以穿透液体。
     * 通过在地面对纵波和横波的反回情况,我们就可以大体断定地球内部的构造了。
     * 
     * 
     * 5.假设现在有一个内部构造未知的类 A，如果我想知道 A 的内部构造及实现 那么我就可以同过反射实现 
     * 
     * Type type = A.getype() ; //获取对象类型 
     * foreach(System.Reflection.PropertyInfo property in type.GetProperties())Console.WriteLine(反射对象的属性);
     * 
     * 大概就是这个样子
     * 
     * *
     * 反射的作用是什么？
     * 获取内部属性未知的类的内容
     * 
     * 他的适用场景是什么？
     * 可以使程序更加灵活，并且开发效率更高，不过这样地代价是带来性能地消耗，
     * 
     * 
     * 有什么优缺点？
     * 优点：
     * 1.提高了程序的灵活性，可扩展性，相应的会带来性能方面的消耗
     * 
     * 
     * *
     * 缺点：
     * 1.由于反射时的一切都是未知的，需要遍历对象 这样会带来性能上的消耗
     * 
     * 他的实现原理（本质）是什么？
     * 
     * 透过表象看本质。
     * 
     * 就是透过现象看本质的一个过程
     * 
     * 他的对立面是什么？正射？
     * 就是实例化对象了
     * 知道这个类的内部构造，使用他时就使用正射（实例化）
     * 
     * 和映射有关系嘛？
     *  
     *  映射是把对象的相对地址映射到指定的位置。
     *  更像是一个指针
     * 
     * 
     * 本质是什么？
     * 
     * 读取已编译成 cil 的元数据
     * 
     * 
     */

    #region 泛型类型上的反射
    /*
     * CLR 2.0 引入了在泛型类型上反射的能力。
     * 在泛型类型上执行运行时反射，可判断出类或方法是否包含泛型类型，以及他可能包含的任何类型参数
     * 
     * 
     * 
     * 
     */
    public class Genericity
    {

    
        public static void Genericity_main()
        {
            Type type;
            type = typeof(System.Nullable<>);
            //ContainsGenericParameters 判断类或方法 是否包含尚未设置的泛型参数
            Console.WriteLine(type.ContainsGenericParameters);
            //IsGenericType 指示类型是否是泛型的布尔属性
            Console.WriteLine(type.IsGenericType);
            type = typeof(System.Nullable<DateTime>);
            Console.WriteLine(type.ContainsGenericParameters);
            Console.WriteLine(type.IsGenericType);
        }

        

    }

    #endregion









    /// <summary>
    ///构造未知类
    /// </summary>
    public class NewTextClass
    {
        public delegate string Statt();
        public static event Statt Ddds;
        public string a;
        public int b;
        public string Name { get; set; }
        public int Age { get; set; }

        public NewTextClass(string aa, int bb)
        {
            a = aa;
            b = bb;
        }
        public NewTextClass()
        {
            Console.WriteLine("NewTextClass 构造函数");
        }

        public void show()
        {
            Console.WriteLine("一个对象" + a + b + this.Name + this.Age);
        }
    }

    public class GetTypes
    {

        public static void GetTypes_Main()
        {
            NewTextClass NewTextClass = new NewTextClass();


            // Type type = NewTextClass.GetType();


            //foreach (System.Reflection.PropertyInfo item in type.GetProperties()) //对属性的访问
            //foreach (System.Reflection.MemberInfo item in type.GetMembers()) //获取当前 Type 的成员（包括属性、方法、字段、事件等）。
            //foreach (System.Reflection.ConstructorInfo item in type.GetConstructors())//获取当前 Type 的构造函数。
            //foreach (var item in type.GetEvents())//获取由当前 Type 声明或继承的事件。
            //foreach (var item in type.GetFields())//获取当前 Type 的字段。

            // Console.WriteLine(item.Name);

            #region typeof
            /*
             * typeof 获得 type 对象的另一种方法 
             * 
             */
            // ThreadPriorityLevel  指定线程的优先级别。
            // Type types = typeof(NewTextClass);
            // foreach (var item in types.GetProperties())
            // Console.WriteLine(item.Name);

            #endregion

            #region 泛型集合反射
            //Stack<int> stack = new Stack<int>();
            //Type stack_type = stack.GetType();
            //foreach (Type item in stack_type.GetGenericArguments())

            //    //FullName 获取该类型的完全限定名称，包括其命名空间，但不包括程序集。
            //    Console.WriteLine(stack_type.FullName);
            #endregion


        }
        #region nameof

        /*
         * nameof 用于提供参数异常中的参数名称
         * 
         * 从技术上讲 nameof 不是反射 但他可以接收程序集的数据
         * 为社么不直接使用异常消息提示呢？还要大费周章地指定一个 nameof
         * 
         * 使用 nameof 操作符有两个优点
         * 1.c# 编译器保证了传给nameof 操作符地实参是一个有效地编程标识符，能有效地方式在编程标识符改变时反射错误
         * 2.相比于支持字面量字符串，ide 能更好地支持 nameof 操作符
         * 
         * 
         */
        class Nameofs : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            public Nameofs(string name)
            {

            }
            private string _Name;
            public string Name
            {
                get { return _Name; }
                set
                {
                    if (_Name != value)
                    {
                        _Name = value;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                    }
                }
            }
        }
        #endregion




    }








    #region 启动类
    class Program
    {
        static void Main(string[] args)
        {
            //GetTypes.GetTypes_Main();

            //Genericity.Genericity_main();

            Type type = GetType("反射_进阶版.ReflectionTest");



            /***************/
            Console.ReadLine();
        }

        public static Type GetType(string s)
        {
            Assembly assembly = Assembly.Load("mscorlib.dll");
            Type type = assembly.GetType(s, true, false);
            return type;
        }


        class ReflectionTest
        {
            private void Test()
            {
                Console.WriteLine("123");
            }
        }


        #endregion
    }
}
