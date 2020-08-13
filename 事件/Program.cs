using System;
using System.Collections.Generic;

namespace 事件
{
    #region ObSever 设计模式
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
     *  2.1可以把发布看作是一个具有放射性的类 而订阅者就是受到辐射的类 而订阅者和发布者链接的方式可以称之为委托链（依稀记得好像有一个概念叫继承连来着。。）
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
             *  += 创建一个监听（订阅者）或者说再绑定一个方法
             *  -= 取消一个监听（订阅者）或者说取消委托对一个方法的绑定
             *  + 合并委托（订阅者） 或者说执行两个委托方法
             *  
             *  - 删除委托 取消指定的委托方法的调用
             *  
             *  system.delegate.combine() 将两个委托的调用列表连接在一起。
             *  
             *  system.delegate.remove()  从一个委托的调用列表中移除另一个委托的所有调用列表。
             *  
             *  无论是 + - 是 += -=   他梦的内部机制都是通过 静态方法 system.delegate.combine()  和 system.delegate.remove() 实现的
             *  
             *  有趣的是combine()  允许两参数都为 null ，如果只有一个为 null 就返回非空的那个，两个都为 null 就返回 null
             *  
             *  这就可以解释 当你调用某一个事件时，即使这个事件的指向为空他也不会报错。
             *  
             *  
             *  
             */
        }
    }
    #endregion

    #region 异常处理
    /*
     * 
     * observer 设计模式
     * 当一个发布者 存在多个订阅者的时候，当发布者发生改变时，订阅者也会相应地发生改变
     * 这个时候就会存在一种情况，就是发布者发布的信息对于某个订阅者来说是无效的，这时订阅者就会报出处理异常
     * 并且后续的订阅者就接收不到通知了（从这里可以看出，observer 设计模式时单线程的）
     * 
     * 如果想继续执行，处理来自订阅者的异常了
     */

  class ObsError

    {
        public Action<float> OnTC;
        public float CurrentTp
        {
            get { return _CurrentTp; }

            set
            {
                if (value != CurrentTp)
                {
                    _CurrentTp = value;
                    Action<float> onTC = OnTC;
                    if (onTC !=null)
                    {
                        //创建异常列表
                        List<Exception> exceptions = new List<Exception>();
                        // GetInvocationList 按照调用顺序返回此多路广播委托的调用列表。
                        //遍历多播委托列表
                        foreach (Action<float>  item in onTC.GetInvocationList())
                        {
                            try
                            {
                                item(value);
                            }
                            catch (Exception e)
                            {
                                //存在异常时 把异常添加到异常列表中
                                exceptions.Add(e);
                            }
                        }
                        //假如异常列表不为空
                        if (exceptions.Count >0)
                        {
                            throw new AggregateException("订阅者存在异常", exceptions);
                        }
                    }
                }
            }
        }

        private float _CurrentTp;
    }


    #endregion

    #endregion

    #region 事件
    /*
     * 关键字 event
     * 
     * event 关键字用于在发行者类中声明事件。
     * 事件是特殊类型的多路广播委托，仅可从声明它们的类或结构（发行者类）中调用。 
     * 如果其他类或结构订阅了该事件，则当发行者类引发该事件时，会调用其事件处理程序方法。 
     * 
     * 
     * 
     * 
     * 
     * 
     */
    #endregion


    #region 启动类
    class Program
    {
        static void Main(string[] args)
        {
            //Connect.Connect_main();
            obsError();
            Console.ReadLine();
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        public static void obsError()
        {
            ObsError obsError = new ObsError();
            Cooler cooler = new Cooler(50);
            Heat heat = new Heat(100);

            obsError.OnTC += heat.OnTC;
            obsError.OnTC += cooler.OnTC;
            string tea;

            Console.WriteLine("esss");
            tea = Console.ReadLine();
            obsError.CurrentTp = int.Parse(tea);

        }
    }
    #endregion








































    /******************************************************/
}
