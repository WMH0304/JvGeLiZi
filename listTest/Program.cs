using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
namespace ListTest
{
   
    class Program
    {

        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            Stopwatch stopwatch = new Stopwatch(); 
            
            List<int> tests = new List<int>();
            List<int> tests1;
            stopwatch.Start();
            for (int i = 0; i < 100000; i++)
            {

                tests.Add(i);
                
               
            }
            stopwatch.Stop();
            stopwatch.Start();
            for (int j = 0; j < 100000; j++)
            {

                tests.Add(j);
                tests1 = tests;

            }
            stopwatch.Stop();
            List<int> a = new List<int>();
            a = tests;

          
            TimeSpan time = stopwatch.Elapsed;

            Console.WriteLine(time);

           
            List<int> b;
            b = a;
            
            TimeSpan timeSpan = stopwatch.Elapsed;
            Console.WriteLine(timeSpan);

            Console.ReadLine();
          
        }


    }
}
