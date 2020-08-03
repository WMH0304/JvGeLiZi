using System;
using FluentScheduler;

namespace FluentScheduler框架体验
{
    class Program 
    {
        static void Main(string[] args)
        {
            //要使用计时器必先初始化 JobManager这个静态类


            //两个参数分别：Job、调度时间
            //JobManager.AddJob(() =>
            //{
            //    //任务逻辑
            //    Console.WriteLine("Timer task,current time:{0}", DateTime.Now);
            //}, t =>
            //{
            //    /*
            //     * 时间
            //     * 从程序启动开始执行，然后每秒钟执行一次
            //     * ToRunNow 现在运行作业。
            //     * AndEvery根据给定的间隔运行作业。
            //     * 
            //     */
            //    t.ToRunNow().AndEvery(1).Seconds();


            //    /* 
            //     *  带有任务名称的任务定时器
            //     *  ToRunOnceAt 在给定的时间运行一次作业。
            //     *  WithName 为该作业调度分配一个名称。
            //     *  AddSeconds 设置秒数
            //     */
            //    //t.WithName("TimerTask").ToRunOnceAt(DateTime.Now.AddSeconds(5));

            //});
            ////==================================
            //JobManager.AddJob(() =>
            //{
            //    Console.WriteLine("经过间隔时间后运行{0}", DateTime.Now);
            //}, t =>
            //{
            //    // ToRunOnceIn 在给定间隔之后运行一次作业。这里是一秒 可以和上面的定时器联合使用感受一下
            //    t.ToRunOnceIn(1).Seconds();
            //});
            //===================================
            //设置在特定时间运行
            //JobManager.AddJob(() => { Console.WriteLine("设置在特点时间运行{0}", DateTime.Now); },
            //    t =>
            //    {
            //        /*
            //         * ToRunEvery 根据给定的间隔运行作业。
            //         * Days 将间隔设置为days。
            //         * 
            //         *  At 在每天给定的时间运行作业。13：52分时执行这段代码
            //         */
            //        t.ToRunEvery(1).Days().At(13,52);
            //    });

            //=====================================
            //设置每月一次的间隔执行
            //JobManager.AddJob(() => { Console.WriteLine("每月一次执行{0}", DateTime.Now); },
            //    /*
            //     * Months 将间隔设置未月份
            //     * OnTheFirst 在每个月的第一个星期的某一天运行作业。
            //     */
            //    t => { t.ToRunNow().AndEvery(1).Months().OnTheFirst(DayOfWeek.Monday).At(3, 0); });
            //===============================
            //使用工厂方法调度作业并将参数传递给构造函数。
            //DateTime dt=DateTime.Now;
            //JobManager.AddJob(() => { Console.WriteLine("使用工厂方法调度作业并将参数传递给构造函数{0}",dt); },
            //    t => { t.ToRunNow().AndEvery(1).Seconds(); });
            //=========================
            /*
             * Minutes  每一分钟执行一次
             */
            //JobManager.AddJob(() => { Console.WriteLine("多个任务{0}", DateTime.Now); },
            //    t => { t.ToRunNow().AndEvery(1).Minutes(); });

            //=====================

            /*
             * NonReentrant 飞虫入，相当于线程锁
             */
            //JobManager.AddJob(() => Console.WriteLine("Late job!"), (s) => s.NonReentrant().ToRunEvery(1).Seconds());
            //Console.ReadKey();
        }

    

       








    }
}
