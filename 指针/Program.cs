using System;

namespace 指针
{
    class Program
    {
        unsafe static void mm(int* p)
        {
            *p *= *p;
        }

        unsafe  static void Main()
        {
            int i = 9;
            mm(&i);
            Console.WriteLine(i);
        }

      
    }
}
