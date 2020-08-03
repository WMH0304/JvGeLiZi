using System;

namespace 基本两个粒子
{
    class Program
    {
        static void Main(string[] args)
        {
            String s1 = new String("abcde");
            String s2 = new String("abcde");
            Boolean b1 = s1.Equals(s2);
            var b2 = s1 == s2;
           Console.WriteLine(b1 + "   " + b2);
        }
    }
}
