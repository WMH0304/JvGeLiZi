using System;

namespace 测试题目
{

    public delegate void BtD(string num);


    public class father
    {
        public string fname;
        public father(string name)
        {
            this.fname = name;
            Console.WriteLine(name);
        }
    }

    public class Sd
    {
        public event BtD BtDevent;

        public Sd(string num)
        {
            BtDevent += new BtD(this.ByT);
        }

        public void ShowNum(string num)
        {
            BtDevent(num);
        }

        private void ByT(string num)
        {
            Console.WriteLine("委托" + num);
        }
    }

  public  static class Test
    {
        public static bool t;
        static public int id { get; set; }

        static public int name { get; set; }

        static public string old { get; set; }
    }



    public class Program//:father


    {
        //private int age;



        /* 

         public Program(int age) : base("dd")
         {
             this.age = age;
             Console.WriteLine(age);
         }
       */
        static void S(int[] t)
        {
            for (int i = 0; i < t.Length; i++)
            {
                Console.Write(t[i]++);
                Console.Write("");
            }
        }
        public int a = 99;
        public int A
        {
            get
            {
                return a;
            }
            set
            {
                a = a;
            }
        }

        //private int days;


     
        static void Main(string[] args)
        {
            if (Test.t)
            {
                Test.id = 1;
                Test.name = 2;
                Test.old = "t =true 的情况";
                Test.t = false;
            }


            if (!Test.t)
            {
                Test.id = 1;
                Test.name = 2;
                Test.old = "t =false 的情况";
                Test.t = true;
            }
           
            /* int[] t = new int[] { 1, 2, 3, 4, 5 };
             S(t);
             Console.WriteLine();

             S(t);

             Program c = new Program(11);
             Console.WriteLine(c.A);
             c.A = 55;

             Hashtable h = new Hashtable();
             h.Add(3, "加3");
             h.Add(2, "加2");
             h.Add(1, "加1");

             Console.WriteLine(h[3]);


     */
            /*
                        Program program = new Program();
                        Console.WriteLine(program.days -1);

                      */

            /* Console.WriteLine("创建");
             Sd st = new Sd("T1");
             Console.WriteLine("完毕");
             st.ShowNum("T1");*/







            Console.ReadLine();
        }
    }
}
