using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Utilities
{
    public static class ColorUtility
    {
        public static Color ToColorArgb(int argb)
        {
            int alpha = (argb >> 24) & 0xFF;
            int red = (argb >> 16) & 0xFF;
            int green = (argb >> 8) & 0xFF;
            int blue = argb & 0xFF;
            return Color.FromArgb(alpha, red, green, blue);
        }

        public static Color ToColorRgb(int rgb)
        {
            int red = (rgb >> 16) & 0xFF;
            int green = (rgb >> 8) & 0xFF;
            int blue = rgb & 0xFF;
            return Color.FromArgb(red, green, blue);
        }

        public static int ToIntArgb(Color color)
        {
            int alpha = color.A;
            int red = color.R;
            int green = color.G;
            int blue = color.B;
            return blue + (green << 8) + (red << 16) + (alpha << 24);
        }

        public static int ToIntRgb(Color color)
        {
            int red = color.R;
            int green = color.G;
            int blue = color.B;
            return blue + (green << 8) + (red << 16);
        }
    }
}
