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
        /// <summary>
        /// 현재 날짜시간을 포멧형태로 반환합니다.
        /// <para>[기본 yyyy/MM/dd HH:mm:ss]</para>
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        /// <remarks>
        /// <para>시간을 나타내는 문자열이 hh=[0-11], HH=[0-23] </para>
        /// </remarks>
        public static string NowToString(string format = "yyyy/MM/dd HH:mm:ss")
        {
            return DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
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
        /// <paramref name="input"/> 문자열 값이 알파벳인지 여부
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsAlphabet(string input)
        {
            return Regex.IsMatch(input, "[A-Z]", RegexOptions.IgnoreCase);
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
