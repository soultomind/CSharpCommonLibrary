using CommonLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    /// <summary>
    /// Color 값과 int ARGB 값을 상호 변환하며, 알파 채널 사용 여부를 판별할 수 있는 컬러 데이터 래퍼 클래스입니다.
    /// </summary>
    public class ColorData
    {
        /// <summary>
        /// 알파 채널(투명도) 사용 여부 (A != 255이면 true)
        /// </summary>
        public bool UseAlpha
        {
            get
            {
                return valueColor.A != 255;
            }
        }
        /// <summary>
        /// Color 구조체 값 (설정 시 int ARGB 값도 동기화)
        /// </summary>
        public Color ValueColor
        {
            get
            {
                return valueColor;
            }
            private set
            {
                valueColor = value;
                valueInt = ColorUtility.ToIntArgb(valueColor);
            }
        }
        private Color valueColor;

        /// <summary>
        /// int ARGB 값 (설정 시 Color 값도 동기화)
        /// </summary>
        public int ValueInt
        {
            get
            {
                return valueInt;
            }
            private set
            {
                valueInt = value;
                valueColor = ColorUtility.ToColorArgb(valueInt);
            }
        }
        private int valueInt;

        private ColorData(Color valueColor)
        {
            ValueColor = valueColor;
        }

        private ColorData(int valueInt)
        {
            ValueInt = valueInt;
        }

        /// <summary>
        /// #이 포함된 16진수 색상 문자열(예: "#FFAABB" 또는 "#80FFAABB")로부터 ColorData를 생성합니다.
        /// </summary>
        /// <param name="value">#이 포함된 6자리 또는 8자리 16진수 색상 문자열</param>
        /// <returns>ColorData 인스턴스</returns>
        public static ColorData ToColorFromHexString(string value)
        {
            Color valueColor = ColorUtility.ToColorFromHexString(value);
            return new ColorData(valueColor);
        }

        /// <summary>
        /// Color 값으로 ColorData 생성
        /// </summary>
        /// <param name="valueColor">Color 값</param>
        /// <returns>ColorData 인스턴스</returns>
        public static ColorData ToColorDataFromColor(Color valueColor)
        {
            ColorData obj = new ColorData(valueColor);
            return obj;
        }

        /// <summary>
        /// int ARGB 값으로 ColorData 생성
        /// </summary>
        /// <param name="valueInt">int ARGB 값</param>
        /// <returns>ColorData 인스턴스</returns>
        public static ColorData ToColorDataFromInt(int valueInt)
        {
            ColorData obj = new ColorData(valueInt);
            return obj;
        }

        /// <summary>
        /// RGB 값으로 ColorData 생성 (알파=255)
        /// </summary>
        /// <param name="red">Red</param>
        /// <param name="green">Green</param>
        /// <param name="blue">Blue</param>
        /// <returns>ColorData 인스턴스</returns>
        public static ColorData ToColorDataArgb(int red, int green, int blue)
        {
            Color valueColor = Color.FromArgb(red, green, blue);
            ColorData obj = new ColorData(valueColor);
            return obj;
        }

        /// <summary>
        /// ARGB 값으로 ColorData 생성
        /// </summary>
        /// <param name="alpha">Alpha</param>
        /// <param name="red">Red</param>
        /// <param name="green">Green</param>
        /// <param name="blue">Blue</param>
        /// <returns>ColorData 인스턴스</returns>
        public static ColorData ToColorDataArgb(int alpha, int red, int green, int blue)
        {
            Color valueColor = Color.FromArgb(alpha, red, green, blue);
            ColorData obj = new ColorData(valueColor);
            return obj;
        }

        /// <summary>
        /// 현재 ColorData의 #이 포함된 16진수 Hex 문자열(예: #FFAABB 또는 #80FFAABB)을 반환합니다.
        /// </summary>
        public string ToHexString()
        {
            if (UseAlpha)
                return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", ValueColor.A, ValueColor.R, ValueColor.G, ValueColor.B);
            else
                return string.Format("#{0:X2}{1:X2}{2:X2}", ValueColor.R, ValueColor.G, ValueColor.B);
        }

        /// <summary>
        /// 현재 ColorData의 ARGB 문자열(예: "A,R,G,B")을 반환합니다.
        /// </summary>
        public string ToArgbString()
        {
            return string.Format("{0},{1},{2},{3}", ValueColor.A, ValueColor.R, ValueColor.G, ValueColor.B);
        }
    }
}
