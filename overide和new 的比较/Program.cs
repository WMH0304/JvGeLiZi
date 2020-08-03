using System;

namespace overide和new_的比较
{
    class Program
    {
        public class A
        {
            public void DisplayName()
            {
                Console.WriteLine("A");
            }
        }
        public class B :A
        {
            public virtual void DisplayName()//虚方法声明
            {
                Console.WriteLine("B");
            }
        }
        public class C : B
        {
            public override  void DisplayName()//重写父类的虚方法
            {
                Console.WriteLine("C");
            }
        }
        public class D:C
        {
            public new  void DisplayName() //
            {
                Console.WriteLine("D");
            }
        }


        static void Main(string[] args)
        {
            D d = new D();
            C c = d;
            B b = d;
            A a = d;

          
            d.DisplayName();
            c.DisplayName();
            b.DisplayName();
            a.DisplayName();



            /*
             * 实验结果如下
             * D   d.DisplayName(); D
             * C
             * C  
             * A
             */
        }
    }
}
