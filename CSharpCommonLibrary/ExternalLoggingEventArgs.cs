using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class ExternalLoggingEventArgs : EventArgs
    {
        /// <summary>
        /// 실제 로깅이 발생한 원천 객체
        /// </summary>
        public object SourceSender { get; set; }

        /// <summary>
        /// 메시지
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 예외
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 로그레벨
        /// </summary>
        public LogLevel Level { get; set; } = LogLevel.Debug;

        /// <summary>
        /// 메시지를 받는 생성자
        /// </summary>
        /// <param name="message"></param>
        public ExternalLoggingEventArgs(string message)
            : this(message, null)
        {
            
        }

        /// <summary>
        /// 예외를 받는 생성자
        /// </summary>
        /// <param name="exception"></param>
        public ExternalLoggingEventArgs(Exception exception)
            : this(null, exception)
        {

        }

        public ExternalLoggingEventArgs(string message, Exception exception)
        {
            Message = message;
            Exception = exception;
        }

        public bool IsError
        {
            get { return Exception != null; }
        }
    }
}
