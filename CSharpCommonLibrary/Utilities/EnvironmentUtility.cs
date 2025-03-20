using System;
using System.Collections.Generic;
using System.IO;
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
        /// 윈도우가 설치된 시스템 드라이브를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static string GetSystemDrive()
        {
            string systemDrive = Path.GetPathRoot(Environment.SystemDirectory);
            return systemDrive;
        }

        /// <summary>
        /// 현재 로컬 환경에 맞는 System32 디렉토리를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static string GetSystem32Directory()
        {
            string systemDrive = GetSystemDrive();
            if (Environment.Is64BitOperatingSystem)
            {
                if (Environment.Is64BitProcess)
                {
                    return systemDrive + "\\Windows\\System32";
                }
                else
                {
                    return systemDrive + "\\Windows\\SysWOW64";
                }

            }
            else
            {
                return systemDrive + "\\Windows\\System32";
            }
        }

        /// <summary>
        /// 현재 로컬 환경에 맞는 System32 에서 <paramref name="path"/> 값을 결합하여 경로를 반환합니다.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CombineSystem32Path(string path)
        {
            return GetSystem32Directory() + Path.DirectorySeparatorChar + path;
        }

        /// <summary>
        /// ProgramData 경로를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static string GetCommonApplicationProgramDataDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        }

        /// <summary>
        /// AppData/Roaming 경로를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationDataDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        /// <summary>
        /// AppData/Local 경로를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static string GetLocalApplicationDataDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetMyDocumentsDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }
}
