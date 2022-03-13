using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 사각형 비율
    /// </summary>
    public class RectangleRatio
    {
        public int WidthRatio { get; internal set; }
        public int HeightRatio { get; internal set; }
        private RectangleRatio() { WidthRatio = HeightRatio = 0; }
        internal RectangleRatio(int widthRatio, int heightRatio)
        {
            WidthRatio = widthRatio;
            HeightRatio = heightRatio;
        }
    }
}
