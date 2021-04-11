using System.Linq;

namespace Queryable_集合进阶
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            1.什么式queryable
            提供对集合或者是序列方法的操作
            就是是 集合的游戏 的基石
            */


            #region Aggregate 一个累加的函数 对一个键值的不断累加的函数


            int[] ints = { 4, 8, 8, 3, 9, 5, 7, 8, 2 };
            var t0 = 0;//累加器的初始值                      带返回类型的委托  委托传的参数 初始值 和 数组值
            var t = ints.AsQueryable().Aggregate(t0, (t0, j) => j % 9 == 0 ? t0 + 1 : t0);

            System.Console.WriteLine(t);

            string[] str = { "sdf", "sfasdf", "asdfasdf", "asdfasdf", "sdfasd", "sdfas" };

            //
            var t1 = str.AsQueryable().Aggregate("sdfcs", (i, j) => i.Length > j.Length ? j : i,ft=>ft.ToUpper());
            System.Console.WriteLine(t1);

            string strTmp = "abcdefg某某某";

            int i = System.Text.Encoding.Default.GetBytes(strTmp).Length;

            int j = strTmp.Length;



            #endregion










        }
    }
}
