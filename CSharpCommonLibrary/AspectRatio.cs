using CommonLibrary.Utilities;
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
    public class AspectRatio
    {
        public int Width { get; internal set; }
        public int Height { get; internal set; }
        private AspectRatio() { Width = Height = 0; }
        internal AspectRatio(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static AspectRatio ToAspectRatio(Size size)
        {
            AspectRatio aspectRatio = MathUtility.ToAspectRatio(size);
            return aspectRatio;
        }
    }
}
