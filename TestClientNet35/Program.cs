using CommonLibrary;
using CommonLibrary.Utilities;
using System;
using System.Windows.Forms;
using TestNet32;

namespace TestNet35
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Toolkit.TraceWriteLine("Start");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestForm());

            Toolkit.TraceWriteLine("End");
            Console.ReadLine();
        }
    }
}
