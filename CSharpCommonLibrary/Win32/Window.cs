using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Win32
{
    public class WindowNative
    {
        #region Static

        /// <summary>
        /// <paramref name="processId"/>에 해당하는 프로세스의 Window 핸들을 <see cref="System.IntPtr"/> 배열로 반환합니다.
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static IntPtr[] GetProcessWindowHandles(int processId)
        {
            IntPtr[] windowHandles = (new IntPtr[256]);
            int count = 0;
            IntPtr nextWindowHandle = IntPtr.Zero;
            do
            {
                nextWindowHandle = User32.FindWindowEx(IntPtr.Zero, nextWindowHandle, null, null);
                int outProcessId;
                User32.GetWindowThreadProcessId(nextWindowHandle, out outProcessId);
                if (outProcessId == processId)
                {
                    windowHandles[count++] = nextWindowHandle;
                }
            } while (nextWindowHandle != IntPtr.Zero);

            System.Array.Resize(ref windowHandles, count);
            return windowHandles;
        }

        /// <summary>
        /// <paramref name="hWnd"/>의 Text 값을 반환합니다.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static string GetWindowText(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = User32.GetWindowTextLength(hWnd);

            // length * 4 한글일때
            StringBuilder builder = new StringBuilder(length * 4);
            User32.GetWindowText(hWnd, builder, builder.Capacity);
            return builder.ToString();
        }

        #endregion
    }
}
