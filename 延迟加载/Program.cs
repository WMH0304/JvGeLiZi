using System;

namespace 延迟加载
{
    class Program
    {

        /*
         * https://www.cnblogs.com/Juning/p/11549080.html
         * 
         * https://www.cnblogs.com/springsnow/p/12188860.html
         * 
         * 延迟加载：
         *  会使用到一个叫做 Lazy 的了
         *  通常我那么在使用对象是都是已实例好的，而延迟加载时在我们要使用对象的时后再创建对象
         *  
         *  Lazy<T> 对象初始化默认是线程安全的，在多线程环境下，第一个访问 Lazy<T> 对象的 Value 属性的线程将初始化 Lazy<T> 对象，
         *  以后访问的线程都将使用第一次初始化的数据。
         *  
         *  
         *  
         *  
         */
       static  void Main(string[] args)
        {
            Lazy<Big> big = new Lazy<Big>();
            Console.WriteLine("对象创建"+big.IsValueCreated);
            big.Value.Test();//执行方法
            Console.WriteLine("对象创建" + big.IsValueCreated);

            //使用委托实现延迟加载

            Lazy<Big> big1 = new Lazy<Big>(() => new Big(100));
            big1.Value.Tes();

           
        }
    }
    class Big
    {
        public int ID { get; set; }
        public Big() { }

        public Big(int ID) {
            this.ID = ID;
        }
        public void Tes(){
            Console.WriteLine("ID=" + ID.ToString()) ;
}

        public void Test()
        {
            Console.WriteLine("Test....");
        }
    }

}
