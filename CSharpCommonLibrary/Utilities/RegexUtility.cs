using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonLibrary.Utilities
{
    public static class RegexUtility
    {
        private static readonly char START_CHAR = '^';
        private static readonly char END_CHAR = '$';

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
        /// <param name="bArgbColor">알파값(투명도) 포함 여부</param>
        /// <returns></returns>
        public static bool IsHexStringColor(string input)
        {
            CheckedInput(input);

            string pattern = String.Empty;

            if (input.Length == 9)
            {
                pattern = START_CHAR + "#[0-9A-Fa-f]{8}" + END_CHAR;
            }
            else if (input.Length == 7)
            {
                pattern = START_CHAR + "#[0-9A-Fa-f]{6}" + END_CHAR;
            }
            else
            {
                throw new ArgumentException(nameof(input) + " is not valid length. (7 or 9)");
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
            string pattern = condition + "{" + input.Length + "}" + END_CHAR;
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// <see cref="String.Format(string, object[])"/>에 사용되는 <paramref name="format"/> 에 {0} ... {9} 문자열이 포함되는지 여부를 반환합니다.
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
                throw new ArgumentException(nameof(min));
            }

            string condition = "{[0-9]}";
            string patternRange = String.Format("{0},{1}", min, max);
            string pattern = condition + "{" + patternRange + "}";

            if (Regex.IsMatch(format, pattern))
            {
                MatchCollection matchCollection = Regex.Matches(format, pattern);
                int Count = matchCollection.Count;

                ISet<int> numberSet = new HashSet<int>();
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
        /// <paramref name="email"/> 값이 유효한 이메일인지 여부를 반환한다.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            // 시작하세요 C# 프로그래밍 [정성태님 정규식 참고]
            string pattern = @"^([0-9A-Za-z]+)@([0-9A-Za-z]+)(\.[0-9A-Za-z]+){1,}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        /// <summary>
        /// <paramref name="input"/> 문자열 값이 한글(완성형, 자모 포함)인지 여부를 반환합니다.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsKorean(string input)
        {
            CheckedInput(input);
            // 한글 완성형(가-힣), 자모(ㄱ-ㅎ, ㅏ-ㅣ) 모두 포함
            string pattern = START_CHAR + "[가-힣ㄱ-ㅎㅏ-ㅣ]+" + END_CHAR;
            return Regex.IsMatch(input, pattern);
        }
    }
}
