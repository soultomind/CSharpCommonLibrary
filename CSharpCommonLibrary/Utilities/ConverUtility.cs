using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Utilities
{
    public static class ConverUtility
    {
        /// <summary>
        /// 최대 버퍼 사이즈
        /// </summary>
        private static int MaxBufferSize = 4096;

        public static void SetMaxBufferSize(int maxBufferSize)
        {
            if (maxBufferSize <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            MaxBufferSize = maxBufferSize;
        }

        private static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        private static byte[] FromBase64String(string text)
        {
            return Convert.FromBase64String(text);
        }

        private static byte[] GetBytesOrNull(string text, string encodingName = "UTF-8")
        {
            Encoding encoding = Encoding.GetEncoding(encodingName);
            if (encoding != null)
            {
                return encoding.GetBytes(text);
            }
            return null;
        }

        private static string GetStringOrNull(byte[] bytes, string encodingName = "UTF-8")
        {
            Encoding encoding = Encoding.GetEncoding(encodingName);
            if (encoding != null)
            {
                return encoding.GetString(bytes);
            }
            return null;
        }

        /// <summary>
        /// <paramref name="size"/>값을 문자열로 변환합니다.
        /// <para>변환하면서 크기 단위값을 뒤에 붙여서 반환(B,KB,MB,GB,TB,PB)</para>
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ConvertUnitSize(double size)
        {
            // https://social.msdn.microsoft.com/Forums/sqlserver/en-US/68ecd276-ef2f-4f49-8352-2f9c23af6a74/convert-from-bytes-to-gb?forum=winforms
            string[] units = new string[] { "B", "KB", "MB", "GB", "TB", "PB" };
            double mod = 1024.0;
            int index = 0;
            while (size >= mod)
            {
                size /= mod;
                index++;
            }
            return Math.Round(size) + units[index];
        }

        /// <summary>
        /// 해당 문자열을 Base64 형태로 인코딩하여 반환
        /// </summary>
        /// <param name="text"></param>
        /// <param name="encodingName"></param>
        /// <returns></returns>
        public static string ToBase64Encode(string text, string encodingName = "UTF-8")
        {
            byte[] bytes = GetBytesOrNull(text, encodingName);
            return ToBase64String(bytes);
        }

        /// <summary>
        /// 해당 Base64 문자열을 디코딩하여 반환
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToBase64Decode(string text)
        {
            byte[] bytes = FromBase64String(text);
            return GetStringOrNull(bytes);
        }

        public static string ToBase64StringFromFile(string path)
        {
            return ToBase64String(File.ReadAllBytes(path));
        }

        /// <summary>
        /// 해당 스트림에서 이미지를 읽어 반환
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static Image GetImage(Stream stream)
        {
            byte[] buffer = new byte[MaxBufferSize];
            Image image = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int count = 0;
                do
                {
                    count = stream.Read(buffer, 0, buffer.Length);
                    memoryStream.Write(buffer, 0, count);
                } while (count != 0);

                // 스트림이 올바른 이미지 형식이 아닐경우 또는 Stream이 null인 경우

                // 올바른 이미지 형식이 아닐경우
                // 정상적인 이미지 포맷이 아니고 적절한 메타 정보가 없어서 오류가 발생할 것으로 추정

                image = Image.FromStream(memoryStream);
            }
            return image;
        }

        /// <summary>
        /// 해당 스트림을 읽어 바이트배열을 반환
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] GetBytes(Stream stream)
        {
            byte[] bytes = null;

            byte[] buffer = new byte[MaxBufferSize];
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int count = 0;
                do
                {
                    count = stream.Read(buffer, 0, buffer.Length);
                    memoryStream.Write(buffer, 0, count);
                } while (count != 0);
                bytes = memoryStream.ToArray();
            }

            return bytes;
        }
    }
}
