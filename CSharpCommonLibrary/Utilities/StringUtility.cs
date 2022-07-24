using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLibrary.Utilities
{
    /// <summary>
    /// <see cref="System.String"/> 관련 유틸리티 클래스
    /// </summary>
    public class StringUtility
    {
        private static void CheckedInput(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (String.IsNullOrEmpty(input))
            {
                throw new ArgumentException(nameof(input));
            }
        }

        /// <summary>
        /// <paramref name="input"/>값의 첫번째 문자를 대문자로 변환하여 반환합니다.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Capitalize(string input)
        {
            CheckedInput(input);
            return Char.ToUpper(input.ToCharArray()[0]) + input.Substring(1);
        }

        /// <summary>
        /// <see cref="String.Format(string, object[])"/>에 사용되는 <paramref name="format"/> 에 {0} ... {9} 문자열이 유효한지 여부를 반환합니다.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsValidStringFormat(string format, int min = 1, int max = 9)
        {
            if (String.IsNullOrEmpty(format))
            {
                return false;
            }

            if (min > max)
            {
                throw new ArgumentException("min > max");
            }

            string condition = "{[0-9]}";
            string patternRange = String.Format("{0},{1}", min, max);
            string pattern = condition + "{" + patternRange + "}";

            if (Regex.IsMatch(format, pattern))
            {
                MatchCollection matchCollection = Regex.Matches(format, pattern);
                int Count = matchCollection.Count;

                HashSet<int> numberSet = new HashSet<int>();
                foreach (Match match in matchCollection)
                {
                    int index = match.Index;
                    string value = match.Value;

                    int number = int.Parse(value.Substring(1, 1));
                    if (!numberSet.Add(number))
                    {
                        throw new ArgumentException(nameof(format) + " is !numberSet.Add(number)");
                    }

                    if (number > Count)
                    {
                        throw new ArgumentException(nameof(format) + " is number > Count");
                    }
                }
                return true;
            }

            return false;
        }


        /// <summary>
        /// 현재 날짜시간을 포멧형태로 반환합니다.
        /// <para>[기본 yyyy/MM/dd HH:mm:ss]</para>
        /// <para>시간을 나타내는 문자열이 hh=[0-11], HH=[0-23] </para>
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string NowToString(string format = "yyyy/MM/dd HH:mm:ss")
        {
            return DateTime.Now.ToString(format);
        }

        /// <summary>
        /// 새로운 Guid 문자열을 반환합니다.
        /// </summary>
        /// <param name="exceptHypen">- 문자 제외 여부</param>
        /// <returns></returns>
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
        /// <paramref name="input"/> 값이 #FF(R)FF(G)FF(B) 형태의 컬러값인지 여부를 반환합니다.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumberSignRgbColor(string input)
        {
            return IsNumberSignColor(false, input);
        }

        /// <summary>
        /// <paramref name="input"/> 값이 #FF(A)FF(R)FF(G)FF(B) 형태의 컬러값인지 여부를 반환합니다.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumberSignARgbColor(string input)
        {
            return IsNumberSignColor(true, input);
        }

        /// <summary>
        /// #FF(R)FF(G)FF(B),#FF(A)FF(R)FF(G)FF(B) 형태의 컬러값인지 여부를 반환합니다.
        /// </summary>
        /// <param name="isArgbColor">Argb 형태의 #FF(A)FF(R)FF(G)FF(B) 값인지 여부</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNumberSignColor(bool isArgbColor, string input)
        {
            CheckedInput(input);

            string pattern = String.Empty;
            // (\\s|%) = 스페이스나 문자열 끝
            if (isArgbColor)
            {
                pattern = "#[0-9A-Fa-f]{8}($)";
            }
            else
            {
                pattern = "#[0-9A-Fa-f]{6}($)";
            }
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// <paramref name="input"/> 문자열 값이 알파벳인지 여부
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsAlphabet(string input)
        {
            CheckedInput(input);

            string condition = "[A-Za-z]";
            string pattern = condition + "{" + input.Length + "}($)";
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// <paramref name="ch"/> 문자 값이 알파벳인지 여부
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static bool IsAlphabet(char ch)
        {
            return IsAlphabet(new String(new char[] { ch }));
        }

        /// <summary>
        /// <paramref name="ch"/> 문자 값이 소문자인지 여부
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
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

        /// <summary>
        /// <paramref name="ch"/> 문자 값이 대문자인지 여부
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
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
