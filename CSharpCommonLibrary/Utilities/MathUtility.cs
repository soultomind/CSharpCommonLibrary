﻿using CommonLibrary.Net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CommonLibrary.Utilities
{
    /// <summary>
    /// 수학 관련 유틸리티 클래스
    /// </summary>
    public class MathUtility
    {
        /// <summary>
        /// 인자로 넘어온 정수에서 최소값을 구해 반환합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static T Min<T>(params T[] nums)
        {
            List<T> list = new List<T>(nums);
            list.Sort();
            return list[0];
        }

        /// <summary>
        /// 인자로 넘어온 정수에서 최대값을 구해 반환합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static T Max<T>(params T[] nums)
        {
            List<T> list = new List<T>(nums);
            list.Sort();
            list.Reverse();
            return list[0];
        }

        /// <summary>
        /// <paramref name="n1"/>, <paramref name="n2"/> 사이에 정수의 합을 구하여 반환합니다.
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 1부터 <paramref name="n"/>까지의 합을 구하여 반환합니다.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int SumOfFromStart1ToEnd(int n)
        {
            if (n < 1)
            {
                throw new ArgumentException(nameof(n) + " 값은 1 이상 입니다.");
            }

            // Gauss(가우스 덧셈)
            return ((1 + n) * (n / 2)) + 
                // 홀수 일때는 추가적으로 수를 더해줘야 한다
                // 1+2+3+4+5 일때 3 더하는 처리 6 * 2 + 3 = 15;
                (n % 2 == 0 ? 0 : (n + 1) / 2);
        }

        /// <summary>
        /// <paramref name="n1"/>, <paramref name="n2"/> 사이의 최대 공약수를 구하여 반환합니다.
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static int Gcd(int n1, int n2)
        {
            int max = Max<int>(n1, n2), min = Min<int>(n1, n2);
            int temp = 0;
            
            while ((max % min) != 0)
            {
                temp = max % min;
                max = min;
                min = temp;
            }

            int gcd = min;
            return gcd;
        }

        /// <summary>
        /// 사각형의 가로세로 비율을 구하여 반환합니다.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static RectangleRatio ToRatio(Size size)
        {
            if (!(size.Width >= 0 && size.Height >= 0))
            {
                throw new ArgumentException("");
            }

            int max = Max<int>(size.Width, size.Height), 
                min = Min<int>(size.Width, size.Height);

            int temp = 0;
            while ((max % min) != 0)
            {
                temp = max % min;
                max = min;
                min = temp;
            }

            int gcd = min;
            int widthRatio = size.Width / gcd;
            int heightRatio = size.Height / gcd;
            return new RectangleRatio(widthRatio, heightRatio);
        }

        /// <summary>
        /// 사각형의 가로세로 비율을 구하여 반환합니다.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static RectangleRatio ToRatio(Rectangle rectangle)
        {
            return ToRatio(rectangle.Size);
        }
    }
}
