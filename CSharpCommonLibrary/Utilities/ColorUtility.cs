using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Utilities
{
    public static class ColorUtility
    {
        public static Color ToColorRgb(int rgb)
        {
            return ToColorArgb(rgb);
        }
        public static Color ToColorArgb(int argb)
        {
            // argb가 0xRRGGBB (6자리)라면 A=255로 해석
            if ((argb >> 24) == 0)
            {
                int red = (argb >> 16) & 0xFF;
                int green = (argb >> 8) & 0xFF;
                int blue = argb & 0xFF;
                return Color.FromArgb(255, red, green, blue);
            }
            else // 8자리라면 A도 사용
            {
                int alpha = (argb >> 24) & 0xFF;
                int red = (argb >> 16) & 0xFF;
                int green = (argb >> 8) & 0xFF;
                int blue = argb & 0xFF;
                return Color.FromArgb(alpha, red, green, blue);
            }
        }

        public static int ToIntArgb(Color color)
        {
            int alpha = color.A;
            int red = color.R;
            int green = color.G;
            int blue = color.B;
            return blue + (green << 8) + (red << 16) + (alpha << 24);
        }

        public static Color ToColorFromHexString(string intValue)
        {
            if (!RegexUtility.IsHexStringColor(intValue))
            {
                throw new ArgumentException(nameof(intValue));
            }

            string input = intValue;
            input = input.TrimStart('#');

            if (input.Length != 6 && input.Length != 8)
            {
                throw new ArgumentException(nameof(intValue) + " is not valid length. (6 or 8)");
            }

            int argb = 0;
            Color color = Color.Empty;
            if (input.Length == 6)
            {
                argb = int.Parse(input, NumberStyles.HexNumber);
                color = ToColorRgb(argb);
            }
            else if (input.Length == 8)
            {
                argb = int.Parse(input, NumberStyles.HexNumber);
                color = ToColorArgb(argb);
            }

            return color;
        }
    }
}
