using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommonLibrary.Utilities
{
    public class ProcessUtility
    {
        public static bool Running(string processName)
        {
            Process[] processList = Process.GetProcessesByName(processName);
            if (processList.Length > 0)
            {
                return true;
            }
            return false;
        }

        public static bool Running(string processName, out int outProcessCount)
        {
            Process[] processList = Process.GetProcessesByName(processName);
            outProcessCount = processList.Length;

            if (processList.Length > 0)
            {
                return true;
            }
            return false;
        }
    }
}
