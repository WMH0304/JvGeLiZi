using System;
using System.Collections.Generic;
using System.Linq;

namespace 逆变和协变
{
    class Program
    {
        /*
         * 资料参考：
         * https://www.cnblogs.com/zhao-yi/p/9759546.html
         * https://www.cnblogs.com/CLR010/p/3274310.html
         * https://www.cnblogs.com/Ninputer/archive/2008/11/22/generic_covariant.html
         * 
         * 官方文档：
         * 协变和逆变可以实现数组类型，委托类型泛型类型参数的隐式引用转换，协变保留分配兼容性，逆变则与之相反
         * 
         * 
         * 协变和逆变就是利用 继承关系 对不同参数类型或返回值类型的委托或者泛型接口之的转变
         * 
         * 协变 ：
         * 
         * 
         * 1.关键字 out
         * 2.在继承关系中 由子类向父类方向转变
         * 3.协变就是让 派生程度 更大的类类型 转换成派生成都更小的类类型
         * 
         * 逆变：
         * 1.关键字 in
         * 2.在继承关系中 有父类向子类方向转变
         * 3.逆变就是让 派生程度 小的类类型 转换成派生程度更大的类型
         * 
         * 
         * 派生程度： 在继承连中的继承关系的一种说法
         * 假设 A 派生自 B  class A :B{}
         * 相对于 B 来说 A 的派生程度更大，同时A也具备一些 B 的属性, 但 B 不具备 A 的属性， 从 A 转向 B 就叫协变
         * 而对于 A 俩说 B 的派生程度更小，同时A也具备一些 B 的属性，但 B 不具备 A 的属性， 从 B 转向 A 就叫逆变
         * 
         * 协变和逆变的原理和 装箱拆箱的原理及其相似
         * 都是从派生程度上描述对对象的操作
         * 但实际上
         * 协变和逆变 面向的是 “类级”（引用类型，只是一个对象，或者说是一个地址）的
         * 
         * 而
         * 
         * 装箱和拆箱 面向的是 “值级”（对象已赋值，面向的是某个内存块，或者说是一个字符串或者其它类型的值）
         * 
         * 装箱：将值类型转换成引用类型
         * 
         * 拆箱：将引用类型转换成值类型
         * 
         */
        static void Main(string[] args)
        {

            string str = "test";
            //  派生类型较多的对象被分配给派生类型较少的对象。
            object obj = str;
            //IEnumerable，该枚举数支持在指定类型的集合上进行简单迭代。
            // 协变性
            //用派生类型参数实例化的对象
            //被分配给实例化的带有派生类型参数较少的对象。
            //保留了分配兼容性。
            //遍历集合的枚举，
            IEnumerable<string> strings = new List<string>();

            //创建一个委托（obj类型）
            Action<object> actObject = SetObject;
            // 逆变性。
            //用派生较少的类型参数实例化的对象
            //分配给实例化的具有派生类型参数的对象。
            //分配兼容性反转。
            Action<string> actString = actObject;
            if (true^false)
            {
                Console.WriteLine("dsfasd");
            }
            if (!true ^true)
            {
                Console.WriteLine("dsfasd");
            }
         

        }

        #region 委托的逆变和协变

       
        static object GetObject() { return null; }

        static void SetObject(object obj) { }

        static string GetString() { return ""; }

        static void SetString(string str) { }

        static void Test()
        {
            // 协变.
            // 委托将返回类型指定为对象，
            // 但是你可以指定一个返回字符串的方法。
            // 派生程度大的转向派生程度小的
            Func<object> del = GetString;
            // 逆变.
            // 委托将参数类型指定为字符串，
            // 但是你可以指定一个接受对象的方法。 
            // 派生程度小的转向派生程度大的
            Action<string> del2 = SetObject;
        }

        static void Setbject(object y)
        {
            Console.WriteLine("haha");
            Console.ReadKey();
        }

        #endregion


