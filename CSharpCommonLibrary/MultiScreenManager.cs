using CommonLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharpCommonLibrary
{
    public class MultiScreenManager
    {
        public const int InvalidScreenIndex = -1;
        /// <summary>
        /// 타겟이 될 <see cref="System.Windows.Forms.Screen"/> 인덱스
        /// </summary>
        public int TargetScreenIndex { get; set; }
        /// <summary>
        /// 타겟이 될 <see cref="System.Windows.Forms.Screen"/> 스크린 Bounds 사이즈
        /// </summary>
        public Size TargetScreenBoundsSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetScreenIndex"></param>
        /// <exception cref="System.InvalidOperationException"></exception>
        public MultiScreenManager(int targetScreenIndex)
        {
            if (ScreenUtility.ScreenLength == 1)
            {
                throw new InvalidOperationException("Screen.AllScreens.Length==1");
            }

            if (!ScreenUtility.IsValidIndex(targetScreenIndex))
            {
                throw new ArgumentException(nameof(targetScreenIndex));
            }

            TargetScreenIndex = targetScreenIndex;
            TargetScreenBoundsSize = Size.Empty;
        }

        public MultiScreenManager(Size targetScreenBoundsSize)
        {
            if (ScreenUtility.ScreenLength == 1)
            {
                throw new InvalidOperationException("Screen.AllScreens.Length==1");
            }

            if (!ScreenUtility.EqualsScreenBoundsSize(targetScreenBoundsSize))
            {
                throw new ArgumentException(nameof(targetScreenBoundsSize));
            }

            TargetScreenBoundsSize = targetScreenBoundsSize;
            TargetScreenIndex = InvalidScreenIndex;
        }

        
        public Screen TargetScreen
        {
            get
            {
                Screen screen = null;
                if (TargetScreenIndex == InvalidScreenIndex)
                {
                    screen = ScreenUtility.GetTargetScreen(TargetScreenBoundsSize);
                }
                else
                {
                    screen = ScreenUtility.GetTargetScreen(TargetScreenIndex);
                }
                return screen;
            }
        }

        public Screen GetContainsPointScreenBounds(Point point)
        {
            return ScreenUtility.ContainsPointScreenBounds(point);
        }

        public Point CalcScreenBoundsInCenterPoint(Screen screen, Size size)
        {
            int x = (screen.Bounds.Size.Width - size.Width) / 2;
            int y = (screen.Bounds.Size.Height - size.Height) / 2;
            return new Point(screen.Bounds.Location.X + x, screen.Bounds.Location.Y + y);
        }
    }
}
