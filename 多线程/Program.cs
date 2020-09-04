using System;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Linq;
using System.Runtime.ExceptionServices;

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
     * 委托属于单线程，他们的本质上是一样的————控制流的走向
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
     * 委托和任务的区别 剥去他们的概念看他们的执行方式 等价于 异步和同步的区别
     * 委托是同步的，也就是说 如果执行一个委托（例如一个 Action） ,当前线程的控制点会立即转移到委托的代码，除非委托结束，否则控制不会返回调用者
     * 相反
     * 启动一个任务，控制几乎会立即返回调用者，无论任务执行多少工作
     * 
     * 或者说 
     * 任务将委托从同步执行模式转变成了异步 Task
     * 
     * 
     * 取消任务
     * 1.TPL (Task Parallel Library )使用的时协作式取消（cooperative cancellation）
     * 2.一种得体的，健壮的，可靠的技术，安全地取消不再需要地任务
     * 3.支持取消地任务 需要监视一个 cancellationtoken 对象 （传播有关应取消操作的通知。）
     * 4.任务会定期轮询它，检测是否发出取消请求
     * 
     * Task.Run()和 Task.Factory.StartNew()
     * 1.Task.Run()是 Task.Factory.StartNew() 的简化形式
     * 2.Task.Factory.StartNew()  用于调用一个要求创建额外线程的CPU 密集型方法（开启一个线程），在。net 4.5 中默认使用 Task.Run()
     * 3.如果要控制任务的创建或执行状态（TaskCreationOptions）要指定其它调度器，可以考虑使用 Task.Factory.StartNew()
     * 
     * 
     * 长任务
     * 一般来说，任务都是短命的，我们可以把任务理解成一个动作，任务就是实现这个动作的过程，当你动作完成时，你的任务也就没了
     * 但是如果我一个动作需要重复做500次1000次呢？创建线程，分配时间片是需要资源的，那么为什么我不只创建一个线程让他反复使用呢？
     * （有点像lol里面的取消后摇，攻击间隔小了，输出不就高了吗？）
     * 1.使用控制任务的创建和执行的可选行为 TaskCreationOptions 枚举
     * 2.定义 为长任务 TaskCreationOptions.LongRunning
     * 3.由于长任务会长时间占用底层资源所以 在合理的情况下尽量少用它
     * 
     * 基于任务的异步模式
     * 在异步任务中任务提供了线程更好的抽象，任务会字段调度为恰当的数量的线程，而大型任务可由多个小任务组成，就像拼图一样，有多个碎片化的方法组成
     * 由于资源分配问题，所以任务执行的顺序无法确定
     * 
     * 使用 async  和 await  实现 基于任务的异步模式 
     * async 修饰符 指示方法是异步的
     * 异步方法提供了一种简便方式完成可能需要长时间运行的工作，而不必阻止调用方的线程。 异步方法的调用方可以继续工作，而不必等待异步方法完成。
     * 
     * await 运算符应用于一个异步方法的任务挂起方法的执行，直到等待任务完成。 任务表示正在进行的工作。（开启一个异步）
     * 1.async 修饰方法 表明该方法是一个异步方法 本质上的等价于 Task 类型返回
     * 2.await 修饰调用方法 表面该调用是一个异步调用 本质上等价于 ContinueWith (延续任务)
     * 
     * 异步lambda
     *  lambda 本质上就是个方法（是一种声明方法的简明的语法，也是一个委托）
     *  而异步 lambda 显而易见的 就是将普通的方法转换成 lambda表达式形式的方法
     *  
     *  1. async lambda 表达式 必须转换成返回类型为 void Task或 Task<T>的委托
     *  2. lambda 表达式中的执行最初都是同步进行的，知道遇到第一个针对未完成的可等待任务的 await 为止
     *  3. await 之后的指令作为被调用异步方法所返回的任务的延续而执行
     *  3. async lambda 可以使用 await 调用
     * 
     * 并行迭代
     * 寻常 for 循环 同步且顺序的执行每一次迭代
     * 那么 这个小小迭代能不能也是用线程呢？
     * 答案是肯定可以呀！
     * Task parallel library(TPL) 提供了 一个for 方法就是玩这个的，
     * 我们可以看看它的执行顺序是不规则的，不按套路出牌不正是线程的看家本领吗?
     * 其原理就是使用到了处理器之间的并行迭代
     * 每个处理器负责一个迭代并和其它正在执行迭代的处理器并行迭代这个迭代，
     * 相当于是 一个人的任务分给了若干个人，那么效率不就上去了吗？
     * 还有一点值得注意的是 对在进行并行迭代的时候应该等迭代结束之后再操作目标数据，否则可能会出现死锁的情况
     * 更有趣的是 TPL 会判断执行多少个线程是效率最高！！！！！（真像知道哇哇哇哇！！！！）
     * 
     * 取消并行迭代
     * 和取消任务一样 需要用到一个 CancellationTokenSoure 的运行状态通知类
     * 从 CancellationTokenSoure 获取一个通知的标志 
     * 
     * 值得注意的是 如果要取消并行循环操作，尚未开始的任何迭代都会通过检查 IsCancellationRequested 
     * (如果要取消当前执行的异步 则值为true ) 的值是否被禁用开始
     * 而正在执行的迭代会运行到各自的终结点，
     * 
     * IsCompleted 返回一个 boolean 指出所有迭代是否已经开启
     * 
     * ParallelLoopResult.LowestBreakIteration   获取从中调用 Break 的最低迭代的索引。(指出执行了一个中断的，索引最低的迭代，long？类型
     * null 表示没有遇到 break )
     * 
     * 
     * 并行执行 linq 查询
     * 连寻常的迭代都能并行，那么令人惊喜的 linq 能不能也并行查询呢？
     * 答案是肯定的 linq 提供了一个 支持并行的查询操作符 ———— Asparpllel()
     * 
     * 由于linq 经常用到，具体实现细节就忽略掉了，讲真 linq 真的写到吐了
     * 
     * 取消并行 linq 
     */



    #region 启动类

    class Program
    {
        public const int Repetitions = 1000;
        //Stopwatch 可用于准确地测量运行时间。登记程序执行时间
        public static Stopwatch Stopwatch = new Stopwatch();
       

      static  void Main(string[] args)
        {

            #region 并行执行 linq 查询

            #endregion

            #region 并行迭代 
            const int TotalDigits = 1000;
            const int BatchSize = 10;
            string pi = null;
            const int iterations = TotalDigits / BatchSize;

            #region 单线程 计算pi 小数点后1000位


            //for (int i = 0; i < iterations; i++)
            //{
            //    pi += PiCalculator.Calculate(BatchSize, i * BatchSize);
            //}



            //Console.WriteLine(pi);
            #endregion

            #region   for并行计算pi 小数点后1000位
            //Parallel.For(0, 100, (i) => { Console.WriteLine(i); });
            //string[] sections = new string[iterations];
            //Parallel.For(0, iterations, (i) =>
            //{
            //    sections[i] = PiCalculator.Calculate(BatchSize, i * BatchSize);
            //});
            //pi = string.Join("", sections);
            //Console.WriteLine(pi);
            #endregion

            #region foreach 并行

            //List<int> vs = new List<int>();
            //for (int i = 0; i < 10; i++)
            //{
            //    vs.Add(i);
            //}
            //Parallel.ForEach(vs, (i) =>
            //{
            //    Console.WriteLine(i);
            //});


            #endregion

            #region 捕捉迭代异常
            /*
             * 使用异常存储集合 AggregateException 
             * 对目标 进行监测，如过出现异常就收集异常
             */
            //List<int> vs = new List<int>();
            //for (int i = 0; i < 10; i++)
            //{
            //    vs.Add(i);
            //}

            //try
            //{
            //    Parallel.ForEach(vs, (i) =>
            //    {
            //        Console.WriteLine(i);
            //    });
            //}
            //catch (AggregateException e)
            //{
            //    Console.WriteLine("ERROR:{0}",e.GetType().Name);
            //    foreach (var item in e.InnerExceptions)
            //    {
            //        Console.WriteLine("{0}-{1}",item.GetType().Name,item.Message);
            //    }
            //}



            #endregion

            #region 取消并行迭代

            #endregion


            #endregion


            #region 基于任务的异步模式

            #region 同步的 web 请求
            /*  
              string url = "http://www.Intellitect.com";
               if (args.Length >0)
               {
                   url = args[0];
               }
               try
               {
                   Console.WriteLine(url);
                   //WebRequest 对统一资源标识符 (URI) 发出请求。
                   WebRequest webRequest = WebRequest.Create(url);
                   // WebResponse 提供来自统一资源标识符 (URI) 的响应。
                   WebResponse response = webRequest.GetResponse();
                   Console.WriteLine("............");
                   //开启一个事务从字节流中读取字符。
                   using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                   {
                       //读取来自流的当前位置到结尾的所有字符。
                       string text = reader.ReadToEnd();

                       Console.WriteLine(FormatBytes(text.Length));
                   }

               }
               catch (WebException)
               {

                   Console.WriteLine("通过可插接协议访问网络期间出错时引发的异常。");
                   throw;
               }
               catch (IOException)
               {
                   Console.WriteLine("发生 I/O 错误时引发的异常。");
                   throw;
               }
               catch (NotSupportedException)
               {
                   Console.WriteLine("当调用的方法不受支持，或试图读取、查找或写入不支持调用功能的流时引发的异常。");
                   throw;
               }

               */
            #endregion

            #region 异步的 web 请求
            /*

                        string url = "http://www.Intellitect.com";
                        if (args.Length > 0)
                        {
                            url = args[0];
                        }
                        Console.WriteLine(url);
                        //开启任务，调用异步方法
                        Task task = WriteWebRequestSizeAsync(url);

                        
           // 这个是最优解：
           // 将处理逻辑封装成一个异步方法
           // 然后直接 在调用方法中 用try 监视 异步方法 是否出现异常 假若出现异常 
           // 就用 AggregateException 这个异常集合类捕获
           // 最后在递归异常集合类 这样就能将所有异常捕获并解决了


            try
            {
                while (!task.Wait(10))
                {
                    Console.Write(".");
                }
            }
            catch (AggregateException e)
            { //Flatten 以递归方式平面化所有这些异常，确保每个异常都能被捕捉到
                e = e.Flatten();

                try
                {
                    // Handle 调用处理程序。
                    e.Handle(innerException =>
                    { //将当前异常的实例 恢复捕获异常时保存的状态
                        ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                        return true;
                    });
                }
                catch (WebException)
                {

                    throw;
                }
                catch (IOException)
                {

                }
                catch (NotSupportedException)
                {

                }

            }

            */

            #endregion

            #region async  和 await 基于任务的异步模式实现 异步web 请求
            /*

            string url = "http://www.Intellitect.com";
            if (args.Length > 0)
            {
                url = args[0];
            }
            Console.WriteLine(url);
            //开启任务，调用异步方法
            Task task = WriteWebRequestSize_Async(url);

            try
            {
                while (!task.Wait(10))
                {
                    Console.Write(".");
                }
            }
            catch (AggregateException e)
            { //Flatten 以递归方式平面化所有这些异常，确保每个异常都能被捕捉到
                e = e.Flatten();

                try
                {
                    // Handle 调用处理程序。
                    e.Handle(innerException =>
                    { //将当前异常的实例 恢复捕获异常时保存的状态
                        ExceptionDispatchInfo.Capture(e.InnerException).Throw();
                        return true;
                    });
                }
                catch (WebException)
                {

                    throw;
                }
                catch (IOException)
                {

                }
                catch (NotSupportedException)
                {

                }

            }
            */
            #endregion

            #region 异步 lambda 

            /*
            string url = "http://www.Intellitect.com";
            if (args.Length > 0)
            {
                url = args[0];
            }
            Console.WriteLine(url);
            Func<string, Task> writeWebRequestSizeAsync = async (string webRequestUrl) =>
                {
                    WebRequest webRequest = WebRequest.Create(url);
                    WebResponse response = await webRequest.GetResponseAsync();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string text = await reader.ReadToEndAsync();
                        Console.WriteLine(FormatBytes(text.Length));
                    }
                };

                Task task = writeWebRequestSizeAsync(url);

                while (!task.Wait(10))
                {
                    Console.Write(".");
                }
           
            */


            #endregion

            #endregion

            #region 长任务

            /*

               Task.Factory.StartNew(() =>
               {
                   Console.WriteLine(9);
               },TaskCreationOptions.LongRunning);
               Console.WriteLine(Task.CompletedTask.Id);

               */
            #endregion

            #region Task.Run()和 Task.Factory.StartNew()
            /*   Task.Factory.StartNew(() =>
               {
                   for (int i = 0; i < 100; i++)
                   {
                       Console.WriteLine(i);
                   }
               });
               Console.WriteLine("两种开启线程的方法，从本质上看是一样的，只不过他们的表现形式不同，类似于水和冰，他们的本质都是氢氧化合物");
               Task.Run(() =>
               {
                   for (int i = 100; i < 200; i++)
                   {
                       Console.WriteLine(i);
                   }
               });
               */

            #endregion

            #region 取消任务
            //返回指定长度地字符串
            /*  string stars = "*".PadRight(Console.WindowWidth - 1, '*');

              Console.WriteLine("按ENTER退出");
              //任务取消类， 提供一个取消任务地标志
              //CancellationToken cancellationToken = new CancellationToken();
              //结束标志来源
              CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

              Task task = Task.Factory.StartNew(
                 () => WritePi(cancellationTokenSource.Token),
                 cancellationTokenSource.Token);
              Console.ReadLine();
              //传达取消请求。
              cancellationTokenSource.Cancel();
              Console.WriteLine(stars);
              //等待线程
              task.Wait();
              Console.WriteLine();
              */
            #endregion
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
            /* Console.WriteLine("延续任务");
              Task taskA = Task.Run(() => Console.WriteLine("一个任务"))
                  //ContinueWith创建一个在目标 Task 完成时异步执行的延续任务。
                  .ContinueWith(an => Console.WriteLine("我是一个任务的延续任务___派大星"));
              Task taskB = taskA.ContinueWith(an => Console.WriteLine("我是延续任务___达瓦里希，我是派大星的延续任务，我和达瓦里稀一同执行"));
              Task taskC = taskA.ContinueWith(an =>
              {
                  //Trace 提供一组方法和属性，帮助您跟踪代码的执行。 此类不能被继承。

                  //Assert 检查条件；如果条件为 false，则显示一个消息框，其中会显示调用堆栈。
                  //IsFaulted 检测是否引发异常 是则为true 

                  Trace.Assert(an.IsFaulted);
                  Console.WriteLine("我是延续任务___达瓦里稀，我是派大星的延续任务，我和达瓦里希一同执行");
              },
              // TaskContinuationOptions 创建的任务指定行为
              TaskContinuationOptions.None
              );

              Task.WaitAll(taskB, taskC);
              Console.WriteLine("结束");
                 */
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

            #region 使用 continuewith 观察未处理异常
            /*
             * 延续任务
             * 当前线程执行完后执行的线程，相当于另开一个线程
             * 也可以看作是两个线程之间的链接的桥梁
             */
            /*
             * 假设任务中发生的异常完全没有被观察到
             * 1.他不会在任务中被捕获
             * 2.永远观察不到任务完成（通过 wait result 或者访问exception属性）（原因：单线程出现异常时 程序会立即中断）
             * 3.出错的continuewith 永远观察不到（continuewith  是一个延续任务，
             * 也是一个线程，线程内异常外界不发捕捉，但可以通过 TaskScheduler.UnobservedTaskException  事件来登记未处理的任务异常）
             * 
             * 
             */

            /* 
             bool PTG = false;
             Task taskA = new Task(() =>
             {
                 throw new InvalidOperationException();//返回一个指定的错误信息
             });
             Task taskB = taskA.ContinueWith((an) =>
             {
                 // IsFaulted 如果任务引发了未经处理的异常 则为 true
                 PTG = an.IsFaulted;
                 //创建的任务指定行为。
                 //OnlyOnFaulted 只有在延续任务前面的任务引发了未处理异常的情况下才应安排延续任务
             }, TaskContinuationOptions.OnlyOnFaulted);
             taskA.Start();
             taskB.Wait();//等待 taskB 完成执行过程。

             Trace.Assert(PTG);
             Trace.Assert(taskA.IsFaulted);
             //用于获取 异常对象
            
             taskA.Exception.Handle(e =>
             {
                 Console.WriteLine($"ERROR{e.Message}");
                 return true;
             });
             */

            #endregion

            #region 使用 TaskScheduler.UnobservedTaskException 登记未处理的任务异常
            /*
             * 每个 APPDomain 都提供了一个机制，为了观察 APPDomain 中发生的未处理异常，必须添加一个 UnhandleException 事件来处理程序
             * APPDomain 中的线程发生的所以未处理异常都会触发  UnhandleException 事件 （当某个异常未被捕获时出现。）
             * 
             * 1.应比避免程序在任何线程上产生未处理异常
             * 2.考虑登记 未处理异常 时间处理程序以进行调试，记录，紧急关闭
             * 3.要取消未完成的任务而不是在程序关闭期间仍然运行
             * 
             */
            /*
           try
           {
               //程序执行时间监听开启
               Stopwatch.Start();
               //获取当前程序的未处理的异常
               AppDomain.CurrentDomain.UnhandledException += (s, e) =>
               {
                   Message("事件处理程序启动");
                   Delay(4000);
               };
               Thread thread = new Thread(() =>
               {
                   Message("抛出异常");
                   throw new Exception();
               });
               //线程开启
               thread.Start();
               Delay(2000);

           }finally
           {
               Message("finally 运行");
           }
         */
            #endregion



            /*****************************************************/

            Console.ReadLine();
        }

        /********************/

        #region 基于任务的异步模式实现 异步web 请求
        private static async Task WriteWebRequestSize_Async(string url)
        {
            try
            {
                //声明请求路径
                WebRequest webRequest = WebRequest.Create(url);
                //异步获取 目标回应
                WebResponse response = await webRequest.GetResponseAsync();
                //声明 事务流 填充目标内容
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    //ReadToEndAsync 读取来自流的当前位置到结尾的所有字符并将它们作为一个字符串返回。
                    string text = await reader.ReadToEndAsync();
                    Console.WriteLine(FormatBytes(text.Length));
                }
            }
            catch (WebException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (NotSupportedException)
            {
                throw;
            }
        }


        #endregion

        #region 异步的 web 请求
        /// <summary>
        /// 异步方法
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static Task WriteWebRequestSizeAsync(string url)
        {
            StreamReader reader = null;
            //向目标地址发送请求
            WebRequest webRequest = WebRequest.Create(url);
            //开启异步任务 异步获取 Internet 请求的响应
            Task task = webRequest.GetResponseAsync().ContinueWith(an =>
            {
                //获取异步返回的网页内容
                WebResponse response = an.Result;
                //写入缓存
                reader = new StreamReader(response.GetResponseStream());
                //异步读取来自流的当前位置到结尾的所有字符并将它们作为一个字符串返回。
                return reader.ReadToEndAsync();
            })
            // Unwrap 脱掉外出的 Task 
            .Unwrap()
            .ContinueWith(an =>
            {
                //关闭缓存
                if (reader != null) reader.Dispose();
                //获取返回内容
                string text = an.Result;
                Console.WriteLine(FormatBytes(text.Length));
            });
            return task;

        }
        #endregion

        #region 同步的 web 请求
        static public string FormatBytes(long bytes)
        {
            string[] magnitudes = new string[] { "GB", "MB", "KB", "Bytes" };
            long max = (long)Math.Pow(1024, magnitudes.Length);
            return string.Format("{1:##.##} {0}",magnitudes.FirstOrDefault(
                magnitude => bytes >(max/=1024)) ?? "0 bytes",(decimal)bytes / (decimal)max
                );
        }


        #endregion

        #region 取消任务
        private static void WritePi(CancellationToken cancellationToken)
        {
            const int batchSize = 1;
            string piSection = String.Empty;
            int i = 0;
            //IsCancellationRequested 获取是否已请求取消此标记。 如果已请求取消此标记，则为 true；否则为 false。
            // i==int.MaxValue 防止数值溢出
            while (!cancellationToken.IsCancellationRequested || i==int.MaxValue)
            {

                piSection = PiCalculator.Calculate(
                    batchSize, (i++) * batchSize);
                Console.Write(piSection);
            }
        }


        #endregion

        #region   使用 TaskScheduler.UnobservedTaskException 登记未处理的任务异常
        public static void Delay(int i)
        {
            Message($"休眠时间 {i} ms");
            Thread.Sleep(i);
            Message("唤醒");
        }
        static void Message(string str)
        {//输出 当前线程id, 获取当前实例测量的出的运行时间
            Console.WriteLine("{0}:{1:0000}:{2}",Thread.CurrentThread.ManagedThreadId,Stopwatch.ElapsedMilliseconds,str);
        }
        #endregion

        #region ThreadStart
        public static void DoWorks()
        {
            for (int i = 0; i < Repetitions; i++)
            {
                Console.Write("--");
            }
        }
        #endregion

        #region 线程池

        public static void DoWork(object state)
        {
            if (state == null)
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

    #region 总结
    /*
 
 有单线程就有多线程
你可以把人体理解成一个多线程的程序
人体的每个器官都有它的功能和运作方式，这些器官之间的运作方式互不影响却又可以同时工作
比如说 心脏供血， 肺供氧，肝解毒等等 协同工作的程序就是多线程了

线程的概念：程序的一种执行方式，系统运算的最小单位，为了充分利用处理器资源


管理线程的方法

1.join 暂停线程
2.isbackgroud 标志位后台线程（后台线程==可以随时终止的线程，并不影响程序的执行）
3.priority 设置线程的优先级 highest > AboveNormal > Normal >BelowNormal > Lowest
4.ThreadState 线程当前的执行状态

线程池： 
1.一个管理活线程的队列集合
2.可以自身分配时间片
3.开源节流，物以致用

任务和委托的区别
1.执行概念不同，任务的执行环境是多线程，任务是异步的，而委托的执行环境是单线程的，同步的
2.从执行方式上看他们都是控制程序的节点的流

取消任务
1.TPL (Task Parallel Library （任务并行库）)使用的时协作式取消（cooperative cancellation）
2.举要监听 CancellationToken （传播取消操作的通知类）
3.当 CancellationToken 地状态发生改变时 处理器就会优雅，得体地关闭取消任务

Task.Run()和 Task.Factory.StartNew()
1. 其实他们两从本质上说是一样地 ，都是开启线程地一种方式
2. run 是 startnew 地简化形式
3. 但是 startnew 却有 run 没有的优势，那就是设置长任务（长时间运行地任务，比如说行星地自转）

长任务

一般来说 当一个任务执行完之后 就会被系统销毁，但是对于某些需要重复操作的任务来说
每操作一次就得创建一个线程，然而创建线程和销毁线程都是要消耗资源的
而长任务的出现就以很好地改善这个问题
1.设置长任务需要使用到 控制任务地创建和执行地可选行为地枚举 TaskCreationOptions
2.将线程定义为长任务 TaskCreationOptions.LongRunning
3.长任务会长期占用底层资源，所以尽量少用
4.长任务和线程池有什么关系呢？ 
4.1.线程池的本质是存储活线程的集合，而长任务是长时间存活的线程
4.2.长任务可以用线程池代替嘛？
4.3.创建一个足够大的公共线程池，可以供整个程序使用嘛？


异步 async  await

1.async 修饰方法 表明该方法是一个异步方法 本质上的等价于 Task 类型返回
2.await 修饰调用方法 表面该调用是一个异步调用 本质上等价于 ContinueWith (延续任务)
         
         */
    #endregion

    partial class PiCalculator
    {
        const int Digits = 100;
        #region Helper
        public static string Pi()
        {
            return Calculate();
        }

        public static string Calculate(int digits = 100)
        {
            return Calculate(digits, 0);
        }

        public static string Calculate(int digits, int startingAt)
        {
            //为 DoWork 事件处理程序提供数据
            System.ComponentModel.DoWorkEventArgs eventArgs = new System.ComponentModel.DoWorkEventArgs(digits);

            CalculatePi(typeof(PiCalculator), eventArgs, startingAt);
            return (string)eventArgs.Result;
        }

        private static void CalculatePi(
            object sender, System.ComponentModel.DoWorkEventArgs eventArgs)
        {
            CalculatePi(sender, eventArgs, 0);
        }

        private static void CalculatePi(
            object sender, System.ComponentModel.DoWorkEventArgs eventArgs, int startingAt)
        {
            int digits = (int)eventArgs.Argument;

            StringBuilder pi;
            if (startingAt == 0)
            {
                pi = new StringBuilder("3.", digits + 2);
            }
            else
            {
                pi = new StringBuilder();
            }
#if BackgroundWorkerThread
            calculationWorker.ReportProgress(0, pi.ToString());
#endif

            // Calculate rest of pi, if required
            if (digits > 0)
            {
                for (int i = 0; i < digits; i += 9)
                {

                    // Calculate next i decimal places
                    int nextDigit = InternalPiDigitCalculator.StartingAt(
                        startingAt + i + 1);
                    int digitCount = Math.Min(digits - i, 9);
                    string ds = string.Format("{0:D9}", nextDigit);
                    pi.Append(ds.Substring(0, digitCount));

                    // Show current progress
#if BackgroundWorkerThread
                    calculationWorker.ReportProgress(
                        0, ds.Substring(0, digitCount));
#endif

#if BackgroundWorkerThread
                    // Check for cancellation
                    if (calculationWorker.CancellationPending)
                    {
                        // Need to set Cancel if you need to  
                        // distinguish how a worker thread completed
                        // ie by checking 
                        // RunWorkerCompletedEventArgs.Cancelled
                        eventArgs.Cancel = true;
                        break;
                    }
#endif
                }
            }

            eventArgs.Result = pi.ToString();
        }
        #endregion
        public class InternalPiDigitCalculator
        {
            public static int mul_mod(long a, long b, int m)
            {
                return (int)((a * b) % m);
            }

            // return the inverse of x mod y
            public static int inv_mod(int x, int y)
            {
                int q = 0;
                int u = x;
                int v = y;
                int a = 0;
                int c = 1;
                int t = 0;

                do
                {
                    q = v / u;

                    t = c;
                    c = a - q * c;
                    a = t;

                    t = u;
                    u = v - q * u;
                    v = t;
                }
                while (u != 0);

                a = a % y;
                if (a < 0) a = y + a;

                return a;
            }

            // return (a^b) mod m
            public static int pow_mod(int a, int b, int m)
            {
                int r = 1;
                int aa = a;

                while (true)
                {
                    if ((b & 0x01) != 0) r = mul_mod(r, aa, m);
                    b = b >> 1;
                    if (b == 0) break;
                    aa = mul_mod(aa, aa, m);
                }

                return r;
            }

            // return true if n is prime
            public static bool is_prime(int n)
            {
                if ((n % 2) == 0) return false;

                int r = (int)(Math.Sqrt(n));
                for (int i = 3; i <= r; i += 2)
                {
                    if ((n % i) == 0) return false;
                }

                return true;
            }

            // return the prime number immediately after n
            public static int next_prime(int n)
            {
                do
                {
                    n++;
                }
                while (!is_prime(n));

                return n;
            }

            public static int StartingAt(int n)
            {
                int av = 0;
                int vmax = 0;
                int N = (int)((n + 20) * Math.Log(10) / Math.Log(2));
                int num = 0;
                int den = 0;
                int kq = 0;
                int kq2 = 0;
                int t = 0;
                int v = 0;
                int s = 0;
                double sum = 0.0;

                for (int a = 3; a <= (2 * N); a = next_prime(a))
                {
                    vmax = (int)(Math.Log(2 * N) / Math.Log(a));
                    av = 1;

                    for (int i = 0; i < vmax; ++i) av = av * a;

                    s = 0;
                    num = 1;
                    den = 1;
                    v = 0;
                    kq = 1;
                    kq2 = 1;

                    for (int k = 1; k <= N; ++k)
                    {
                        t = k;
                        if (kq >= a)
                        {
                            do
                            {
                                t = t / a;
                                --v;
                            }
                            while ((t % a) == 0);

                            kq = 0;
                        }

                        ++kq;
                        num = mul_mod(num, t, av);

                        t = (2 * k - 1);
                        if (kq2 >= a)
                        {
                            if (kq2 == a)
                            {
                                do
                                {
                                    t = t / a;
                                    ++v;
                                }
                                while ((t % a) == 0);
                            }

                            kq2 -= a;
                        }

                        den = mul_mod(den, t, av);
                        kq2 += 2;

                        if (v > 0)
                        {
                            t = inv_mod(den, av);
                            t = mul_mod(t, num, av);
                            t = mul_mod(t, k, av);
                            for (int i = v; i < vmax; ++i)
                            {
                                t = mul_mod(t, a, av);
                            }
                            s += t;
                            if (s >= av) s -= av;
                        }
                    }

                    t = pow_mod(10, n - 1, av);
                    s = mul_mod(s, t, av);
                    sum = (sum + (double)s / (double)av) % 1.0;
                }

                return (int)(sum * 1e9);
            }
        }


    }

}
