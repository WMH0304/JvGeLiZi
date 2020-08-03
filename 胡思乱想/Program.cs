using System;
using System.Collections.Generic;

namespace 胡思乱想
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    //for (int i = 0; i < 9; i++)
        //    //{

        //    //    for (int j = 0; j < 9; j++)
        //    //    {
        //    //        if (i < j)
        //    //        {
        //    //            Console.Write(" ");
        //    //        }

        //    //    }

        //    //    for (int d = 0; d < 9; d++)
        //    //    {
        //    //        if (d < i)           
        //    //        {
        //    //            Console.Write("*");
        //    //        }
        //    //    }
        //    //    Console.WriteLine("");
        //    //}
        //    //for (int i = 0; i < 15; i++)
        //    //{
        //    //    if (i < 8)
        //    //    {
        //    //        Console.Write(" ");
        //    //    }
        //    //}

        //    // i 纵 j 横
        //    //for (int i = 0; i < 10; i++)
        //    //{
        //    //    for (int j = 0; j < 50; j++)
        //    //    {
        //    //        if (j<10)
        //    //        {
        //    //            if (i<j)
        //    //            {
        //    //                Console.Write(" ");
        //    //            }
        //    //        }
        //    //        if (j>20 && j<30)
        //    //        {
        //    //            Console.Write(" ");
        //    //        }
        //    //        Console.Write("*");
        //    //    }

        //    //}
        //   // Console.WriteLine();
        //    for (int i = 0; i < 50; i++)
        //    {
        //        for (int j = 0; j < 50; j++)
        //        {
        //            if (i<10 &&j<10)
        //            {
        //                if (i<j)
        //                {
        //                    Console.Write(" ");
        //                }
        //                if (i < j)
        //                {
        //                    Console.Write("*");
        //                }
        //            }
        //        }
        //    }







        //}

        #region 求三角形面积  
        //public static void Main()
        //{

        //    //int a = 3, b = 4, c = 5;
        //    ////海伦公式 求三角形面积  s = 根号 l(l-a)(l-b)(l-c)
        //    ////l = a+b+c/2                                      a
        //    //int l = (a + b + c)/2;                             /\
        //    //int ll = l*(l - a)*(l - b)*(l - c);               /  \
        //    //Console.WriteLine(l);//6                         /    \
        //    //Console.WriteLine(ll);//36                      /______\
        //    //double t =  Math.Sqrt(ll);                     b         c
        //    //Console.WriteLine(t);//开根号

        //    double a, b, c,l,r;
        //    Console.WriteLine("输入边长,回车结束");
        //    a =Convert.ToInt32 (Console.ReadLine());
        //    Console.WriteLine("输入第一条边边长{0}", a);
        //    b = Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine("输入第二条边边长{0}", b);
        //    Console.WriteLine(b);
        //    c = Convert.ToInt32(Console.ReadLine());
        //    Console.WriteLine("输入第三条边边长{0}", c);
        //    Console.WriteLine(c);
        //    // Convert.ToInt32(a);Convert.ToInt32(b); Convert.ToInt32(b);
        //    if ( (a+b) >c && (a+c)>b && (b+c)>a)
        //    {
        //        #region 海伦公式
        //        //l = (a + b + c) / 2;
        //        //r = l * (l - a) * (l - b) * (l - c);
        //        //double s = Math.Sqrt(r);
        //        //Console.WriteLine("边长和的一半为：{0}", l);
        //        //Console.WriteLine("根据海伦公式可知三角形面积为：{0}", s);
        //        #endregion
        //        #region 三角函数
        //        // s= absinc /2
        //        double[] t = {a,b,c };
        //        Program program = new Program();
        //        program.Sort(t);

        //        Console.WriteLine(t);


        //        #endregion

        //    }
        //    else
        //    {
        //        Console.WriteLine("输入的边长关系，不能构成一个三角形，请重新输入");
        //        Main();
        //    }



        //}


        public static void Main()
        {
            double[] arr = {5,3,4,6,1,7,9,2,8};
            int i, j, temp;
            bool done = false;
            j = 1;
            while ((j < arr.Length) && (!done))//判断长度    
            {
                done = true;
                for (i = 0; i < arr.Length - j; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        done = false;
                        
                        temp = Convert.ToInt32(arr[i]);
                       
                        arr[i] = arr[i + 1];//交换数据    
                        arr[i + 1] = temp;
                    }
                }
                j++;
            }
            for (int k = 0;  k< arr.Length; k++)
            {
                Console.WriteLine(arr[k]);
            };
        }
        //public static void Main()
        //{
        //    int[] list = { 2, 5, 6, 4, 1, 3, 7, 9, 8 };
        //        int temp;
        //            for (int i = 0; i<list.Length ; i++)
        //              {
        //            for (int j = 1; j < list.Length ; j++)
        //            {
        //                if (list[j - 1] > list[j]) //当前一位大于后一位时
        //                {
        //                    temp = list[j - 1]; //用 temp 装起前一位
        //                    list[j - 1] = list[j];//将后一位的值赋给前一位
        //                    list[j] = temp;//将前一位的值赋给后一位
        //                Console.Write( "\0{0}",list[j]);
        //                }

        //          // Console.Write("\0 a {0} ",list[i]) ;
        //            }
        //        //Console.Write(" \0{0}", list[i]);
              
        //    }
         
        //}

       
            
            
        


    }
        #endregion
    }

