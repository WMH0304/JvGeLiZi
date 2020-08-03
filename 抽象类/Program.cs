using System;

namespace 抽象类
{


    /*
     * https://www.cnblogs.com/wzl0106/p/4810060.html
     */
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            Terrorist terrorist = new Terrorist();
            terrorist.SetGun(new Rifle());
            terrorist.KillEnemy();
            terrorist.SetGun(new post());
            terrorist.KillEnemy();
        }
    }
    /// <summary>
    /// 抽象类声明
    /// </summary>

    /*
     * 啥是抽象类？
     * 
     * 在面向对象语言中，世间万物（简称对象）都可以用类来描述。然而反过来，缺不一样，
     * 并不是所有的类都可以直接描绘一个具体的对象的，这样的类就叫做抽象类
     *  
     * 我所理解的抽象的意思就是对具体事物的概念的表诉。
     * 
     * 刚刚去吃了碗饭，然后脑子里突然有了一个很有意思的念头。
     * 
     * 回归到类和对象的层次，类是对对象的抽象，那么抽象类就是对对象的抽象的抽象
     *然后连结之前所说的，/抽象是对某一个概念的表述，说白了就是找共同点，或或者说是创建共性，，，， 
     * 
     * 抽象类的特性
     * 
     * 1.抽象类不能被直接实例化，如果需要可以实例抽象类派生出的子类，通过继承关系从而实例话该抽象类
     * 2.抽象类可以包含抽象方法和抽象访问器（随让还没有去了解uo）
     * 3.抽象类不能被密封，所以不能用sealed 修饰符修饰抽象类
     * 4.从抽象类派生的非抽象类必须包含继承的所有抽象方法和抽象访问器的实际实现
     * 
     * 
     *抽象类和具体类的区别
     * 
     * 1.抽象类不能直接实例话，但可以实例从他派生出的子类从而实例化他自身
     * 2.允许抽象类包含抽象成员
     *
     * 抽象类和接口（接口在下一章，提前了解【interface】）的比较
     * 
     * 首先得了解什么是接口。
     * 
     * 1.印象中的接口，不可被实例化，可以被直接调用
     * 
     * 2.关键字是interfac 
     * 
     * 3.接口内只对方法声明，和抽象类一样不存在具体内容
     * 
     * 4.差点就忘了，一个类可以有多个接口，而基类只有一个
     */

   
      
    public abstract class AbstractGun
    {
        public abstract void Shoot();
    }
    /// <summary>
    /// 派生自抽象类的子类
    /// 工具类! 1.0
    /// </summary>
    public class Rifle : AbstractGun
    {
        public override void Shoot()
        {
            Console.WriteLine("步枪开始射击:哒哒哒。。");
        }
    }
   public class post : AbstractGun
    {
        public override void Shoot()
        {
            Console.WriteLine("一枪没中：哈哈哈。。");
        }
    }

    /// <summary>
    /// 使用类
    /// </summary>
    public class Terrorist
    {
        private AbstractGun gun;
        public Terrorist()
        {
        }
        public void SetGun(AbstractGun gun)
        {
            this.gun = gun;
        }
        public void KillEnemy()
        {
            gun.Shoot();
        }
    }

}
