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
        public const string DllName = "User32.dll";

        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        [DllImport(DllName)]
        public static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, int lParam);


        [DllImport(DllName)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport(DllName)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);


        [DllImport(DllName)]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, out RECT rect);
        [DllImport(DllName)]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);



        [DllImport(DllName)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport(DllName)]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport(DllName)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr windowHandle, IntPtr windowHandleInsertAfter, int x, int y, int width, int height, SetWindowPos swp);


        [DllImport(DllName)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WindowPlacement lpwndpl);


        [DllImport(DllName)]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr window, out int processId);




        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Auto)]

        public static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

        [DllImport(DllName, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport(DllName)]
        public static extern int IsWindowVisible(IntPtr hWnd);
    }
}
