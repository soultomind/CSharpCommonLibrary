using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

        public static bool IsValidCssSharpRgba(string styleSharpRgba)
        {
            // #[[:xdigit:]{8}] 테스트해보기
            Regex regex = new Regex("#[0-9A-Fa-f]{8}");
            return regex.IsMatch(styleSharpRgba);
        }
    }
}
