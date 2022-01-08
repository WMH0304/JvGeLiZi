using System;

namespace 平台互操作性和不安全的代码
{

    #region 启动类
    

    class Program
    {
        static void Main(string[] args)
        {
            unsafe
            {
                var t = 7;
                var tt = 14;
                int* p = &t;
                int* q =&(tt);
                System.Diagnostics.Debug.WriteLine(p->ToString());
                System.Diagnostics.Debug.WriteLine(p->GetType().Name);
                System.Diagnostics.Debug.WriteLine(p->GetType());
                System.Diagnostics.Debug.WriteLine((int)p);

            }
        }

    
        
    }


    #endregion

}
