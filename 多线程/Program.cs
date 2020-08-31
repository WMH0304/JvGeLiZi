using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace 多线程
{
    /**基础术语
     * 
     * 
     * 1.CPU（中央处理器/内核）： 是实际执行给定程序的硬件单元
     * 2.进程 ：是给定程序当前正在执行的实例，操作系统的一个基本功能就是管理进程
     * 3.单线程： 只包含一个线程的进程
     * 4.多线程： 包含一个以上的线程的进程
     * 5.任务： 是可能有高延迟的工作单元，目的是生成一个结果值，或者是产生想要的效果 （代表要执行的作业（动作））
     * 6.线程： 执行任务（动作）的工作者
     * 7.线程池： 多线程的集合，决定如何向线程分配工作的逻辑。有任务要执行时，他分配池中的一个工作者线程执行任务，并且任务结束后接触分配
     * 8.时间分片机制： 模拟多个线程并发执行，
     * 9.时间片（量子）： 处理器切换到下一个线程之前，执行一个特定线程的周期（时间片的分配也会影响性能，
     * 假设现在有一个处理器由两个线程共享，时间分片会在一个线程上执行若干次过后，在切换到另一个线程，再切换回来，以此类推，
     * 每个任务消耗 处理器一秒的执行时间）
     * 
     * 10.上下文切换：在一个给定的内核中改换执行线程的动作 （上下文切换时有代价的，
     * 必须把CPU当前的内部状态保存到内存中（反射），还必须加载于新线程关联的状态
     * 如果线程太多，切换开销就会影响性能，处理器会花费大量时间用作线程间的转换）
     * 
     * 11.并发： 指多个线程 ‘一起运行’
     * 12.并行编程： 将一个问题分解成较小的部分，并异步的发起对每个部分的处理
     * 
     * 
     * 
     */

    /*
     * 啥是多线程？
     *  线程是操作系统能够进行运算调度的最小单位
     *  
     *  所以多线程 指的是 程序的一种执行方式。
     * 
     *  用于多个处理器键分配CPU 受限的工作来缩短计算时间。
     *  
     *  使用原因
     *  1.假设 从应用程序进行网络调用需要一定的时间，我不希望用户界面停止响应，让用户一直等待，
     *  直到从服务器返回一个响应，用户可以执行其它一些操作，或者取消发送给服务器的请求 这些都可以用多线程来完成
     *  2.对于所有需要等待的操作，可以在等待操作完成期间 启动一个新的线程，同时完成其它任务
     *  3.一个进程可以有多个线程同时运行在不同的cpu 上，或者多核CPU 的不同内核上
     *  
     * 
     * 作用是什么？
     * 
     * 本质是什么？
     * 充分利用计算资源
     * 优缺点
     * 
     * 委托是线程嘛？
     * 
     * 
     * 
     * 
     * 控制（操作/管理）线程的方法
     * 
     * 1.join 暂停执行当前线程，直到另一个线程终止，可以指定等待执行时间（join(123)）
     * 2.isbackground  将进程的所有前台线程完成后终止进程，可以使用 s.isbackgroud =true 将线程标志为 后台线程(允许进程终止) 
     * 3.priority 设置线程的优先级（线程执行的顺序）操作系统会倾向于将时间片调拨给高优先级线程
     * 将priority 属性设置为 Threadpritiry 属性 用于分配线程执行优先级 
     * 线程优先级权重  highest > AboveNormal > Normal >BelowNormal > Lowest
     * 4.ThreadState  指定线程的当前执行状态。(一个枚举类)
     * 
     * 为什么不能随意使用 Abort() 终止线程？
     * 1.当调用 abort 时 系统会引发  ThreadAbortException 来终止他，
     * threadabortexception 是一个特殊的异常可由应用程序代码中捕捉但被重新引发在末尾
     * catch 阻止，除非 ResetAbort 调用。 ResetAbort 取消请求后，若要中止，并防止 ThreadAbortException 从终止该线程。 
     * 未执行 finally 块被执行之前在线程终止。（而运行时系统不会引发 ThreadAbortException 异常）
     * 
     * 2. 被终止的线程可能正在执行 lock 语句保护的关键代码。 lock 无法阻止异常，
     * 所以lock 中的代码会因为异常而中断， lock 对象会自动释放
     * 运行正在等待的其他线程进入关键区域
     * 
     * 线程池是什么？
     * 1.一种利用线程资源的方式，
     * 2.一种防止过度分配时间片的手段
     * 3.一个存储线程的队列
     * 4.一句话总结 ====》 开源节流，物以致用
     * 
     * 多线程编程的复杂性只要体现在那些方面？
     * 1.监视异步操作的状态，知道它在何时完成
     * 判断一个异步操作何时完成，最好不要采取轮询线程状态的方法，也不要采用阻塞并等待的方法
     * 2.线程池 避免启动和终止线程的开销，线程池避免了过度创建线程，并防止系统将大部分时间花在线程的切换上
     * 3.避免死锁 避免死锁的同时，也要防止数据同时被两个不同的线程访问
     * 4.为不同的操作提供原子性并同步数据访问。为不同操作组提供同步
     * 
     * 站在被撞者的角度上思考，你们为什么会相撞
     * 1.为什么撞的不是别人？而是他自己？
     * 2.为什么原本在你身后的人要跑到你前面故意被你撞？
     * 3.制造这次车祸的背后的目的是什么？
     * 
     * 综上所述得出以下结论
     * 1.我家瘦糊涂长得好看，这家伙见色起义，
     * 2.重要的是他能不惜以受伤作为代价换取你俩认识的机会
     * 3.为了避免不必要的麻烦，如果是我，我会向他道歉
     * 
     * 所以，瘦糊涂这不是向不向着你的问题
     * 
     * 委托和任务的区别
     * 委托是同步的，也就是说 如果执行一个委托（例如一个 Action） ,当前线程的控制点会立即转移到委托的代码，除非委托结束，否则控制不会返回调用者
     * 相反
     * 启动一个任务，控制几乎会立即返回调用者，无论任务执行多少工作
     * 
     * 或者说 
     * 任务将委托从同步执行模式转变成了异步 Task
     * 
     * 
     * 
     */


    #region 启动类

    class Program
    {
        public const int Repetitions = 1000;
      static  void Main(string[] args)
        {

            // stase();
            #region Task

            /*
             * 
             * Task.Run 和 Task.Factory.StartNew 区别
             * 1. run可以看作是 startnew 的简化版 因为除了需要指定一个线程长时间运行，否则都是用run
             * 2.长线程在指定时间内，不会被回收
             * 3.注意线程异常要在线程内捕捉
             * 
             */

            //声明一个异步操作
            /*        
               Task task = Task.Run(() => {
                    for (int count = 0; count < Repetitions; count++)
                    {
                        Console.Write("-");
                    }
                });
              */

            //声明一个异步操作 长任务时使用
            /*   Task task = Task.Factory.StartNew(() =>
               {
                   for (int i = 0; i < Repetitions; i++)
                   {
                       Console.WriteLine("-");
                   }
               });
               for (int i = 0; i < Repetitions; i++)
               {
                   Console.Write("+");
               }

               //等待完成
               task.Wait();
                */

            /* 
             * 
             * 延续任务
             * ContinueWith 连接两个任务，当先驱任务执行完后，延续任务会以异步方式开启
             * 
             */
            Console.WriteLine("延续任务");
            Task taskA = Task.Run(() => Console.WriteLine("一个任务"))
                //ContinueWith创建一个在目标 Task 完成时异步执行的延续任务。
                .ContinueWith(an => Console.WriteLine("我是一个任务的延续任务___派大星"));
            Task taskB = taskA.ContinueWith(an => Console.WriteLine("我是延续任务___达瓦里希，我是派大星的延续任务，我和达瓦里稀一同执行"));
            Task taskC = taskA.ContinueWith(an => Console.WriteLine("我是延续任务___达瓦里稀，我是派大星的延续任务，我和达瓦里希一同执行"));

            Task.WaitAll(taskB, taskC);
            Console.WriteLine("结束");
            #endregion

            #region ThreadStart

            /*
              // ThreadStart 表示在 Thread 上执行的方法。线程委托 将方法委托到线程上执行
              ThreadStart threadStart = DoWorks;
              //实例化一个线程
              Thread thread = new Thread(threadStart);
              //开启线程
              thread.Start();

              for (int i = 0; i < Repetitions; i++)
              {
                  Console.Write("++");
              }
            //  Join 阻止调用线程，直到某个线程终止时为止。
              thread.Join();
              */
            #endregion

            Console.ReadLine();
        }

      

        public static void DoWork(object state)
        {
            if (state ==null)
            {
                for (int i = 0; i < Repetitions; i++)
                {
                    Console.Write("--");
                }
            }
            else
            {
                for (int i = 0; i < Repetitions; i++)
                {
                    Console.WriteLine(state);
                }
            }
           
        }
        public static void DoWorks()
        {
            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write("--");
            }
        }

        #region 线程池

        public static void ThreadPools()
        {
            // QueueUserWorkItem 将方法排入队列以便执行
            ThreadPool.QueueUserWorkItem(DoWork, "+");

            for (int i = 0; i < Repetitions; i++)
            {
                Console.WriteLine("-");
            }
            Thread.Sleep(1000);
        }

        #endregion



        static void stase()
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

    #endregion

}
