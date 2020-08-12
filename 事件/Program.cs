using System;

namespace 事件
{

    /*
     * https://baike.baidu.com/item/c%23%E5%A7%94%E6%89%98
     * 
     * publish - subcribe （observer 设计模式）
     *  发布   -  订阅
     *  Observer设计模式是为了定义对象间的一种一对多的依赖关系，
     *  以便于当一个对象的状态改变时，其他依赖于它的对象会被自动告知并更新。
     *  Observer模式是一种松耦合的设计模式。
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
        //Cooler
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
        //Heat
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
            ////创建监听 （创建订阅事件）
            //thermostat.action += heat.OnTC;
            //thermostat.action += cooler.OnTC;
            ////使用 -=会返回一个新的实例
            //thermostat.action -= cooler.OnTC;

            /*
              IL_0058:  castclass  class [System.Runtime]System.Action`1<float32>
              IL_005d:  callvirt   instance void '事件'.Thermostat::set_action(class [System.Runtime]System.Action`1<float32>)
             */

            //Console.WriteLine("esss");
            //tea = Console.ReadLine();
            //thermostat.CurrentT = int.Parse(tea);

            //使用 + - 委托操作符

            Action<float> action1;
            Action<float> action2;
            Action<float> action3;

            action1 = heat.OnTC;
            action2 = cooler.OnTC;
            Console.WriteLine("/***********/");
            //合并委托
            action3 = action1 + action2; //老一 
            action3(60);
            Console.WriteLine("/************/");
            //去除指定委托 action2
            action3 = action1 - action2;//note： 使用赋值操作符会清空之前所有的订阅者  老一：我变成null啦，快给我赋值吧
            action3(60);
            /*
             * += 创建一个监听（订阅者）或者说再绑定一个方法
             * -= 取消一个监听（订阅者）或者说取消委托对一个方法的绑定
             *  + 合并委托（订阅者） 或者说执行两个委托方法
             *  
             *  - 删除委托 取消指定的委托方法的调用
             *  
             *  system.delegate.combine() 将两个委托的调用列表连接在一起。
             *  
             *  system.delegate.remove()  从一个委托的调用列表中移除另一个委托的所有调用列表。
             *  无论是 + - 是 += -=   他梦的内部机制都是通过 静态方法 system.delegate.combine()  和 system.delegate.remove() 实现的
             *  
             *  有趣的是combine()  允许两参数都为 null ，如果只有一个为 null 就返回非空的那个，两个都为 null 就返回 null
             *  
             *  这就可以解释 当你调用某一个事件时，即使这个事件的指向为空他也不会报错。
             */
        }
    }
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
