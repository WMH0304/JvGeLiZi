using System;

namespace 良构类型
{
    class Program
    {

        /*
         * 避免使用相等性操作符惊醒null检查（a ==null）否则可能会造成死循环
         */
        static void Main(string[] args)
        {
            #region 重写tostring


            //Demo d = new Demo("重写输出");
            //Demo1 d1 = new Demo1("原始输出");
            //c dd = new c();
            //Console.WriteLine(d); //输出：重写输出,重写ToString之后输出你重写的内容
            //Console.WriteLine(d1); //输出d1的类型：OverToString.Demo1,就是如果没有重写是默认输出值得类型
            //Console.WriteLine(dd);

            #endregion


            #region 重写Equals
            /*
            Person p1 = new Person("1");
            Person p2 = new Person("1");
            //True
            Console.WriteLine(p1 == p2);

            //True
            Console.WriteLine(p1.Equals(p2));

            //False
            Console.WriteLine(ReferenceEquals(p1, p2));

            Console.Read();
            */
            #endregion

            #region 重写一元操作符
            one_operator o = new one_operator();
            o.show();
            o++;
            o.show();
            o--;
            o.show();
            Console.ReadKey();
            #endregion
        }


    }

    #region 重写一元操作符
    class one_operator
    {
        public int one;
        public int two;

        public one_operator()
        {
            one = 1;
            two = 2;
        }
        public static one_operator operator ++(one_operator o)
        {
            o.one = o.one + 7;
            o.two = o.two + 6;
            return o;
        }
        public static one_operator operator --(one_operator o)
        {
            o.one = o.one -1;
            o.two = o.two - 2;
            return o;
        }
         
        public void show()
        {
            Console.WriteLine(one);
            Console.WriteLine(two);
        }
      
    }


    #endregion

    #region 重写Equals
    /*
     * 灵魂质问： 为啥要重写equlas呢？
     * 1.自定义结构体是，处于性能考虑，避免不必要的装箱和反射
     * 2.class类型需要相等性而非同一性的比较，例如System.String类型，虽然是引用类型，
     * 但是两个string变量进行比较时，是进行相等性比较，而非引用。
     * 
     * 1.equals 是啥？ 判断两个对象是否一致的api
     * 理解equals 要搞清楚两个概念 同一性和相等性
     * 
     * 1.1同一性是指两个变量引用的是内存中的同一个对象，C#使用Object.ReferenceEquals(obj,obj)进行判断； 针对地是对象，也就是我么常说地引用类型
     * 1.2相等性是指两个变量所包含的数值相等，一般来说是对各个字段进行值比较，像我们平时查询数据用的就比较多了，针对地是值类型
     * 
     * 2.重写 equals 的步骤
     * 2.1 检查是否为null
     * 2.2 如果是引用类型，检查引用是否相等。ReferenceEquals
     * 2.3 检查数据类型是否相同
     * 2.4 一个知道了具体类型的辅助方法，他能将操作数视为要比较的类型，而不是将其视为对象
     * 2.5 可能要检查散列码是否相当，如果散列码都不相等，就没有必要继续执行异常全面的，逐个字段的比较（相等的两个对象的散列码相同）
     * 2.6 如果基类重写了equals() 就检查base.equals
     * 2.7 比较每一个标识字段，判断是否相等
     * 2.8 重写getHashCode()
     * 2.9 重写 == 和 ！= 操作符
     * 
     *   
     */

    public class Person : IEquatable<Person>
    {
        //IEquatable 定义一个通用的方法，由值类型或类实现以创建类型特定的方法，用于确定实例间的相等性。
        private string _Id;

        public Person(string id)
        {
            _Id = id;
        }
        //比较引用类型
        public bool Equals(Person other)
        {
            //this非空，obj如果为空，则返回false
            if (ReferenceEquals(null, other)) return false;

            //如果为同一对象，必然相等
            if (ReferenceEquals(this, other)) return true;

            //对比各个字段值
            if (!string.Equals(_Id, other._Id, StringComparison.InvariantCulture))
                return false;

            //如果基类不是从Object继承，需要调用base.Equals(other)
            //如果从Object继承，直接返回true
            return true;
        }

        public override bool Equals(object obj)
        {
            //this非空，obj如果为空，则返回false
            if (ReferenceEquals(null, obj)) return false;

            //如果为同一对象，必然相等
            if (ReferenceEquals(this, obj)) return true;

            //如果类型不同，则必然不相等
            if (obj.GetType() != this.GetType()) return false;

             //1.1
            //调用强类型对比
            //return Equals((Person)obj);
            //1.2
            //值类型
            return obj is Person && Equals((Person)obj);
        }

        //实现Equals重写同时，必须重写GetHashCode
        public override int GetHashCode()
        {
            return (_Id != null ? StringComparer.InvariantCulture.GetHashCode(_Id) : 0);
        }

        //重写==操作符
        public static bool operator ==(Person left, Person right)
        {
            return Equals(left, right);
        }

        //重写!=操作符
        public static bool operator !=(Person left, Person right)
        {
            return !Equals(left, right);
        }

    }
    #endregion
    #region 重写tostring
    class c
    { }
    public class Demo
    {
        public Demo(string str)
        {
            this.Str = str;
        }
        public override string ToString()
        {
            return this.Str.ToString();
        }
        public string Str { get; set; }

    }
    public class Demo1
    {
        public Demo1(string str)
        {
            this.Str = str;
        }

        public string Str { get; set; }

    }
    #endregion
}
