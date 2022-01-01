using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CommonLibrary.Win32
{
    public class User32
    {
        public const string FileName = "User32.dll";

        #region Window
        [DllImport(FileName)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport(FileName)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        #endregion

        [DllImport(FileName)]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport(FileName)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        
    }
}
