using System;

namespace interface_显式实现和隐式实现
{
    class Program
    {
        #region 显式实现和隐式实现

        interface Iclear
        {
            void 显式实现接口方法();
        }
        interface Iconceal
        {
            void 隐式实现接口方法();
        }
        //接口之间地继承
        interface Iinherit : Iclear
        {
            void inherit();
        }
        class Test1 : Iinherit
        {
           public void inherit()
            {
                Console.WriteLine("接口原生方法");
            }
            public void 显式实现接口方法()
            {
                Console.WriteLine("继承派生方法");
            }
        }

        class Test : Iclear, Iconceal
        {
            void Iclear.显式实现接口方法()
            {
                Console.WriteLine("显式实现接口方法");
            }
            public void 隐式实现接口方法()
            {
                Console.WriteLine("隐式实现接口方法");
            }


        }
        #endregion
        /*
         *
         * https://www.cnblogs.com/ben-zhang/archive/2012/12/18/2823455.html
         * 
         * “显示接口实现”就是使用接口名称作为方法名的前缀;
         * 而传统的实现方式称之为：“隐式接口实现”。
         */


        static void Main(string[] args)
        {
            /*
             * 隐式接口实现
             * 
             * 有两种方法
             * 一种是通过接口地实现类来实现接口
             * 一种是通过调用接口来实现
             */
            //实例化test 类调用类下地方法，通过类调用
            Test test = new Test();
            test.隐式实现接口方法();
            //通过实例换test 类 实现接口方法 通过接口调用
            Iconceal iconceal = new Test();
            iconceal.隐式实现接口方法();

            /*
             * 显式调用
             * 
             * 一般只通过接口调用，如果不嫌麻烦可以通过转型调用
             */
            //通过接口调用
            Iclear iclear = new Test();
            iclear.显式实现接口方法();
            //类转型调用，麻烦
            Test test1 = new Test();
            (test1 as Iclear).显式实现接口方法();
            /*
             * 那么问题来了，什么时候要用到他们呢？
             * 可以说接口是C#对多继承地补充，观察代码可以很清楚地看到
             * 各有什么优缺点呢？
             * 他们地区别是什么呢？
             * 1.如果一个类是继承一个接口地话，可以用隐式接口实现，这样可以方便的访问接口方法和类自身具有的方法和属性。
             * 2.如果一个类是继承多个接口地话，应该用显式接口实现，这样可以避免当派生接口有相同地方法签名时发生地不必要地冲突
             * 3.隐式接口实现，接口内方法可以通过类和接口访问，显示接口实现只能通过接口访问
             */

            /*
             * 接口之间继承关系
             * 
             * 1.接口地继承概念和类地继承概念类似，可以继承基类地所有属性
             * 2.在继承接口时需要为基类接口地属性或者方法 申明
             * 
             */
            Iinherit test2 = new Test1();
            test2.inherit();
            test2.显式实现接口方法();

        }
    }
}
