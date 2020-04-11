using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Utilities
{
    public class ConvertUtility
    { 
        public static string Base64Encode(string text, string encodingName = "UTF-8")
        {
            byte[] bytes = GetBytes(text, encodingName);
            return ToBase64String(bytes);
        }

        public static string Base64Decode(string text)
        {
            byte[] bytes = FromBase64String(text);
            return GetString(bytes);
        }

        public static string ToBase64StringFromFile(string path)
        {
            return ToBase64String(System.IO.File.ReadAllBytes(path));
        }

        private static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        private static byte[] FromBase64String(string text)
        {
            return Convert.FromBase64String(text);
        }

        public static byte[] GetBytes(string text, string encodingName = "UTF-8")
        {
            if (encodingName == null)
            {
                throw new ArgumentNullException("EncodingName");
            }

            Encoding encoding = Encoding.GetEncoding(encodingName);
            if (encoding == null)
            {
                throw new ArgumentNullException("Encoding");
            }

            return encoding.GetBytes(text);
        }

        public static string GetString(byte[] bytes, string encodingName = "UTF-8")
        {
            if (encodingName == null)
            {
                throw new ArgumentNullException("EncodingName");
            }

            Encoding encoding = Encoding.GetEncoding(encodingName);
            if (encoding == null)
            {
                throw new ArgumentNullException("Encoding");
            }

            return encoding.GetString(bytes);
        }
    }
}
