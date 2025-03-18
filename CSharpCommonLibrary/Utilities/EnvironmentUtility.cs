using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Utilities
{
    /// <summary>
    /// 환경 유틸리티 클래스
    /// </summary>
    public static class EnvironmentUtility
    {
        /// <summary>
        /// 루트 드라이브(Windows 설치)를 반환
        /// </summary>
        /// <returns></returns>
        public static string GetRootDrive()
        {
            string rootDrive = System.IO.Path.GetPathRoot(Environment.SystemDirectory);
            return rootDrive;
        }

        /// <summary>
        /// User Downloads 경로를 반환
        /// </summary>
        /// <returns></returns>
        public static string GetUserProfileDownloadsDirectory()
        {
            string directory = System.Environment.GetEnvironmentVariable("HOMEPATH") + @"\" + "Downloads";
            return directory;
        }

        /// <summary>
        /// ProgramData 경로를 반환
        /// </summary>
        /// <returns></returns>
        public static string GetCommonApplicationProgramData()
        {
            string directory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
            return directory;
        }

        /// <summary>
        /// AppData/Roaming 경로를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationData()
        {
            string directory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            return directory;
        }

        /// <summary>
        /// AppData/Local 경로를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static string GetLocalApplicationData()
        {
            string directory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            return directory;
        }
    }
}
