using CommonLibrary.Utilities;
using CommonLibrary.Win32;
using CommonLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace CommonLibrary.Tools
{
    /// <summary>
    /// 윈도우 매니저 클래스
    /// <para>창 관련 기능 제공</para>
    /// </summary>
    public class WindowManager
    {
        private IWindowFunctionStrategy FunctionStrategy
        {
            get { return _functionStrategy; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                if (_functionStrategy != null && _functionStrategy.WorkerThread != null && !_functionStrategy.IsFinishedWorkThread)
                {
                    _functionStrategy.StopWorkerThread();
                }

                _functionStrategy = value;
            }
        }
        private IWindowFunctionStrategy _functionStrategy;

        public WindowManager(IWindowFunctionStrategy functionStrategy)
        {
            if (functionStrategy == null)
            {
                throw new ArgumentNullException(nameof(functionStrategy));
            }
            FunctionStrategy = functionStrategy;
        }

        /// <summary>
        /// 워커 스레드 작업 완료 여부
        /// </summary>
        public bool IsFinishedWorkThread
        {
            get { return FunctionStrategy.IsFinishedWorkThread; }
        }

        /// <summary>
        /// 워커 스레드
        /// </summary>
        public Thread WorkerThread
        {
            get { return FunctionStrategy.WorkerThread; }
        }

        /// <summary>
        /// 워커 스레드를 시작합니다.
        /// </summary>
        public void StartWorkerThread()
        {
            FunctionStrategy.StartWorkerThread();
        }

        /// <summary>
        /// 워커 스레드를 중지합니다.
        /// </summary>
        public void StopWorkerThread()
        {
            FunctionStrategy.StopWorkerThread();
        }

        #region Static

        /// <summary>
        /// <paramref name="processId"/>에 해당하는 프로세스의 Window 핸들을 <see cref="System.IntPtr"/> 배열로 반환합니다.
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static IntPtr[] GetProcessWindowHandles(int processId)
        {
            IntPtr[] windowHandles = (new IntPtr[256]);
            int count = 0;
            IntPtr nextWindowHandle = IntPtr.Zero;
            do
            {
                nextWindowHandle = User32.FindWindowEx(IntPtr.Zero, nextWindowHandle, null, null);
                int outProcessId;
                User32.GetWindowThreadProcessId(nextWindowHandle, out outProcessId);
                if (outProcessId == processId)
                {
                    windowHandles[count++] = nextWindowHandle;
                }
            } while (nextWindowHandle != IntPtr.Zero);

            System.Array.Resize(ref windowHandles, count);
            return windowHandles;
        }

        /// <summary>
        /// <paramref name="hWnd"/>의 Title 값을 반환합니다.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static string GetWindowText(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = User32.GetWindowTextLength(hWnd);

            // length * 4 한글일때
            StringBuilder builder = new StringBuilder(length * 4);
            User32.GetWindowText(hWnd, builder, builder.Capacity);
            return builder.ToString();
        }

        #endregion
    }
}
