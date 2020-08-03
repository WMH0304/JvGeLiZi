using System;

namespace 结构
{
    /*
     * struct 和class的区别
     * 1. struct 是值类型
     * 1.1  既然是值类型，那么他创建时说被分配到栈上面的
     * 1.2 struct  实例赋值是赋值
     * 1.3 可以直接应用
     * 1.4 结构体内的构造函数必须有参数
     * 1.5 不支持基础，但支持接口
     * 1.6 初始值是 0 有初始值 
     * 2.class 是引用类型
     * 2.1 class 是引用类型 所以他有引用类型的特征
     * 2.2 class 实例赋值时引用地址（指向的内存）
     * 2.3 需要分配内存 使用new 关键字
     * 2.4 构造函数随便
     * 2.5 都支持
     * 2.6 没有初始值 引用类型
     */
    struct jiegou
    { 
         public int B{ set; get; }
         public int C{ set; get; }
    }
    class classs
    {
        public int B { set; get; }
        public int C { set; get; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            #region MyRegion

            
            //声明结构体，并赋值
            jiegou jiegou = new jiegou() { B =6,C =1};
            Console.WriteLine("struct赋值前：B={0},C={1}", jiegou.B, jiegou.C);
            var jis = jiegou;
            jis.B = 7;
            jis.C = 9;
           // 值类型 特点，自身改变不影响其他
            Console.WriteLine("struct赋值前赋值后：B={0},C={1}", jiegou.B, jiegou.C);

            classs classs = new classs();
            Console.WriteLine("classs 赋值前：B={0},C={1}", classs.B, classs.C);
            var cls = classs;
            cls.B = 7;
            cls.C = 9;
            //引用类型特点：是对值得引用，当值改变时，其他地方得引用值也将发送改变
            Console.WriteLine("classs 赋值前：B={0},C={1}", classs.B, classs.C);

            #endregion



        }


    }
}
