using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommonLibrary.Utilities
{
    /// <summary>
    /// <see cref="System.Diagnostics.Process"/> 관련 유틸 클래스
    /// </summary>
    public class ProcessUtility
    {
        /// <summary>
        /// <para><paramref name="processName"/>에 해당하는 프로세스 실행 여부를 반환합니다.</para>
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static bool Running(string processName)
        {
            Process[] processList = Process.GetProcessesByName(processName);
            if (processList.Length > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// <para><paramref name="processName"/>에 해당하는 프로세스 실행 여부를 반환합니다.</para>
        /// <para><paramref name="outProcessCount"/> 값으로 현재 실행중인 프로세스 값을 설정합니다.</para>
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="outProcessCount"></param>
        /// <returns></returns>
        public static bool Running(string processName, out int outProcessCount)
        {
            Process[] processList = Process.GetProcessesByName(processName);
            outProcessCount = processList.Length;

            if (processList.Length > 0)
            {
                return true;
            }
            return false;
        }
    }
}
