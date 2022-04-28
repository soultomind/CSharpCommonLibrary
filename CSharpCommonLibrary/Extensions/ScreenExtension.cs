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
        /// <see cref="System.Windows.Forms.Screen.Bounds"/> 영역안에 <paramref name="points"/> 좌표들중에 포함되어있는게 있는지 여부를 반환합니다.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static bool BoundsContains(this Screen screen, Point[] points)
        {
            foreach (Point point in points)
            {
                if (screen.Bounds.Contains(point))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// <see cref="System.Windows.Forms.Screen.WorkingArea"/> 영역안에 <paramref name="points"/> 좌표들중에 포함되어있는게 있는지 여부를 반환합니다.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        public static bool WorkingAreaContains(this Screen screen, Point[] points)
        {
            foreach (Point point in points)
            {
                if (screen.WorkingArea.Contains(point))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
