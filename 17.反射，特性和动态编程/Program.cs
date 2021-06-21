using System;
using System.Collections.Generic;
using System.Reflection;
/*
 * AssemblyFileVersion 指定文件版本。
 * 
 * AssemblyTitle 程序集标题。 
 * 
 */
[assembly :AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyTitle("Test")]
namespace _17.反射_特性和动态编程


/**。。。
 *说明书上说这一章主要将了8个知识点
 * 
 * 1.访问元数据
 *   1.1 gettype()这个方法以前使用类获取对象的类型的
 * 2.成员的调用
 * 3.泛型上的反射
 * 4.自定义特性
 * 5.特性构造器
 * 6.命名参数
 * 7.预定义特性
 * 8.动态编程
 * 
 * 资料： https://www.cnblogs.com/sosoft/p/3506015.html
 * 
 * 
 */
{
    public class test
    {
        public string str { set; get; }
        public float flo { set; get; }
        public double dou { set; get; }
        //特性修饰属性
        [Obfuscation]
        public string te { set; get; }
    }

    #region 启动类
    class Program
    {

        #region 反射


        #region java中的反射
        /*
         * Java中的反射机制也是在执行状态中的
         * 一般来说，一个程序运行有两个时期，之前也有讲过 一个编译期：将Java代码编译成class文件 和 c sharp 的类似，不过他是编译成中间层语言IL 
         * 
         * 一个是 运行期：程序编译后执行时期，简单的来说就是将编译后的文件交给计算机执行，知道程序结束
         * 特点：
         * 1.和csharp 类似 
         * 2.对于任何一个类，都能知道这个类的方法和属性、
         * 3.对于任何一个对象，都能调用他的方法和属性。
         * 
         */
        #endregion

        /*
         * 什么是反射呢？
         * 
         * 1.反射是指对程序集中的元数据进行检查的过程
         * 
         * 2.反射是指在程序运行时可以动态地创建对象用于访问其他对象地类型
         * 
         * 3.反射类似于接口，在引用对象中都没有实体
         * 
         * 4.一般来说在程序中要用到某个类地话，需要知道这个类所在地命名空间（可以理解为绝对路径），
         * 然而如果用到反射只需要知道对象类就能直接调用（相对路径）
         *
         * 5.这个是一种动态调用的体现，结合本章的要讲的内容可知
         * 
         * 6.包括特性也是一种动态机制，不过貌似特性这玩意好像不需要通过编译
         * 更多的是对某一个对象的说明
         * 
         * 
         * 原理是什么？
         * 
         * 通俗的说就是 对 对象所支持的 方法 属性 接口 的遍历，选取并调用。
         * 
         * 基于编译时通过成员名称（元数据（元数据被定义为：描述数据的数据，对数据及信息资源的描述性信息。））
         * 调用成员，进而在执行时实现动态绑定
         * 可以在执行时调用未知目标。
         * 
         * 
         * 
         * 和映射有什么关系？
         * 
         * 为什么要用到他？
         * 可以使程序更加灵活，并且开发效率更高，不过这样地代价是带来性能地消耗，
         * 因为反射他的原理类似于在一大堆东西里面查询出你想要的那个然后将他的每个成员都反射到对象上
         * 
         * 有什么优缺点？
         * 优点：
         * 1. 反射提高了程序的灵活性和扩展性
         * 2. 降低耦合性，提高自适应能力
         * 3. 他允许程序创建和控制任何类的对象，，无需提前应变嘛目标类 （动态特点）
         * 
         * 缺点：
         * 1.性能问题： 使用反射基本上是一种解释操作，用于字段和方法接入是要远慢于直接代码（原因：调用方法或属性未知，需要遍历对象）
         * 因此反射机制主要用于应用在对灵活性和拓展性要求很高的系统框架上，普通程序不建议使用。
         * 
         * 2.使用反射会模糊程序内部逻辑；程序员希望在源代码中看到程序的逻辑，反射却绕过了源代码的技术，
         * 因而会带来维护的问题，反射代码比相应的直接代码更复杂
         * 
         * 有没有类似的可以代替的呢？
         * 
         * 
         */
        #endregion
        static void Main(string[] args)
        {
             _Gettype();
            // _typeof();
            //泛型反射();
            //泛型类型反射();
            //  _nameof();
            // 特性_1();
            Console.ReadLine();
        }


        #region 反射
        public static void _Gettype()
        {   //反射 datetime 数据类型
            DateTime dateTime = new DateTime(); //实例对象
            Type type = dateTime.GetType();//获取数据类型
            /*
             * Reflection 反射
             * System.Reflection 命名空间包含通过检查托管代码中程序集、模块、成员、
             * 参数和其他实体的元数据来检索其相关信息的类型。
             * 这些类型还可用于操作加载类型的实例，
             * 例如挂钩事件或调用方法。
             * 
             *  PropertyInfo 发现属性的特性并提供对属性元数据的访问。
             *  
             *  GetProperties  返回当前 Type 的所有公共属性。
             */
            //System.Reflection.MemberInfo property in type.GetMethods()
            List<MethodInfo> n = new List<MethodInfo>();
           // Dictionary<string, MethodInfo> mi = new Dictionary<string, MethodInfo>();
                foreach (var item in type.GetMethods())
            {
                n.Add(item);
              //  mi.Add(item.Name,item);
                Console.WriteLine(item.Name);
            }
            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                /*
                 * System.Reflection.MemberInfo property in type.GetMethods()
                 * 反射 type 对象的方法
                 */
                Console.WriteLine(property.Name);
            }
        }

        public int sampleMember;
        public void SampleMethod() { }
        public static void _typeof()
        {
            //动态获取对象类型
            Type t = typeof(Program);//装箱操作
            // Alternatively, you could use
            // Program obj = new Program();
            // Type t = obj.GetType();
            Console.WriteLine("Methods:");
            /*
             *  MethodInfo 获取有关成员特性的信息并提供对成员元数据的访问。
             *  
             *  GetMethods  获取对象方法 返回当前 Type 的所有公共方法。
             *  
             *  GetMembers 获取当前 Type 的成员（包括属性、方法、字段、事件等。
             */
            MethodInfo[] methodInfo = t.GetMethods();
            /**/
            foreach (MethodInfo mInfo in methodInfo)
                Console.WriteLine(mInfo.ToString());
            Console.WriteLine("Members:");
            ///元数据 创建一个存储元数据的数组
            MemberInfo[] memberInfo = t.GetMembers();
            foreach (MemberInfo mInfo in memberInfo)
                Console.WriteLine(mInfo.ToString());
        }

        public static void 泛型反射()
        {
            Type type = typeof(System.Nullable<>);//反射泛型类

            Console.WriteLine(type.ContainsGenericParameters);//ContainsGenericParameters  获取一个值，该值指示当前系统是否。类型对象具有未被特定类型替换的类型参数。
            Console.WriteLine(type.IsGenericType);//IsGenericType 获取一个值，该值指示当前类型是否为泛型类型。
        }

        public static void 泛型类型反射()
        {
            Stack<int> vs = new Stack<int>();
            Type type = vs.GetType();

            // GetGenericArguments 返回一个系统数组。表示的类型参数的类型对象一个封闭的泛型类型或泛型类型定义的类型参数。
            foreach (Type item in type.GetGenericArguments())
            {
                Console.WriteLine(type.FullName); //FullName 获取类型的完全限定名，包括其命名空间，但不包括其组装。
            }



        }



        #endregion

        public static void 特性初探_1()
        {
            //报错原因：原方法有声明了一个不可使用的特性。
            //string str = texing.LegacyReverseString("dsojkfn");

        }
        static void 特性_1()
        {
            特性_2(typeof(OldClass));
            Console.WriteLine("=============");
            特性_2(typeof(NewClass));
            Console.ReadKey();


        }

        public static void 特性_2(Type type)
        {
            Author_Attribute author_Attribute = (Author_Attribute)Attribute.GetCustomAttribute(type, typeof(Author_Attribute));
            if (author_Attribute == null)
            {
                Console.WriteLine(type.ToString() + "类中自定义特性不存在！");
            }
            else
            {
                Console.WriteLine("特性描述:{0}\n加入事件{1}", author_Attribute.Discretion, author_Attribute.date);
            }
        }


        #region nameof操作符
        /*
         * nameof 用于提供参数异常中的参数名称
         * nameof的优点
         * 1.防止在编程标识符名称改变时发生错误，有助于防止拼写错误，
         * 2.相比于支持字面量字符串，ide 工具可以更好的支持nameof 操作符
         * 
         */
        static void _nameof()
        {
            test test = new test();


            Console.WriteLine(nameof(test.str));
            Console.WriteLine(nameof(test.dou));//
            //等价于 //和语法糖类似
            Console.WriteLine("str");

        }

        #endregion



    }

    #region 特性
    /*
     * https://www.cnblogs.com/roucheng/p/cstexing.html
     * 
     * 
     * 特性是什么？
     * 1.将额外数据关联到属性（以及其他构造）的一种方式
     * 2.可以使用特性来修饰类，接口，结构，枚举，委托，事件
     * 3.是一个特殊的类，因为他继承与 attribute类（抽象类，在反射的命名空间下）
     * 4.如果说反射只是把对象的类型映射到目标对象上的话，那么特性就是将一组特定的关系绑定到目标对象上面
     * 5.特性存在于 反射类的下面
     * 他和反射有什么联系？
     * 1.一个是开空头支票，啥都没有
     * 2.一个是口头承诺，有关系。
     * 
     * 他的本质是什么？
     * 将额外数据关联到属性（以及其他构造）的一种方式
     *  官方解释：特性是给指定的某一声明的一则附加的声明性信息。 
     *  允许类似关键字的描述声明。它对程序中的元素进行标注，如类型、字段、方法、属性等。
     *  
     * 他适用于什么场景？
     *  1.不依赖于选项名称与属性名称的完全匹配
     *  2.利用特性可以指定与被修饰的构造有关的额外元数据
     *  3.使用特性可以将一个属性修饰为 required 并提供 /? 选项别名
     * 他和谁类似？是否可以被取代？
     * 
     * 他的优点和缺点分别是什么？
     * 
     * 还有就是，特性和属性的异同在哪里（盲猜应该和属性有一些些关联）？
     * 就是对属性，方法，接口等等等等的修饰，由于他是一种关系式的，zuo
     * 
     * 
     */
    static class texing
    {
        public static string ReverseString(string str)
        {
            Console.WriteLine("call reverseString");
            return "";
        }
        /// <summary>
        /// 为这个方法标注一个不能使用的特性
        /// 如果在其他地方调用这个方法，ide 提示错误。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [Obsolete("this legacy method should not be used", true)]

        public static string LegacyReverseString(string str)
        {
            Console.WriteLine("call legacyReverseString");
            return "";
        }
    }

    #region 自定义特性
    /*
     * System.AttributeTargets.Class | AttributeTargets.Struct
     * 指定特性修饰对象类型
     * 
     * AllowMultiple =true   声明特性是否可以多次使用， true 为允许多次使用。
     */
    [System.AttributeUsage(System.AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
    public class Author_Attribute : Attribute
    {
        private string discretion;

        public string Discretion
        {
            get { return discretion; }
            set { discretion = value; }
        }
        public DateTime date;
        public Author_Attribute(string discretion)
        {
            this.discretion = discretion;
            date = DateTime.Now;
        }
    }
    [Author_("这个类将过期")]//使用定义的新特性
    class OldClass
    {
        public void OldTest()
        {
            Console.WriteLine("测试特性");
        }
    }
    class NewClass : OldClass
    {
        public void NewTest()
        {
            Console.WriteLine("测试特性的继承");
        }
    }


    #endregion
    /*
     * 
     */
    // [AttributeUsage(AttributeTargets.Class,Inherited =false,AllowMultiple =false)]





    #endregion
    #endregion

}
