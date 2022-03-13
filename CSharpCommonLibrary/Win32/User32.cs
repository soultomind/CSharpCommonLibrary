using CommonLibrary.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CommonLibrary.Win32
{
    /// <summary>
    /// User32.dll Native 메소드
    /// </summary>
    public class User32
    {
        public const string FileName = "User32.dll";

        [DllImport(FileName)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);


        [DllImport(FileName)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport(FileName)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);


        [DllImport(FileName)]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        [DllImport(FileName)]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);



        [DllImport(FileName)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport(FileName)]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);



        [DllImport(FileName)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
    }
}
