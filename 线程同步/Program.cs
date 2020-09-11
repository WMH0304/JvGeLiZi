using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace 线程同步
{
    /*
     * 线程同步：
     * https://blog.csdn.net/dz45693/article/details/78635112
     * https://blog.csdn.net/u010155023/article/details/52401267?utm_medium=distribute.pc_relevant_t0.none-task-blog-BlogCommendFromMachineLearnPai2-1.channel_param&depth_1-utm_source=distribute.pc_relevant_t0.none-task-blog-BlogCommendFromMachineLearnPai2-1.channel_param
     * https://zhuanlan.zhihu.com/p/219657420
     * 非常生动形象·
     * 
     * 啥事线程同步？
     * 1.由于多线程编程的复杂性是在于识别多个线程是访问的数据
     * （假如多个线程对同一条数据进行操作的话会出现 竟态条件（多个线程同时访问相同的数据元素时会出现的状况，这样会破坏数据的完整性）的情况）
     * 2.所以程序必须对数据进行同步
     * 3.同步对象不能是值类型（原因 排他锁值会限制代码块执行地模式，并不改变实际值，如强行使用 编译会报错（原因，存在生殖隔离））
     * 
     * 为啥要同步？
     * 避免 程序执行时出现 死锁 或者竟态条件
     * 
     * 
     * 他的概念是什么？
     * 避免死锁的同时防止出现竟态条件
     * 
     * 他能做什么？
     * 避免对线程执行时争夺资源时出现混乱，或者是直接导致程序错误
     * 
     * 适用场景是什么？
     * 1.修改静态数据是确保线程安全
     * 2.同步需要围绕着静态数据进行
     * 3.应该声明私有的静态变量，并提供公共方法来修改数据
     * 4.相反的，实例数据不需要包含同步机制，应为同步会显著降低性能，并增大夺取锁或者死锁的概率
     * 5.如果在多线程中 共享对象是，应当针对共享的数据，解决好他们的同步问题
     * 
     * 
     * 他的本质是什么？
     * 假如把多线程执行时比喻成时泛滥的洪水的话，那么线程同步就是治水的大禹
     * 
     * 他的优点是什么？
     * 让线程更安全，执行时更有条理
     * 是在这混乱的世界中的一缕清明的规则
     * 是约束混乱的规则
     * 
     * 什么是竟态条件？
     * 当多个线程竞争同一个资源是，对资源的访问的顺序发生错误 这种场景就叫竟态条件
     * 
     * 排他锁：
     * 优点：
     * 能改善多线程执行时的不稳定
     * 并且可以确保多个线程访问时能够安全地调用到
     * 从而实现调用者地线程安全
     * 
     * 缺点：
     * lock 地速度相当慢 （多线程一下子被改成单线程肯定慢呀）
     * 
     * 
     * \\\\\\\\___//////////
     * ////////---\\\\\\\\\\
     * 
     * 
     * 局部变量：
     * 局部变量加载到栈上，每个线程都有自己的逻辑栈
     * 针对每个方法的调用，局部变量都有自己的实例，不同分方法调用之间，局部变量是不共享的
     * 所以 局部变量在线程之间也是不共享的
     * 有趣的是 局部变量在c#的层级上是局部变量，而在 IL的层级上是字段，而字段是可以多个线程访问的。
     * 
     * Monitor 实现步
     * 为什么要用到Monitor？
     * 为了同步多个线程，防止他们同时执行特点的代码，
     * 需要用到监视器 阻止第二个线程进入受保护的代码段
     * 知道第一个线程推迟那个代码段
     * 1.标志了受保护代码段的开始和结束未知，需要分别调用静态方法 monitor.enter （他是一个排他锁） 和 monitor.exit（释放对象上的排他锁）
     * 2.一定要记住，在 monitor.enter 和 monitor.exit 两个调用之间
     * 所以代码都要用 try/finally包围起来，否则受保护段代码可能会发生异常，造成 monitor.exit 永远无法调用
     * 从而长时间地阻塞其他线程
     * 3.是一个同步访问对象的机制。
     * 4.他的本质就是将知道 代码块 指定为单线程模式
     * 5. Monitor.Pulse  通知等待队列中的线程锁定对象状态的更改。通常用于生产者——消费者模式
     * 值得注意的时 等待队列一次只能进一个线程 以确保 没有生产就没有消费
     * 生产者和消费者的关系可以理解成传球的动作
     * 当拿球的人（生产者）拿到球的时候，他会通过给 pulse 向队友（消费者）发送一个传球（球只有一个（队列中也只有一个线程））的信号，
     * 这是后队友就开始做出接球动作（消费者）
     * 
     * lock 关键字实现同步
     * 也许有小伙伴就会问，每次使用 monitor 进行同步 都要 try/finally 一下太麻烦了有没有优雅一丢丢的的写法呢？
     * 答案是肯定是有滴，c# 提供了特殊的关键字lock 来处理锁定同步模式
     * lock 作用和 monitor 一样 就像 run 和StartNew一样
     * 可能唯一的不同就是他们的版本不同把哈哈哈哈哈
     * 1.应该避免 锁定this typeof(type) 和 string 
     * 2.不是要使用 MethodImplAttribute
     *
     * 
     * volatile 关键字
     *  为啥要用到  volatile?
     *  这里就不得不提一下 编译器在编译时会对代码进行优化，在多线程中，这样地优化就可能造成两个线程对同一个字段地读写顺序发生混乱
     *  而解决地方法就是 使用 volatile 关键字声明字段
     *  这个关键字强迫对 volatile 字段地所有读写操作都在代码指示地位置发生，而不是通过优化而生成地其它地某个位置
     * 
     * 1.指示一个字段可以由多个同时执行地线程修改
     * 2.通常用于由多个线程访问但不使用 lock 语句对访问进行序列化的字段。
     * 3. 说明书上说 lock 比volatile 更好
     * 4.但是它存在就有它存在的意义，不是吗？
     *
     * system.threading.mutex
     * 1.system.threading.mutex 在概念上和 system.threading.Monitor 几乎完全一致（ mutex 没有 pulse 方法的支持）
     * 2.mutex 同步对文件或者其它跨进程资源的访问
     * 3.mutex 限制程序不能同时运行多个实例
     * 4.mutex 派生自 system.threading.waithandle 可以通过 waitall waitany signalandwait 自动获取锁
     * 
     * system.threading.waithandle
     * 1. waithandle 是一个基础同步类
     * 2.它派生出 mutex eventwaithandle semaohore等同步类
     * 3.有关键方法 wiatone  他会阻塞当前线程，waithandle 实例收到型号 或者被调用时 当前线程才会继续执行
     * 4.waitone 方法有多个重载 无参 waitone（） 无线器等待  ，waitone (int32) 等待指定毫秒， waitone（timespan） 
     * 等待一个 timespan 指定的事件，只要 waithandle 超时之前收到信号，就会返回一个true值
     * 5.类似于红绿灯
     * 6.值得注意的是，waithandle 需要自己释放资源 IDisposable
     * 7.更详细的资料请看官方文档
     * 
     * 重置事件类 manualresetevent 和 manualreseteventslim（manualresetevent 的改进版）
     * 简单地把它理解成红绿灯样子，  
     * 当事件类辐射信号时（绿灯，允许通行），允许当前线程执行
     * 当事件类释放信号时（红灯，禁止通行），阻塞当前线程
     * 当事件类重置信号时（黄灯，禁止通行），将事件状态设置为非终止，从而导致线程受阻。
     * 
     * set()   辐射信号
     * wait()  释放信号
     * reset() 重置信号
     * 
     * 1. 通知一个或多个正在等待的线程已发生事件
     * 2. 值得注意的是 重置事件和 委托和事件没有半毛钱关系
     * 3. 重置事件用于强迫代码等待另一个线程的执行，知道获取事件的通知。
     * 4. 一般使用在多线程测试上，
     * 5.使用场景 一般都是（wait）阻塞本线程让后执行其它线程 其它线程执行完后会辐射出一个（set）信号,通知被阻断地线程可以继续执行，和释放自身信号（wait）
     * 6. manualresetevent 和 manualreseteventslim 地区别在于 前者默认使用核心同步，而前者进行了优化应该尽量避免使用核心机制
     * 因此 manualreseteventslim 的性能更好！当然还有特殊情况就是处理复杂的等待多个事件或者跨越多个线程的时候就用 核心同步
     * 
     * 
     * 自动重置事件 autoresetevent 
     * 就像说明书所说的那样，可以将他理解成一个开门的动作
     * autoresetevent 辐射信号 通知大门开启 
     * 大门开启后 辐射信号消失，大门重新关闭 
     * autoresetevent 通知正在等待的线程已发生事件
     * autoresetevent 和 maualreseteventslim 非常相似，但前者只接触一个线程的 wait 调用造成的阻塞，因为在线程通过自动重置的大门后，它就会回复锁定状态
     * 它允许 线程A 通知线程B(通过一个set调用)，但他的通知模式机制是单向的。在通知完后（辐射信号完成后） autoresetevent 自动回复锁定状态
     * 
     * semaphore /semaphoreslim 
     * semaphore /semaphoreslim  的差异和manualresetevent/manualreseteventslim 一样
     * semaphore （信号量） 限制在一个关键执行区域中同时通过的调用，本质上保持了对资源池的计数
     * 当计数为 0 时，就阻止对资源池的访问，知道其中一个资源返回后，有可用资源后，才把他拿给队列中的下一个阻塞的请求
     * 从某种意义上说，信号量就是 可使用的资源的多少
     * 
     * 有点像时生产者/消费者，但是更像时一个队列集合
     * 
     * countdownevent 反向同步 表示在计数变为零时会得到信号通知的同步基元。
     * countdownevent 和 semaphore 类似 反向同步不是指阻止对已经枯竭的资源池进行访问，而是只有资源池计数为0 时才允许访问
     * 
     * 
     * 
     * 
     * 同步总结
     * 1.避免死锁，两个或多个线程都在等待对方释放同一个同步锁就会发生死锁（可以理解为开门，都等着对方开门，然后就一直耗着耗着）
     * 死锁满足条件
     * 1.1.排他或者互斥，一个线程(A) 占领一个资源，其它线程（B）无法获取资源 （占着茅坑不拉屎）
     * 1.2.占有并等待，互斥的一个线程（A） 请求另一个线程（B）占用的资源 （吃着碗里的想着锅里的）
     * 1.3.不可抢先， 一个线程（A）占用的资源不能被强制拿走（只能等待 A 主动释放锁定资源）  （没有主权意识）
     * 1.4.循环等待条件，两个或多个线程构成一个循环等待链，他们锁定两个或多个相同的资源，每个线程都在等待链中的下一个线程占用的资源 （这种情况最惨）
     * 
     * 造成死锁的本质就是计算机的资源分配矛盾所致，
     * 
     * 并发集合
     * 1.并发集合包含内置的同步代码，这使他们能够支持多个线程同时访问而不必关系竟态条件
     * +2. Blockingcollection 阻塞集合，允许生产者/消费者模式中，生产者向集合写入数据，同时消费者从集合中读取数据，
     * 支持同步添加和删除的操作，实现了 IProduceconsumercollection 接口 提供了阻塞和 bounding 支持
     * +3. concurrentBag<T> 线程安全的无序集合
     * 4. concurrentDictionary 线程安全的字典，键值对构成的集合
     * +5. concurrentQueue 线程安全的队列
     * +6. concurrentStack 线程安全的栈
     * 7.+ 实现了IProduceconsumercollection 接口的集合类
     * 8.IProduceconsumercollection 允许一个或多个类将数据写入集合，而一个不同的集合将其督促并删除，
     * 数据添加的和删除的顺序由实现了IProduceconsumercollection 接口的单独的集合类决定
     * 
     * 线程本地存储
     * 同步锁可能导致让人无法接受的性能，并对伸缩性造成限制，
     * 围绕特定数据元素提供同步可能过于复杂，尤其使在以前写好的原始代码的基础上进行修补
     * 1.threadlocal 本地存储线程 每个线程存储的值是独立的
     * 2. threadstaticattribute 提供线程本地存储
     * 
     * 计时器
     * 指定时间后发出通知信号
     * 
     * 总结
     * 本章主要讲了 同步机制的各种姿势，以及利用各种类来避免出现竟态条件
     * 首先讨论了 lock 锁 ，lock 本质上还是由 monitor 类实现的，可以说是它的简化版
     * 还有 Interlocked （为多个线程共享的变量提供原子操作。） mutex  waithandle  重置事件 信号量 并发集合类 
     * 
     * 
     * 
     * 
     */

    #region 并发集合
    #region blockingcollection
    class AddTakeDemo
    {
        // Demonstrates:
        //      BlockingCollection<T>.Add()
        //      BlockingCollection<T>.Take()
        //      BlockingCollection<T>.CompleteAdding()
        public static void BC_AddTakeCompleteAdding()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {

                // Spin up a Task to populate the BlockingCollection  启动一个任务来填充BlockingCollection
                using (Task t1 = Task.Factory.StartNew(() =>
                {
                    bc.Add(1);
                    bc.Add(2);
                    bc.Add(3);
                    bc.CompleteAdding();
                }))
                {

                    // Spin up a Task to consume the BlockingCollection
                    //旋转一个任务以使用BlockingCollection
                    using (Task t2 = Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            // Consume consume the BlockingCollection 消耗BlockingCollection
                            while (true) Console.WriteLine(bc.Take());
                        }
                        catch (InvalidOperationException)
                        {
                            // An InvalidOperationException means that Take() was called on a completed collection 
                            // InvalidOperationException意味着在完成的集合上调用Take()
                            Console.WriteLine("That's All!");
                        }
                    }))

                        Task.WaitAll(t1, t2);
                }
            }
        }
    }

    class TryTakeDemo
    {
        // Demonstrates:
        //      BlockingCollection<T>.Add()
        //      BlockingCollection<T>.CompleteAdding()
        //      BlockingCollection<T>.TryTake()
        //      BlockingCollection<T>.IsCompleted
        public static void BC_TryTake()
        {
            // Construct and fill our BlockingCollection
            //构造并填充我们的BlockingCollection 开启一个事务
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {
                int NUMITEMS = 10;
                for (int i = 0; i < NUMITEMS; i++) bc.Add(i);
                bc.CompleteAdding();
                int outerSum = 0;

                // Delegate for consuming the BlockingCollection and adding up all items 委托使用BlockingCollection并将所有项相加
                Action action = () =>
                {
                    int localItem;
                    int localSum = 0;

                    while (bc.TryTake(out localItem)) localSum += localItem;
                    Interlocked.Add(ref outerSum, localSum);
                };

                // Launch three parallel actions to consume the BlockingCollection 启动三个并行操作来使用BlockingCollection
                Parallel.Invoke(action, action, action);

                Console.WriteLine("Sum[0..{0}) = {1}, should be {2}", NUMITEMS, outerSum, ((NUMITEMS * (NUMITEMS - 1)) / 2));
                Console.WriteLine("bc.IsCompleted = {0} (should be true)", bc.IsCompleted);
            }
        }
    }

    class FromToAnyDemo
    {

        // Demonstrates:
        //      Bounded BlockingCollection<T>
        //      BlockingCollection<T>.TryAddToAny()
        //      BlockingCollection<T>.TryTakeFromAny()
        public static void BC_FromToAny()
        {
            BlockingCollection<int>[] bcs = new BlockingCollection<int>[2];
            bcs[0] = new BlockingCollection<int>(5); // collection bounded to 5 items
            bcs[1] = new BlockingCollection<int>(5); // collection bounded to 5 items

            // Should be able to add 10 items w/o blocking  应该能够添加10项w/o阻塞
            int numFailures = 0;
            for (int i = 0; i < 10; i++)
            {
                if (BlockingCollection<int>.TryAddToAny(bcs, i) == -1) numFailures++;
            }
            Console.WriteLine("TryAddToAny: {0} failures (should be 0)", numFailures);

            // Should be able to retrieve 10 items 应该能够检索10个项目
            int numItems = 0;
            int item;
            while (BlockingCollection<int>.TryTakeFromAny(bcs, out item) != -1) numItems++;
            Console.WriteLine("TryTakeFromAny: retrieved {0} items (should be 10)", numItems);
        }
    }

    class ConsumingEnumerableDemo
    {
        // Demonstrates:
        //      BlockingCollection<T>.Add()
        //      BlockingCollection<T>.CompleteAdding()
        //      BlockingCollection<T>.GetConsumingEnumerable()
        public static void BC_GetConsumingEnumerable()
        {
            using (BlockingCollection<int> bc = new BlockingCollection<int>())
            {

                // Kick off a producer task
                Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        bc.Add(i);
                        Thread.Sleep(100); // sleep 100 ms between adds  间隔100毫秒的睡眠时间会增加
                    }

                    // Need to do this to keep foreach below from hanging  需要这样做，以防止下面的每一个挂起
                    bc.CompleteAdding();
                });

                // Now consume the blocking collection with foreach. 现在使用foreach使用阻塞集合。
                // Use bc.GetConsumingEnumerable() instead of just bc because the 使用bc. getconsumingenumerable()而不只是bc，因为
                // former will block waiting for completion and the latter will 前者阻塞等待完成，后者阻塞等待完成
                // simply take a snapshot of the current state of the underlying collection. 简单地获取底层集合当前状态的快照。
                foreach (var item in bc.GetConsumingEnumerable())
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
        #endregion



        #endregion





        #region 启动类


        class Program
    {
        //默认初始状态为 false
        static ManualResetEventSlim MainSignaledResetEvent;

        static ManualResetEventSlim DoWorkSignaledResetEvent;

        private static AutoResetEvent event_1 = new AutoResetEvent(true);
        private static AutoResetEvent event_2 = new AutoResetEvent(false);
        static ThreadLocal<double> _CountT = new ThreadLocal<double>(() => 0.0111);
        public static double Count
        {
            get { return _CountT.Value; }
            set { _CountT.Value = value; }
        }
        [ThreadStatic]
        static double _Cs = 0.0001;
        public static double Cs
        {
            get { return Program._Cs; }
            set { Program._Cs = value; }
        }

        readonly static object _Sync = new object();
        const int _Total = int.MaxValue;
        static long _Count = 0;
        static void Main(string[] args)
        {
            #region  Monitor 同步
            /*
            Task task = Task.Run(() => Decrement());

            for (int i = 0; i < _Total; i++)
            {
                bool lockTaken = false;
                try
                {
                    //声明排他锁  必须输入 false
                    Monitor.Enter(_Sync, ref lockTaken);
                    // 已获得锁时 为 true
                    //Console.WriteLine(lockTaken);
                    _Count--;
                }
                finally
                {
                    //释放锁
                    if (lockTaken)
                    {
                        Monitor.Exit(_Sync);
                    }
                }

            }
            task.Wait();
            Console.WriteLine(_Count);
            */
            #endregion

            #region lock
            /*   Task task = Task.Factory.StartNew(() => Decrement());
               for (int i = 0; i < _Total; i++)
               {
                   //锁定要访问的代码
                   lock (_Sync)
                   {
                       _Count++;
                   }
               }
               task.Wait();
               Console.WriteLine(_Count);
               */
            #endregion

            #region mutex
            /*    bool firstApplictionInstance;
                // Assembly 表示一个程序集，它是一个可重用、无版本冲突并且可自我描述的公共语言运行时应用程序构建基块。
                // GetEntryAssembly 获取在其中定义指定类的当前加载的程序集。
                string mutexName = Assembly.GetEntryAssembly().FullName;
                using (Mutex mutex =new Mutex(false,mutexName,out firstApplictionInstance))
                {
                    if (!firstApplictionInstance)
                    {
                        Console.WriteLine("该应用程序已经在运行");
                    }
                    else
                    {
                        Console.WriteLine("进入关闭");
                        Console.ReadLine();
                    }
                }
                */
            #endregion

            #region 重置事件 manualresetevent 和 manualreseteventslim（

            /* using (MainSignaledResetEvent = new ManualResetEventSlim()) 
             using (DoWorkSignaledResetEvent = new ManualResetEventSlim())
             {
                 Console.WriteLine("应用程序开始……");
                 Console.WriteLine("任务开始");

                 Task task = Task.Run(() => DoWork());

                 //阻挡当前线程 
                 DoWorkSignaledResetEvent.Wait();
                 Console.WriteLine("线程执行时等待…");
                 //将事件状态设置为有信号，从而允许一个或多个等待该事件的线程继续。
                 MainSignaledResetEvent.Set();

                 //等待线程结束
                 task.Wait();
                 Console.WriteLine("线程完成");
                 Console.WriteLine("线程关闭");
             }

             MRES_SetWaitReset();
             MRES_SpinCountWaitHandle();
             */


            #endregion

            #region 自动重置事件 autoresetevent
            /*
            Console.WriteLine("按Enter创建三个线程并启动它们。\r\n"+
"线程等待AutoResetEvent #1，它被创建\r\n" +
"所以第一个线程被释放。\r\n"+
"这将使AutoResetEvent #1进入未标记状态。");
            Console.ReadLine();

            for (int i = 1; i < 4; i++)
            {
                Thread t = new Thread(ThreadProc);
                t.Name = "Thread_" + i;
                t.Start();
            }
            Thread.Sleep(250);

            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("按Enter键释放另一个线程。");
                Console.ReadLine();
                event_1.Set();
                Thread.Sleep(250);
            }

            Console.WriteLine("\r\nAll线程现在正在等待AutoResetEvent #2。");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("按Enter键释放一个线程。");
                Console.ReadLine();
                event_2.Set();
                Thread.Sleep(250);
            }
            */
            #endregion

            #region 并发集合
            #region blockding collection
            /*
            AddTakeDemo.BC_AddTakeCompleteAdding();
            TryTakeDemo.BC_TryTake();
            FromToAnyDemo.BC_FromToAny();
            ConsumingEnumerableDemo.BC_GetConsumingEnumerable();
            
            */
            #endregion


            #endregion

            #region 线程本地存储

            /* Thread thread = new Thread(Dt);
             thread.Start();
             for (double i = 0; i < short.MaxValue; i++)
             {
                 Count++;
             }
             thread.Join();
             Console.WriteLine("Main"+Count);
             */
            #region ThreadStaticattribute
            /*
            //指示静态字段的值对于每个线程都是唯一的。
            Thread thread = new Thread(Dt);
            thread.Start();
            for (double i = 0; i < short.MaxValue; i++)
            {
                _Cs++;
            }
            thread.Join();
            Console.WriteLine("Main" + Count);


*/
            #endregion

            #endregion


            /********************************/
            Console.ReadLine();
        }


        #region 重置事件 manualresetevent 和 manualreseteventslim（
        public static void DoWork()
        {

            Console.WriteLine("DoWork 方法开始");
            //将事件状态设置为有信号，从而允许一个或多个等待该事件的线程继续。
            DoWorkSignaledResetEvent.Set();
            //阻止当前线程，直到设置了当前 ManualResetEventSlim 为止。
            MainSignaledResetEvent.Wait();
            Console.WriteLine("DoWork 方法结束");

        }

        static void MRES_SetWaitReset()
        {
            ManualResetEventSlim mres1 = new ManualResetEventSlim(false); // initialize as unsignaled
            ManualResetEventSlim mres2 = new ManualResetEventSlim(false); // initialize as unsignaled
            ManualResetEventSlim mres3 = new ManualResetEventSlim(true);  // initialize as signaled

            // 启动一个操作mres3和mres2的异步任务
            var observer = Task.Factory.StartNew(() =>
            {
                mres1.Wait();
                Console.WriteLine("观察者看到信号mres1!");
                Console.WriteLine("观察者重置mres3……");
                mres3.Reset(); // 应该切换到无信号状态吗
                Console.WriteLine("观察者信号mres2");
                mres2.Set();
            });

            Console.WriteLine("主线程:mres3.IsSet= {0} (应该是真的)", mres3.IsSet);
            Console.WriteLine("主线程信号mres1");
            mres1.Set(); // 这将“启动”观察者任务
            mres2.Wait(); // 在观察者任务完成重新设置mres3之前，它不会返回
            Console.WriteLine("主线程看到信号mres2!");
            Console.WriteLine("主线程:mres3.IsSet= {0} (应该是真的)", mres3.IsSet);

            // 当您完成时，Dispose()一个ManualResetEventSlim是一种很好的形式
            observer.Wait(); // 确保这已经完全完成
            mres1.Dispose();
            mres2.Dispose();
            mres3.Dispose();
        }

        // Demonstrates:
        //      ManualResetEventSlim construction w/ SpinCount
        //      ManualResetEventSlim.WaitHandle
        static void MRES_SpinCountWaitHandle()
        {
            // Construct a ManualResetEventSlim with a SpinCount of 1000
            // Higher spincount => longer time the MRES will spin-wait before taking lock
            ManualResetEventSlim mres1 = new ManualResetEventSlim(false, 1000);
            ManualResetEventSlim mres2 = new ManualResetEventSlim(false, 1000);

            Task bgTask = Task.Factory.StartNew(() =>
            {
                // 稍等片刻
                Thread.Sleep(100);

                // 现在两个都发信号
                Console.WriteLine("任务信号两个MRESes");
                mres1.Set();
                mres2.Set();
            });

// MRES的常见用法。WaitHandle是使用MRES作为参与者
// WaitHandle.WaitAll / WaitAny。注意访问MRES。WaitHandle将
//导致基本的ManualResetEvent的无条件膨胀。
            WaitHandle.WaitAll(new WaitHandle[] { mres1.WaitHandle, mres2.WaitHandle });
            Console.WriteLine("WaitHandle.WaitAll (mres1。WaitHandle mres2.WaitHandle)完成。");

            // Clean up
            bgTask.Wait();
            mres1.Dispose();
            mres2.Dispose();
        }

        #endregion

        #region autoresetevent

        static void ThreadProc()
        {
            string name = Thread.CurrentThread.Name;

            Console.WriteLine("{0} waits on AutoResetEvent #1.", name);
            event_1.WaitOne();
            Console.WriteLine("{0} is released from AutoResetEvent #1.", name);

            Console.WriteLine("{0} waits on AutoResetEvent #2.", name);
            event_2.WaitOne();
            Console.WriteLine("{0} is released from AutoResetEvent #2.", name);

            Console.WriteLine("{0} ends.", name);
        }

        #endregion

        #region 线程本地存储
        static void Dt()
        {
            Count = -Count;
            for (double i = 0; i < short.MaxValue; i++)
            {
                // Count--;
                _Cs--;
            }
            Console.WriteLine("DT"+Count);
        }

        #endregion

        static void Decrement()
        {
            for (int i = 0; i < _Total; i++)
            {
                #region Monitor 同步
                /*
                 bool lockTaken = false;
                 try
                 {
                     Monitor.Enter(_Sync, ref lockTaken);
                     _Count--;
                 }
                 finally
                 {
                     if (lockTaken)
                     {
                         Monitor.Exit(_Sync);
                     }
                 }
                 */
                #endregion

                //锁定要访问的代码
                lock (_Sync)
                {
                    _Count--;
                }

            }
        }

 



    }



    #endregion































































    /*************************************************/
}
