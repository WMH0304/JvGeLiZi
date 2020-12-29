using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Linq一对多
{

    #region Select() 和 SelectMany() 的区别
    /*
     * select  总原始数据中筛选 出目标数据 
     * 1.select 可以一边投射一边转换，且项的数量不会发生改变
     * 
     * SelectMany 会从 select 过的数据中 整合到新集合里
     * 1.遍历 select 过的数据 标识每一项，并存放到一个新集合中
     * 
     * 总的来所
     * 
     * select 筛选数据
     * 
     * selectmany 整合数据
     * 
     * * 
     */
     class Select_selectmany
    {
      public  static void Select_selectmanys()
        {
            string[] text = { "dadf af", "dsfa asdf", "asdff as" };
            //Select 筛选
            var tokens = text.Select(s => s.Split(' '));
            foreach (string [] item in tokens)
            {
                foreach (string iem in item)
                {
                    Console.WriteLine(iem);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Selectmany");
            Console.WriteLine();
            //SelectMany 筛选并整合
            var to = text.SelectMany(d => d.Split(' '));
            foreach (var item in to)
            {
                Console.WriteLine(item);
            }
        }
    }


    #endregion



    public class Patent
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }
    }
    public class Inventor
    {
        /// <summary>
        /// id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
    }

    #region 启动类
    class Program
    {
        #region Linq一对多
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
        #endregion
        /// <summary>
        /// 启动方法
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            #region MyRegion
            Select_selectmany.Select_selectmanys();
            #endregion

            #region   Linq一对多

            /*   List<A> A = new List<A>()
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
               }*/

            #endregion

            #region 数据添加
           // Data_count();
            #endregion

            #region Linq 并行查询
            /*
             * AsParallel()方法
             * 一边查询一边返回数据（类似于并行集合的队列集合管道）
             */
            #endregion

            /****************************************************/
            Console.ReadKey();
        }
         static void Data_count()
        {
            //数据添加，并枚举出来
            IEnumerable<Patent> patents = PatentData.Patents;
            Print(patents);
            IEnumerable<Inventor> inventors = PatentData.Inventors;
            Print(inventors);
        }
        private static void Print<T>(IEnumerable<T> ts)
        {
            foreach (T item in ts)
            {
                Console.WriteLine(item);
            }
        }
        /// <summary>
        /// 数据添加
        /// </summary>
        public static class PatentData
        {
            public static readonly Inventor[] Inventors = new Inventor[]
            {
                new Inventor(){ID =1,Name ="one"},
                new Inventor(){ID =2,Name ="two"},
                new Inventor(){ID =3,Name ="three"},
                new Inventor(){ID =4,Name ="fire"}

            };
            /// <summary>
            /// 数据添加
            /// </summary>
            public static readonly Patent[] Patents = new Patent[]
            {
            new Patent(){Title ="1111",Contents="1111"},
            new Patent(){Title ="2222",Contents="2222"},
            new Patent(){Title ="3333",Contents="3333"},
            new Patent(){Title ="4444",Contents="4444"},
            };
        }
    }
    #endregion
    #region Select 和 where 的区别
    /*
     * 解释这两个方法前我们可以先将集合想象成一个数据表
     * 
     * 
     * Select  横向 影响的是数据表的列 （影响数据表的列的数量）
     * 1.Select 的原理和 SQL 中的 select 原理非常相似  返回查询数据项的列（横向）
     * 2.select 查询原理是将符合 条件的 数据项 投射到一个新的集合中（也就是所这个集合存储的是数据项的相对地址）
     * 3 实现了IEnumerable 接口 支持迭代
     * 
     * Where  纵向 影响的是数据表的数据的条数
     *  
     *  或者说是减少数据表中 项的数量
     */
    #endregion
}

