using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace CommonLibrary.Win32
{
    /// <summary>
    /// 윈도우즈 Native 프로그램 창 크기
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public static RECT Empty = new RECT(0, 0, 0, 0);
        public int Left, Top, Right, Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(Rectangle rect) : this(rect.Left, rect.Top, rect.Right, rect.Bottom)
        {

        }

        public int X
        {
            get
            {
                return Left;
            }
            set
            {
                Right -= (Left - value);
                Left = value;
            }
        }

        public int Y
        {
            get
            {
                return Top;
            }
            set
            {
                Bottom -= (Top - value);
                Top = value;
            }
        }

        public int Height
        {
            get
            {
                return Bottom - Top;
            }
            set
            {
                Bottom = value + Top;
            }
        }

        public int Width
        {
            get
            {
                return Right - Left;
            }
            set
            {
                Right = value + Left;
            }
        }

        public Point Location
        {
            get
            {
                return new Point(Left, Top);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Size Size
        {
            get
            {
                return new Size(Width, Height);
            }
            set
            {
                Width = value.Width;
                Height = value.Height;
            }
        }

        public static implicit operator Rectangle(RECT rect)
        {
            return new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static implicit operator RECT(Rectangle rect)
        {
            return new RECT(rect);
        }

        public static bool operator ==(RECT r1, RECT r2)
        {
            return r1.Equals(r2);
        }

        public static bool operator !=(RECT r1, RECT r2)
        {
            return !r1.Equals(r2);
        }

        public bool Equals(RECT r)
        {
            return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
        }

        public override bool Equals(object obj)
        {
            if (obj is RECT)
            {
                return Equals((RECT)obj);
            }
            else if (obj is Rectangle)
            {
                return Equals(new RECT((Rectangle)obj));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ((Rectangle)this).GetHashCode();
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(Left, Top, Right - Left, Bottom - Top);
        }

        public POINT[] ToNativePoints()
        {
            POINT[] points = new POINT[4];
            points[0] = LeftTop;
            points[1] = RightTop;
            points[2] = LeftBottom;
            points[3] = RightBottom;

            return points;
        }

        public Point[] ToPoints()
        {
            Point[] points = new Point[4];
            points[0] = LeftTop;
            points[1] = RightTop;
            points[2] = LeftBottom;
            points[3] = RightBottom;

            return points;
        }

        public POINT LeftTop
        {
            get { return new POINT(X, Y); }
        }

        public POINT RightTop
        {
            get { return new POINT(X + Width, Y); }
        }

        public POINT LeftBottom
        {
            get { return new POINT(X, Y + Height); }
        }

        public POINT RightBottom
        {
            get { return new POINT(X + Width, Y + Height); }
        }

        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "X={0}, Y={1}, Width={2}, Height={3}", X, Y, Width, Height);
        }
    }
}
