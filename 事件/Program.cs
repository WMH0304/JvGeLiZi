using System;

namespace 事件
{

    /*
     * publish - subcribe 
     *  发布   -  订阅
     *  
     *  多播委托是什么？
     *  定义是什么？
     *  作用是什么？
     *  和传统的委托的区别在哪里？
     *  优缺点?
     *  使用场景？
     *  是否可以使用其它方法代替？
     *  
     *  1.是一种通用的开发模式，
     *  2.这个模式成为 observer（观察者)或者 publish - subcribe 
     *  3.可以避免大量的手工代码 (懒人的最爱)
     *  4.应对场景： 需要将单一事件的通知（如对象状态发生的一个变化）广播给多个订阅者  ps:有点像udp
     *  
     */

    #region 订阅者
        
        class Cooler
    {
        public Cooler(float fa)
        {
            Fa = fa;
        }

        public float Fa { get; set; }

        public void OnTC(float newfa)
        {
            if (newfa >Fa)
            {
                Console.WriteLine("Cooler:on");
            }
            else
            {
                Console.WriteLine("Cooler:off");
            }
        }
    }

    class Heat
    {
        public Heat(float fa)
        {
            Fa = fa;
        }

        public float Fa { get; set; }

        public void OnTC(float newfa)
        {
            if (newfa < Fa)
            {
                Console.WriteLine("Heat:on");
            }
            else
            {
                Console.WriteLine("Heat:off");
            }
        }
    }

    #endregion

    #region 发布者
    public class Thermostat
    {
        public Action<float> action { get; set; }

        public float CurrentT { set; get; }
    }

    #endregion

    #region 连接订阅者和发布者
    public class Connect
    {
        public static void Connect_main()
        {
            Thermostat thermostat = new Thermostat();
            Cooler cooler = new Cooler(50);
            Heat heat = new Heat(100);
            string tea;
            thermostat.action += heat.OnTC;
            thermostat.action += cooler.OnTC;

            Console.WriteLine("esss");
            tea = Console.ReadLine();
            thermostat.CurrentT = int.Parse(tea);
        }
    }
    #endregion





    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }










































/******************************************************/
}
