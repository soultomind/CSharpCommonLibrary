using CommonLibrary.Utilities;
using CommonLibrary.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CommonLibrary.Tools
{
    /// <summary>
    /// 특정 스크린 마우스 이동 방지 전략 클래스
    /// </summary>
    public class MousePreventMoveScreenFunctionStrategy : IMouseFunctionStrategy
    {
        /// <summary>
        /// 마우스 이동 방지 스크린인덱스
        /// </summary>
        public int PreventMoveScreenIndex
        {
            get { return _preventMoveScreenIndex; }
            private set { _preventMoveScreenIndex = value; }
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
        /// 오래된 마우스 좌표정보
        /// </summary>
        public Stack<Point> OldMousePoint
        {
            get { return _oldMousePoint; }
        }
        private Stack<Point> _oldMousePoint = new Stack<Point>();

        /// <summary>
        /// 오래된 마우스 좌표정보 삭제 기준 수 (기본값=60)
        /// <para>값이 <see cref="LessThanOldMousePointClear"/>속성값보다 커야 합니다.</para>
        /// </summary>
        public int OldMousePointClear
        {
            get { return _oldMousePointClear; }
            set
            {
                if (value > _lessThanOldMousePointClear && value >= 30)
                {
                    _oldMousePointClear = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        private int _oldMousePointClear = 60;

        /// <summary>
        /// 오래된 마우스 좌표정보 삭제 수 (기본값=30)
        /// <para>오래된 마우스 좌표정보 삭제시 실제로 삭제할 개수</para>
        /// <para><see cref="AutoOldMousePointClear"/> 값이 False 일 때 사용되며, <see cref="OldMousePointClear"/>속성값보다 작아야 합니다.</para>
        /// </summary>
        public int LessThanOldMousePointClear
        {
            get { return _lessThanOldMousePointClear; }
            set
            {
                if (value > 0 && _oldMousePointClear > value)
                {
                    _lessThanOldMousePointClear = value;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }
        private int _lessThanOldMousePointClear = 30;

        /// <summary>
        /// 오래된 마우스 좌표정보 삭제시 자동으로 처리
        /// <para>자동이 되는 기준은 오래된 좌표에서 특정 스크린으로 넘어가기전 최근의 마우스 좌표를 기준으로 합니다.</para>
        /// </summary>
        public bool AutoOldMousePointClear
        {
            get { return _autoOldMousePointClear; }
            set { _autoOldMousePointClear = value; }
        }
        private bool _autoOldMousePointClear = false;

        /// <summary>
        /// 마우스 이동 처리후 오래된 좌표 모두 삭제 여부
        /// </summary>
        public bool IsSetMouseCursorPointAfterOldPointClear
        {
            get { return _setMouseCursorPointAfterOldPointClear; }
            set { _setMouseCursorPointAfterOldPointClear = value; }
        }
        private bool _setMouseCursorPointAfterOldPointClear = true;

        private volatile bool _isStartMouseWork;

        private Thread _workerThread;
        private volatile bool _finishedWorkThread;

        /// <summary>
        /// 마우스 이동 방지 기본 생성자
        /// </summary>
        /// <exception cref="InvalidOperationException">모니터가 1개 일경우 발생함</exception>
        /// <exception cref="ArgumentException">preventMoveScreenIndex 값이 유효하지 않을경우</exception>
        public MousePreventMoveScreenFunctionStrategy()
            : this(ScreenUtility.GetFirstScreenIndexAndExceptPrimaryScreenIndex())
        {

        }

        /// <summary>
        /// 마우스 이동 방지 스크린인덱스를 받는 생성자
        /// </summary>
        /// <param name="preventMoveScreenIndex"></param>
        /// <exception cref="InvalidOperationException">모니터가 1개 일경우 발생함</exception>
        /// <exception cref="ArgumentException">preventMoveScreenIndex 값이 유효하지 않을경우</exception>
        public MousePreventMoveScreenFunctionStrategy(int preventMoveScreenIndex)
        {
            if (Screen.AllScreens.Length == 1)
            {
                throw new InvalidOperationException();
            }

            if (preventMoveScreenIndex == -1 || preventMoveScreenIndex >= Screen.AllScreens.Length)
            {
                throw new ArgumentException(nameof(preventMoveScreenIndex));
            }
            PreventMoveScreenIndex = preventMoveScreenIndex;
        }

        private void StartInternalWork()
        {
            Toolkit.TraceWriteLine("특정 스크린 마우스 이동 방지를 시작합니다.");

            _oldMousePoint.Clear();
            _isStartMouseWork = true;

            Screen primaryScreen = Screen.PrimaryScreen;
            Screen preventScreen = Screen.AllScreens[_preventMoveScreenIndex];
            Point useCurrentPt = Point.Empty;

            try
            {
                while (_isStartMouseWork)
                {
                    Thread.Sleep(Interval);

                    MousePoint mousePoint = MouseNative.GetCursorPoint();
                    if (!mousePoint.IsEmpty)
                    {
                        useCurrentPt.X = mousePoint.X;
                        useCurrentPt.Y = mousePoint.Y;

                        _oldMousePoint.Push(useCurrentPt);

                        // 오래된 좌표 삭제
                        if (_oldMousePoint.Count >= _oldMousePointClear)
                        {
                            Stack<Point> reversePoints = new Stack<Point>();
                            int count = 0;
                            foreach (Point point in _oldMousePoint.Reverse())
                            {
                                bool isBreak = (_autoOldMousePointClear) ? !preventScreen.Bounds.Contains(point) : count >= _lessThanOldMousePointClear;
                                if (isBreak)
                                {
                                    break;
                                }
                                reversePoints.Push(point);
                                count++;
                            }

                            _oldMousePoint.Clear();
                            _oldMousePoint = reversePoints;

                            string text = String.Format("오래된 마우스 좌표를 삭제하였습니다. 삭제개수:{0}", count);
                            Toolkit.TraceWriteLine(text);
                        }

                        // 특정 스크린에 좌표가 오기전의 좌표를 찾는다.
                        Point lastPreventScreenOutsidePoint = Point.Empty;
                        foreach (Point point in _oldMousePoint)
                        {
                            if (!preventScreen.Bounds.Contains(point))
                            {
                                lastPreventScreenOutsidePoint = point;
                                break;
                            }
                        }

                        int x = lastPreventScreenOutsidePoint.X;
                        int y = lastPreventScreenOutsidePoint.Y;

                        // 비어 있을 경우 주 모니터 중앙으로
                        if (lastPreventScreenOutsidePoint.IsEmpty)
                        {
                            Point primaryCenterPoint = ScreenUtility.GetPrimaryScreenBoundsCenterPoint();
                            x = primaryCenterPoint.X;
                            y = primaryCenterPoint.Y;
                        }

                        if (preventScreen.Bounds.Contains(useCurrentPt))
                        {
                            MouseNative mouseNative = new MouseNative();
                            mouseNative.SetCursorPoint(x, y);

                            mouseNative.MouseEvent(MouseEventFlags.MOVE);

                            mouseNative.SetCursorPoint(x, y);

                            string text = String.Format("{0} 스크린으로 마우스가 넘어갔습니다. 마우스 이동 처리를 시작합니다.", preventScreen.Bounds.ToString());
                            Toolkit.TraceWriteLine(text);
                            text = String.Format("마우스를 (X={0}, Y={1}) 좌표로 이동을 완료하였습니다.", x, y);
                            Toolkit.TraceWriteLine(text);

                            if (IsSetMouseCursorPointAfterOldPointClear)
                            {
                                _oldMousePoint.Clear();
                                _oldMousePoint.Push(new Point(x, y));

                                Toolkit.TraceWrite("기존에 있는 모든 좌표를 삭제하고 마지막 이동한 좌표를 추가합니다.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Toolkit.DebugWriteLine(ex);
            }
        }

        private void StopInternalWork()
        {
            _isStartMouseWork = false;

            if (_workerThread != null)
            {
                _workerThread.Join();
            }
            _workerThread = null;

            Toolkit.TraceWriteLine("특정 스크린 마우스 이동 방지를 종료합니다.");
        }

        #region IMouseManagerFunctionStrategy 인터페이스 구현

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
            if (_isStartMouseWork)
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
