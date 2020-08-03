using System;

namespace c井_方法重载
{




    public class BaseA
    {
        public static MyTest a1 = new MyTest("a1");

        public MyTest a2 = new MyTest("a2");

        static BaseA()
        {
            MyTest a3 = new MyTest("a3");
        }

        public BaseA()
        {
            MyTest a4 = new MyTest("a4");
        }

        public virtual void MyFun()
        {
            MyTest a5 = new MyTest("a5");
        }
    }

    public class BaseB : BaseA
    {
        public static MyTest b1 = new MyTest("b1");

        public MyTest b2 = new MyTest("b2");

        static BaseB()
        {
            MyTest b3 = new MyTest("b3");
        }

        public BaseB()
        {
            MyTest b4 = new MyTest("b4");
        }

        public new void MyFun()
        {
            MyTest b5 = new MyTest("b5");
        }
    }

    static class Program
    {
        static void Main()
        {
            BaseB baseb = new BaseB();
            baseb.MyFun();
        }
    }

    public class MyTest
    {
        public MyTest(string info)
        {
            Console.WriteLine(info);
        }
    }
  
    
}
