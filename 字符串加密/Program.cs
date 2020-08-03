using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;


namespace 字符串加密
{
    class Program
    {
        /// <summary>
        /// 获取密钥
        /// </summary>
        private static string Key
        {
            get { return @")O[NB]6,YF}+efcaj{+oESb9d8>Z'e9M"; }
        }

        static void Main(string[] args)
        {

            #region 
           // string newpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password.Trim(), "MD5");

            string sd = MDFive("123456");
            Console.WriteLine("MD%加密：  "+sd);

            System.Uri uri1 = new Uri(@"C:\filename.txt");
            System.Uri uri2 = new Uri(@"C:\mydirectory\anotherdirectory\");

            Uri relativeUri = uri2.MakeRelativeUri(uri1);

            Console.WriteLine(relativeUri.ToString());

            string s1 = "The quick brown fox jumps over the lazy dog";
            string s2 = "fox";
            bool b;
            b = s1.Contains(s2); //20121201
            Console.WriteLine("Is the string, s2, in the string, s1?: {0}", b);


            // CreateId();

            //  string newpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password.Trim(), "MD5");

            #endregion

            string str = "TQjZBreWwz2mj+nJ+H7wyafIPoVnsg83taU5aZbVC+U=";
          string a =  Decrypt(str);
            Console.WriteLine(a);
            Debug.WriteLine(a);
            Debug.WriteLine(Trim(a));

        }
        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MDFive(string str)
        {
            if (str is null)
            {
                return null;
            }
            //MD5 实例
            //string md = System.Security.Cryptography.MD5.Create(System.Text.Encoding.UTF8.GetBytes(str).ToString());

            // return md;

            //实现接口方法
            System.Security.Cryptography.MD5 mymd = System.Security.Cryptography.MD5.Create();
            //哈希加密
            byte[] by = mymd.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            //可变字符字符串
            System.Text.StringBuilder mysb = new System.Text.StringBuilder();
           
            for (int i = 0; i < by.Length; i++)
            {
                mysb.Append(by[i].ToString("x2"));

            }
            return mysb.ToString();

        }
       
        
        
        public static string Trim(string str)
        {
            //去掉前六个
            string  sr = str.Remove(0,6);
            Debug.WriteLine(sr);
            Debug.WriteLine(str);
            // string st = str.Substring(7, sr.Length);
            string stt = sr.Substring(0, 8);
            Debug.WriteLine(stt);
            return sr;
        }



        /// <summary>
        /// 256位AES解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt)
        {
            // 256-AES key    
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(Key);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }




        /// <summary>
        /// 生成唯一编码
        /// </summary>
        /// <returns></returns>
        public static string CreateId()
        {
            long i = 1;
            // Guid 表示全局唯一标识符 (GUID)
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {  // b 随机获取
                i *= ((int)b + 1); //累乘
            }
            // Format 用arg0的字符串表示形式替换任何格式项的格式的副本。
            //1毫秒==10,000个DateTime.Now.Ticks，最小时间刻度
            string str = string.Format("{0:x}", i - DateTime.Now.Ticks);
            Debug.WriteLine(str);

            //70ba17c8c688b4f9
            //3a135d3a32a0b235
            //70ba17c8c688b4f9
            //282047d945c779c

            byte[] buffer = Guid.NewGuid().ToByteArray();
            long cc = BitConverter.ToInt64(buffer, 0);
            Debug.WriteLine(cc);
            //5307910132303561341
            //5107798965464022293
            string num = Guid.NewGuid().ToString();
            Debug.WriteLine(num);
            Debug.WriteLine(Guid.NewGuid());

            return str;
          
        }
    }
}
