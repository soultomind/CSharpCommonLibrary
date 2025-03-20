using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonLibrary.Utilities
{
    public class StringUtility
    {
        /// <summary>
        /// <paramref name="input"/>값의 첫번째 문자를 대문자로 변환하여 반환합니다.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Capitalize(string input)
        {
            return Char.ToUpper(input.ToCharArray()[0]) + input.Substring(1);
        }

        /// <summary>
        /// <paramref name="input"/>값이 알파벳 문자열인지 반환합니다.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsAlphabet(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException();
            }

            if (input.Length == 0)
            {
                throw new ArgumentException(nameof(input) + ".Length == 0");
            }

            return Regex.IsMatch(input.ToString(), "[A-Z]", RegexOptions.IgnoreCase);
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
