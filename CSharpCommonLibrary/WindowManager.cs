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

namespace CommonLibrary
{
    public class ProcessWndHandleWindowStateEventHandler : EventArgs
    {
        public Process Process { get; private set; }
        public IntPtr Handle { get; private set; }
        public WINDOWPLACEMENT WindowPlacement { get; private set; }
        public string Title { get; private set; }
        public ProcessWndHandleWindowStateEventHandler(Process process, IntPtr handle, WINDOWPLACEMENT windowPlacement, string title)
        {
            Process = process;
            Handle = handle;
            WindowPlacement = windowPlacement;
            Title = title;
        }
    }
    public delegate bool SetWindowPosEventHandler(object sender, ProcessWndHandleWindowStateEventHandler e);
    /// <summary>
    /// 윈도우 매니저
    /// </summary>
    public class WindowManager
    {
        public event SetWindowPosEventHandler SetWindowPos;
        /// <summary>
        /// 특정 프로세스에 관련된 핸들 창이 이동 방지될 스크린 인덱스
        /// </summary>
        public int ProcessWndHandlesPreventScreenIndex
        {
            get { return _processWndHandlesPreventScreenIndex; }
            set
            {
                if (value >= 0 && value <= Screen.AllScreens.Length - 1)
                {
                    _processWndHandlesPreventScreenIndex = value;
                }
            }
        }
        private int _processWndHandlesPreventScreenIndex;

        /// <summary>
        /// 특정 프로세스에 관련된 핸들 창이 이동 방지 워커 스레드 시작 여부
        /// </summary>
        public bool IsStartProcessWndHandlesFixedLocationWorker
        {
            get { return _startProcessWndHandlesFixedLocationWorker; }
            private set { _startProcessWndHandlesFixedLocationWorker = value; }
        }
        private volatile bool _startProcessWndHandlesFixedLocationWorker;

        /// <summary>
        /// 창 제어에서 제외할 프로세스 목록
        /// <para>explorer 프로세스는 목록에서 기본적으로 제외됩니다.</para>
        /// </summary>
        public List<string> ExceptProcessNames
        {
            get { return _exceptProcessNames; }
        }
        private List<string> _exceptProcessNames = new List<string>();

        /// <summary>
        /// 프로세스의 모든 윈도우 핸들을 창제어 처리할지에 대한 프로세스 목록
        /// </summary>
        public List<string> AllProcessWndHandlesCheckProcessNames
        {
            get { return _allProcessWndHandlesCheckProcessNames; }
        }
        private List<string> _allProcessWndHandlesCheckProcessNames = new List<string>();

        private Thread _processWndHandlesFixedLocationWorker;


        public WindowManager()
        {
            ProcessWndHandlesPreventScreenIndex = ScreenUtility.GetFirstScreenIndexAndExceptPrimaryScreen();
            ExceptProcessNames.Add("explorer");
        }

        public void StartProcessHandleWindowMovePrevent()
        {
            if (AllProcessWndHandlesCheckProcessNames.Count > 1)
            {
                if (SetWindowPos == null)
                {
                    throw new ArgumentException("AllProcessWndHandlesCheckProcessNames.Count is Zero");
                }
            }
            _processWndHandlesFixedLocationWorker = new Thread(StartProcessHandleWindowMovePreventWorker);
            _processWndHandlesFixedLocationWorker.IsBackground = true;
            _processWndHandlesFixedLocationWorker.Start();
        }

        private void StartProcessHandleWindowMovePreventWorker()
        {
            IsStartProcessWndHandlesFixedLocationWorker = true;

            Screen preventScreen = Screen.AllScreens[ProcessWndHandlesPreventScreenIndex];
            while (IsStartProcessWndHandlesFixedLocationWorker)
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (ExceptProcessNames.Contains(process.ProcessName))
                    {
                        continue;
                    }

                    // 프로세스의 모든 윈도우 핸들 찾아서 처리할지 여부
                    if (AllProcessWndHandlesCheckProcessNames.Contains(process.ProcessName))
                    {
                        // TODO: 특정 프로세스의 모든 윈도우 핸들을 찾아서 처리할지 여부도 이벤트핸들러로 정의 필요
                        foreach (IntPtr windowHandle in GetProcessWindowHandles(process.Id))
                        {
                            string title = GetTitle(windowHandle);
                            WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
                            if (User32.GetWindowPlacement(windowHandle, ref wp))
                            {
                                RECT outRect = RECT.Empty;
                                User32.GetWindowRect(windowHandle, out outRect);
                                string text = String.Format("ProcessName={0}, Handle={1}, ShowWindowCommand={2}, Title={3}", process.ProcessName, windowHandle, wp.ShowCmd.ToString(), title); ;
                                Toolkit.TraceWriteLine(text);
                                if (SetWindowPos.Invoke(this, new ProcessWndHandleWindowStateEventHandler(process, windowHandle, wp, title)))
                                {
                                    if (preventScreen.BoundsContains(outRect.ToPoints()))
                                    {
                                        Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                                        int x = (rect.Width - outRect.Width) / 2;
                                        int y = (rect.Height - outRect.Height) / 2;

                                        User32.SetWindowPos(
                                            windowHandle,
                                            User32.HWND_NOTOPMOST,
                                            x,
                                            y,
                                            outRect.Width,
                                            outRect.Height,
                                            Win32.SetWindowPos.SWP_NOZORDER | Win32.SetWindowPos.SWP_SHOWWINDOW
                                        );
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
                        if (User32.GetWindowPlacement(process.MainWindowHandle, ref wp))
                        {
                            RECT outRect;
                            User32.GetWindowRect(process.MainWindowHandle, out outRect);
                            if (preventScreen.BoundsContains(outRect.ToPoints()))
                            {
                                Rectangle rect = Screen.PrimaryScreen.WorkingArea;
                                int x = (rect.Width - outRect.Width) / 2;
                                int y = (rect.Height - outRect.Height) / 2;

                                User32.SetWindowPos(
                                    process.MainWindowHandle,
                                    User32.HWND_NOTOPMOST,
                                    x,
                                    y,
                                    outRect.Width,
                                    outRect.Height,
                                    Win32.SetWindowPos.SWP_NOZORDER | Win32.SetWindowPos.SWP_SHOWWINDOW
                                );
                            }
                        }
                    }
                }
            }
        }

        public void StopProcessHandleWindowMovePrevent()
        {
            StopProcessHandleWindowMovePreventWorker();
        }

        private void StopProcessHandleWindowMovePreventWorker()
        {
            IsStartProcessWndHandlesFixedLocationWorker = false;

            if (_processWndHandlesFixedLocationWorker != null)
            {
                _processWndHandlesFixedLocationWorker.Join();
                _processWndHandlesFixedLocationWorker = null;
            }
        }

        #region Static

        /// <summary>
        /// <paramref name="processId"/>에 해당하는 프로세스의 Window 핸들을 <see cref="System.IntPtr"/> 배열로 반환합니다.
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public IntPtr[] GetProcessWindowHandles(int processId)
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
        public static string GetTitle(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = User32.GetWindowTextLength(hWnd);
            StringBuilder builder = new StringBuilder(length + 1);
            User32.GetWindowText(hWnd, builder, builder.Capacity);
            return builder.ToString();
        }

        #endregion
    }
}
