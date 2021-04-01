using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            double s = 223.4;
            string f = "kdf";
            string ss = "3242";
            object sss = "3234";
            object ssss = "24234";
            int a = int.Parse(ss);//string 转 int 
            int aa = Convert.ToInt32(sss);
            int aaa = (int)s;
            double b = (double)aaa;
           // int e = int.Parse(f.ToString());
            //Console.WriteLine(e);
            Console.WriteLine(a);
            Console.WriteLine(aa);
            Console.WriteLine(aaa);
            Console.WriteLine(b);
        }
    }



}
