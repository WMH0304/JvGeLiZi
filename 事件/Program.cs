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
     *  5.可以很方便的再多个方法内创建监听，并改变方法内的值 （脑袋里灵光一闪，吗的想不起来了了）
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
          
        public float CurrentT {
            get
            {
                return _CurrentT;
            }
            set
            {
                if (value!= _CurrentT)
                {
                    _CurrentT = value;
                    /*将温度变化传播给 他的订阅者们    
                     * thermostat.action += heat.OnTC;    
                     * thermostat.action += cooler.OnTC;
                     * 
                     * 检测是否为null 值
                     * 假如订阅者没有注册接收通知，那么 action 就会为空 
                     * action(value); 指向后会报一个 NullReferenceException（尝试取消引用空对象引用时引发的异常。）
                     * 
                     * 
                     *  null 条件操作符   变量名?.属性  
                     *  
                     *  语法糖 等价于    string pName = p == null ? null : p.Name;
                     *  
                     */
                    //未检测 null
                    // action(value);
                    //检测 null
                    //方法一
                    action?.Invoke(value);
                    //方法二
                    //先给一个委托赋值，然后再比较是否为空
                    //Action<float> actions = action;
                    //if (actions !=null)
                    //{
                    //    action(value);
                    //}
                    

                }
            }
        }
        private float _CurrentT;
    }


    #endregion

    #region 连接订阅者和发布者
    public class Connect
    {
        public static void Connect_main()
        {
            //声明对象
            Thermostat thermostat = new Thermostat();
            Cooler cooler = new Cooler(50);
            Heat heat = new Heat(100);
            string tea;
            //创建监听 （创建订阅事件）
            thermostat.action += heat.OnTC;
            thermostat.action += cooler.OnTC;

            thermostat.action -= cooler.OnTC;

            /*
              IL_0058:  castclass  class [System.Runtime]System.Action`1<float32>
              IL_005d:  callvirt   instance void '事件'.Thermostat::set_action(class [System.Runtime]System.Action`1<float32>)

             
             */

            //Console.WriteLine("esss");
            //tea = Console.ReadLine();
            //thermostat.CurrentT = int.Parse(tea);
        }
    }
    #endregion


    #region  -= 操作符应用于委托会返回一个新实例

    #endregion


    class Program
    {
        static void Main(string[] args)
        {
            Connect.Connect_main();
        }
    }










































/******************************************************/
}
