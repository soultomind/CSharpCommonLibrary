using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CommonLibrary
{
    /// <summary>
    /// 윈도우 매니저
    /// </summary>
    public class WindowManager
    {
        /// <summary>
        /// 특정 프로세스에 관련된 핸들 창이 이동 방지될 스크린 인덱스
        /// </summary>
        public int ProcessHandleWindowMovePreventScreenIndex
        {
            get { return _processHandleWindowMovePreventScreenIndex; }
            set
            {
                if (value >= 0 && value <= Screen.AllScreens.Length - 1)
                {
                    _processHandleWindowMovePreventScreenIndex = value;
                }
            }
        }
        private int _processHandleWindowMovePreventScreenIndex;

        /// <summary>
        /// 특정 프로세스에 관련된 핸들 창이 이동 방지 워커 스레드 시작 여부
        /// </summary>
        public bool StartProcessHandleWindowMovePreventScreenIndex
        {
            get { return _startProcessHandleWindowMovePreventScreenIndex; }
            private set { _startProcessHandleWindowMovePreventScreenIndex = value; }
        }
        private volatile bool _startProcessHandleWindowMovePreventScreenIndex;


        private Thread _processHandleWindowMovePreventThread;
        public WindowManager()
        {

        }

        public void StartProcessHandleWindowMovePrevent()
        {
            _processHandleWindowMovePreventThread = new Thread(StartProcessHandleWindowMovePreventWorker);
            _processHandleWindowMovePreventThread.IsBackground = true;
            _processHandleWindowMovePreventThread.Start();
        }

        private void StartProcessHandleWindowMovePreventWorker()
        {
            StartProcessHandleWindowMovePreventScreenIndex = true;

            while (StartProcessHandleWindowMovePreventScreenIndex)
            {

            }
        }

        public void StopProcessHandleWindowMovePrevent()
        {
            StopProcessHandleWindowMovePreventWorker();
        }

        private void StopProcessHandleWindowMovePreventWorker()
        {
            StartProcessHandleWindowMovePreventScreenIndex = false;

            if (_processHandleWindowMovePreventThread != null)
            {
                _processHandleWindowMovePreventThread.Join();
                _processHandleWindowMovePreventThread = null;
            }
        }
    }
}
