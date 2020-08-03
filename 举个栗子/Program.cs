using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Drawing;
using System.Collections;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;
using System.Net;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace 举个栗子
{

    #region 终止任务和取消任务
    /*
     *  AppDomain （应用程序域，一般用来观察程序中未被捕捉的异常 有一个 UnhandledException 方法专门用来解决这种情况，当发现有未处理异常的时候他会强行把线程杀死（又叫野蛮终止）
     *  这时程序处于中断模式）
     *但这不是一个很好的方法，最佳的事件就是避免所有未处理异常，不管是是工作者线程还是主线程。
     *
     *当然， 还有一种温油的方法可以使线程终止（CancellationToken），那就是取消任务（又叫协作是取消，他和野蛮终止不同，他会等待当前线程执行完成后才会终止线程）相对于 App Domain  来说
     * CancellatTokenSour.cancel() 并不是野蛮终止 正在执行的TAsk,任务会继续运行，直到他检查状态，发现标志所有者已发送请求状态，这时才会得体地关掉任务
     * 调用cancel ()实际上会在 CancellationToken 复制的所有取消上设置 ISCancellationRequested 属性
     * note：
     * 提供给异步任务的是cancellationToken(而不是CancellationTokenSource, 他只是一个事件触发类)cancellationToken 是我们能轮询请求（并行编程的魅力），
     * 而CancellationTokenSoure 负责提供标志，并在取消时发出通知（cancel() 方法接受信号（取消任务信号）
     * 
     * CancellationToken 是结构，所以能复值，cts.Token 返回的是标志（也就是 Asyncoperation1（）方法）的副本。
     * 
     * 为了监视ISCancellationRequested 属性，CancellationToken的一个实例（从cancellationTokenSoure.Token 获取） 传给并行任务 ，Asyncoperation4 方法 每次循环都会检查  
     * ISCancellationRequested 属性，如果返回true就退出
     * 
     * 说明书上提到 的register()方法（重载），通过这个方法可以登记一个操作，在标志取消时调用，调用 Register（）方法将登记
     * cancellationTokenSoure的cancel 上的一个监视器
     * 
     * 
     * 
     */


    //举个栗子  AppDomain  下的 UnhandledException 方法

    #region AppDomain 


    //    public static Stopwatch clock = new Stopwatch();//计时器
    //    public static void Main()
    //    {
    //        try
    //        {
    //            clock.Start();
    //            /*
    //             * s  未处理异常事件的源   e 一个包含事件数据的UnhandledExceptionEventArgs。  UnhandledExceptionEventArgs 为在任何应用程序域中不处理异常时引发的事件提供数据。
    //             * 
    //             */
    //            AppDomain.CurrentDomain.UnhandledException += (s, e) => { Message("Event handler starting"); Delay(4000); };
    //            /*  当一个系统   表示处理没有事件数据的事件的方法。*/
    //            //AppDomain.CurrentDomain.DomainUnload  += (s,e) => { Message("Event handler starting"); Delay(1111); };

    //           // var tt = AppDomain.MonitoringIsEnabled.ToString();//获取或设置一个值，该值指示是否对应用程序的CPU和内存进行监视为当前进程启用域，一旦为流程启用了监视，就不能禁用它。若启用监察，则为true;

    //            Thread thread = new Thread(() => { Message("Throwing exception."); throw new Exception();});//实例线程
    //            thread.Start();//启动
    //            Delay(2000);
    //            //Console.WriteLine("好似天上的星，沉淀在水底的梦。再加上 {0}", tt);
    //        }
    //        finally//始终返回这个内容
    //        {
    //            Message("Finally block running.");
    //        }
    //    }

    //    static void Delay(int i)
    //    {

    //        Message($"Sleeping for {i} ms");
    //        Thread.Sleep(i);//挂起线程
    //        Message("Awake");
    //    }
    //    static void Message(string text)
    //    {
    //        Console.WriteLine("{0}:{1:0000}:{2}", Thread.CurrentThread.ManagedThreadId, clock.ElapsedMilliseconds, text);
    //        //由于使用了线程，所以结果顺序可能会不一样，但结果始终是一样的，类似一 Thread.for() 那个例子
    //    }
    //}
    #endregion

    #region CancellationTokenSource
    //   https://www.cnblogs.com/lori/p/7483960.html
    //http://www.codefans.net/articles/1687.shtml

    //public static void Main()
    //{
    //    using (var cts = new CancellationTokenSource()) // CancellationTokenSource 时候触发类。
    //    {

    //        CancellationToken token = cts.Token;// CancellationToken  传播应取消操作的通知。
    //        ThreadPool.QueueUserWorkItem(_ => Asyncoperation1(token));//看一下能用到谁
    //        Thread.Sleep(TimeSpan.FromSeconds(1));//万金油角色，计时器供应商
    //        cts.Cancel();//取消请求
    //        token.Register(Asyncoperation5);//登记调用一个方法
    //    }
    //    using (var cts = new CancellationTokenSource())
    //    {
    //        CancellationToken token = cts.Token;
    //        ThreadPool.QueueUserWorkItem(_ => Asyncoperation2(token));
    //        Thread.Sleep(TimeSpan.FromSeconds(2));
    //    }
    //    using (var cts = new CancellationTokenSource())
    //    {
    //        CancellationToken token = cts.Token;
    //        ThreadPool.QueueUserWorkItem(_ => Asyncoperation3(token));
    //        Thread.Sleep(TimeSpan.FromSeconds(2));
    //    }
    //    using (var cts = new CancellationTokenSource())
    //    {
    //        CancellationToken token = cts.Token;
    //        ThreadPool.QueueUserWorkItem(_ => Asyncoperation4(token));
    //        Thread.Sleep(TimeSpan.FromSeconds(2));
    //    }
    //        Thread.Sleep(TimeSpan.FromSeconds(2));
    //    Console.ReadKey();
    //}
    //static void Asyncoperation1(CancellationToken token)
    //{
    //    Console.WriteLine("第一个");
    //    for (int i = 0; i < 5; i++)
    //    {
    //        if (token.IsCancellationRequested)//盘对任务是否取消  如果任务被取消，就是true 
    //        {
    //            Console.WriteLine("取消第一个");
    //            return;
    //        }
    //        Thread.Sleep(TimeSpan.FromSeconds(1));//相当于一个计时器
    //    }
    //    Console.WriteLine("第一项任务完成，估计用不上");
    //}

    //static void Asyncoperation2(CancellationToken token)
    //{
    //    try
    //    {
    //        Console.WriteLine("第二");
    //        for (int i = 0; i < 5; i++)
    //        {
    //            token.ThrowIfCancellationRequested();
    //            Thread.Sleep(TimeSpan.FromSeconds(1));
    //        }
    //        Console.WriteLine("取消第二个任务");
    //    }
    //    catch (Exception)
    //    {

    //        Console.WriteLine("取消第二个任务，完成");
    //    }
    //}
    //static void Asyncoperation3(CancellationToken token)
    //{

    //    bool cancellationFlag = false;
    //    token.Register(() => cancellationFlag = true);
    //    Console.WriteLine("第三");
    //    for (int i = 0; i < 5; i++)
    //    {
    //        if (cancellationFlag)
    //        {
    //            Console.WriteLine("取消第三个");
    //            return;
    //        }
    //        Thread.Sleep(TimeSpan.FromSeconds(1));
    //    }
    //    Console.WriteLine("第三个已经取消");
    //}
    //static void Asyncoperation4(CancellationToken token)
    //{
    //    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    //    int i = 0;
    //    Console.WriteLine("第四个");
    //    while (!token.IsCancellationRequested)
    //    {
    //        Console.WriteLine(DateTime.Now); //输出当前时间
    //        Thread.Sleep(3000);
    //        Console.WriteLine("结束了");
    //        Console.ReadLine();//现在这个状态是把线程挂起了呢？还是把线程杀死了呢？嗯，等待键入然后线程继续？
    //    }

    //}
    //static void Asyncoperation5()
    //{
    //    Console.WriteLine("被登记调用的方法");
    //}







    #endregion
    #endregion

    #region Task.Run()
    /* 
     * cpu 密集型方法：参考资料
     * https://blog.csdn.net/youanyyou/article/details/78990156
     * 
     * Task.Run() 和Task.Factory.StarNew()的区别
     * https://www.cnblogs.com/wangwust/p/9493028.html
     * 
     * https://www.cnblogs.com/zhao123/p/9999607.html
     * 
     * Task.Run() 是Task.Factory.StarNew()的简化形式（总的来说，Task.Factory.StarNew() 
     * 可以设置线程长时间运行，所以如果需要线程长时间运行行就用Task.Factory.StarNew() 否则就用 Task.Run(),反过
     * Task.Factory.StarNew() 就是 Task.Run()的原始版，比Tsak.Run 要多一些功能。
     * 比如说设置长时间运行的（线程）任务 啥的），他一般用于调用一个要求创建额外线程的cpu 密集型方法。在.net 4.5中，应该默认使
     * Task.Run() 方法
     * 
     * http://www.mamicode.com/info-detail-1780424.html
     */
    //举个栗子

    //public static void Main()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        Task.Factory.StartNew(() =>
    //        {
    //            try
    //            {
    //                Console.WriteLine("Task.Factory.StarNew()--- 线程id{0} --- i 的值{1}---当前时间{2}", Task.CurrentId, i, DateTime.Now);
    //                throw new Exception();
    //            }
    //            catch (Exception e)
    //            {

    //                Console.WriteLine("出现并抛出一个异常");

    //            }

    //        });
    //        Thread.Sleep(100);
    //    }
    //    for (int j = 0; j < 10; j++)
    //    {
    //        Task.Run(() =>
    //        {
    //            try
    //            {
    //                Console.WriteLine("Task.Run()---- 线程id{0} --- j 的值{1}---当前时间{2}", Task.CurrentId, j, DateTime.Now);
    //            }
    //            catch (Exception e)
    //            {

    //                Console.WriteLine("出现并抛出一个异常");

    //            }
    //        });
    //    }
    //  Task task = Task.Factory.StartNew(() => Console.WriteLine("好似天上的星，沉淀在水底的梦{0}", DateTime.Now), TaskCreationOptions.LongRunning);
    // Task task = Task.Factory.StartNew(() => test());
    // }

    //static void Main()
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            Task.Factory.StartNew(Launch).Wait();
    //        }
    //    }
    //    static void Launch()
    //    {
    //        Console.WriteLine("开始: {0}",
    //                          Thread.CurrentThread.ManagedThreadId);
    //        Task.Factory.StartNew(Nested).Wait();//等待任务执行完成
    //    }
    //    static void Nested()
    //    {
    //        Console.WriteLine("结束: {0}",
    //                          Thread.CurrentThread.ManagedThreadId);
    //    }
    //}



    #endregion

    #region  TaskCreationOptions.LongRunning

    /*
     * TaskCreationOptions.LongRunning  线程占用CPU的时间是由时间片（防止占用的线程过多，而卡死，
     * 所以就给线程一个在处理器的运行时间，不过你有没有完成，时间到了，就把线程停止）
     * 决定的，并且线程运行的很快，运行时间很短，
     * 
     * 但是，如果我想让一个任务长时间运行呢？（就比如那个轮询，那个监听啥的，
     * 我想让一个人就在一条路上长时间的来回奔走（公交车）那么这个人（独立的）就要是一直活跃的，又该怎么做呢？
     * TaskCreationOptions.LongRunning 就能很好的解决这个问题）
     * 
     * 说明书上说当一个任务要长时间运行时，会霸占 一个底层线程，
     * 就可以通知调度器任务不会太快结束任务。 这个通知有两方面的作用。
     * 1. 他可以提醒调度器或许应该为这个要长时间运行的任务创建一个单独的线程（而不是来自线程池的，就相当于一块地，
     * 有若干部分，其中有一部分就只种一种作物，不如说西红柿，鸡蛋啥的）。
     * 
     * 2. 他提醒调度器可能应该调度比平时更多的线程。这样就会造成更多的时间片,有了更多的线程，就能做更多的事
     * 我们不希望长时间运行的任务霸占整个处理器，让其他短时间的任务没法运行，
     * 短时间运行的任务利用分配到的时间片，能在短时间内完成大部分工作，
     * 而长时间运行的任务基本注意不到因为和其它任务共好像处理器而产生的些许延迟，为此要再调用StartNew（）时使用 TackCreationOptions 选项
     * 
     * 我们并不希望长时间执行的任务霸占整个处理器，就让其他短时间的任务没法运行。段时间允许的任务利用分配到底时间片，
     * 在短时间内完成大部分工作，而长时间运行的任务   基本注意不到 （因为短任务执行时间段，所以个可看成是暂时将长线程挂起）
     * 因为其他任务共享处理器而产生的延迟。
     * 因此调用StarNeew()时应该使用 Task.Factory.StarNew() 下面的 TaskCreationOptions.LongRunning() 方法，
     * 因为Task.Run() 可以说是  Task.Factory.StarNew() 的简化版 Task.Run()碰巧不支持这个长时间运行的要求，但 ask.Factory.StarNew() 
     * 下面的 TaskCreationOptions.LongRunning() 方法 可以
     * 
     * https://www.cnblogs.com/wangdaijun/p/5924462.html
     * 
     * 
     * 举个栗子
     */
    //public static void Main( )
    //{

    //    for (int i = 0; i < 100; i++)
    //    {


    //        Task task = Task.Factory.StartNew(
    //           () =>
    //            Test(i),
    //            TaskCreationOptions.LongRunning);
    /*
     * TaskCreationOptions
     * 
     * 指定控制创建和执行的可选行为的标志
     * 
     * LongRunning
     * 指定任务将是长期运行的粗粒度操作
     * 
     * 那么问题来了，啥时粗粒度呢？ 总的来说就是把程序看成一个房子，
     * 粗粒度就是房间，细粒度就是 房间的装饰（或者说时线程占用cpu 的时间的长短） 这个东西是相对的，
     * 相对于线程来说，他的粗细粒度就是他 占用cpud 的时间，又比如说数据库的设计 他的粗粒度就是他的表的数量和表和图表之间的连接，
     * 或者说是他的数据表的冗余程度（数据库设计原则：挨最少的打，输出最高的仔），（冗余这东西现在还看不出来。。以后有时间研究）
     * 嗯？那他的标准又是什么呢？额，为什么要有标准呢？对呀，为什么要有标准呢？哈哈
     * 粗细粒度 资料参考
     * https://zhidao.baidu.com/question/420895088.html
     * https://blog.csdn.net/yechaodechuntian/article/details/21601659
     * https://blog.csdn.net/tashanhongye/article/details/47665989
     * 比细粒度系统更少、更大的组件。它提供了一个提示
     * System.Threading.Tasks。
     * 任务调度程序的超订阅可能是必要的。
     * 
     * 超订阅 ：总的来说，就是创建比机器能控制的更多的线程（这个多出来地线程就是要长时间运行地任务），理论上线程可以创建无数个，
     * 宏观上说线程是同步的（因为CPU运算速度很快，所以可以看成时同步的），但从微观上来说，线程还是一条一条地执行的，
     * 所以他可以创建无数个线程等待执行但是 容器就那么大，
     * 如果线程地数量大于容器地话很可能会把容器撑坏 
     * 
     * 
     * 超订阅允许您创建比可用硬件数量更多的线程
     * 线程。它还向任务调度程序提供了一个提示，即有一个额外的线程
     * 可能需要该任务，以便它不会阻碍前进的进度
     * 本地线程池队列上的其他线程或工作项。
     * 
     * 
     */
    //        task.Wait();
    //    }
    //}



    //public static void Test(int i)
    //{
    //    Console.WriteLine(i);
    //}




    //网上造的

    #endregion

    #region 匹配字符
    //public static void Main()
    //{
    //    Program t = new Program();
    //    t.ttt();
    //}
    //public  void ttt()
    //{
    //    string t = "huhuhuhuhuhhuhuhuhuhhhhhhhhhhhuhuhuhuhuhhuhuhu";
    //    int a = 0;
    //    int b = 0;


    //    foreach (Match m in Regex.Matches(t,"uh")) 
    //    {
    //        a++;
    //    }


    //    foreach(char value in t)
    //    {
    //        if (Convert.ToString(value) =="h")
    //            b++;
    //    }

    //    Console.WriteLine("这个使用正则写的{0} \n 这个是用foreach写的{1}", a,b);

    //}

    #endregion

    #region 任务进行资源清理  IDisposable
    /*
     * 
     * https://www.cnblogs.com/wyt007/p/9304564.html
     * 经常会听到说释放资源，那么到底什么是资源呢？简单地来说，
     * 在c# 中每种类型都是一种资源，而资源又分托管资源和非托管资源
     * 
     * 非托管资源：不受CLR（公共语言运行库,只是一个类库，一个将编程语言类型转化的类库）
     * 控制的资源，也就是不属于.NET本身的功能，
     * 往往是通过调用跨平台程序集(如C++)或者操作系统提供的一些接口，
     * 比如Windows内核对象、文件操作、数据库连接、socket、Win32API、网络等。
     * 
     * 托管资源：由CLR管理分配和释放的资源，也就是我们直接new出来的对象；
     * 
     * IDisposable 是一个用于垃圾回收的接口，里面定义有一个dispose() 方法  对任务资源进行清理的方法
     * 
     * Task 支持IDisposable 因为Task 可能在等待完成时分配一个WaitHandle.
     * 由于Waithandle 支持 Idsposable(接口被继承了),所以可以用
     * 
     * 
     * 
     * 
     */

    //public class MyClass : IDisposable
    //{
    /// <summary>
    /// 模拟一个非托管资源
    /// </summary>
    /// 
    /*
     *  IntPtr 一种特定于平台的类型，用于表示指针或句柄。
     *  参考资料：
     *  https://www.cnblogs.com/nimorl/p/9829271.html
     *  
     */
    //private IntPtr NativeResource { get; set; } = Marshal.AllocHGlobal(100);
    // * Marshal 提供一组方法，用于分配非托管内存、复制非托管内存块、将托管类型转换为非托管类型，
    // * 以及在与非托管代码交互时使用的其他杂项方法。
    // * 
    // * AllocHGlobal(int 内存中所需的字节数.)使用指定的字节数从进程的非托管内存中分配内存。
    // * 
    // */
    ///// <summary>
    ///// 模拟一个托管资源
    ///// </summary>
    //public Random ManagedResource { get; set; } = new Random();//随机类
    //    /// <summary>
    //    /// 释放标记
    //    /// </summary>
    //    private bool disposed;
    //    /// <summary>
    //    /// 为了防止忘记显式的调用Dispose方法
    //    /// </summary>
    //    ~MyClass()
    //    {
    //        //必须为false
    //        Dispose(false);
    //    }
    //    /// <summary>执行与释放或重置非托管资源关联的应用程序定义的任务。</summary>
    //    public void Dispose()
    //    {
    //        //必须为true
    //        Dispose(true);
    //        //通知垃圾回收器不再调用终结器
    //        GC.SuppressFinalize(this);
    //    }
    //    /// <summary>
    //    /// 非必需的，只是为了更符合其他语言的规范，如C++、java
    //    /// </summary>
    //    public void Close()
    //    {
    //        Dispose();//这个是 IDisposable 接口下的一个清理资源的方法
    //    }
    //    /// <summary>
    //    /// 非密封类可重写的Dispose方法，方便子类继承时可重写
    //    /// </summary>
    //    /// <param name="disposing"></param>
    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (disposed)
    //        {
    //            return;
    //        }
    //        //清理托管资源
    //        if (disposing)
    //        {
    //            if (ManagedResource != null)
    //            {
    //                ManagedResource = null;
    //            }
    //        }
    //        //清理非托管资源
    //        if (NativeResource != IntPtr.Zero)
    //        {
    //            Marshal.FreeHGlobal(NativeResource);
    //            NativeResource = IntPtr.Zero;//一个只读字段，表示已初始化为零的指针或句柄。
    //    }
    //        //告诉自己已经被释放
    //        disposed = true;
    //    }
    //}


    //#region IDisposable 成员
    //public string _BarFont ="vdzvzz";


    //public void Dispose()
    //{
    //    // TODO:  添加 Code128Rendering.Dispose 实现
    //    this.Dispose(true);
    //    GC.SuppressFinalize(true);
    //    // GC 控制系统垃圾收集器，该服务自动回收未使用的内存。
    //    //SuppressFinalize 请求公共语言运行时不调用指定对象的终结器。
    //}
    //protected virtual void Dispose(bool disposing)
    //{
    //    if (!disposing)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        //要释放的资源
    //        if (_BarFont != null) this._BarFont.Dispose();
    //    }
    //}
    //#endregion

    #endregion

    #region 基于任务的异步模式
    /*
     * 在处理异步工作时，任务（task 不单是☞线程，也可以是进程，
     * 线程寄托于进程中，可以共享进程的所有资源，而任务就是对这一系列动作的描述。
     * 
     * 在Mac、Windows NT等采用微内核结构的操作系统中，进程的功能发生了变化：
     * 它只是资源分配的单位，而不再是调度运行的单位。在微内核系统中，真正调度运行的基本单位是线程。
     * 因此，实现并发功能的单位是线程。）提供了比线程更好的抽象，任务自动调度为恰当数量的线程。
     * 任务自动调度为恰当数量的线程。
     * 
     * 进程，线程，任务的区别：参考资料
     * https://www.cnblogs.com/tianqiang/p/9994516.html
     * 
     * 同步和异步的概念：
     * 
     * 同步：就是单线程模式，(假设 有 a,b 两个方法) a 调用了 b 就只执行 b
     * 异步：多线程模式 ， a 调用了b 执行b 的同时也执行 a ,他们之间互不影响 
     **************************************************
     * 
     * 消息的同步： 很像 tcp 的三次握手
     * 消息的异步： 很像 udp 的广播式通信
     * 
     * https://www.cnblogs.com/rainbow70626/p/8094199.html
     * 
     * 
     * 举个栗子 说明书上的
     */
    //同步 WEB 请求
    #region 同步 WEB 请求
    /*
     * 有点像 jQuery 的思路，当他调用其他方法是当前方法会被阻塞，知道I/O 操作结束。异步工作进行期间，线程就被白白浪费了。
     * 就相当于 原本有三个人，但却只用到了一个人，并且让这一个人去做一系列的事，但他只能一件一件地去做，
     * 在做这件事的时候，其他的任务就阻塞了，原本有三个人，却只让一个人去做，这不就白白浪费了资源了吗？
     * 
     * 用 try/catch 描述控制流 ，给一个 WebRequest 让后调用GetResponse （）方法下载页面  利用 GetResponseStream () 方法读取网页信息
     * 并将信息赋值给 reader 然后调用 ReadToEnd  读取流 然后在判断网页的大小并打印。
     */
    //public class Program
    //{

    //    public static Stopwatch k = new Stopwatch();


    //    public static void Main(string[] args)
    //    {

    //        string url = "https://docs.microsoft.com/en-us/dotnet/api/system.net.webrequest.getresponse?view=netframework-4.8#System_Net_WebRequest_GetResponse";//要访问的网页

    //        if (args.Length > 0)
    //        {
    //            url = args[0];
    //        }
    //        k.Start();
    //        try
    //        {
    //            Console.WriteLine(url);
    //            WebRequest webRequest = WebRequest.Create(url);
    //            //WebRequest 向统一资源标识符(URI)发出请求。发出请求
    //            //Create（url）访问网页
    //            WebResponse response = webRequest.GetResponse();
    //            //WebResponse 提供来自统一资源标识符(URI)的响应。 响应网页
    //            // GetResponse 包含对Internet请求的响应的WebResponse。  下载网页
    //            Console.WriteLine(".......");

    //            // StreamReader 从特定编码的字节流中读取字符的TextReader。
    //            //GetResponseStream 用于从Internet（英特网）资源中读取数据。GetResponseStream  ,对网页进行流的访问
    //            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
    //            {
    //                string text = reader.ReadToEnd();
    //                //流的其余部分作为字符串，从当前位置到结束。如果当前位置位于流的末尾，则返回一个空字符串
    //                Console.WriteLine(
    //                    FormatBytes(text.Length));
    //            }
    //        }
    //        catch (WebException)//通过可插拔协议访问网络时发生错误时引发的异常。
    //        {

    //            throw;
    //        }
    //        catch (IOException)//发生I/O错误时引发的异常。
    //        {
    //            throw;
    //        }
    //        catch (NotSupportedException)
    //        //当调用的方法不受支持时，或当试图读取、查找或写入不支持所调用功能的流时引发的异常。
    //        {
    //            throw;
    //        }
    //        k.Stop();
    //        Console.WriteLine("{0}毫秒", k.ElapsedMilliseconds);
    //    }

    //    static public string FormatBytes(long bytes)
    //    {
    //        string[] manitudes = new string[] { "GB", "MB", "KB", "Bytes" };

    //        long max = (long)Math.Pow(1024, manitudes.Length);//设置容量
    //        return string.Format("{1:##.##} {0}",//用字符串表示形式替换格式项的格式的副本即arg0和arg1。
    //                                             //  public static String Format(String format, object arg0, object arg1);
    //        manitudes.FirstOrDefault(
    //                //FirstOrDefault  取序列中满足条件的第一个元素，如果没有元素满足条件，则返回默认值
    //                //还有一个 First  取序列中满足条件的第一个元素，如果没有元素满足条件， 抛出异常
    //                //linq 的方法，理解不深，回头补上
    //                manitude => bytes > (max /= 1024)) ?? "0 Bytes", (decimal)bytes / (decimal)max);
    //        //?? 是 C#2.0 中新增的运算符，可以认作三元运算符的简化版，
    //        // 起主要作用是 如果 ?? 运算符的左操作数非空，运算符就返回左操作符树，否则就返回右操作符数
    //        //相当于 if else
    //    }
    //}
    #endregion

    //再举个栗子
    #region TPL 异步调用高延迟操作  
    //public class Program
    //{
    //    //TPL 面向连接的、可靠的、基于字节流的传输层通信协议
    //    public static Stopwatch Stopwatch = new Stopwatch();//定义一个计时器
    //    public static void Main(string[] args)
    //    {
    //        // Stopwatch.Start();
    //        //https://www.cnblogs.com/tianqiang/p/9994516.html &&
    //        string url = "https://baike.baidu.com/item/TCP/33012?fr=aladdin";//要访问的地址
    //        if (args.Length > 0)
    //        {
    //            url = args[0];
    //        }
    //        Console.WriteLine(url);
    //        Task task = WriteWebReauestSizeAsync(url);//委托一个线程执行这个方法，并且将地址传过去
    //        try
    //        {
    //            while (!task.Wait(100))//等待时间，单位是毫秒
    //            {
    //                Console.WriteLine("如果目标还没执行完就输出。。。。。");
    //                //如果目标还没执行完就输出。。。。。
    //            }
    //        }
    //        catch (AggregateException exception)//描述错误的消息
    //        {
    //            exception = exception.Flatten();
    //            // 将异常包装成一个实例，传递然后传递给他的父任务
    //            try
    //            {     //处理异常
    //                exception.Handle(innerException =>
    //                {   //表示一个异常，
    //                    //Capture 表示代码中当前点上指定的异常的对象。
    //                    //InnerException 描述当前的异常的对象
    //                    ExceptionDispatchInfo.Capture(exception.InnerException).Throw(); return true;
    //                    //ExceptionDispatchInfo 引发每个异常，以确保不会丢失原始的栈跟踪
    //                });
    //            }
    //            catch (WebException e)
    //            {
    //                Console.WriteLine("错误信息：{0}", e.Message);
    //                Console.WriteLine("堆栈信息：{0}", e.StackTrace);
    //            }
    //            catch (IOException e)
    //            {
    //                Console.WriteLine("错误信息：{0}", e.Message);
    //                Console.WriteLine("堆栈信息：{0}", e.StackTrace);
    //            }
    //            catch (NotSupportedException e)
    //            {
    //                Console.WriteLine("错误信息：{0}", e.Message);
    //                Console.WriteLine("堆栈信息：{0}", e.StackTrace);
    //            }
    //        }
    //        //Stopwatch.Stop();
    //        //Console.WriteLine("总时间{0} 毫秒", Stopwatch.ElapsedMilliseconds);
    //    }
    //    private static Task WriteWebReauestSizeAsync(string url)
    //    {
    //        StreamReader reader = null;//读取流
    //        WebRequest webRequest = WebRequest.Create(url);//创建一个请求地址的动作
    //        //ContinueWith 延续
    //        Task task = webRequest.GetResponseAsync().ContinueWith(antecedent =>//表示异步操作的任务对象。
    //        {
    //            WebResponse respomse = antecedent.Result;//
    //            reader = new StreamReader(respomse.GetResponseStream());//初始化 io 实例
    //            return reader.ReadToEndAsync();
    //            //表示异步读取操作的任务。TResult的值参数包含一个字符串，其中包含从当前位置到流末尾的字符。
    //            //Unwrap()  System.Threading.Tasks。任务，表示提供的任务<任务< T > >的异步操作(c#)或Task(Of Task(Of T)) (Visual Basic)。
    //        }).Unwrap().ContinueWith(antecedent =>{ if (reader != null) reader.Dispose();// Dispose 释放System.IO使用的所有资源
    //            string text = antecedent.Result;//获取当前结果
    //            // Console.WriteLine(text);
    //            Console.WriteLine(FormatBytes(text.Length));
    //        });
    //        return task;
    //    }
    //    static public string FormatBytes(long bytes)//用于转换单位
    //    {
    //        string[] manitudes = new string[] { "GB", "MB", "KB", "Bytes" };
    //        long max = (long)Math.Pow(1024, manitudes.Length);//设置容量
    //        return string.Format("{1:##.##} {0}",
    //            manitudes.FirstOrDefault(manitude => bytes > (max /= 1024)) ?? "0 Bytes", (decimal)bytes / (decimal)max);
    //用字符串表示形式替换格式项的格式的副本即arg0和arg1。
    //  public static String Format(String format, object arg0, object arg1);
    //FirstOrDefault  取序列中满足条件的第一个元素，如果没有元素满足条件，则返回默认值
    //还有一个 First  取序列中满足条件的第一个元素，如果没有元素满足条件， 抛出异常
    //linq 的方法，理解不深，回头补上，还有lambda 表达式啥的也一样
    //?? 是 C#2.0 中新增的运算符，可以认作三元运算符的简化版，
    // 起主要作用是 如果 ?? 运算符的左操作数非空，运算符就返回左操作符树，否则就返回右操作符数
    //相当于 if else
    //    }
    //}
    #endregion


    //这个是需要深入研究的，现在只懂皮毛，等以后基础扎实了加来深入研究
    #region 通过 async 和 await 实现基于任务的异步模式
    /*
     * 
     * 关于线程其他好玩的东西
     * https://www.cnblogs.com/yaopengfei/p/9206924.html
     * 
     * 参考资料：
     * https://www.cnblogs.com/feipeng8848/p/10188871.html
     * https://www.cnblogs.com/yaopengfei/archive/2018/07/02/9249390.html
     * 
     * cpu-bound 和 io-bound
     * https://www.jianshu.com/p/6cfafdf0a320
     * async 上下文关键字指示编译器表达式重写为一个状态机来代表 异步的web 请求的控制流 注意，状态机里面时不创建线程的 。它可以做的事调用线程
     * await 代表等待该行方法执行完成
     * 
     * async await 是一种异步编程的模型，他本身并不能开启新线程，多用于一些非阻止 API 或者 开启新线程的操作封装起来，使其调用的时候像同步方法一样使用
     * 
     * async /await 只是一个状态机，await 时释放当前线程 -> 进入状态机等待异步操作完成 ->退出状态级在一个线程中继续执行
     * “进入状态机等待异步操作完成”，有两种操作，
     * 一种时 cpu-bound ,如 Task.Run,这时异步操作会在另一个线程中执行；
     * 一种是 IO-bound,这时异步操作不会占用线程
     * 
     * 
     */
    //public class Program
    //{
    //    private static async Task WriteWebRequestSizeAsync(string url)
    //    {   // async 修饰的方法必须返回 Task Task<T> 或 void 这个方法返回的是Task
    //        //方法名使用了 Async 后缀，一般用这个来标识异步方法。
    //        try
    //        {
    //            WebRequest webRequest = WebRequest.Create(url);//请求地址
    //            //针对和同步方法等价的每个异步版本，都在异步版本之前插入了 await 关键字
    //            WebResponse response = await webRequest.GetResponseAsync();//表示异步操作的任务对象。
    //            using (StreamReader reader = new StreamReader(response.GetResponseStream()))//读取从请求地址下载的流文件
    //            {
    //                string text = await reader.ReadToEndAsync(); //其中包含从当前位置到流末尾的字符。
    //                Console.WriteLine(FormatBytes(text.Length));
    //            }
    //        }
    //        catch (WebException e)
    //        {
    //            Console.WriteLine("错误信息：{0}", e.Message);
    //        }
    //        catch (IOException e)
    //        {
    //            Console.WriteLine(e.Message);
    //        }
    //        catch (NotSupportedException e)
    //        {
    //            Console.WriteLine(e.Message);
    //        }

    //    }
    //    public static void Main(string[] args)
    //    {
    //        string url = "https://blog.csdn.net/weixin_39984161/article/details/96969704";
    //        if (args.Length > 0)
    //        {
    //            url = args[0];
    //        }
    //        Console.WriteLine(url);
    //        Task task = WriteWebRequestSizeAsync(url);//线程调用
    //        while (!task.Wait(100))
    //        {
    //            Console.WriteLine("仁德，律(立)己，自强");//严于律己，宽以恕人。
    //        }
    //    }
    //    static public string FormatBytes(long bytes)//用于转换单位3
    //    {
    //        string[] manitudes = new string[] { "GB", "MB", "KB", "Bytes" };

    //        long max = (long)Math.Pow(1024, manitudes.Length);//设置容量
    //        return string.Format("{1:##.##} {0}",//用字符串表示形式替换格式项的格式的副本即arg0和arg1。
    //                                             //  public static String Format(String format, object arg0, object arg1);
    //        manitudes.FirstOrDefault(manitude => bytes > (max /= 1024)) ?? "0 Bytes", (decimal)bytes / (decimal)max);
    //FirstOrDefault  取序列中满足条件的第一个元素，如果没有元素满足条件，则返回默认值
    //还有一个 First  取序列中满足条件的第一个元素，如果没有元素满足条件， 抛出异常
    //linq 的方法，理解不深，回头补上，还有lambda 表达式啥的也一样
    //?? 是 C#2.0 中新增的运算符，可以认作三元运算符的简化版，
    // 起主要作用是 如果 ?? 运算符的左操作数非空，运算符就返回左操作符树，否则就返回右操作符数
    //相当于 if else

    //    }

    //}




    #endregion

    #region AggregateException2
    /*
     * 平时用到的都是捕捉单个的异常，而在将任务分解成多个并行的小任务，这时候出现的异常是难以预料的，
     * 而 AggregateException 这份类就是用来解决这个问题的 也是用try catch 捕捉，并将异常信息包装成一个实例，
     * 然后将这个里面的信息传递到父任务，【根据他报错的先后顺序，和他打印的顺序，嗯？好像发现了什么？倒过来的，
     * 嗯，错误纯粹的方式有点像栈的储存，既然用到了栈，那他是的错误信息应该是储存在堆栈上面的
     * 因为他是将错误信息包装成一个实例储存的，而实例说白了就是分配内存（虚拟的，碰巧堆栈也是虚拟的
     * 而他们的储存和读取方式非常相似（可以看成一个弹匣）】
     * 
     */
    //public class Example
    //{
    //    public static void Main()
    //    {
    //        //在一个线程里面嵌套一个线程再嵌套一个线程，还是异步的那种
    //        var task1 = Task.Factory.StartNew(() =>//将一个任务分解
    //        {
    //            var child1 = Task.Factory.StartNew(() =>
    //            {
    //                var child2 = Task.Factory.StartNew(() =>
    //                {
    //                    // This exception is nested inside three AggregateExceptions.
    //                    throw new CustomException("Attached child2 faulted.");

    //                }, TaskCreationOptions.AttachedToParent);
    //                //TaskCreationOptions 指定控制用于创建和执行任务的可选行为的标志。

    //                // This exception is nested inside two AggregateExceptions.
    //                throw new CustomException("Attached child1 faulted.");

    //            }, TaskCreationOptions.AttachedToParent);
    //        });
    //        try
    //        {
    //            task1.Wait();
    //        }
    //        catch (AggregateException ae)
    //        {
    //            foreach (var e in ae.Flatten().InnerExceptions) //遍历异常，总的来说  task1 只算
    //            {
    //                if (e is CustomException)
    //                {
    //                    Console.WriteLine(e.Message);
    //                }
    //                else
    //                {
    //                    throw;
    //                }
    //            }
    //        }
    //    }
    //}
    //public class CustomException : Exception
    //{
    //    public CustomException(String message) : base(message)
    //    { }
    //}
    /********************************/
    //    public static void Main()
    //    {
    //       var   task = Task.Run(() =>
    //       { 
    //           var task1 = Task.Factory.StartNew(() =>
    //           {
    //               var task2 = Task.Factory.StartNew(() =>
    //               {
    //                   throw new CustomException("a");
    //               }, TaskCreationOptions.AttachedToParent);
    //               throw new CustomException("a");
    //           }, TaskCreationOptions.AttachedToParent);
    //       });
    //    }
    //    public static void tt()
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            Console.WriteLine(i);
    //        }
    //    }
    //}

    /****************MSDN 的栗子*****************/

    //using System;
    //using System.Collections.Generic;
    //using System.IO;
    //using System.Threading.Tasks;
    //public class Example
    //{
    //    public static void Main()
    //    {
    //        try
    //        {
    //            ExecuteTasks();
    //        }
    //        catch (AggregateException ae) //异常集合
    //        //https://www.xin3721.com/ArticlePrograme/C_biancheng/3516.html
    //        {
    //            foreach (var e in ae.InnerExceptions) //InnerExceptions 内部错误
    //            {
    //                Console.WriteLine("{0}:\n   {1}", e.GetType().Name, e.Message);// 表示当前实例的确切运行时类型的类型对象。（就是返回错误类型），详细信息

    //                /*
    //                 * 
    //                 */
    //            }
    //        }
    //    }

    //    static void ExecuteTasks()
    //    {
    //        String path = @"C:\";//要访问c 盘的节奏喔
    //        List<Task> tasks = new List<Task>();//用一个有序列表储存线程，相当于让线程排队

    //        tasks.Add(Task.Run(() =>
    //        {// 在 tasks 创建一个线程并且运行他
    //            // 这应该会抛出一个UnauthorizedAccessException。
    //            return Directory.GetFiles(path, "*.txt",//访问指定的路径
    //                                      SearchOption.AllDirectories);// AllDirectories  搜索当前目录的所有子目录，嗯？忽然想到了递归
    //        }));

    //        tasks.Add(Task.Run(() =>
    //        {
    //            if (path == @"C:\")
    //                throw new ArgumentException("系统根不是有效路径。");
    //            return new String[] { ".txt", ".dll", ".exe", ".bin", ".dat" };
    //        }));

    //        tasks.Add(Task.Run(() =>
    //        {
    //            throw new NotImplementedException("此操作尚未实现。");
    //        }));

    //        try
    //        {
    //            Task.WaitAll(tasks.ToArray());//等待tasks 
    //        }
    //        catch (AggregateException ae)
    //        {
    //            throw ae.Flatten();
    //            //将异常信息扁平化，说白了就是创建一个实例，将错误信息都封装到这个实例里面，
    //            //然后再由这个实例将异常信息传递到他的父任务？如果时多层嵌套，他是不是一层一层地传呢？

    //        }
    //    }
    //}
    // The example displays the following output:
    //       UnauthorizedAccessException:
    //          Access to the path 'C:\Documents and Settings' is denied.
    //       ArgumentException:
    //          The system root is not a valid path.
    //       NotImplementedException:
    //          This operation has not been implemented.
    // 示例显示如下输出:
    // UnauthorizedAccessException:
    //拒绝访问路径“C:\Documents and Settings”。
    // ArgumentException:
    //系统根目录不是有效路径。
    // NotImplementedException:
    //这项行动尚未执行。
    #endregion



    #endregion

    #region 委托  Delegate
    //https://www.cnblogs.com/pengze0902/p/6088870.html
    //class T
    //{
    /*
     * 当要把方法传递给其他方法是就要用到委托（为什么要用到他）
     * 
     * 有时候一个方法执行的操作并不是针对数据进行的，而是对另一个方法的调用，麻烦的是在编译是我们不知道第二个方法是什么，而这些信息只有在运行是得到（比如线程）这是由委托就很好用了（使用环境）
     * 他将一个方法作为参数传递给另一个方法（本质）
     * 
     * 委托是一种特殊类型的对象，它可以包含一个或者多个方法的地址，为什么特殊呢？ 
     * 因为我们之前定义的所有对象都包含数据哇，而委托就厉害类，他包含的是方法（方法地址，嗯这个跟那个指针有点类似）
     * 
     * 委托的特性 就是他的类型安全性很高，在定义委托时，必须给出他所表示的方法的签名和返回类型等全部细节。
     * 
     * 比如
     * delegate void dd (int d)   要有 返回的类型 void   要有方法的签名 ，就是 d 了，
     * 这个 d 代表的时 这个dd 表示的委托的方法有一个 int 类型的参数
     * 
     * 说明书上说 理解委托的一种好方式就是将委托看成 是给方法的签名和返回类型指定名称
     * 
     * 委托的作用域：
     * 定义委托基本上就是定义一个新类，所以可以在定义类的任何相同的地方定义委托。可以在另一个类里面定义，
     * 也可以再类的外部定义，还可以再命名空间定义为顶层对象，
     * 常用修饰符：public，privat，protected，
     * 
     * 说明书的说法：定义一个委托实际上是定义一个新类，委托实现为派生自基类 System.MulticastDelegate 类，
     * 而System.MulticastDelegate 又派生自 System.Delegate.
     * 
     * 委托是接受参数的构造函数，这个参数就是委托引用的方法
     * 
     * 委托能用到的地方
     * 
     * 事件 ：通知代码发生了甚事件，GUI编程主要就是处理事件，在引发时间是运行库需要知道该执行那个方法，就需要把处理事件的方法作为参数传递给委托
     * 
     * 通用库类：许多库包含执行各种标准任务的代码，
     * 假设要编写一个类，他带有一个对象数组，然后将数组排序，在排序的过程中会重复涉及数组中的两个对象，并对其进行比较，
     * 如果要编写的类能对付各种对象数组的排序，就得提前告诉计算机如何比较对象，
     * 处理类中的对象数组的客户端代码要告诉类如何比较要排序的特定对象，也就是说，客户端不想要给类传递某一个可以调用并进行比较的方法，这个过程就是委托
     * 
     * 启动线程和任务：线程就是告诉计算机并行运行某些新的执行序列，同时运行新的任务，这个就叫线程
     * 举个栗子，在一个 Thread 的实例上使用方法 Start() 启动一个线程，这个过程就是告诉计算机启动一个新的执行序列，并说明在那里启动
     * 这个并行的方法的过程也用到了委托，
     * 
     * 
     * 
     * 
     */
    //好废话不多说，放码上来

    //定义委托
    //public delegate string GetAString();

    //public static void Main()
    //{
    //    int x = 10;//
    //    GetAString getAString = new GetAString(x.ToString);//给委托赋值
    //    Console.WriteLine($"String is{getAString()}");
    //}
    //  }

    //***********************************************
    //class K
    //{    
    //    public uint Dollars;
    //    public ushort Cents;
    //    public  K(uint dollars, ushort cents)//构造方法，用于接收值，并改变内部的元素的值
    //    {
    //        this.Dollars = dollars; 
    //        this.Cents = cents;
    //    }
    //    public override string ToString() => $"输出格式{Dollars}.{Cents,2:00}";//重载string 的输出格式
    //    public static string GetCurrencyUnit() => "Dollars";
    //    public static explicit operator K(float value)
    //    {
    //        checked
    //        {
    //            uint dollars = (uint)value;
    //            ushort cents = (ushort)((value-dollars) * 100);
    //            return new K(dollars, cents);
    //        }
    //    }
    //    public static implicit operator float (K value) => value.Dollars + (value.Cents/100.0f);
    //    public static implicit operator K(uint value)   => new K(value,0);
    //    public static implicit operator uint (K value)  => value.Dollars;
    //    private delegate string GetAString();
    //    public static void Main()
    //    {
    //        int x = 30;
    //        GetAString getAString = x.ToString;
    //        Console.WriteLine($"String is {getAString()}");
    //        var balance = new K(34, 90);
    //        getAString = balance.ToString;
    //        Console.WriteLine($"String is {getAString()}");
    //        getAString = new GetAString(K.GetCurrencyUnit);// GetCurrencyUnit 将这个方法里面的值输出
    //        Console.WriteLine($"String is {getAString()}");
    //    }
    //}
    //********************************
    //class T
    //{  //这是一个委托类，放置执行的方法，就是不断地调用这个类里面封装好的执行方法
    //    public static double a(double value) => value * 2;
    //    public static double b(double value) => value * value;
    //    public static double c(double value) => value * value - value;
    //}
    //delegate double Doubleop(double x);//关键委托，将得到的值委托给上面的委托类
    //class K
    //{
    //    static void Main()
    //    {
    //        Doubleop[] length =  //给这个委托定义一个储存 要委托的数据的数组
    //            {
    //            T.a,
    //            T.b,
    //            T.c      
    //            };
    //        for (int i = 0; i < length.Length; i++)//循环委托数组，委托几次就循环几次
    //        {
    //            Console.WriteLine($"看他怎么玩[{i}]");
    //            GC(length[i],1);   //当i  =0 时 执行 value*2
    //            GC(length[i],2.34);//当i !=0 时 执行 value*value
    //            GC(length[i],3.56);//当i  =2 时 执行 value*value-value
    //            Console.WriteLine();
    //        }
    //    }
    //    static void GC(Doubleop action,double value)//Doubleop 上面生命的委托，将这个方法的 vale 值传递给 Doubleop
    //    {
    //        double result = action(value);//获取 委托返回的数据
    //        Console.WriteLine($"{value},只看结果{result}");
    //    }
    //}
    //委托 和方法的直接调用有什么不一样呢？，就只是他的数据类型安全？感觉没有直接调用方便
    //好像还有一个 invoke 线程异步的执行函数好像是一个 委托函数来着
    //*******************************

    #region Action<T> 和 Func<T>
    /*
     * 详细资料 https://www.cnblogs.com/kissdodog/p/5674629.html
     * 
     *  Action<T> 引用一个void 返回类型的方法，可以传递 16 种不同类型的方法
     *  Action 没有泛型的话可以调用没有参数的方法 
     *  
     *  
     *  Func<T> 允许调用有返回类型的方法，和 Action<T> 类似 也可以传递 16 种不同类型的方法和一个返回类型
     *  Func<out TResult>委托类型可以调用带返回类型且无参数的方法
     *  Func<in T1,out TResult> 调用带有一个参数的方法
     * 
     * 
     * 区别：
     *  Func<Result>有返回类型；
     *  Action<T>只有参数类型，不能传返回类型。所以Action<T>的委托函数都是没有返回值的。
     *
     * 
     */
    //class T
    //{
    //    //public static void DH()
    //    //{
    //    //    for (;;)Console.WriteLine("DH今天，你敲代码了吗？");
    //    //}

    //}
    //***********************************
    /*
     * Func<T>
     * 
     * 代码思路：
     * 首先要有一个Main入口啥的，然后就是在 main 里面声明要排序的数组
     * 然后后就是要有排序的类（BubbleSorter）了
     * 将声明的数组通过 构造的委托类（Employee） 传递到这个方法（Sort）
     * 
     * public static void DH()
     * {
     *    for (;;)Console.WriteLine("DH,今天你思考了吗？");
     * }
     */
    //class Program
    //{
    //    static void Main()
    //    {
    //        Employee[] employees =
    //        {   new Employee("热爱生命                      ", 1),
    //            new Employee("既然选择了远方                ", 2),
    //            new Employee("我不去想是否能够成功          ", 1),
    //            new Employee("便只顾风雨兼程                ", 3),
    //            new Employee("就勇敢地吐露真诚              ", 6),
    //            new Employee("我不去想能否赢得爱情          ", 4),
    //            new Employee("我不去想身后会不会袭来寒风冷雨", 7),
    //            new Employee("一切，都在意料之中            ", 12),
    //            new Employee("我不去想未来是平坦还是泥泞    ", 10),
    //            new Employee("既然钟情于玫瑰                ", 5),
    //            new Employee("留给世界的只能是背影          ", 9),
    //            new Employee("只要热爱生命                  ", 11),
    //            new Employee("既然目标是地平线              ", 8)
    //        };

    //        foreach (var employee in employees)
    //        {
    //           Console.WriteLine(employee);
    //        }
    //    }
    //}
    ////构造类，用来放置 Main 传递过来的值
    //class Employee 
    //{
    //    public Employee(string name, decimal salary)
    //    {
    //        this.Name = name;
    //        this.Salary = salary;
    //    }
    //    public string Name { get; }
    //    public decimal Salary { get; private set; }
    //    public override string ToString() => $"{Name}, {Salary:C}";
    //    public static bool CompareSalary(Employee e1, Employee e2) =>
    //      e1.Salary < e2.Salary;
    //}
    //class BubbleSorter
    //{                                     
    //    static public void Sort<T> (IList<T> sortArray, Func<T, T, bool> comparison)//sortArray 转递的是值  comparison 就是传递的方法的返回值
    //    {
    //        bool swapped = true;
    //        do
    //        {
    //            swapped = false;
    //            for (int i = 0; i < sortArray.Count - 1; i++)
    //            {
    //                if (comparison(sortArray[i + 1], sortArray[i]))
    //                   //将 i+1 和 i 委托到 Employee 里面进行比较 注意，这个 i 代表的是 这个值在数组中的位置
    //                {
    //                    T temp = sortArray[i]; //将 这个 数 提取出来
    //                    sortArray[i] = sortArray[i + 1];//赋值给 第 i+1 个
    //                    sortArray[i + 1] = temp; //将 第 i 个数的值赋值给第i+1个
    //                    swapped = true;
    //                }
    //            }
    //        } while (swapped);
    //    }
    //}
    //*************************************


    /*
     * 多播委托
     * 
     * Action 这个委托可以被多次显式调用 ，委托里面包含多个方法的委托 就叫多播委托，类似于 Udp 那个多播协议
     * 多播委托，可以按顺序调用多个方法。因此 委托的签名就必须返回void 否则 就只能得到最后一个方法的结果
     * 
     * 
     * https://www.cnblogs.com/bianlan/archive/2013/01/18/2867065.html
     * 
     * 
     * 
     * 和其他委托不一样，它可以调用多个方法，而其他委托只能调用一个（类似于 单线程，多线程 的区别，一顿吃一吨，一顿吃多吨）
     * 
     * 支持运算符，就像这个 
     * 
     *  用于从委托中删除或添加方法的调用
     *  
     * 
     *  Action<double> operation = K.M;
     *  operation += K.N;
     *  Action<double> action3 = action1 + action2;   
     * 
     */
    //class T
    //{
    //    static void Main()
    //    {             // Action 封装具有单个参数且不返回值的方法。
    //        Action<double> a = K.M;//将 a 的值 委托到这个方法
    //        a += K.N;
    //        a += K.V;
    //        // 感觉像一个数组，不断调用数组里面的值
    //        //这里是和 Fun 地区别 对方法 可以像运算符一样， 
    //        G(a,2);
    //        G(a,3);
    //        G(a,5);
    //        G(a,8);
    //        //Console.WriteLine(Math.PI);
    //    }
    //    public static void G(Action<double> action, double value)
    //    {
    //        Console.WriteLine();
    //        Console.WriteLine($"传递的值 {value}");
    //        action(value);//通过这个多播委托将值传递
    //    }
    //}
    //class K
    //{
    //    public static void M(double value)
    //    {
    //        double result = value * 2;
    //          Console.WriteLine($"乘2, 2 X {value} ={result}");
    //        //throw new Exception("哎呀，出错了。");//如果某一个环节报了异常，委托会停止 但有时候我并不希望这样 和他的执行顺序有关
    //    }                                                                                 
    //    public static void N(double value)
    //    {
    //        double result = value * value;
    //        Console.WriteLine($"{value} 的平方值为{result}");
    //    }
    //    public static void V(double value)
    //    {
    //        double result = Math.PI;
    //        Console.WriteLine($"乘圆周率 {value} X π = {result}");       
    //    }
    //}
    //*********************  利用 delegate 里面的 getinvoactionlist 这个方法  获取委托的链表
    //class T
    //{
    //    public static void V()
    //    {
    //        Console.WriteLine($"量子叠加态");
    //    }
    //    public static void W()
    //    {
    //        Console.WriteLine($"量子纠缠");
    //        throw new Exception();
    //    }
    //    public static void M()
    //    {
    //        Console.WriteLine("量子自旋");
    //    }
    //    static void Main()
    //    {
    //        Action  a = V;
    //        a += W;
    //        a += M;
    //        //如果某一个环节报了异常，委托会停止 但有时候我并不希望这样 
    //        // delegate 类 定义了一个 GetInvocationlist() 方法 返回一个 delegate 地对象数组， 
    //        //可以使用委托调用委托直接相关的方法 异常不影响
    //        Delegate[] delegates = a.GetInvocationList();//一个委托数组，其调用列表与此实例的调用列表相匹配。
    //        foreach (Action d in delegates)
    //        {
    //            try
    //            {
    //                d();    
    //            }
    //            catch (Exception)
    //            {
    //                Console.WriteLine("波函数坍塌");
    //            }
    //        }
    //    }
    //}
    //class K
    //{
    //}
    /*
     *  匿名方法
     *  
     *  委托是用于引用与其具有相同标签的方法。换句话说，
     *  可以使用委托对象调用可由委托引用的方法。
     *  匿名方法（Anonymous methods） 提供了一种传递代码块作为委托参数的技术。
     *  
     *  匿名方法是没有名称只有主体的方法。
     *  在匿名方法中不需要指定返回类型，它是从方法主体内的 return 语句推断的。
     * 
     *  值得一提的是，在匿名方法中不能使用跳转语句 (break,goto,continue )跳转到匿名方法的外部，反之亦然
     *  
     * 
     */
    //class T
    //{
    //    static void Main()
    //    {
    //        string mid = ", middle part,";
    //        //Func<string, string> anonDel = delegate (string param)//一个委托
    //        //{   
    //        //    param += mid;
    //        //    param += " and this was added to the string.";
    //        //    return param;
    //        //};
    //        //lambad 写法
    //        Func<string, string> lambad = param =>
    //         {
    //             param += mid;
    //             param += "and this was added to the string.";
    //             return param;
    //         };
    //        Console.WriteLine(lambad("Start of string"));
    //    }
    /*
     * 在匿名方法的外面定义一个 字符串变量 mid ,并将变量添加到要传递的参数中，
     * 代码返回字符串的值，在调用委托时，把一个字符串作为参数传递，将返回的字符串输出到控制台上
     * 
     * 
     * 可以用 lambda 表示
     */
    //  }

    //namespace DelegateAppl
    //{
    //    delegate void NumberChanger(int n);
    //    class TestDelegate
    //    {
    //        static int num = 10;
    //        public static void AddNum(int p)
    //        {
    //            num += p;
    //            Console.WriteLine("Named Method: {0}", num);
    //        }
    //        public static void MultNum(int q)
    //        {
    //            num *= q;
    //            Console.WriteLine("Named Method: {0}", num);
    //        }
    //        static void Main(string[] args)
    //        {
    //            // 使用匿名方法创建委托实例
    //            NumberChanger nc = delegate (int x)
    //            {
    //                Console.WriteLine("Anonymous Method: {0}", x);
    //            };
    //            // 使用匿名方法调用委托
    //            nc(10);
    //            // 使用命名方法实例化委托
    //            nc = new NumberChanger(AddNum);
    //            // 使用命名方法调用委托
    //            nc(5);
    //            // 使用另一个命名方法实例化委托
    //            nc = new NumberChanger(MultNum);
    //            // 使用命名方法调用委托
    //            nc(2);
    //            Console.ReadKey();
    //        }
    //    }
    //}
    #endregion


    #endregion

    #region lambda


    /*
     * 什么是 lambda 
     * 作用是什么
     * 本质
     * 使用环境
     * 使用的前提条件是什么？
     * 
     * 要理解委托，实际上 lambda（换个名字 ==== ld ）就是一个委托，啥时委托呢?就是将一个方法变成参数然后给其他方法调用
     * 语法糖，一种偷懒的方式。
     * lambda  微软发明出来的，用于 简化 "匿名方法" 的一种表达式了,也就是简化委托的过程
     * https://www.cnblogs.com/kingmoon/archive/2011/05/03/2035696.html
     */
    /*  lambad 运算符 => 左边列出了需要的参数，二右边定义了lambad 变量的方法的实现方法
     *  
     *  
     * 有几种定义参数的方式， => 指定
     * s => x*x   前面的是参数，指定 参数 s 的值为 x*x
     * 
     * 如果使用委托  ，可以不指定，编译器会根据前后推算出参数的数据类型
     * Func<double,double,double> a = (x,y) => x*y;
     * 也可以 指定参数的数据类型
     * Func<double,double,double> a = (double x,double y) => x*y  
     * 
     * 如果 lambda 只有一行代码，在方法块内就不需要花括号和return ,因为 编译器会添加一条隐式的 return 语句
     * 可以这样
     *  Func<double,double> a =  x => x*x;
     *  也可以这样
     *  Func<double,double> a =  x =>{ retutn x*x;}
     * 
     * 上面时一行代码的情况下，如果是多行就必须加上 花括号和 return 
     * 
     * Task task = new Task(() => 
     * {
     * console.writeline("Hello,Word!");
     * console.writeline("Hello,Word!");
     * });
     */

    /* 
     *  闭包 ----将函数内部和外部连接起来的桥梁 
     * https://baike.baidu.com/item/%E9%97%AD%E5%8C%85/10908873?fr=aladdin
     * 
     *  闭包 ---- 通过 lambda 表达式 访问表达式外部变量
     *  
     *  https://www.jianshu.com/p/c22db2a91989
     *  https://www.jb51.net/article/115528.htm
     *  lambda表达式的闭包是定义在外部上下文（环境）
     *  中特定的符号集，它们给这个表达式中的自由符号赋值。它将一个开放的、
     *  仍然包含一些未定义的符号lambda表达式变为一个关闭的lambda表达式，
     *  使这个lambda表达式不再具有任何自由符号。
     *  
     *  闭包是个什么东西呢？
     *  闭包就是把函数以及变量包起来，使得变量的生存周期延长。
     *  闭包跟面向对象是一棵树上的两条枝，实现的功能是等价的。
     *  
     * 资料看了一大堆，按照自己的理解就是
     * 
     * 你在方法 a 里面定义一个方法 b ，然后通过访问  b    访问  a  里面的 变量  
     * 这个时候的 b 就是---闭包，
     * 无论 b 在哪里被调用，都能访问到 a 里面的变量。 ---闭包的作用
     * 
     * A: 闭包的作用场景是啥呢？
     * B: 闭包可以读取其他函数内部变量。
     * C: 可以简单理解为定义在一个方法里面的方法， 内部方法拥有外部方法的内部变量的引用
     * D: 可以利用闭包 将某个方法里面的局部变量镶嵌到内存中，供某些方法在外部使用
     * E: 小D ,如果是这样的话，他就避开了垃圾回收机制，会一直储存在内存中，嗯，是个好主意,就是有点占内存。。。。
     * F: 
     *  
     * A: 为什么要用到闭包呢？
     * B: 因为编译器 规定不能直接获取方法内部变量的值，闭包就是用来填这个坑的。
     * 
     * A：那为什么不直接调用外层方法呢？
     * B: 如果因为，如果每次调用都得先实例这个方法会很麻烦，如果通过闭包实现的话，他的内部变量是一直储存在内存里的（类似于静态方法）
     * D: 而闭包可以很优雅地完成这个任务，偷懒是人类进步hahahahaaaaaaa
     * E: 更直白地说就是 延长了变量生命周期，
     * F: 那他为什么要延长生命周期呢？
     * G: 方便其他方法访问呐
     *  
     *  
     * A:什么是 作用域链 呢？
     * B: 在执行闭包程序的时候 当他检索自身没有检索到相应变量时，他会像上一层再次检索，知道找到为止
     */
    //class T
    //{
    //    public static void Main()
    //    {
    //        var x = 1;

    //        Action action = () =>
    //        {
    //            var y = 2;
    //            var result = x + y;
    //            Console.Out.WriteLine("result = {0}", result);
    //        };
    //        action();
    //    }
    //}

    //*********************************************
    //class T
    //{
    //     public static void Main()
    //    {
    //        A();
    //    }
    //    public static string A()
    //    {
    //        var x = 9;
    //         string  gg()
    //        {
    //            Console.Out.WriteLine("在这个方法里面调用方法 A 的局部变量 x {0}",x);
    //            return (string) gg();           
    //        }
    //        return (string)gg();
    //    }
    //}
    //*************
    //class T
    //{
    //    public static void Main()
    //    {
    //        int someVal = 7;
    //        Func<int, int> f = x => x + someVal;
    //        someVal = 3;
    //        Console.WriteLine(f(2));

    /*
     * 对于 lambda 表达式  x=> x+someVal,编译器会创造一个匿名类，他有一个构造函数来传递外部变量的值。构造函数取决于从外部访问的变量数
     *  就像这样
     *  
     * public class 匿名类
     {
         private int someVal;
         public  匿名类 (int someVal)
         {
             this.someVal=someVal;
         }
         public int AnonymousMethod(int x) => x + someVal;
     }
     *
     * 如果多线程使用闭包，可能会遇到并发冲突，
     * 
     * 
     */
    //    }
    //}          




    #endregion

    #region  异步lambda
    /*
     * 
     * 
     */
    //class T
    //{
    //    public static void Main(string[] args)
    //    {
    //        string url = "https://www.zhihu.com/pub/book/119627162";
    //        if (args.Length > 0)
    //        {
    //            url = args[0];
    //        }
    //        Console.WriteLine(url);
    //        Func<string, Task> writeWebRequestSizeAsync = async (string webRequestUrl) =>
    //        {
    //            WebRequest webRequest = WebRequest.Create(url);
    //            WebResponse response = await webRequest.GetResponseAsync();
    //            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
    //            {
    //                string text = (await reader.ReadToEndAsync());
    //                Console.WriteLine(FormatBytes(text.Length));
    //            }
    //        };
    //        Task task = writeWebRequestSizeAsync(url);
    //        while (!task.Wait(100))
    //        {
    //            Console.WriteLine(".");
    //        }
    //    }
    //    static public string FormatBytes(long bytes)//用于转换单位3
    //    {
    //        string[] manitudes = new string[] { "GB", "MB", "KB", "Bytes" };
    //        long max = (long)Math.Pow(1024, manitudes.Length);//设置容量
    //        return string.Format("{1:##.##} {0}",//用字符串表示形式替换格式项的格式的副本即arg0和arg1。
    //                                             //  public static String Format(String format, object arg0, object arg1);
    //        manitudes.FirstOrDefault(manitude => bytes > (max /= 1024)) ?? "0 Bytes", (decimal)bytes / (decimal)max);
    //    }
    //}

    #endregion

    #region 异步任务的补充 whenall 和 whwnany
    /*
     * 他是什么？
     * A: 他是
     * 和 async 的区别？
     * 为什么要用到他？
     * 基本思路？
     * 
     * 
     */
    //放码过来
    class T {
        public async static void Tost()
        {
           // Task<string> t1 = Gree
        }
        static Task<string> GreeingAsync(string name)//异步方法，调用 Greeting
        {
            return Task.Run<string>(() => { return Greeting(name); });
        }
        static string Greeting(string name)//同步方法
        {
            Task.Delay(3000).Wait();
            return $"Hello,{name}";
        }
    }
    #endregion

































































    /******************************************************************************************************************************************/

}



