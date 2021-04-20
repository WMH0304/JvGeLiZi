using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IQueryable
{
    /*
     * https://www.cnblogs.com/guogangj/archive/2013/02/22/2920961.html
     * 
     * IQueryable  提供对未指定数据类型的特定数据源的查询进行计算的功能。 IQueryable 派生自 IEnumerable
     * 
     * 1.IQueryable 派生自 IEnumerable，所以 IQueryable 接口 几乎和 IEnumerable<T> 完全一样 
     * 2.IQueryable 和IEnumerable 区别在于 他他只继承了 和IEnumerable 中直接声明的方法 
     * 比如 GetEnumerato ，由于扩展方法不会被继承。 所以 IQueryable 没有 IEnumerable 的任何扩展方法
     * 3.有趣的是 ， IQueryable 有一个类似的扩展类， system.linq.queryable 在这个类中 
     * Enumerable 为 IEnumerable 添加的所以方法  Queryable 都会为 IQueryable<T> 添加，
     * 所以 IQueryable提供了一个几乎和 IEnumerable 一样的编程接口
     * 
     * 果然世界是充满矛盾的，每个事务都有他的对立面，每个不足的背后都有他的弥补的方法。
     * 
     * 
     *  都说 IQueryable 和  IEnumerable 及其相似，那么为什么不直接用 IEnumerable 呢？
     *  为什么还要大费周章地  推迟  IQueryable 接口呢？
     *  答案是
     *  1.可以通过 IQueryable 实现自定义地 LINQ Provider（linq 提供程序）
     *  1.1  LINQ Provider 的作用是将表达式分解成各个组成部分， 一经分解就可以传换成另一种语言，可以序列化以便可以再远程执行，可以通过一个异步执行模式来注入
     *  1.2 
     *  
     * 
     */
    #region linq to object
    class IinqToObject
    {
       static IEnumerable<int> FindGreaterThan5(IEnumerable<int> st)
        {
            //foreach (var item in st)
            //{
            //    if (item >= 5)
            //    {
            //       // 在语句中使用 yield 关键字，则指示在的方案、运算符或 get 访问器是迭代器。 使用的迭代器对集合的自定义迭代。 下面的示例演示 yield 语句的两种形式。

            //        //yield return 语句返回每个元素一个节点。

            //        yield return item;
            //    }
            //}
            
            //foreach 使用 linq 写法
            return st.Where(i => i >= 5);
        }

     public   static void IinqToObjects()
        {
            List<int> listTest = new List<int> { 8, 2, 7, 9, 1, 5, 3, 4 };

            //找出所有大于等于5的数
            IEnumerable<int> result = FindGreaterThan5(listTest);
            foreach (var i in result)
            {
                
                Console.WriteLine(i);
            }
        }
    }
    #endregion



    #region 启动类
    class Program
    {
        static void Main(string[] args)
        {
            IinqToObject.IinqToObjects();

            Console.ReadKey();
        }
    }
    #endregion

}
