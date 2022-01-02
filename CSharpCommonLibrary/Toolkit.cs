using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;

namespace CommonLibrary
{
    public static class Toolkit
    {
        /// <summary>
        /// DebugView Filter 이름
        /// <para>Filter/Hightlight 메뉴 Include 항목에 사용될 값</para>
        /// </summary>
        public static string IncludeFilterName
        {
            get { return _sIncludeFilterName; }
            set
            {
                if (!String.IsNullOrEmpty(value) && value.Length > 1)
                {
                    _sIncludeFilterName = value;
                }
            }
        }
        public static string _sIncludeFilterName;

        /// <summary>
        /// <see cref="System.Diagnostics.Debug"/>.WriteLine 메서드 출력 여부
        /// </summary>
        public static bool IsDebugEnabled;

        /// <summary>
        /// <see cref="System.Diagnostics.Trace"/>.WriteLine 메서드 출력 여부
        /// </summary>
        public static bool IsTraceEnabled;

        /// <summary>
        /// 메시지 출력시 현재시간 출력 여부
        /// </summary>
        public static bool UseNowToString;
        static Toolkit()
        {

#if DEBUG
            _sIncludeFilterName = CreateNamespace();
            IsDebugEnabled = true;
#else
            _sIncludeFilterName = "ApplicationName";
            IsDebugEnabled = false;
#endif
            IsTraceEnabled = true;
            UseNowToString = false;
        }

        private static string CreateNamespace()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Toolkit));
            return assembly.GetName().Name;
        }

        private static string NowToString(string format = "yyyy/MM/dd HH:mm:ss")
        {
            return DateTime.Now.ToString(format);
        }

        public static void DebugWriteLine(string message)
        {
            if (IsDebugEnabled)
            {
                string className = new StackFrame(1).GetMethod().ReflectedType.Name;
                string methodName = new StackFrame(1, true).GetMethod().Name;
                if (UseNowToString)
                {
                    message = String.Format("[{0}] [{1}.{2}] {3} DEBUG - {4}",
                        _sIncludeFilterName, className, methodName, NowToString(), message);
                }
                else
                {
                    message = String.Format("[{0}] [{1}.{2}] DEBUG - {3}",
                        _sIncludeFilterName, className, methodName, message);
                }

                Debug.WriteLine(message);
            }
        }
        public static void TraceWriteLine(string message)
        {
            if (IsTraceEnabled)
            {
                string className = new StackFrame(1).GetMethod().ReflectedType.Name;
                string methodName = new StackFrame(1, true).GetMethod().Name;
                if (UseNowToString)
                {
                    message = String.Format("[{0}] [{1}.{2}] {3} TRACE - {4}",
                        _sIncludeFilterName, className, methodName, NowToString(), message);
                }
                else
                {
                    message = String.Format("[{0}] [{1}.{2}] TRACE - {3}",
                        _sIncludeFilterName, className, methodName, message);
                }
                Trace.WriteLine(message);
            }
        }

        internal static bool IsAdministrator()
        {
            bool flag;

            WindowsIdentity identity = null;
            try
            {
                identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                flag = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception ex)
            {
                TraceWriteLine(ex.Message);
                TraceWriteLine(ex.StackTrace);

                flag = false;
            }
            finally
            {
                if (identity != null)
                {
                    identity.Dispose();
                }
            }

            return flag;
        }
    }
}