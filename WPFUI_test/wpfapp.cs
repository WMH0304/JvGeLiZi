using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFUI_test
{
    class wpfapp :System.Windows.Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        public void showWindow()
        {
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
