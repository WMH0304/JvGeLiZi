using System;

System.Console.WriteLine("asdf");
namespace Csharp_语言版本Test
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    Console.WriteLine("Hello World!");
        //    System.Random random = new Random();
        //    var c = random.Next(100);

        //    System.Console.WriteLine(int_test(c));

        //    var test = new MyClass() { age = "dsaf", name = "sdaf" };
        //}

        public static int int_test(int num)
        {
            string s = "asdfasdfsd";
            var bo = false;
          var tt=   s.Substring(num, 1);
            var t = num switch
            {
                >= 10 and <=20 => num+10,
                >30 and <=35 =>num +1,
               _=>num%2
            };

            return num;
        }

        class MyClass
        {
           public string name { get; init; }

            public string age { get; init; }
        }
    }
}

