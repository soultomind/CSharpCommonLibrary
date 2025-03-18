using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Utilities
{
    public static class RegistryUtility
    {
        public static string GetLocalMachineBaseKey()
        {
            return GetLocalMachineBaseKey(
                Environment.Is64BitOperatingSystem,
                Environment.Is64BitProcess);
        }

        public static string GetLocalMachineBaseKey(bool is64BitOperatingSystem, bool is64BitProcess)
        {
            if (is64BitOperatingSystem)
            {
                if (is64BitProcess)
                {
                    return @"SOFTWARE";
                }
                else
                {
                    return @"SOFTWARE\WOW6432Node";
                }
            }
            else
            {
                return @"SOFTWARE";
            }
        }
    }
}
