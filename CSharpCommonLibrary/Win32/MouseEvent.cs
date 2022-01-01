using System;
using System.Runtime.InteropServices;

namespace CommonLibrary.Win32
{
    public class MouseEvent
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll", EntryPoint = "mouse_event")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var cursorPos = GetCursorPos(out currentMousePoint);
            if (!cursorPos)
            {
                currentMousePoint = new MousePoint(0, 0);
            }
            return currentMousePoint;
        }

        public void SetMouseEvent(MouseEventFlags value, int dwData = 0, int dwExtraInfo = 0)
        {
            MousePoint point = GetCursorPosition();
            mouse_event((int)value, point.X, point.Y, dwData, dwExtraInfo);

        }
    }
}
