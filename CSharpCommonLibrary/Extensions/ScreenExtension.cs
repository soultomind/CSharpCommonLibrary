using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.Extensions
{
    /// <summary>
    /// <see cref="System.Windows.Forms.Screen"/> 확장 클래스
    /// </summary>
    public static class ScreenExtension
    {
        /// <summary>
        /// 스크린에 화면 비율값을 가져옵니다.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static AspectRatio ToAspectRatio(this Screen @this)
        {
            return AspectRatio.ToAspectRatio(@this.Bounds.Size);
        }

        /// <summary>
        /// 해당 스크린에서 작업표시줄을 사용하고 있는지 여부를 나타냅니다.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool UseTaskBar(this Screen @this)
        {
            if ((@this.WorkingArea.Width == @this.Bounds.Width) && (@this.WorkingArea.Height == @this.Bounds.Height))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// <paramref name="this"/>값의 스크린 왼쪽에 스크린이 존재하는지 여부를 나타냅니다.
        /// <para>해당 기능은 스크린상의 좌표값을 통하여 판단합니다.</para>
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsValidLeftNextScreen(this Screen @this)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (@this.Equals(screen))
                {
                    continue;
                }

                if (@this.Bounds.Left > screen.Bounds.Left)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// <paramref name="this"/>값의 스크린 오른쪽에 스크린이 존재하는지 여부를 나타냅니다.
        /// <para>해당 기능은 스크린상의 좌표값을 통하여 판단합니다.</para>
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsValidRightNextScreen(this Screen @this)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (@this.Equals(screen))
                {
                    continue;
                }

                if (@this.Bounds.Left < screen.Bounds.Left)
                {
                    return true;
                }
            }

            return false;
        }

        public static Compare CompareWidth(this Screen @this, Screen other)
        {
            if (other.Bounds.Width > @this.Bounds.Width)
            {
                return Compare.GreaterThan;
            }
            else if (other.Bounds.Width < @this.Bounds.Width)
            {
                return Compare.LessThan;
            }
            else
            {
                return Compare.EqualTo;
            }
        }

        public static Compare CompareHeight(this Screen @this, Screen other)
        {
            if (other.Bounds.Height > @this.Bounds.Height)
            {
                return Compare.GreaterThan;
            }
            else if (other.Bounds.Height < @this.Bounds.Height)
            {
                return Compare.LessThan;
            }
            else
            {
                return Compare.EqualTo;
            }
        }

        /// <summary>
        /// <see cref="System.Windows.Forms.Screen.Bounds"/> 영역안에 <paramref name="points"/> 좌표들중에 포함되어있는게 있는지 여부를 반환합니다.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static bool BoundsContains(this Screen @this, Point[] points)
        {
            foreach (Point point in points)
            {
                if (@this.Bounds.Contains(point))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// <see cref="System.Windows.Forms.Screen.WorkingArea"/> 영역안에 <paramref name="points"/> 좌표들중에 포함되어있는게 있는지 여부를 반환합니다.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static bool WorkingAreaContains(this Screen @this, Point[] points)
        {
            foreach (Point point in points)
            {
                if (@this.WorkingArea.Contains(point))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
