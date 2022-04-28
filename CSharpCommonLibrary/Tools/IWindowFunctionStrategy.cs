using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CommonLibrary.Tools
{
    /// <summary>
    /// 전략 인터페이스
    /// </summary>
    public interface IWindowFunctionStrategy
    {
        bool IsFinishedWorkThread { get; }
        Thread WorkerThread { get; }
        bool StartWorkerThread();
        void StopWorkerThread();
    }
}
