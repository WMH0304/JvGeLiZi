using System;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace 动态编程
{

    /* *
     *  动态编程是什么？
     *  提供一个通用的解决方案与“运行时”环境对话
     *  1. 针对底层CLR 类型使用反射
     *  2. 调用自定义 IDnamicMetaobjectProvider, 使DnamicMetaobjectProvider 变得可用
     *  3. 通过COM 的IUnknown 和IDispatch 接口来调用
     *  4. 调用由动态语言（如 IronPython）定义的类型
     *  
     *  什么是动态？
     *  指程序正在运行时的状态
     *  
     *  动态编程的作用是什么？
     *  是程序更加地灵活，扩展性更强
     *  优缺点？
     *  优点：
     *  是程序更加地灵活，扩展性更强
     *  
     *  缺点：
     *  加大程序地开销
     *  
     *  和静态编程的区别？
     *  动态编程： 在运行时编译 ，第一次编译时会跳过（编译前一切都是未知地）
     *  
     *  静态编译： 编译后运行，他的一起操作都是可预测地
     *  
     *  
     *  本质是什么？
     *  程序运行时获取程序状态地操作
     *  
     *  也就是执行时而不是编译时定义要调用的代码
     *  
     *  note:
     *  反射的关键功能之一就是动态查找和调用特定类型的成员，
     *  
     * 
     * 
     * 
     */

    #region dynamic
    /*
     * dynamic
     * 
     * 1.可以用来调用反射成员
     * 2.编译时需要知道成员名称和前面
     * 3.告诉编译器生成代码的指令， 
     * 4.任何类型都能转换成 dynamic
     * 5.从 dynamic 到一个代替类型的成功转换要依赖于基础类型的支持
     * 6.任何 dynamic 成员调用返回的都是一个dynamic 对象 （这么说的话和object 挺像的）
     * 7.如果知道尘缘在运行时不存在, 运行时 会引发
     * 8.用 dynamic 实现的反射不支持扩展方法
     * 9.刚刚看到了  dynamic 就是一个object （在cil 中 dynamic 就是一个object）
     * 
     * 
     * dynamic 和object 的异同
     * 
     * 同：
     * 1.在cil 中 dynamic 类型实际上就是一个 object 
     * 2.如果没有任何调用 dynamic 类型的声明和object 没有任何区别
     * 
     * 异：
     * 1.再到调用时，为了调用成员，编译器要什么 system.runtime.compilerServices.CallSiter<T> 类型的一个变量 T随成员前面变化
     * 2.动态定义一个方法，该方法可通过参数 callsitesite object dynamictarget string 进行调用
     * 
     * 
     */
    class Dynamic
    {
        public static void Dynamic_mian()
        {
            //data 声明 dynamic 类型 并直接调用方法 ，编译时，不会检查知道成员是否可用
            dynamic data = "原谅我这一生不羁放纵爱自由。";
            Console.WriteLine(data);
            data = (double)data.Length;
            data = data * 3.5 + 28.6;
            if (data == data)
            {
                Console.WriteLine($"{ data } 长时间");
            }
            else
            {
                data.NonExistenMethodCallStillCompiles();
            }

          


        }
    }
    #region 自定义动态对象
    class DynamicXml :DynamicObject
    {
        // XElement 表示一个 XML 元素。 请参阅 XElement 类概述和有关使用情况信息和示例的此页上的备注部分。

        private XElement Element { get; set; }

        public DynamicXml(XElement element)
        {
            Element = element;
        }

        public static DynamicXml Parse(string text)
        {
            return new DynamicXml(XElement.Parse(text));
        }
        /// <summary>
        /// 重写动态 取值
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            bool success = false;
            result = null;
            XElement firstDescendant = Element.Descendants(binder.Name).FirstOrDefault();

            if (firstDescendant != null)
            {
                if (firstDescendant.Descendants().Count() > 0)
                {
                    result = new DynamicXml(firstDescendant);
                }
                else
                {
                    result = firstDescendant.Value;
                }
                success = true;

            }
            return success;
        }

        /// <summary>
        /// 重写动态赋值赋值
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            bool success = false;
            // Descendants 按文档顺序返回此文档或元素的子代元素集合。
            XElement firstDescendant = Element.Descendants(binder.Name).FirstOrDefault();
            if (firstDescendant !=null)
            {
                if (value.GetType() == typeof(XElement))
                {
                    //ReplaceWith 将此节点替换为指定的内容。
                    firstDescendant.ReplaceWith(value);
                }
                else
                {
                    firstDescendant.Value = value.ToString();
                }
                success = true;
            }
            return success;
        }



    }
    #endregion


    #endregion
    #region 启动类

    class Program
    {
        static void Main(string[] args)
        {
            Dynamic.Dynamic_mian();
            Console.ReadLine();
        }
    }


    #endregion




}
