using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace 事务_Csharp
{
    class Program
    {
        /*
         1.什么事务？
        事务实际上就是一个操作方式（行为方式），限制提交的数据（如果提交数据时出现异常就会回滚到提交数据钱），可以理解为一根绳上的蚂蚱，一荣俱荣，一损俱损
         2.事务可以做什么？
        可以保证数据的完整性，统一性
         3.使用事务可以带来什么？
        参考2
         4.有没有可以替代事务的解决方案？
        他是一个行为，行为可以复制，但无法替代，至少现在还没有找到可以替代的解决方案
         5.使用事务需要注意哪些细节？
         6. 事务的种类有哪些？
         一个是操作数据库的事务 
         一个是操作内存数据的事务
         7. 我就是单纯喜欢这个 7

         */

        class Test
        {
            public int id { get; set; }

            public string name { get; set; }

            public int old { get; set; }

            public bool sex { get; set; }//false 是男
        }

        static void Main(string[] args)
        {
            List<Test> tests = new List<Test>
            {
                new Test{id=1,name ="大王叫我来巡山",old =77,sex =false}
            };

          
            //TransactionScope 事务标示
            using (TransactionScope transaction = new TransactionScope(new Transaction()) )
            {
                try
                {
                    var te = tests.Where(c => c.id == 1).Single();
                    te.id = 7;
                    te.name = "dad";
                    te.old = 8;
                    te.sex = true;

                    Random random = new Random();
                    int rd = random.Next(1, 7);
                    if (rd %2==0)
                    {
                        throw new Exception();
                    }
                    transaction.Complete();//提交事务
                }
                catch (Exception)
                {

                    transaction.Dispose();
                }
             
            }

            var test = tests;
            
        }
    }
}
