using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Utilities
{
    /// <summary>
    /// 변환 유틸리티
    /// </summary>
    public class ConvertUtility
    {
        private static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        private static byte[] FromBase64String(string text)
        {
            return Convert.FromBase64String(text);
        }

        /// <summary>
        /// <paramref name="text"/> 값을 Base64 문자열로 변환합니다.
        /// </summary>
        /// <param name="text">Base64로 변환할 Text</param>
        /// <param name="encodingName">인코딩 명</param>
        /// <returns></returns>
        public static string Base64Encode(string text, string encodingName = "UTF-8")
        {
            byte[] bytes = GetBytes(text, encodingName);
            return ToBase64String(bytes);
        }

        /// <summary>
        /// <paramref name="text"/> Base64 값을 디코딩 하여 반환합니다.
        /// </summary>
        /// <param name="text">Base64 디코딩 할 텍스트</param>
        /// <returns></returns>
        public static string Base64Decode(string text)
        {
            byte[] bytes = FromBase64String(text);
            return GetString(bytes);
        }

        /// <summary>
        /// <paramref name="encodingName"/>에 해당하는 객체를 반환합니다.
        /// </summary>
        /// <param name="encodingName"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="encodingName"/> 값이 null 이거나 <paramref name="encodingName"/>에 해당하는 
        /// <see cref="System.Text.Encoding"/> 을 찾을 수 없을경우
        /// </exception>
        public static Encoding GetEncoding(string encodingName = "UTF-8")
        {
            if (encodingName == null)
            {
                throw new ArgumentNullException(nameof(encodingName));
            }

            Encoding encoding = Encoding.GetEncoding(encodingName);
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            return encoding;
        }

        /// <summary>
        /// <paramref name="text"/> 값을 byte[] 배열로 반환합니다.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encodingName"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string text, string encodingName = "UTF-8")
        {
            Encoding encoding = GetEncoding(encodingName);
            return encoding.GetBytes(text);
        }

        /// <summary>
        /// <paramref name="bytes"/>[] 배열 값을 문자열로 반환합니다.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="encodingName"></param>
        /// <returns></returns>
        public static string GetString(byte[] bytes, string encodingName = "UTF-8")
        {
            Encoding encoding = GetEncoding(encodingName);
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// <paramref name="value"/> 값을 byte[] 배열로 변환합니다.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] IntToByteArray(int value)
        {
            return BitConverter.GetBytes(value);
            
            // DeadCode
            /*
            if (useBitConverter)
            {
                return BitConverter.GetBytes(value);
            }
            else
            {
                if (BitConverter.IsLittleEndian)
                {
                    byte[] byteArray = new byte[4];
                    byteArray[3] = (byte)(value >> 24);
                    byteArray[2] = (byte)(value >> 16);
                    byteArray[1] = (byte)(value >> 8);
                    byteArray[0] = (byte)(value);
                    return byteArray;
                }
                else
                {
                    byte[] byteArray = new byte[4];
                    byteArray[0] = (byte)(value >> 24);
                    byteArray[1] = (byte)(value >> 16);
                    byteArray[2] = (byte)(value >> 8);
                    byteArray[3] = (byte)(value);
                    return byteArray;
                }
            }
            */
        }

        /// <summary>
        /// <paramref name="value"/> 값을 int 형으로 변환하여 반환합니다.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int ByteArrayToInt(byte[] value, int startIndex = 0)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!(value.Length >= 4))
            {
                throw new ArgumentException(nameof(value) + ".length=" + value.Length);
            }

            return BitConverter.ToInt32(value, startIndex);
        }
    }
}
