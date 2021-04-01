using System;
using System.Collections.Generic;
using System.Linq;

namespace lambda进阶
{
    class Program
    {
        class Test
        {
            public int id { get; set; }

            public string name { get; set; }
        }

        static void T(Func<string, string> func)
        {
            var name = "大海";
            var og = func(name);//给委托传参

        }
        static void Tv(Func<string,string,string> func)
        {
            var name = "二海";
            var cs = "go";
            var og = func(name,cs);
        }
        static string Ts(string name)
        {
            return name + "test";
        }

        static string Tvs(string name,string go)
        {
            return name + "喜欢" + go;
        }


        static void Main(string[] args)
        {
            /* 1.什么是lambda？
             * 基于匿名方法（包括匿名委托）实现的语法糖
             * lambda 可以分为两种结构
             * 第一种是 lambda  单语句形式  (input-parameters) => expression
             * (input-parameters) 是传入的参数  expression 是表达方程式（表达主体）
             * 
             * 第二种式 lambda 多语句形式  (input-parameters) => { <sequence-of-statements> }
             * 多语句形式的表达主题要用 花括号包含
             * 
             * 2.他的实现原理是什么？
             *  通过匿名的方式可以忽略返回的类型
             * 3.使用它可以带来什么？
             * 可以让代码更加简洁，优雅
             * 4.它的使用场景是什么？
             * 结合linq 使用 对集合进行操作
             * 5.它的替代方案是什么？
             * 例如对集合的操作，可以使用 sql 实现 
             * 对求值可以用方程式实现
             * 
             * 
             */

            //定义一个委托 接受 string  返回string


            /*
            delegate (string name)
            {
                return name + "test";
            };
            */
            //委托
            T(Ts); 

            //匿名委托 1.
            T(delegate (string name)
            {
                return name + "test";
            });
            //ide 可以做类型推断 当把1. 传入T 的时候 ide可以根据方法的参数类型做推断 所以可以把委托类型去掉
            T((string name) =>{ var na = name + "test"; return na; });
            //再进一步 既然委托类型都可以推断了，那么参数类型一定也可以推断所以我们又可以把 给委托传参的参数类型的类型也去掉
            T(nx => nx + "tee");
            //


            Tv(Tvs);

            Tv(delegate(string name,string cs) { return name + "喜欢" + cs; });

            Tv((string name,string cs) => { var na = name + "test "+cs; return na; });

            Tv((x, test) => x + test + "fdsf");
            /*
            delegate (string name)
            {
                return name + "test";
            };
            */



            List<string> l1 = new List<string>()
            {
                "是","的","发","给","和","就","下","从","v",
            };
            List<int> l2 = new List<int>()
            {
            7,8,9,10
            };
            List<int> l3 = new List<int>()
            {
                1,2,3,4,5,6,7,8,9,10
            };
            List<Test> m1 = new List<Test>()
            {
               new Test(){id =1,name ="one"},
               new Test(){id =2,name ="two"},
               new Test(){id =3,name ="three"},
               new Test(){id =4,name ="four"}

            };
            List<Test> m2 = new List<Test>()
            {
               new Test(){id =1,name ="sadf"},
               new Test(){id =5,name ="two"},
               new Test(){id =3,name ="df"},
               new Test(){id =7,name ="four"}

            };
            /*
             lambda 查询表达式
             */
            var t0 = from t in l2 where t > 5 select t;

            var t1 = from t in l2 select t;

            var t2 = from a in l2 from b in l3 select a * b;

            var t3 = from a in l3 let i = a % 2 == 0 ? true : false where i select a;

            var t4 = from a in l3 orderby a descending select a;//倒序

            var t5 = from a in l3 group a by a % 2;

            var t6 = from a in l3 group a by a % 2 into Ay from b in Ay select Ay;

            var t7 = from a in l3 join b in l2 on a equals b select a;

            var t8 = from a in m1
                     join b in m2 on a.id equals b.id into c
                     from d in c.DefaultIfEmpty()
                     select new
                     {
                         id = a.id,
                         name = a.name,
                     };

            var t9 = m1.GroupJoin(m2, c => c.id, g => g.id, (c, g) => new
            {
                id = c.id,
                name = c.name,
            });
            //foreach (var item in t9)
            //{
            //    Console.WriteLine(item.id+" " +item.name);
            //}
            GroupJoinEx1();
        }
        public static int test(int a, int b) => a * b; //等价于

        public static int tt(int a, int b)
        {
            return a * b;
        }


        public static void GroupJoinEx1()
        {
            Person magnus = new Person { Name = "Hedlund, Magnus" };
            Person terry = new Person { Name = "Adams, Terry" };
            Person charlotte = new Person { Name = "Weiss, Charlotte" };

            Pet barley = new Pet { Name = "Barley", Owner = terry };
            Pet boots = new Pet { Name = "Boots", Owner = terry };
            Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
            Pet daisy = new Pet { Name = "Daisy", Owner = magnus };

            List<Person> people = new List<Person> { magnus, terry, charlotte };
            List<Pet> pets = new List<Pet> { barley, boots, whiskers, daisy };

            // Create a list where each element is an anonymous
            // type that contains a person's name and
            // a collection of names of the pets they own.
            var query =
                people.GroupJoin(pets,
                                 person => person,
                                 pet => pet.Owner,
                                 (person, petCollection) =>
                                     new
                                     {
                                         OwnerName = person.Name,
                                         Pets = petCollection.Select(pet => pet.Name)
                                     });

            foreach (var obj in query)
            {
                // Output the owner's name.
                Console.WriteLine("{0}:", obj.OwnerName);
                // Output each of the owner's pet's names.
                foreach (string pet in obj.Pets)
                {
                    Console.WriteLine("  {0}", pet);
                }
            }
        }

    }

    class Person
    {
        public string Name { get; set; }
    }

    class Pet
    {
        public string Name { get; set; }
        public Person Owner { get; set; }
    }



    /*
     This code produces the following output:

     Hedlund, Magnus:
       Daisy
     Adams, Terry:
       Barley
       Boots
     Weiss, Charlotte:
       Whiskers
    */
}
