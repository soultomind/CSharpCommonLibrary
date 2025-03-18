using CommonLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
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

        public static AspectRatioF ToAspectRatio(Size size)
        {
            AspectRatioF aspectRatio = MathUtility.ToAspectRatio(size);
            return aspectRatio;
        }
    }
}
