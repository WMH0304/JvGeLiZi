using System;
using System.Web.Http.Common;
using System.Net.Cache;

namespace net_页面传值方法
{
    static class num
    {
        static public string c { get; set; } = "sdfasf";
    }
 
    class Program
    {
        //https://www.jb51.net/article/103337.htm
        static void Main(string[] args)
        {
            // string str = Request.
            num.c = "aaaaaa";
            Test test = new Test();

            



                  Test test1 = new Test("sdf");


        }
        /*
         页面传值本质上就是类与类之间的传值 大体分为两种思路 依赖中间商，或者直接牵手传输
        1.static 通过静态传值
        2.在目标类上面创造可以接受值的参数
        3.session 通过存放到一个临时内存中 传值 web 使用 
        4.Application 使用全局对象 使用简单，但容易混淆
        5.cache  写入缓存中
         */

    }
    class Test
    {
       public  Test()
        {
            Console.WriteLine(num.c);
        }
        public Test(string t)
        {
            Console.WriteLine(t);
           
           
        }

    }



}
