using CommonLibrary.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Utilities
{
    public class MathUtility
    {
        public static T Min<T>(params T[] nums)
        {
            List<T> list = new List<T>(nums);
            list.Sort();
            return list[0];
        }

        public static T Max<T>(params T[] nums)
        {
            List<T> list = new List<T>(nums);
            list.Sort();
            list.Reverse();
            return list[0];
        }

        public static int Sum(int n1, int n2)
        {
            int min = Math.Min(n1, n2);
            int max = Math.Max(n1, n2);

            int sum = 0;
            for (int i = min; i <= max; i++)
            {
                sum += i;
            }
            return sum;
        }

        public static int SumOfFromStart1ToEnd(int n)
        {
            if (n < 1)
            {
                throw new ArgumentException(nameof(n) + " 값은 1 이상 입니다.");
            }

            // Gauss(가우스 덧셈)
            return ((1 + n) * (n / 2)) + 
                // 홀수 일때는 마지막 중간에 위치하는 수를 더해줘야 한다
                // 1+2+3+4+5 일때 3 더하는 처리 6 * 2 + 3 = 15;
                (n % 2 == 0 ? 0 : (n + 1) / 2);
        }
    }
}
