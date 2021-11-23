using CommonLibrary.Win32;
using System;

namespace CommonLibrary
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Toolkit.TraceWriteLine("Start");

            MouseOperationManager mouseOperationManager = 
                new MouseOperationManager() { MouseMovePreventInterval = 100 };
            mouseOperationManager.MouseMovePreventStart();

            Console.ReadLine();

            Toolkit.TraceWriteLine("End");
        }
    }
}
