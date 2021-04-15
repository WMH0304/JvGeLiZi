using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFUI_test.Test;

namespace WPFUI_test
{
    class Startup 
    {
        [STAThread()]//单线程标注
        static void Main()
        {

            //Application app = new Application();
            //MainWindow win = new MainWindow();
            //ControlTest cont = new ControlTest();
            //app.Run(cont);
            wpfapp wpfapp;
            wpfapp = new wpfapp();
            wpfapp.Run();



        }


        
    }
}
