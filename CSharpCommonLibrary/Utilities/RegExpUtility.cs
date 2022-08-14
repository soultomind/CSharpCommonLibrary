using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonLibrary.Utilities
{
    public class RegExpUtility
    {

        public static readonly char StartChar = '^';
        public static readonly char EndChar = '$';

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
        /// # 문자로 시작하는 컬러 문자열이 유효한지를 반환합니다.
        /// <para>#FF(R)FF(G)FF(B)=#FFFFFF</para>
        /// <para>#FF(A)FF(R)FF(G)FF(B)=#FF998877</para>
        /// </summary>
        /// <param name="input">#시작하는 컬러 문자열값</param>
        /// <param name="isArgbColor">알파값(투명도) 포함 여부</param>
        /// <returns></returns>
        public static bool IsNumberSignColor(string input, bool isArgbColor)
        {
            CheckedInput(input);

            string pattern = String.Empty;

            if (isArgbColor)
            {
                pattern = StartChar + "#[0-9A-Fa-f]{8}" + EndChar;
            }
            else
            {
                pattern = StartChar + "#[0-9A-Fa-f]{6}" + EndChar;
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
            string pattern = condition + "{" + input.Length + "}" + EndChar;
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// <see cref="String.Format(string, object[])"/>에 사용되는 format 값에 {0} ... {9} 문자열이 유효한지 여부를 반환합니다.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool IsValidStringFormat(string input, int min = 1, int max = 9)
        {
            CheckedInput(input);

            if (min > max)
            {
                throw new ArgumentException("min > max");
            }

            string condition = "{[0-9]}";
            string patternRange = String.Format("{0},{1}", min, max);
            string pattern = condition + "{" + patternRange + "}";

            if (Regex.IsMatch(input, pattern))
            {
                MatchCollection matchCollection = Regex.Matches(input, pattern);
                int Count = matchCollection.Count;

                HashSet<int> numberSet = new HashSet<int>();
                foreach (Match match in matchCollection)
                {
                    int index = match.Index;
                    string value = match.Value;

                    int number = int.Parse(value.Substring(1, 1));
                    if (!numberSet.Add(number))
                    {
                        throw new ArgumentException(nameof(input) + " is !NumberSet.Add(number)");
                    }

                    if (number > Count)
                    {
                        throw new ArgumentException(nameof(input) + " is number > count");
                    }
                }
                return true;
            }

            return false;
        }
    }
}
