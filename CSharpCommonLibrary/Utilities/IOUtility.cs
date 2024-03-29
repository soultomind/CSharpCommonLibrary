﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonLibrary.Utilities
{
    /// <summary>
    /// IO 관련 유틸리티 클래스
    /// <para><see cref="System.IO"/> 관련</para>
    /// </summary>
    public class IOUtility
    {
        /// <summary>
        /// Path 경로가 디렉토리인지 여부값을 반환합니다.
        /// </summary>
        /// <param name="path">경로</param>
        /// <returns></returns>
        public static bool IsDirectory(string path)
        {
            FileAttributes fileAttributes = File.GetAttributes(path);

            //detect whether its a directory or file
            if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Path 디렉토리에 있는 파일들을 삭제합니다.
        /// </summary>
        /// <param name="path">경로</param>
        /// <param name="recursive"></param>
        public static void DeleteFiles(string path, bool recursive = true)
        {
            if (IsDirectory(path))
            {
                Directory.Delete(path, recursive);
            }
        }
    }
}
