using System;
using System.Linq;

namespace Tools_工具方法测试
{
    public class Thisclass
    {
        public string tyName { get; set; }
        public string tyFullNametyName { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {

            decimal t = 22222;
            Console.WriteLine(ToCap(t));
            Console.WriteLine(ToCapital(t));
            Console.WriteLine(ToLow("叁分"));
            string str = "大王叫我来巡山";

            Console.WriteLine(isOrNo(str, "String"));
            Console.WriteLine(isOrNo(str, "int32"));
            Program thisclass = new Program();
            //Console.WriteLine(isOrNo(thisclass, "Program", "Tools_工具方法测试"));
            Thisclass thisclass1 = new Thisclass();

            Console.WriteLine(isOrNo(thisclass1, "Program+Thisclass", "Tools_工具方法测试"));//如果要反射某个类里面的子类的话要 用 + 拼接 子类名称

            // Console.WriteLine(isString(t));

        }

        #region MyRegion
        private struct Thisclass
        {
            public string tyName { get; set; }
            public string tyFullNametyName { get; set; }
        }
        #endregion




        /// <summary>
        /// 是否是某一种数据类型
        /// </summary>
        /// <param name="o">判断对象</param>
        /// <param name="str">对象类型</param>
        /// <returns></returns>
        public static bool isOrNo(object o, string _class)
        {
            _class = _class.Substring(0, 1).ToUpper() + _class.Substring(1, _class.Length - 1);

            #region MyRegion
            /*
             System.Collections.Generic.Dictionary<string, Thisclass> dn = new System.Collections.Generic.Dictionary<string, Thisclass>()
            {
                {"string",new Thisclass(){tyName = "String", tyFullNametyName= "System.String" } },
                {"decimal",new Thisclass(){tyName = "Decimal", tyFullNametyName= "System.Decimal" }},
                {"double",new Thisclass(){tyName = "Double", tyFullNametyName= "System.Double" } },
                {"float",new Thisclass(){tyName = "float", tyFullNametyName= "System.Float" } },
                {"int",new Thisclass(){tyName = "Int32", tyFullNametyName= "System.Int32" } },
            };
             
             */
            #endregion

            Type type = o.GetType();
            Type type1 = Type.GetType($"System.{_class}");

            #region MyRegion
            /*
           str = str.ToLower();
           var name = dn[str].tyName;
           var fullName = dn[str].tyFullNametyName;
           */

            #endregion

            if (type.Name == type1.Name && type.FullName == type1.FullName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是某一种类型
        /// </summary>
        /// <param name="o">判断对象</param>
        /// <param name="str">对象类型</param>
        /// <param name="namesPace">命名空间</param>
        /// <returns></returns>
        public static bool isOrNo(object o, string _class, string namesPace)
        {
            bool returns;
            _class = _class.Substring(0, 1).ToUpper() + _class.Substring(1, _class.Length - 1);
            namesPace = namesPace.Substring(0, 1).ToUpper() + namesPace.Substring(1, namesPace.Length - 1);
            Type type = o.GetType();
            /*
              Assembly.Losad("Tools_工具方法测试").GetType("Thisclass");
             */
            Type type1 = Type.GetType($"{namesPace}.{_class}");

            #region MyRegion
            //if (type.Name == type1.Name && type.FullName == type1.FullName)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            #endregion
            returns = (type.Name == type1.Name && type.FullName == type1.FullName) ? true : false;
            return returns;
        }


        /// <summary>
        /// 判断是否是字符串类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool isString(object o)
        {
            Type type = o.GetType();
            if (type.Name == "String" && type.FullName == "System.String")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否是decimal 类型
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool isDecimal(object o)
        {
            Type type = o.GetType();
            if (type.Name == "Decimal" && type.FullName == "System.Decimal")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 小写金额转大写金额
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ToCap(decimal num)
        {
            num = Decimal.Round(num, 2);
            var content = "";
            if (num < decimal.MaxValue && num > decimal.MinValue)
            {
                var nums = num.ToString("f2").ToCharArray().ToList();
                nums.RemoveAll(c => c.ToString() == ".");
                string[] bgW = { "仟", "佰", "拾", "亿", "仟", "佰", "拾", "万", "仟", "佰", "拾", "元", "角", "分" };
                System.Collections.Generic.Dictionary<int, string> dc = new System.Collections.Generic.Dictionary<int, string>()
                {
                    { 0,"零"},{ 1,"壹"}, {2,"贰" },{3,"叁"},{4,"肆"},{5,"伍"},{6,"陆"},{7,"柒"},{8,"捌"},{9,"玖"}
                };
                var bgw = bgW.ToList();
                for (int i = nums.Count; i > 0; i--)
                {
                    int n = int.Parse(nums[nums.Count - i].ToString());//数字
                    var u = bgw[(bgW.Length - i)].ToString();//单位
                    content += dc[n] + u;
                }
            }
            else
                throw new ArgumentNullException("参数为空或异常");
            return content;
        }

        /// <summary>
        /// 金额大写转小写
        /// </summary>
        /// <returns></returns>
        public static string ToLow(string str)
        {
            //贰万贰仟贰佰贰拾贰元叁角叁分
            var num = str.ToArray().ToList();
            System.Collections.Generic.Dictionary<string, int> dc = new System.Collections.Generic.Dictionary<string, int>()
                {
                    { "零",0},{ "壹",1}, {"贰",2 },{"叁",3},{"肆",4},{"伍",5},{"陆",6},{"柒",7},{"捌",8},{"玖",9}
                };
            var nums = "";
            foreach (var item in num)
            {
                try
                {
                    nums += dc[item.ToString()];
                }
                catch (System.Collections.Generic.KeyNotFoundException)//忽略查询不到对应建的异常
                {
                }
                catch (Exception)
                {
                    throw;
                }
            }
            if (str.Length <= 5 && nums.Length <= 3)
            {
                nums = nums.Insert(0, "0");
            }
            if (str.Length <= 2 && nums.Length <= 3)
            {
                nums = nums.Insert(0, "0");
            }
            return nums.Insert(nums.Length - 2, ".");
        }

        /// <summary>
        /// 小写金额转大写金额
        /// </summary>
        /// <param name="smallnum"></param>
        /// <returns></returns>
        public static string ToCapital(decimal smallnum)
        {
            if (smallnum > new decimal(1000000000000L) || smallnum < new decimal(-99999999999L) || smallnum == 0m)
            {
                return "";
            }

            string text = "仟佰拾亿仟佰拾万仟佰拾元角分";
            string text2 = "壹贰叁肆伍陆柒捌玖";
            string text3 = "";
            string text4 = "";
            string text5 = "";
            string text6 = "";
            string text7 = decimal.Round(smallnum, 2).ToString("############.00");
            int length = text7.Length;
            int num = length - 2;
            string text8 = "";
            int num2 = 15 - length;
            int num3 = 1;
            int num4 = 0;
            if (length > 15)
            {
                return "";
            }

            for (num4 = 0; num4 < length; num4++)
            {
                if (num4 == num - 1)
                {
                    continue;
                }

                text5 = text.Substring(num2, 1);
                num2++;
                text8 = text7.Substring(num4, 1);
                if (text8 == "-")
                {
                    text3 += "负";
                    continue;
                }

                if (text8 == "0")
                {
                    text4 = text3.Substring(text3.Length - 2, 1);
                    if (num2 == 4 || num2 == 8 || text4.IndexOf("亿") >= 0 || num2 == 12)
                    {
                        text3 += text5;
                        num3 = ((text2.IndexOf(text4) >= 0) ? 1 : 0);
                    }
                    else
                    {
                        num3 = 0;
                    }

                    continue;
                }

                if (text8 != "0" && num3 == 0)
                {
                    text3 += "零";
                    num3 = 1;
                }

                text6 = text2.Substring(Convert.ToInt32(text8) - 1, 1);
                text3 = text3 + text6 + text5;
            }

            if (text7.Substring(text7.Length - 2, 1) == "0")
            {
                return text3 + "整";
            }

            return text3;
        }
    }
}


//if (num.Contains('角') || num.Contains('分'))
//{

//}
