using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CommonLibrary.Tools
{
    /// <summary>
    /// 마우스 매니저 클래스 기능 전략 인터페이스
    /// </summary>
    public interface IMouseFunctionStrategy
    {
        /// <summary>
        /// 워커 스레드 작업이 끝났는지 여부
        /// <para>스레드가 완전히 종료되어 Null 이 된 경우</para>
        /// </summary>
        bool IsFinishedWorkThread { get; }

        /// <summary>
        /// 워커 스레드
        /// </summary>
        Thread WorkerThread { get; }

        /// <summary>
        /// 워커 스레드 시작
        /// </summary>
        /// <returns></returns>
        bool StartWorkerThread();

        /// <summary>
        /// 워커 스레드 정지
        /// </summary>
        void StopWorkerThread();
    }
}
