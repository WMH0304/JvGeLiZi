using System;
using System.Collections.Generic;
using System.Linq;

namespace Linq一对多
{
    class Program
    {
        public class A
        {
            public int AID { get; set; }
            public string Class { get; set; }
        }

        public class B
        {
            public int BID { get; set; }
            public string BName { get; set; }
            public int AID { get; set; }
        }


        static void Main(string[] args)
        {
            List<A> A = new List<A>()
            {
                new A(){ AID = 1, Class="班级1" },
                new A(){ AID = 2, Class="班级2" },
            };

            List<B> B = new List<B>()
            {
                new B(){ BID = 1 , BName = "学生1", AID=1 },
                new B(){ BID = 2 , BName = "学生2", AID=2 },
                new B(){ BID = 3 , BName = "学生3", AID=1 },
                new B(){ BID = 4 , BName = "学生4", AID=2 },
            };

            var lastResult = (from p in A
                             join q in B.GroupBy(x => x.AID).Select(x => new { Key = x.Key, Value = string.Join("=>", B.Where(y => y.AID == x.Key).Select(y => y.BName)) })
                             on p.AID equals q.Key
                             select new
                             {
                                 CLASS = p.Class,
                                 Name = q.Value,
                             }).ToList();

            foreach (var item in lastResult)
            {
                Console.WriteLine(item);
            }
        }
       

       
    }
}

