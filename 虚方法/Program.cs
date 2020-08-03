using System;

namespace 虚方法
{
    class Program
    {
        public  void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            a();
        }

      
     public   virtual void a()
        {
            Console.WriteLine("1");
            b();
            c();
        }

        void b()
        {
            Console.WriteLine("2");
        }


        void c()
        {
            Console.WriteLine("3");
        }
    }
}
