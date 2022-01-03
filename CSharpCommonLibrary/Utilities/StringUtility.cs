using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLibrary.Utilities
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

        public static bool IsAlphabet(string input)
        {
            return Regex.IsMatch(input, "[A-Z]", RegexOptions.IgnoreCase);
        }

        public static bool IsAlphabet(char ch)
        {
            return IsAlphabet(new String(new char[] { ch }));
        }

        public static bool IsLowerChar(char ch)
        {
            if (IsAlphabet(ch))
            {
                if ('a' <= ch && 'z' >= ch) // Lower
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsUpperChar(char ch)
        {
            if (IsAlphabet(ch))
            {
                if ('A' <= ch && 'Z' >= ch) // Upper
                {
                    return true;
                }
            }

            return false;
        }
    }
}
