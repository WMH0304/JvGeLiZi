using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPFUI_test.Test;
using System.IO;

namespace WPFUI_test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //当程序异常时返回异常
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);
            e.Cancel = true;
            MessageBox.Show("无法关闭");

        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //ControlTest controlTest = new ControlTest();
            MainWindow window = new MainWindow();
            if (e.Args.Length >0)
            {
                string str = e.Args[0];
                if (File.Exists(str))
                {
                    window.lodFile(str);
                }
            }
            window.Show();
        }
    }
}