        #region 接口的逆变和协变
        static void 接口的逆变和协变()
        {

            /*
             *  官方文档
             *   https://docs.microsoft.com/zh-cn/dotnet/csharp/programming-guide/concepts/covariance-contravariance/variance-in-generic-interfaces
             * 
             */

            //协变允许方法具有的返回类型比接口的泛型类型参数定义的返回类型的派生程度更大。

            /* 
             * 
             * IEnumerable 泛型接口 也是支持协变的接口
             * 
             * 接口协变
             * 有意思的是  IEnumerable<String> 派生于 IEnumerable<Object> 接口
             * 但是 String 类型派生自 Object 类型
             * 
             */
            IEnumerable<String> strings = new List<String>();
            IEnumerable<Object> objects = strings;


            //逆变允许方法具有的实参类型比接口的泛型形参定义的类型的派生程度更小。






        }

        
        class BaseClass { }
        class DerivedClass : BaseClass { }


        /*

         public interface IEqualityComparer<in T>

         IEqualityComparer<T> 定义方法以支持对象的相等比较。 一个比较的接口，看到 in 关键字没？她支持逆变哟！
         */

        // 比较器  BaseComparer 实现 IEqualityComparer 接口 接口类型为 BaseClass
        class BaseComparer : IEqualityComparer<BaseClass>
        {
            //实现方法
            public int GetHashCode(BaseClass baseInstance)
            {
                return baseInstance.GetHashCode();
            }
            //实现方法
            public bool Equals(BaseClass x, BaseClass y)
            {
                return x == y;
            }
        }
        class Tes
        {
             static void Test()
            {
                //接口的逆变
                IEqualityComparer<BaseClass> baseComparer = new BaseComparer();
                // DerivedClass 派生自 BaseClass  
                // 派生程度小 的转向 派生程度大 的叫逆变

                IEqualityComparer<DerivedClass> childComparer = baseComparer;
            }
        }
        #endregion


        #region 泛型集合的协变

        // Simple hierarchy of classes.  
        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class Employee : Person { }
      
        /*
         * 转换泛型集合
         */
        class Prram
        {
            //  IEnumerable<Person> 类型参数 方法
            public static void PrintFullName(IEnumerable<Person> persons)
            {
                foreach (Person person in persons)
                {
                    Console.WriteLine("Name: {0} {1}",
                    person.FirstName, person.LastName);
                }
            }

            public static void Test()
            {

                //枚举接口，将list 的所有数据遍历出来 并赋值给 employees
                IEnumerable<Employee> employees = new List<Employee>();

                //调用方法遍历
                PrintFullName(employees);
            }
        }


        /*
         * 比较泛型集合
         */
        class PersonComparer : IEqualityComparer<Person>
        {
            public bool Equals(Person x, Person y)
            {
                //ReferenceEquals 是否是相同的实例。
                if (Object.ReferenceEquals(x, y)) return true;
                if (Object.ReferenceEquals(x, null) ||
                    Object.ReferenceEquals(y, null))
                    return false;
                return x.FirstName == y.FirstName && x.LastName == y.LastName;
            }
            public int GetHashCode(Person person)
            {
                if (Object.ReferenceEquals(person, null)) return 0;
                int hashFirstName = person.FirstName == null ? 0 : person.FirstName.GetHashCode();
                int hashLastName = person.LastName.GetHashCode();

                /*
                    二元 ^ 运算符是为整型和 bool 类型预定义的。 对于整型，^
                    将计算操作数的按位“异或”。 对于 bool 操作数，^
                    将计算操作数的逻辑“异或”；也就是说，当且仅当只有一个操作数为
                    true 时，结果才为 true。
                 */

                return hashFirstName ^ hashLastName;
            }
        }

        class Pam
        {

            public static void Test()
            {
                List<Employee> employees = new List<Employee> {
               new Employee() {FirstName = "Michael", LastName = "Alexander"},
               new Employee() {FirstName = "Jeff", LastName = "Price"}
            };

                //Distinct 返回序列中的非重复元素。
                IEnumerable<Employee> noduplicates = employees.Distinct<Employee>(new PersonComparer());

                foreach (var employee in noduplicates)
                    Console.WriteLine(employee.FirstName + " " + employee.LastName);
            }
        }
        #endregion






































        /*************** 我也是有底线的  ***************************/
    }
}
