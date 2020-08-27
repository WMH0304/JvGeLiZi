using System;
using System.Diagnostics;

namespace 特性_终极版
{
    /* *
     * 
     * 
     * MADN的定义：公共语言运行时允许添加类似关键字的描述声明，叫做attribute，它对程序中的元素进行标注，
     * 如类型、字段、方法和属性等。Attribute和Microsoft . Net Framework文件的元数据（metadata）保存在一起，
     * 可以用来向运行时描述你的代码，或者在程序运行时影响程序的行为。 
     * 
     * 
     * 什么是特性？
     * 1.特性（attribute） 是继承自  attribute 类的基类
     * 2.所有继承自  attribute 的类都可以称之为特性
     * 3.特性是一种标识
     * 4.类似于 public static 之类的修饰符
     * 5.声明 特性的类要用  Attribute 做后缀
     * 6.创建特性是 C# 默认允许对所有可能的特性目标使用特性‘
     * 7.限制特性（用魔法打败魔法）用特性限制特性
     * 8.特性只做元数据使用（也就是说 特性可以被反射？（因为反射就是作用在元数据上的））
     * 特性的作用是什么？ 
     * 
     * 使用场景是什么？
     * 特性可用于多个目标
        程序集(assembly)
        模块(module)
        类型(type)
        属性(property)
        事件(event)
        字段(field)
        方法(method)
        参数(param)
        返回值(return)，
     * 
     * 属性和特性有什么联系？
     * 毫无联系。
     * 属性实际上就是一个用于存储数据的容器，而特性更多的是一个说明（声明）
     * 
     * 他的优缺点是什么？
     * 优点：
     * 1.使程序更容易读懂
     * 2.省去许多冗余注释
     * 缺点：
     * 他呼吸了。
     * 
     * 他的本质是什么？
     * 本质上是一个类，一个派生自 Attribute 的类
     * 
     * 
     * 
     */
    [CommandLineInfo(Name ="特性进阶",Date ="2020-8-26",Description ="天地无极，乾坤借法")]
     class Attributes
    {
        public static void Attributes_main()
        {
            Type type = typeof(Attributes);
            //检索应用于程序集、模块、类型成员或方法参数的指定类型的自定义属性。
            CommandLineInfoAttribute c = (CommandLineInfoAttribute)Attribute.GetCustomAttribute(type, typeof(CommandLineInfoAttribute));
            Console.WriteLine(c.Name);
            Console.WriteLine(c.Date);
            Console.WriteLine(c.Description);
            c.Show();

            Method_A();
            Method_B();
        }



        /// <summary>
        /// ConditionalAttribute 定义无操作的特性
        /// </summary>
        [Conditional("CONDTION_A")]
        public static void Method_A()
        {
            Console.WriteLine("CONDTION_A");
        }
        [Conditional("CONDTION_B")]
        public static void Method_B()
        {
            Console.WriteLine("CONDTION_B");
        }
    }
    class CommandLineInfoAttribute : Attribute
    {
      public string Name { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public void Show()
        {
            Console.WriteLine("名称：{0} 日期：{1} 描述：{2}", Name, Date, Description);
        }


    }













    #region 启动类
    class Program
    {
        static void Main(string[] args)
        {
            Attributes.Attributes_main();

            Console.ReadLine();
        }
    }

    #endregion
}
