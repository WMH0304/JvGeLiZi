using System;
using System.Data;
using System.Xml;

namespace XML初探
{
   static  class Program
    {
        static void Main(string[] args)
        {
            xmlTest();
            Test();
        }

        /// <summary>
        /// xml 
        /// </summary>
        static void xmlTest()
        {
            XmlDocument xml = new XmlDocument();
            XmlElement xmlElement = xml.CreateElement("Test1");
            xmlElement.SetAttribute("version","1.0");
            xmlElement.SetAttribute("type","大王叫我来巡山");
            xml.AppendChild(xmlElement);
            
            xmlElement = xml.CreateElement("Test2");
            XmlNode xmlNode = xml.SelectSingleNode("Test1");
            xmlNode.AppendChild(xmlElement);
            xmlNode.InnerText = "哈哈";
            try
            {
                xml.Save("D:/举个栗子/举个栗子/test.xml");
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="xml">文本</param>
        /// <param name="xmlNode">节点</param>
        /// <param name="name">名字</param>
        /// <param name="value">值</param>
        static XmlNode createNode(XmlDocument xml, XmlNode xNode, string name, string value)
        {
            XmlNode node = xml.CreateNode(XmlNodeType.Element, name, "");
            node.InnerText = value;
            return xNode.AppendChild(node);
        }


        public static T ToEntity<T>(this DataTable table) where T : new()
        {
            T entity = new T();
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (row.Table.Columns.Contains(item.Name))
                    {
                        if (DBNull.Value != row[item.Name])
                        {
                            Type newType = item.PropertyType;
                            //判断type类型是否为泛型，因为nullable是泛型类,
                            if (newType.IsGenericType
                                    && newType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))//判断convertsionType是否为nullable泛型类
                            {
                                //如果type为nullable类，声明一个NullableConverter类，该类提供从Nullable类到基础基元类型的转换
                                System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(newType);
                                //将type转换为nullable对的基础基元类型
                                newType = nullableConverter.UnderlyingType;
                            }

                            item.SetValue(entity, Convert.ChangeType(row[item.Name], newType), null);

                        }

                    }
                }
            }

            return entity;
        }

        public static void Test()
        {
            DataTable dataTable = new DataTable();
       
            dataTable.Columns.Add("第一列");
            dataTable.Columns.Add("第二列");
            dataTable.Columns.Add("第三列");


            for (int i = 0; i < 10; i++)
            {
                dataTable.Rows.Add($"dafasdf{i}",$"asdf{i}",$"asdfa{i}");
            }
            ToEntity<object>(dataTable);
        }


    }
}
