using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CSharpCommonLibrary.Utilities
{
    public class StringUtility
    {
        public string ToNewGuid(bool exceptHypen = false)
        {
            string newGuid = Guid.NewGuid().ToString();
            if (exceptHypen)
            {
                newGuid = newGuid.Replace("-", "");
            }
            return newGuid;
        }

        /// <summary>
        /// 유효한 #[00FF0000] 형태의 CSS 
        /// </summary>
        /// <param name="styleSharpRgba"></param>
        /// <returns></returns>
        public static bool IsValidCssSharpRgba(string styleSharpRgba)
        {
            // #[[:xdigit:]{8}] 테스트해보기
            Regex regex = new Regex("#[0-9A-Fa-f]{8}");
            return regex.IsMatch(styleSharpRgba);
        }
    }
}
