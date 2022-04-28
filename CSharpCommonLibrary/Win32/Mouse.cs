using System;
using System.Runtime.InteropServices;

namespace CommonLibrary.Win32
{
    #region 마우스 프로세스

    /// <summary>
    /// 마우스 포인트 구조체
    /// </summary>
    public struct MousePoint
    {
        internal static MousePoint Empty = new MousePoint(0, 0);

        public int X;
        public int Y;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 좌표가 비어있는지 여부(X=0, Y=0)
        /// </summary>
        public bool IsEmpty
        {
            get { return X == 0 && Y == 0; }
        }
    }

    /// <summary>
    /// 마우스 이벤트 플래그
    /// </summary>
    [Flags]
    public enum MouseEventFlags : uint
    {
        LEFTDOWN = 0x00000002,
        LEFTUP = 0x00000004,
        MIDDLEDOWN = 0x00000020,
        MIDDLEUP = 0x00000040,
        MOVE = 0x00000001,
        ABSOLUTE = 0x00008000,
        RIGHTDOWN = 0x00000008,
        RIGHTUP = 0x00000010,
        WHEEL = 0x00000800,
        XDOWN = 0x00000080,
        XUP = 0x00000100
    }

    //Use the values of this enum for the 'dwData' parameter
    //to specify an X button when using MouseEventFlags.XDOWN or
    //MouseEventFlags.XUP for the dwFlags parameter.
    public enum MouseEventDataXButtons : uint
    {
        XBUTTON1 = 0x00000001,
        XBUTTON2 = 0x00000002
    }

    /// <summary>
    /// 마우스 Win32 관련 API 함수
    /// </summary>
    public class MouseNativeMethods
    {
        private const string DllName = "User32.dll";

        [DllImport(DllName, EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport(DllName, EntryPoint = "GetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out MousePoint outMousePoint);

        [DllImport(DllName, EntryPoint = "mouse_event")]
        public static extern void MouseEvent(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
    }

    /// <summary>
    /// 마우스 Win32 
    /// </summary>
    public class MouseNative
    {
        /// <summary>
        /// 마우스 좌표를 설정합니다.
        /// <para>좌표를 설정후에 <see cref="MouseEvent(MouseEventFlags)"/> 메서드를 호출하여야 합니다.</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetCursorPoint(int x, int y)
        {
            MouseNativeMethods.SetCursorPos(x, y);
        }

        /// <summary>
        /// 마우스 좌표를 설정합니다.
        /// <para>좌표를 설정후에 <see cref="MouseEvent(MouseEventFlags)"/> 메서드를 호출하여야 합니다.</para>
        /// </summary>
        /// <param name="mousePoint"></param>
        public void SetCursorPoint(MousePoint mousePoint)
        {
            SetCursorPoint(mousePoint.X, mousePoint.Y);
        }

        /// <summary>
        /// 마우스 좌표를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public static MousePoint GetCursorPoint()
        {
            MousePoint currentMousePoint = MousePoint.Empty;
            var result = MouseNativeMethods.GetCursorPos(out currentMousePoint);
            if (!result)
            {
                currentMousePoint = new MousePoint(0, 0);
            }
            return currentMousePoint;
        }

        /// <summary>
        /// 마우스 이벤트를 설정합니다.
        /// </summary>
        /// <param name="dwFlags"></param>
        public void MouseEvent(MouseEventFlags dwFlags, int dwData = 0, int dwExtraInfo = 0)
        {
            MousePoint point = GetCursorPoint();
            MouseNativeMethods.MouseEvent((int)dwFlags, point.X, point.Y, dwData, dwExtraInfo);
        }
    }

    #endregion
}
