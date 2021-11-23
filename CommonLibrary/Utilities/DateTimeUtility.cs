using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Utilities
{
    public class DateTimeUtility
    {
        /// <summary>
        /// 현재 날짜시간을 포멧형태로 반환.[기본 yyyy/MM/dd HH:mm:ss]
        /// 시간을 나타내는 문자열이 hh=[0-11], HH=[0-23] 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string NowToString(string format = "yyyy/MM/dd HH:mm:ss")
        {
            return DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
        }
    }
}
