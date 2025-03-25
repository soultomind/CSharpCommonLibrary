using CommonLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class FirefoxPreference
    {
        public string Path { get; set; }
        public FirefoxPreference(string path)
        {
            Path = path;
        }

        public void SetRootEnabled(string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.Write("user_pref(\"security.enterprise_roots.enabled\", true);");
            }
        }

        public bool HasRootEnabled(string path)
        {
            bool exist = false;
            using (StreamReader sr = new StreamReader(path))
            {
                string data = null;
                while ((data = sr.ReadLine()) != null)
                {
                    if (data == "user_pref(\"security.enterprise_roots.enabled\", true);")
                    {
                        exist = true;
                    }
                }
            }

            return exist;
        }

        #region Static

        public static string Profiles
        {
            get
            {
                return EnvironmentUtility.GetApplicationDataDirectory() + "\\Mozilla\\Firefox\\Profiles";
            }
        }

        #region UserJavaSciprt

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"><see cref="Profiles"/></param>
        /// <param name="outValue">user.js <see cref="DirectoryInfo"/></param>
        /// <returns></returns>
        public static bool TryGetUserJavascriptFile(string path, out DirectoryInfo outValue)
        {
            if (Directory.Exists(path))
            {
                foreach (DirectoryInfo di in new DirectoryInfo(path).GetDirectories())
                {
                    if (isEndsWithDotDefaultDirectory(di))
                    {
                        if (isExistUserJavaScriptFile(di))
                        {
                            outValue = di;
                            return true;
                        }
                    }
                }
            }

            outValue = null;
            return false;
        }

        #region Private
        private static bool isEndsWithDotDefaultDirectory(DirectoryInfo di)
        {
            return di.FullName.Substring(di.FullName.Length - 8) == ".default";
        }

        private static bool isExistUserJavaScriptFile(DirectoryInfo di)
        {
            return File.Exists(di.FullName + "\\user.js");
        }

        #endregion

        #endregion

        #endregion
    }
}
