using CommonLibrary.Utilities;
using CommonLibrary.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CommonLibrary
{
    /// <summary>
    /// 마우스 관련 매니저 클래스
    /// </summary>
    public class MouseManager
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
                    if (value == ScreenUtility.GetPrimaryScreenIndex())
                    {
                        throw new System.ArgumentException(nameof(value) + " 값이 주모니터 값입니다.");
                    }
                    _mouseMovePreventScreenIndex = value;
                }
                else
                {
                    throw new System.ArgumentException(nameof(value));
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
        private volatile bool _startmouseMovePrevent;

        /// <summary>
        /// 마우스 이동 방지 스크린으로 이동전 마지막 좌표로 이동 여부
        /// </summary>
        public bool IsMouseMoveLastPreventPoint
        {
            get { return _mouseMoveLastPreventPoint; }
            set { _mouseMoveLastPreventPoint = value; }
        }
        private bool _mouseMoveLastPreventPoint;

        private Stack<Point> _mouseMovePoints;

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

        private Thread _mouseMovePreventThread;

        public MouseManager()
        {
            MouseMovePreventInterval = 2000;
            // 기본값은 주 모니터를 제외한 첫 번째 스크린 인덱스
            MouseMovePreventScreenIndex = ScreenUtility.GetFirstScreenIndexAndExceptPrimaryScreen();
        }

        public void StartMouseMovePrevent()
        {
            if (MouseMovePreventScreenIndex == ScreenUtility.GetPrimaryScreenIndex())
            {
                Toolkit.TraceWriteLine("MouseMovePreventScreenIndex == ScreenUtility.GetPrimaryScreenIndex()");
                return;
            }

            if (IsMouseMoveLastPreventPoint)
            {
                if (_mouseMovePoints == null)
                {
                    _mouseMovePoints = new Stack<Point>();
                }
                else
                {
                    _mouseMovePoints.Clear();
                }
            }

            if (MouseMovePreventScreenIndex >= 0)
            {
                _mouseMovePreventThread = new Thread(StartMouseMovePreventWorker);
                _mouseMovePreventThread.IsBackground = true;
                _mouseMovePreventThread.Start();
            }
        }

        public void StopMouseMovePrevent()
        {
            StopMouseMovePreventWorker();
        }

        private void StartMouseMovePreventWorker()
        {
            _startmouseMovePrevent = true;

            Screen primaryScreen = Screen.PrimaryScreen;
            Screen preventScreen = Screen.AllScreens[MouseMovePreventScreenIndex];
            Point pt = Point.Empty;

            while (_startmouseMovePrevent)
            {
                Thread.Sleep(MouseMovePreventInterval);
                MousePoint mousePoint = MouseEvent.GetCursorPosition();

                if (!mousePoint.IsEmpty())
                {
                    pt.X = mousePoint.X;
                    pt.Y = mousePoint.Y;

                    int x = 0, y = 0;
                    if (IsMouseMoveLastPreventPoint)
                    {
                        _mouseMovePoints.Push(pt);

                        if (_mouseMovePoints.Count >= 50)
                        {
                            // 최근 좌표가 Top 위치에 있도록 
                            Stack<Point> reversePoints = new Stack<Point>();

                            int count = 0;
                            foreach (Point item in _mouseMovePoints.Reverse())
                            {
                                if (count >= 20)
                                {
                                    break;
                                }
                                reversePoints.Push(item);
                                count++;
                            }

                            _mouseMovePoints.Clear();
                            _mouseMovePoints = reversePoints;
                            Toolkit.TraceWriteLine("Clear Old Point 30 MouseMovePoints");
                        }

                        Point lastPreventPoint = Point.Empty;
                        foreach (Point item in _mouseMovePoints)
                        {
                            if (!preventScreen.Bounds.Contains(item))
                            {
                                lastPreventPoint = item;
                                break;
                            }
                        }

                        x = lastPreventPoint.X;
                        y = lastPreventPoint.Y;

                        if (lastPreventPoint.IsEmpty)
                        {
                            Point centerPt = ScreenUtility.GetPrimaryScreenBoundsCenter();
                            x = centerPt.X;
                            y = centerPt.Y;
                        }
                    }
                    else
                    {
                        Point centerPt = ScreenUtility.GetPrimaryScreenBoundsCenter();
                        x = centerPt.X;
                        y = centerPt.Y;
                    }

                    if (preventScreen.Bounds.Contains(pt))
                    {
                        MouseEvent mouseOperation = new MouseEvent();

                        mouseOperation.SetCursorPosition(x + 1, y + 1);
                        mouseOperation.SetMouseEvent(MouseEvent.MouseEventFlags.Move);

                        mouseOperation.SetCursorPosition(x, y);
                    }
                }
            }
        }

        private void StopMouseMovePreventWorker()
        {
            _startmouseMovePrevent = false;

            if (_mouseMovePreventThread != null)
            {
                _mouseMovePreventThread.Join();
                _mouseMovePreventThread = null;
            }
        }
    }
}
