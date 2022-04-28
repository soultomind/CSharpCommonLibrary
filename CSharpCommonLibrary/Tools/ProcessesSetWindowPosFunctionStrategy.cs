using CommonLibrary.Extensions;
using CommonLibrary.Utilities;
using CommonLibrary.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CommonLibrary.Tools
{
    /// <summary>
    /// 프로세스의 모든 창 위치 조정 이벤트 아규먼트 클래스
    /// </summary>
    public class ProcessAllSetWindowPosEventArgs : EventArgs
    {
        /// <summary>
        /// 프로세스
        /// </summary>
        public Process Process { get; private set; }

        /// <summary>
        /// 프로세스에 포함된 특정 핸들
        /// </summary>
        public IntPtr Handle { get; private set; }
        
        /// <summary>
        /// 핸들에 대한 윈도우 상태
        /// </summary>
        public WindowPlacement WindowPlacement { get; private set; }

        /// <summary>
        /// 윈도우 Text
        /// </summary>
        public string Text { get; private set; }

        public ProcessAllSetWindowPosEventArgs(Process process, IntPtr handle, WindowPlacement windowPlacement, string text)
        {
            Process = process;
            Handle = handle;
            WindowPlacement = windowPlacement;
            Text = text;
        }
    }

    /// <summary>
    /// 프로세스 모든 창 위치 조정 델리게이트
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns>위치 조정이 필요한경우 True 값을 리턴합니다.</returns>
    public delegate bool ProcessAllWindowsHandleSetPosEventHandler(object sender, ProcessAllSetWindowPosEventArgs e);

    /// <summary>
    /// 모든 프로세스 창 위치 조정 전략 클래스
    /// </summary>
    public class ProcessesSetWindowPosFunctionStrategy : IWindowFunctionStrategy
    {
        /// <summary>
        /// 프로세스 모든 창 제어 이벤트 핸들러
        /// </summary>
        public event ProcessAllWindowsHandleSetPosEventHandler SetProcessAllWindowsHandlePos;

        /// <summary>
        /// 특정 프로세스에 관련된 핸들 창이 이동 방지될 스크린 인덱스
        /// </summary>
        public int PreventMoveSceenIndex
        {
            get { return _preventMoveScreenIndex; }
            set
            {
                if (value >= 0 && value <= Screen.AllScreens.Length - 1)
                {
                    _preventMoveScreenIndex = value;
                }
            }
        }
        private int _preventMoveScreenIndex;

        /// <summary>
        /// 워커스레드 Interval (값 단위 Milliseconds)
        /// <para>기본값 = 500</para>
        /// </summary>
        public int Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }
        private int _interval = 500;

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
        public List<string> ProcessAllWindowsHandleSetPosProcessNames
        {
            get { return _processAllWindowsHandleSetPosProcessNames; }
        }
        private List<string> _processAllWindowsHandleSetPosProcessNames = new List<string>();

        private volatile bool _isStartWindowWorker;

        private Thread _workerThread;
        private volatile bool _finishedWorkThread;

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public ProcessesSetWindowPosFunctionStrategy()
        {
            PreventMoveSceenIndex = ScreenUtility.GetFirstScreenIndexAndExceptPrimaryScreenIndex();

            // explorer 프로세스에는 
            // 윈도우탐색기, 작업표시줄 등 다수의 창이 존재하므로 해당 프로세스는 제외한다.
            // 윈도우에서 제공되는 프로세스에 대한 창을 제어하기 위해서는 관리자 권한이 필요하다.
            ExceptProcessNames.Add(Explorer.ProcessName);
        }

        private void StartInternalWork()
        {
            Toolkit.TraceWriteLine("특정 스크린 프로세스 창 이동 방지를 시작합니다.");

            _isStartWindowWorker = true;

            Screen preventScreen = Screen.AllScreens[PreventMoveSceenIndex];
            while (_isStartWindowWorker)
            {
                foreach (Process process in Process.GetProcesses())
                {
                    if (ExceptProcessNames.Contains(process.ProcessName))
                    {
                        continue;
                    }

                    // 프로세스의 모든 윈도우 핸들 찾아서 처리할지 여부
                    if (_processAllWindowsHandleSetPosProcessNames.Contains(process.ProcessName))
                    {
                        // TODO: 특정 프로세스의 모든 윈도우 핸들을 찾아서 처리할지 여부도 이벤트핸들러로 정의 필요
                        foreach (IntPtr windowHandle in WindowManager.GetProcessWindowHandles(process.Id))
                        {
                            string windowText = WindowManager.GetWindowText(windowHandle);
                            WindowPlacement wp = new WindowPlacement();
                            if (User32.GetWindowPlacement(windowHandle, ref wp))
                            {
                                RECT outRect = RECT.Empty;
                                User32.GetWindowRect(windowHandle, out outRect);

                                if (SetProcessAllWindowsHandlePos.Invoke(this, new ProcessAllSetWindowPosEventArgs(process, windowHandle, wp, windowText)))
                                {
                                    if (preventScreen.BoundsContains(outRect.ToPoints()))
                                    {
                                        ProcessSetWindowPos(windowHandle, outRect);

                                        string text = String.Format("ProcessSetWindowPos ProcessName={0}, Handle={1}, ShowWindowCommand={2}, Text={3}",
                                            process.ProcessName, windowHandle, wp.ShowCmd.ToString(), windowText);

                                        Toolkit.TraceWriteLine(text);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        WindowPlacement wp = new WindowPlacement();
                        if (User32.GetWindowPlacement(process.MainWindowHandle, ref wp))
                        {
                            RECT outRect;
                            User32.GetWindowRect(process.MainWindowHandle, out outRect);
                            if (preventScreen.BoundsContains(outRect.ToPoints()))
                            {
                                ProcessSetWindowPos(process.MainWindowHandle, outRect);

                                string text = String.Format("ProcessSetWindowPos ProcessName={0}, Handle={1}, ShowWindowCommand={2}, Title={3}",
                                            process.ProcessName, process.MainWindowHandle, wp.ShowCmd.ToString(), process.MainWindowTitle);

                                Toolkit.TraceWriteLine(text);
                            }
                        }
                    }
                }
            }
        }

        private void StopInternalWork()
        {
            _isStartWindowWorker = false;

            if (_workerThread != null)
            {
                _workerThread.Join();
            }
            _workerThread = null;

            foreach (Delegate item in SetProcessAllWindowsHandlePos.GetInvocationList())
            {
                SetProcessAllWindowsHandlePos -= (ProcessAllWindowsHandleSetPosEventHandler)item;
            }
            SetProcessAllWindowsHandlePos = null;

            Toolkit.TraceWriteLine("특정 스크린 프로세스 창 이동 방지를 종료합니다.");
        }

        private void ProcessSetWindowPos(IntPtr handle, RECT outRect)
        {
            Rectangle primaryWorkingArea = Screen.PrimaryScreen.WorkingArea;
            int x = (primaryWorkingArea.Width - outRect.Width) / 2;
            int y = (primaryWorkingArea.Height - outRect.Height) / 2;

            User32.SetWindowPos(
                handle,
                User32.HWND_NOTOPMOST,
                x,
                y,
                outRect.Width,
                outRect.Height,
                SetWindowPos.SWP_NOZORDER | SetWindowPos.SWP_SHOWWINDOW
            );
        }

        #region IWindowFunctionStrategy 구현
        /// <summary>
        /// 워커 스레드 작업이 끝났는지 여부
        /// </summary>
        public bool IsFinishedWorkThread
        {
            get { return _finishedWorkThread; }
        }

        /// <summary>
        /// 워커 스레드
        /// </summary>
        public Thread WorkerThread
        {
            get { return _workerThread; }
        }

        /// <summary>
        /// 워커 스레드 시작
        /// </summary>
        /// <returns></returns>
        public bool StartWorkerThread()
        {
            _workerThread = new Thread(StartInternalWork);
            _workerThread.IsBackground = true;
            _workerThread.Start();

            _finishedWorkThread = false;
            return true;
        }

        /// <summary>
        /// 워커 스레드 중지
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void StopWorkerThread()
        {
            if (_isStartWindowWorker)
            {
                if (_workerThread == null)
                {
                    throw new InvalidOperationException();
                }

                StopInternalWork();
                _finishedWorkThread = true;
            }
        }

        #endregion
    }
}
