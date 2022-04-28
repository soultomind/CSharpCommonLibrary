using CommonLibrary.Utilities;
using CommonLibrary.Win32;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System;
using System.Runtime.InteropServices;

namespace CommonLibrary.Tools
{
    /// <summary>
    /// 마우스 관리자 클래스
    /// <para>마우스 관련 기능 제공</para>
    /// </summary>
    public class MouseManager
    {
        /// <summary>
        /// 마우스 기능 전략 인터페이스입니다.
        /// <para>설정시에 현재 전략에 워커 스레드가 실행중이면 정지 후 설정합니다.</para>
        /// </summary>
        public IMouseFunctionStrategy FunctionStrategy
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
        private IMouseFunctionStrategy _functionStrategy;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="functionStrategy"></param>
        public MouseManager(IMouseFunctionStrategy functionStrategy)
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
        /// 현재 마우스 좌표를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static Point GetCursorPoint()
        {
            MousePoint mousePoint = MouseNative.GetCursorPoint();
            return new Point(mousePoint.X, mousePoint.Y);
        }

        /// <summary>
        /// 마우스 좌표를 이동시킵니다.
        /// </summary>
        /// <param name="point"></param>
        public static void MoveCursorPoint(Point point)
        {
            MouseNative mouseNative = new MouseNative();
            mouseNative.SetCursorPoint(point.X, point.Y);
            mouseNative.MouseEvent(MouseEventFlags.MOVE);
        }

        #endregion
    }
}
