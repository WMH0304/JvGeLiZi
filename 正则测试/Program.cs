using System;

namespace 正则测试
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(isDouble("1.2"));
            Console.WriteLine(isDouble("2"));
            Console.WriteLine(isDouble("w"));
            Console.WriteLine(isDouble(Math.PI.ToString()));
            Console.WriteLine(isDouble("dds"));
            Console.WriteLine(isDouble(string.Empty));

        }

        /// <summary>
        /// 限制输入的是 double 或 int 类型数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        static bool isDouble(string str)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[0-9]+\.{0,1}[0-9]{0,2}$");
            return regex.IsMatch(str.Trim());
        }

    }
}
