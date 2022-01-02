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
        /// <summary>
        /// 정수로부터 <see cref="System.Drawing.Color"/> 가져온다.
        /// </summary>
        /// <param name="argb"></param>
        /// <returns></returns>
        public static Color FromArgb(int argb)
        {
            int alapha = (argb >> 24) & 0xFF;
            int red = (argb >> 16) & 0xFF;
            int green = (argb >> 8) & 0xFF;
            int blue = argb & 0xFF;
            return Color.FromArgb(alapha, red, green, blue); ;
        }
    }
}
