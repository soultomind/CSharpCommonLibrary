using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonLibrary.Utilities
{
    public class ColorUtility
    {
        public static Color FromArgb(int argb)
        {
            int alapha = (argb >> 24) & 0xFF;
            int red = (argb >> 16) & 0xFF;
            int green = (argb >> 8) & 0xFF;
            int blue = argb & 0xFF;
            return Color.FromArgb(alapha, red, green, blue); ;
        }


        public static bool IsValidCssStyleSharpRgba(string styleSharpRgba)
        {
            Regex regex = new Regex("#[0-9A-Fa-f]{8}");
            return regex.IsMatch(styleSharpRgba);
        }

        public static int ToCssStyleRgbaNumber(string styleRgbaNumber)
        {
            if (styleRgbaNumber == null)
            {
                throw new ArgumentNullException("styleRgbaNumber");
            }

            if (styleRgbaNumber.Length == 0)
            {
                throw new ArgumentException("styleRgbaNumber.Length is zero");
            }

            if (!styleRgbaNumber.StartsWith("rgba("))
            {
                throw new ArgumentException("styleRgbaNumber is invalid value, usage is rgba(255,0,0,0)");
            }

            if (!styleRgbaNumber.EndsWith(")"))
            {
                throw new ArgumentException("styleRgbaNumber is invalid value, usage is rgba(255,0,0,0)");
            }

            styleRgbaNumber = styleRgbaNumber.Substring(5, styleRgbaNumber.Length - 6);
            string[] arrRgba = styleRgbaNumber.Split(',');
            if (arrRgba.Length != 4)
            {
                throw new ArgumentException("argb is invalid value, usage is rgba(255,0,0,0)");
            }

            int red = int.Parse(arrRgba[0]);
            int green = int.Parse(arrRgba[1]);
            int blue = int.Parse(arrRgba[2]);
            int alpha = int.Parse(arrRgba[3]);
            alpha = alpha << 24;
            red = red << 16;
            green = green << 8;
            return alpha + red + green + blue;
        }
    }
}
