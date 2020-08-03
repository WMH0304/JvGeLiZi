using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace 多线程
{
    class Program
    {
      static  void Main(string[] args)
        {
          
          stase();
        }

     static   void stase()
        {
            teach();
            teach(); 
            teach();
            teach();
            teach();
            teach();


            Console.WriteLine("haosilakfdnaokfmakdmflkamdf");

            TaskFactory taskFactory = new TaskFactory();

            List<Task> tasks = new List<Task>();
            tasks.Add(taskFactory.StartNew(() => asd()));
            tasks.Add(taskFactory.StartNew(() =>asd()));
            tasks.Add(taskFactory.StartNew(() => asd()));
            tasks.Add(taskFactory.StartNew(() =>asd()));
            tasks.Add(taskFactory.StartNew(() =>asd()));
            tasks.Add(taskFactory.StartNew(() =>asd()));
            Console.WriteLine("帅气的我");
            //  Thread.Sleep(20000);//不确定执行完成时间，不用
            Task.WaitAny(tasks.ToArray(),1000);
            Task.WaitAll(tasks.ToArray());
            //ContinueWhenAll  非阻塞式的回调，所有任务完成后可能会开启一个新的线程，也可能时刚完成任务的线程
            //taskFactory.ContinueWhenAll(tasks.ToArray(), t => { Console.WriteLine("oewjmrfokameokfmokew"); });

            //
            taskFactory.ContinueWhenAny(tasks.ToArray(), t => { Console.WriteLine("最先完成线程"); });
        }

        static void asd()
        {
            Console.WriteLine($" 我hai一名广东靓仔。");

            long lresd = 0;
            for (int i = 0; i < 1000_000; i++)
            {
                lresd += i;
            }
            Console.WriteLine($"我hai一名广东靓仔。.");
        }









        static void teach()
        {
            Console.WriteLine($" 我是一名广东靓仔。");

            long lresd = 0;
            for (int i = 0; i < 1000_000; i++)
            {
                lresd += i;
            }
            Console.WriteLine($"我是一名广东靓仔。.");
        }
    }
}
