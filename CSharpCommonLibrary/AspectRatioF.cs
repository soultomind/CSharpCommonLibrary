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
    public class AspectRatioF
    {
        public float Width { get; internal set; }
        public float Height { get; internal set; }
        private AspectRatioF() { Width = Height = 0; }
        internal AspectRatioF(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public static AspectRatioF ToAspectRatio(Size size)
        {
            AspectRatioF aspectRatio = MathUtility.ToAspectRatioF(size);
            return aspectRatio;
        }
    }
}
