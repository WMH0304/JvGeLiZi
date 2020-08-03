using System;
using System.IO;

namespace 说明书栗子
{
    class Program
    {
        static void Main(string[] args)
        {
            int t = 0;
            string s;
            if (args.Length > 0)
            {
                s = args[0];
            }
            else
            {
                s = Directory.GetCurrentDirectory();//获取当前目录
            }
            t = DirectoryCountLine(s);
            Console.WriteLine(t);
            //stad(sez:"sdfaf", dsfa:"safd");
            //static void stad(string sez,
            //  string sdf =default(string), string dsfa = default(string))
            //{
            //}
        }
        static int DirectoryCountLine(string s)
        {
            int lineCount = 0;
            foreach (string item in Directory.GetFiles(s,"*.cs"))//返回指定目录中文件的名称（包括其路径）。
            {
                lineCount += CountLine(item);
            }
            foreach (string str in Directory.GetDirectories(s))
            {
                lineCount += DirectoryCountLine(str);
            }
            return lineCount;
        }
        private static int CountLine(string file)
        {
            string lin;
            int lc =0;
            FileStream stream = new FileStream(file,FileMode.Open);//文件打开流
            StreamReader reader = new StreamReader(stream);//文件读取流
            lin = reader.ReadLine();//读取对象文件
            while (lin !=null)
            {
                if (lin.Trim() !="")
                {
                    lc++;
                }
                lin = reader.ReadLine();//读取行
            }
            reader.Close();
            return lc;





        }
        private static int CountLine(string file,int a)
        {
            return a;
        }
       


    }

   
}
