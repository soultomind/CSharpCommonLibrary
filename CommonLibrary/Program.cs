using CommonLibrary.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            MouseOperationManager mouseOperationManager = 
                new MouseOperationManager() { MouseMovePreventInterval = 100 };
            mouseOperationManager.MouseMovePreventStart();

            Console.ReadLine();
        }
    }
}
