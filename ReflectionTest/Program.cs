using System;
using System.Reflection;

namespace ReflectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.Load("ReflectionTest");//锁定程序集
            Type type = assembly.GetType("ReflectionTest.Reflections");//反射类型
            Reflections kiba2 = (Reflections)Activator.CreateInstance(type);//打开对象

            MethodInfo ii = Assembly.Load("ReflectionTest").GetType("ReflectionTest.Reflections").GetMethod("ptintName"); ;
            //反射内部方法
            MethodInfo i = type.GetMethod("ptintName");

           
            object kiba = assembly.CreateInstance("ReflectionTest.Reflections");
            object[] pmts = new object[] { "Kiba518" };
            //执行方法

            Reflections reflections = new Reflections();
            reflections.str = "234134";
            object n = type.GetProperty("str");

            //PropertyInfo propertyInfo =("")
            i.Invoke(kiba, pmts);
            ii.Invoke(kiba, pmts);
            Console.WriteLine();
            Console.WriteLine(Convert.ToString(n));
        }


        static Type GetType0(string s)
        {
            Assembly assembly = Assembly.Load("ReflectionTest");
            Type type = assembly.GetType(s, true, false);
            return type;
        }

        static Type GetType1(string fullName)
        {
            Type t = Type.GetType(fullName);
            return t;
        }

    }



    class Reflections
    {
        public string str { get; set; } 

        public string strs { get; set; } 

        public void ptintName(string s)
        {
            Console.WriteLine(s);
        }

        public Reflections()
        {
            Console.WriteLine("大王叫我来巡山");
        }
    }

}
