using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CommonLibrary.Win32
{
    /// <summary>
    /// Gdi32.dll Native 메소드
    /// </summary>
    public class Gdi32
    {
        public const string FileName = "gdi32.dll";

        public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter

        [DllImport(FileName)]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest,
            int nWidth, int nHeight, IntPtr hObjectSource,
            int nXSrc, int nYSrc, int dwRop);

        [DllImport(FileName)]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth,
            int nHeight);

        [DllImport(FileName)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport(FileName)]
        public static extern bool DeleteDC(IntPtr hDC);

        [DllImport(FileName)]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport(FileName)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
    }
}
