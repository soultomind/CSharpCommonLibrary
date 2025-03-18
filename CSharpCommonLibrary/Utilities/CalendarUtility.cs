using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Utilities
{
    public static class CalendarUtility
    {
        public static readonly string[] MonthKorean = new string[] { "일", "월", "화", "수", "목", "금", "토" };

        /// <summary>
        /// 요일(day)을 일-토 문자열로 반환한다.
        /// </summary>
        /// <param name="year">년도</param>
        /// <param name="month">월</param>
        /// <param name="day">일</param>
        /// <returns></returns>
        public static string CalcDayMonthKorean(int year, int month, int day)
        {
            DayOfWeek dayOfWeek = DayOfWeek(year, month, day);
            return MonthKorean[(int)dayOfWeek];
        }

        /// <summary>
        /// 년 월 대한 첫 주 시작 요일과 마지막 일을 구한다.
        /// </summary>
        /// <param name="year">년도</param>
        /// <param name="month">월</param>
        /// <param name="outStartDayOfWeek">구할 시작 요일 DayOfWeek</param>
        /// <param name="outLastDay">해당 월에 대한 마지막 일</param>
        public static void SetStartDayOfWeekAndLastDay(int year, int month, out int outStartDayOfWeek, out int outLastDay)
        {
            int dayOfWeek = (int)DayOfWeek(year, month, 1);
            outStartDayOfWeek = dayOfWeek;

            // 해당 월의 마지막 일
            int endLastDay = DaysInMonth(year, month);
            outLastDay = endLastDay;
        }

        /// <summary>
        /// 요일을 반환한다.
        /// </summary>
        /// <param name="year">년도</param>
        /// <param name="month">월</param>
        /// <param name="day">일</param>
        /// <returns></returns>
        public static DayOfWeek DayOfWeek(int year, int month, int day)
        {
            DayOfWeek dayOfWeek = new DateTime(year, month, day).DayOfWeek;
            return dayOfWeek;
        }

        /// <summary>
        /// 년(<paramref name="year"/>) 월(<paramref name="month"/>)의 마지막날을 반환 합니다.
        /// </summary>
        /// <param name="year">년도</param>
        /// <param name="month">월</param>
        /// <returns></returns>
        public static int DaysInMonth(int year, int month)
        {
            int lastDay = DateTime.DaysInMonth(year, month);
            return lastDay;
        }
    }
}
