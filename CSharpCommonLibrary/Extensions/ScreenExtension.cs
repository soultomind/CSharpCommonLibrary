using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.Extensions
{
    public static class ScreenExtension
    {
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
    }
}
