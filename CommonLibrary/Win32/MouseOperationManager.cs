using CommonLibrary.Utilities;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CommonLibrary.Win32
{
    public class MouseOperationManager
    {
        /// <summary>
        /// 마우스 이동 방지 스크린 인덱스
        /// </summary>
        public int MouseMovePreventScreenIndex
        {
            get { return _mouseMovePreventScreenIndex; }
            set
            {
                if (value >= 0 && value <= Screen.AllScreens.Length - 1)
                {
                    _mouseMovePreventScreenIndex = value;
                }
            }
        }
        private int _mouseMovePreventScreenIndex;


        /// <summary>
        /// 마우스 이동 방지 시작 여부
        /// </summary>
        public bool IsStartMouseMovePrevent
        {
            get { return _startmouseMovePrevent; }
            private set { _startmouseMovePrevent = value; }
        }
        private bool _startmouseMovePrevent;

        /// <summary>
        /// 마우스 이동 방지 주기
        /// </summary>
        public int MouseMovePreventInterval
        {
            get { return _mouseMovePreventInterval; }
            set
            {
                if (value >= 100 && value <= 3000)
                {
                    _mouseMovePreventInterval = value;
                }
            }
        }
        private int _mouseMovePreventInterval;

        private Thread _thread;

        public MouseOperationManager()
        {
            // 기본값은 주 모니터를 제외한 첫 번째 스크린 인덱스
            MouseMovePreventScreenIndex = ScreenUtility.GetFirstScreenIndexAndExceptPrimaryScreen();
        }

        public void MouseMovePreventStart()
        {
            if (MouseMovePreventScreenIndex == ScreenUtility.GetPrimaryScreenIndex())
            {
                Toolkit.TraceWriteLine("MouseMovePreventScreenIndex == ScreenUtility.GetPrimaryScreenIndex()");
                return;
            }

            if (MouseMovePreventScreenIndex >= 0)
            {
                _thread = new Thread(MouseMovePreventStartWorker);
                _thread.Start();
            }
        }

        public void MouseMovePreventStop()
        {
            MouseMovePreventStopWorker();
        }

        private void MouseMovePreventStartWorker()
        {
            _startmouseMovePrevent = true;

            Screen primaryScreen = Screen.PrimaryScreen;
            Point pt = Point.Empty;

            while (_startmouseMovePrevent)
            {
                Thread.Sleep(MouseMovePreventInterval);
                MousePoint mousePoint = MouseEvent.GetCursorPosition();

                if (!mousePoint.IsEmpty())
                {
                    pt.X = mousePoint.X;
                    pt.Y = mousePoint.Y;
                    if (Screen.AllScreens[MouseMovePreventScreenIndex].Bounds.Contains(pt))
                    {
                        int x = (primaryScreen.Bounds.X + primaryScreen.Bounds.Width) / 2;
                        int y = (primaryScreen.Bounds.Y + primaryScreen.Bounds.Height) / 2;

                        MouseEvent mouseOperation = new MouseEvent();

                        mouseOperation.SetCursorPosition(x + 1, y + 1);
                        mouseOperation.SetMouseEvent(MouseEvent.MouseEventFlags.Move);

                        mouseOperation.SetCursorPosition(x, y);
                    }
                }
            }
        }

        private void MouseMovePreventStopWorker()
        {
            _startmouseMovePrevent = false;

            if (_thread != null)
            {
                _thread.Join();
                _thread = null;
            }
        }
    }
}
