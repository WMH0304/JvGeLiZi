using System;
using System.Reflection;

namespace TestSpace
{
    public class TestClass
    {
        private string _value;
        public TestClass()
        {
        }
        public TestClass(string value)
        {
            _value = value;
        }
        public string GetValue(string prefix)
        {
            if (_value == null)
                return "NULL";
            else
                return prefix + "  :  " + _value;
        }
        public string Value
        {
            set
            {
                _value = value;
            }
            get
            {
                if (_value == null)
                    return "NULL";
                else
                    return _value;
            }
        }
    }
}



namespace 根据类型动态创建对象
{
    class Program
    {
        static void Main(string[] args)
        {
            //获取类型信息
            Type t = Type.GetType("TestSpace.TestClass");
            //构造器的参数
            object[] constuctParms = new object[] { "timmy" };
            //根据类型创建对象
            object dObj = Activator.CreateInstance(t, constuctParms);
            //获取方法的信息
            MethodInfo method = t.GetMethod("GetValue");
            //调用方法的一些标志位，这里的含义是Public并且是实例方法，这也是默认的值
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance;
            //GetValue方法的参数
            object[] parameters = new object[] { "Hello" };
            //调用方法，用一个object接收返回值
            object returnValue = method.Invoke(dObj, flag, Type.DefaultBinder, parameters, null);
        }
    }
}
