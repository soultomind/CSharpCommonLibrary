using System.Drawing;
using System.Runtime.InteropServices;

namespace CommonLibrary.Win32
{
    /// <summary>
    /// 윈도우즈 Native 포인터 좌표
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator Point(POINT point)
        {
            return new Point(point.X, point.Y);
        }

        public static implicit operator POINT(Point point)
        {
            return new POINT(point.X, point.Y);
        }
    }
}
