using System;


/*
 * 
 * 
 * 1. 接口方法不能用public abstract等修饰。接口内不能有字段变量，构造函数。
 * 2.接口内可以定义属性（有get和set的方法）。如string color { get ; set ; }这种。
 * 3.实现接口时，必须和接口的格式一致。
 * 4.必须实现接口的所有方法
 * 
 */
namespace interface_接口_小试牛刀
{
    #region 接口申明

 
    /// <summary>
    /// 定义一个接口
    /// </summary>
    public interface ITransactions
    {
        void showTransaction();
      
        /*定义接口成员，这个和抽象类的方法属性定义类似，
         * 接口成员就是接口的类型的定义，
         * 也就是所要想调用接口就得先实现接口成员，这个就是
         * 
         * 抽象类，接口 的作用和方法的重写有什么区别呢？
         */
         
        double getAmount();
    }
    public class Transactions : ITransactions  //继承接口
    {
        private string tCode;
        private string date;
        private double amount;
        public Transactions()
        {
            tCode = " ";
            date = " ";
            amount = 0.0;
        }
        public Transactions(string c, string d, double a)
        {
            tCode = c;
            date = d;
            amount = a;
        }
        public double getAmount()
        {
            return amount;
        }
        public void showTransaction()
        {
            Console.WriteLine("Transaction: {0}", tCode);
            Console.WriteLine("Date: {0}", date);
            Console.WriteLine("Amount: {0}", getAmount());
        }
    }
    #endregion

    /*
     * 由于C#时单继承结构的语言，也就是说接口是C#对多继承的一种补充，也是多态的一种体现
     * 那么问题来了，啥是多继承呢？
     * 顾名思义多继承就是一个类可以派生一个或者多个类，由于这样会令程序变得混乱复杂，
     * 所以java c#，PHP，就取消了多多继承，不过接口特特性和多继承的概念非常的相像
     * 
     */







    class Program 
    {
        static void Main(string[] args)
        {
            #region 接口声明
            Transactions t1 = new Transactions("001", "8/10/2012", 78900.00);
            Transactions t2 = new Transactions("002", "9/10/2012", 451900.00);
            t1.showTransaction();
            t2.showTransaction();
            Console.ReadKey();
            #endregion



        }
    }
}
