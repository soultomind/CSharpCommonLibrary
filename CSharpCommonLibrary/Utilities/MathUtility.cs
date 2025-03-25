using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Utilities
{
    public static class MathUtility
    {
        /// <summary>
        /// 인자로 넘어온 정수에서 최소값을 구해 반환합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static T Min<T>(params T[] nums) where T : struct
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
        public static T Max<T>(params T[] nums) where T : struct
        {
            List<T> list = new List<T>(nums);
            list.Sort();
            list.Reverse();
            return list[0];
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
        /// <paramref name="n1"/>, <paramref name="n2"/> 사이의 최대 공약수를 구하여 반환합니다.
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static float GcdF(float n1, float n2)
        {
            float max = Max<float>(n1, n2), min = Min<float>(n1, n2);
            float temp = 0;

            while ((max % min) != 0)
            {
                temp = max % min;
                max = min;
                min = temp;
            }

            float gcd = min;
            return gcd;
        }

        /// <summary>
        /// 원본 사각영역(<paramref name="srcRect"/>)에서의 좌표(<paramref name="srcPt"/>)값을 다른 목적 사각영역(<paramref name="destRect"/>)애서의 좌표를 계산하여 반환한다.
        /// </summary>
        /// <param name="srcPt"></param>
        /// <param name="srcRect"></param>
        /// <param name="destRect"></param>
        /// <returns></returns>
        public static Point CalcDestPoint(Point srcPt, Rectangle srcRect, Rectangle destRect)
        {
            // 알고 싶은 좌표 = (기존좌표 * 알고 싶은 좌표 크기) / 기존크기
            Point destPt = Point.Empty;
            destPt.X = (int)Math.Ceiling((double)(srcPt.X * destRect.Width) / srcRect.Width);
            destPt.Y = (int)Math.Ceiling((double)(srcPt.Y * destRect.Height) / srcRect.Height);
            return destPt;
        }

        /// <summary>
        /// 원본 사각영역(<paramref name="srcRect"/>)에서의 좌표(<paramref name="srcPt"/>)값을 다른 목적 사각영역(<paramref name="destRect"/>)애서의 좌표를 계산하여 반환한다.
        /// </summary>
        /// <param name="srcPt"></param>
        /// <param name="srcRect"></param>
        /// <param name="destRect"></param>
        /// <returns></returns>
        public static PointF CalcDestPointF(PointF srcPt, RectangleF srcRect, RectangleF destRect)
        {
            // 알고 싶은 좌표 = (기존좌표 * 알고 싶은 좌표 크기) / 기존크기
            PointF destPt = PointF.Empty;
            destPt.X = (float)Math.Ceiling((double)((srcPt.X * destRect.Width) / srcRect.Width));
            destPt.Y = (float)Math.Ceiling((double)((srcPt.Y * destRect.Height) / srcRect.Height));
            return destPt;
        }

        /// <summary>
        /// 두점 사이의 각도를 구하여 반환합니다.
        /// </summary>
        /// <param name="pt1">좌표1</param>
        /// <param name="pt2">좌표2</param>
        /// <returns></returns>
        public static double CalcAngle(Point pt1, Point pt2)
        {
            // Atan2 각도 구하기
            // 참고 = http://zzoyu.tistory.com/73
            Point p1 = (pt1.Y > pt2.Y) ? pt1 : pt2;
            Point p2 = (pt1.Y > pt2.Y) ? pt2 : pt1;

            int a = p1.X - p2.X;
            int b = p1.Y - p2.Y;
            a = Math.Abs(a);
            b = Math.Abs(b);

            double angle = Math.Atan2((double)a, (double)b) * 180 / Math.PI;
            return angle;
        }

        #region Internal

        internal static AspectRatio ToAspectRatio(Size size)
        {
            if (!(size.Width >= 0 && size.Height >= 0))
            {
                throw new ArgumentException("");
            }

            int min = Min<int>(size.Width, size.Height),
                max = Max<int>(size.Width, size.Height);

            int gcd = Gcd(min, max);
            int width = size.Width / gcd;
            int height = size.Height / gcd;
            return new AspectRatio(width, height);
        }

        internal static AspectRatioF ToAspectRatioF(SizeF size)
        {
            if (!(size.Width >= 0 && size.Height >= 0))
            {
                throw new ArgumentException("");
            }

            float min = Min<float>(size.Width, size.Height),
                  max = Max<float>(size.Width, size.Height);

            float gcd = GcdF(min, max);
            float width = size.Width / gcd;
            float height = size.Height / gcd;
            return new AspectRatioF(width, height);
        }



        #endregion
    }
}
