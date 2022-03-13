using System.Runtime.InteropServices;

namespace CommonLibrary.Win32
{
    /// <summary>
    /// 마우스 관련 좌표
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsEmpty()
        {
            return X == 0 & Y == 0;
        }
    }
}
