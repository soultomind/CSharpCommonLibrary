using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 모듈에서 사용되는 툴킷 클래스
    /// </summary>
    internal static class Toolkit
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
        private static string _sIncludeFilterName;

        /// <summary>
        /// <see cref="Debug.WriteLine(object)"/>,<see cref="Debug.Write(object)"/>
        /// <para>메서드 출력 여부</para>
        /// </summary>
        public static bool IsDebugEnabled;

        /// <summary>
        /// <see cref="Trace.WriteLine(object)"/>,<see cref="Trace.Write(object)"/>
        /// <para>메서드 출력 여부</para>
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
            _sIncludeFilterName = "CLIPSOFT";
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

        private static string MakeMessage(string header, string message)
        {
#if DEBUG
            message = String.Format("[{0}] [{1}] DEBUG - {2}", _sIncludeFilterName, header, message);
#else
            message = String.Format("[{0}] [{1}] TRACE - {2}", _sIncludeFilterName, header, message);
#endif
            return message;
        }

        /// <summary>
        /// <see cref="System.Diagnostics.Debug.WriteLine(object)"/>을 활용하여 메시지를 출력합니다.
        /// </summary>
        /// <param name="message"></param>
        internal static void DebugWriteLine(string message)
        {
            if (IsDebugEnabled)
            {
                string className = new StackFrame(1).GetMethod().ReflectedType.Name;
                string methodName = new StackFrame(1, true).GetMethod().Name;
                string header = String.Format("{0} :: {1}", className, methodName);
                message = MakeMessage(header, message);
                Debug.WriteLine(message);
            }
        }

        /// <summary>
        /// <see cref="System.Diagnostics.Debug.WriteLine(object)"/>을 활용하여 예외를 출력합니다.
        /// </summary>
        /// <param name="ex"></param>
        internal static void DebugWriteLine(Exception ex)
        {
            string className = new StackFrame(1).GetMethod().ReflectedType.Name;
            string methodName = new StackFrame(1, true).GetMethod().Name;
            string header = String.Format("{0} :: {1}", className, methodName);

            string message = MakeMessage(header, ex.Message);
            DebugWriteLine(message);

            message = MakeMessage(header, ex.StackTrace);
            DebugWriteLine(message);
        }

        /// <summary>
        /// <see cref="System.Diagnostics.Debug.Write(object)"/>을 활용하여 메시지를 출력합니다.
        /// </summary>
        /// <param name="message"></param>
        internal static void DebugWrite(string message)
        {
            if (IsDebugEnabled)
            {
                string className = new StackFrame(1).GetMethod().ReflectedType.Name;
                string methodName = new StackFrame(1, true).GetMethod().Name;
                string header = String.Format("{0} :: {1}", className, methodName);
                message = MakeMessage(header, message);
                Debug.Write(message);
            }
        }

        /// <summary>
        /// <see cref="System.Diagnostics.Trace.WriteLine(object)"/>을 활용하여 메시지를 출력합니다.
        /// </summary>
        /// <param name="message"></param>
        internal static void TraceWriteLine(string message)
        {
            if (IsTraceEnabled)
            {
                string className = new StackFrame(1).GetMethod().ReflectedType.Name;
                string methodName = new StackFrame(1, true).GetMethod().Name;
                string header = String.Format("{0} :: {1}", className, methodName);
                message = MakeMessage(header, message);
                Trace.WriteLine(message);
            }
        }

        /// <summary>
        /// <see cref="System.Diagnostics.Trace.WriteLine(object)"/>을 활용하여 예외를 출력합니다.
        /// </summary>
        /// <param name="ex"></param>
        internal static void TraceWriteLine(Exception ex)
        {
            string className = new StackFrame(1).GetMethod().ReflectedType.Name;
            string methodName = new StackFrame(1, true).GetMethod().Name;
            string header = String.Format("{0} :: {1}", className, methodName);

            string message = MakeMessage(header, ex.Message);
            TraceWriteLine(message);

            message = MakeMessage(header, ex.StackTrace);
            TraceWriteLine(message);
        }

        /// <summary>
        /// <see cref="System.Diagnostics.Trace.Write(object)"/>을 활용하여 메시지를 출력합니다.
        /// </summary>
        /// <param name="message"></param>
        internal static void TraceWrite(string message)
        {
            if (IsTraceEnabled)
            {
                string className = new StackFrame(1).GetMethod().ReflectedType.Name;
                string methodName = new StackFrame(1, true).GetMethod().Name;
                string header = String.Format("{0} :: {1}", className, methodName);
                message = MakeMessage(header, message);
                Trace.Write(message);
            }
        }

        internal static bool IsCurrentProcessAdministrator()
        {
            bool flag;

            WindowsIdentity identity = null;
            try
            {
                identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                flag = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception)
            {
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