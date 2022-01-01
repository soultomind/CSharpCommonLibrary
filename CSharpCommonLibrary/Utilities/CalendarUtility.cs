using System;

namespace CommonLibrary.Utilities
{
    public class CalendarUtility
    {
        public static readonly string[] MonthKoreans = new string[] { "일", "월", "화", "수", "목", "금", "토" };
        public static readonly int[] MonthTotalDays = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        /// <summary>
        /// 년월일에 대한 DayOfWeek 인스턴스 반환
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static DayOfWeek NewDayOfWeek(int year, int month, int day)
        {
            DayOfWeek dayOfWeek = new DateTime(year, month, day).DayOfWeek;
            return dayOfWeek;
        }

        /// <summary>
        /// 년월일에 대한 요일 반환[한글로]
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static string GetMonthKorean(int year, int month, int day)
        {
            DayOfWeek dayOfWeek = NewDayOfWeek(year, month, day);
            return MonthKoreans[(int)dayOfWeek];
        }

        /// <summary>
        /// 년월에 대한 마지막날을 반환
        /// </summary>
        /// <param name="year">년도</param>
        /// <param name="month">월</param>
        /// <returns></returns>
        public static int DaysInMonth(int year, int month)
        {
            int daysInMonth = DateTime.DaysInMonth(year, month);
            return daysInMonth;
        }

        /// <summary>
        /// 년월일에 대한 첫 주 시작 요일과 마지막 일을 구한다.
        /// </summary>
        /// <param name="year">년도</param>
        /// <param name="month">월</param>
        /// <param name="outStartFirstDay">구할 시작 요일 DayOfWeek</param>
        /// <param name="outDaysInMonth">해당 월에 대한 마지막 일</param>
        public static void SetStartDayOfWeekAndDaysInMonth(int year, int month, out int outDayOfWeek, out int outDaysInMonth)
        {
            // 해당 월의 마지막 일
            outDaysInMonth = DaysInMonth(year, month);
            outDayOfWeek = (int)NewDayOfWeek(year, month, 1);
        }
    }
}
