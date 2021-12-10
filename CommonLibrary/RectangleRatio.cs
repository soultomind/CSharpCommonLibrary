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
        public int WidthRatio { get; set; }
        public int HeightRatio { get; set; }
        private RectangleRatio() { WidthRatio = HeightRatio = 0; }
        private RectangleRatio(int widthRatio, int heightRatio)
        {
            WidthRatio = widthRatio;
            HeightRatio = heightRatio;
        }

        public static RectangleRatio ToRectangleRatio(int width, int height)
        {
            if (!(width >= 0 && height >= 0))
            {
                throw new ArgumentException("");
            }

            int max = 0, min = 0, temp = 0, gcd = 0;
            if (width < height)
            {
                max = height;
                min = width;
            }
            else
            {
                max = width;
                min = height;
            }

            while ((max % min) != 0)
            {
                temp = max % min;
                max = min;
                min = temp;
            }

            gcd = min;

            int widthRatio = width / gcd;
            int heightRatio = height / gcd;
            return new RectangleRatio(widthRatio, heightRatio);
        }
    }
}
