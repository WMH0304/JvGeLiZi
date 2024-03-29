﻿using System;

namespace ArrayList_装箱拆箱探索
{
    class Program
    {
        /*
         * 相关资料
         * 
         * https://www.cnblogs.com/zhangzhu/archive/2012/05/10/2495037.html
         * https://yq.aliyun.com/articles/693430
         * 
         */

        /*
         * 最新定义
         * 装箱：将值类型转换成引用类型
         * 拆箱：将引用类型转换成值类型
         * 
         * 什么是装箱和拆箱呢？
         * 是对数据类型的一种操作方式。
         * 他们分别代表的是什么？
         * 装箱： 将基本数据类型转换为包装类的过程称为包装类的过程
         * 
         * 
         * 理解错误
         * //将小的数据类型转换成大的数据类型 例如  int a =1;
         * //object d =(object)a;这个隐式转换的过程就叫装箱*
         * 
         * 
         * 
         * 拆箱：与装箱的操作相反， a =(int)d;
         * 他们的概念是什么？
         * 就是引用类型和值类型的相互转换。
         * 那啥是引用类型，啥是值类型呢？
         * 
         * 引用类型：所有称之为类的类型，都是引用类型
         * 既然这样我们可以想象一下C#给一个实例分配内存的过程
         * 
         * 1.首先他会再托管对上面分配出一块空白内存，
         * 也可以说不是空白的，类似于磁盘的存储原理。
         * 
         * 2.分配好内存之后就是初始化类的成员，比如说，属性啊，方法啊什么的
         * 
         * 3.最后就是将对象的值修改成你想要的值。
         * 
         * 4.由于c#是支持垃圾回收的，所以在你使用完该引用类型
         * （这个类）之后，C#可能会在未来的某一个时间段执行一次垃圾回收
         * 
         * 值类型：他们的标注为  struct(结构)或者（enum） 枚举
         * 总所周知，使用他们的时候是不需要实例的，也就是说他们可以直接使用
         * 和静态方法类似。
         * 现在再来看看从c#执行他的过程
         * 资料上面说，他是从线程栈（是可以对托管直接操作的嚣张内存，或者说我可以把它理解成是一个动作。）
         * 上面分配内存的，程序就是一个调配内存的游戏。。。
         * 
         * 1. 在线程栈上面分配内存
         * 
         * 2.这个值就是对象。或者说他是那值存储到的线程栈上面，当你需要使用的时候，他会在你调用的时候将他自身赋值给你
         * 
         * 3. 值类型不受垃圾回收机制控制，减少了垃圾回收的次数
         * 
         * 
         * 他们的使用场景是什么？为什么？好处是什么？有什么弊端吗？
         * 很多时候我们都应该避免装箱拆箱的操作，因为在在装箱时编译器会为他分配一块内存（这让计算机本就不宽裕的内存更是雪上加霜），
         * 然而销毁类又是一大笔开销（这对计算机来说无疑又是一场厄梦）
         * 他们的本质是什么？
         * 值类型和引用类型的相互转换
         * 或者说时存储方式的相互转换 在值类型就是个实在人 ，而引用类型就类似于
         * （你站在这里，我去给你买袋橘子）一个指针，指向某个存储块
         * 
         * 
         */
        /// <summary>
        /// 声明一个坐标结构体
        /// </summary>
        struct Point
        {
            //横坐标
            public int x;
            //纵坐标
            public int y;
        }
        public static void Main()
        {
            #region 装箱

            System.Collections.ArrayList list = new System.Collections.ArrayList();
            Point point;
            /* 分配一个point 对象, 注意该对象是一个结构体，
             * 所以他是一个值类型，值类型内存在线程栈上面分配，
             * 而不是托管堆
             */
            point.x =  point.y = 3;//给对象赋值，初始化值类型成员
            object p = point;//给电脑省点事
            list.Add(point);//list 的方法是object 类型的并且他还是一个值类型，所以这是一个装箱的操作
            #endregion

            #region 拆箱

            point = (Point)list[0];
            /*从list 里面获取对象信息然后进行拆箱操作
             * 
             */

           
            #endregion
            /*
             * 后记，说白了就是数据类型（值类型和引用类型的转换（也就是对象了））的转换，那他和普通的数据类型转换有什么不一样的呢？
             */
        }

    }
}
