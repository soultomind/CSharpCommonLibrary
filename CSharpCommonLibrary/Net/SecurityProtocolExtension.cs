using System.Net;
using System.Security.Authentication;

namespace CommonLibrary.Net
{
    /// <summary>
    /// <para>.NET Framework 3.5 Supported </para>
    /// <see cref="System.Net.ServicePointManager.SecurityProtocol"/> 확장 클래스
    /// </summary>
    public class SecurityProtocolExtension
    {
        /// <summary>
        /// Tls1.1 SslProtocols
        /// </summary>
        public const SslProtocols _Tls11 = (SslProtocols)0x00000300;

        /// <summary>
        /// Tls1.2 SslProtocols
        /// </summary>
        public const SslProtocols _Tls12 = (SslProtocols)0x00000C00;

        /// <summary>
        /// Tls1.1 SecurityProtocolType
        /// </summary>
        public const SecurityProtocolType Tls11 = (SecurityProtocolType)_Tls11;

        /// <summary>
        /// Tls1.2 SecurityProtocolType
        /// </summary>
        public const SecurityProtocolType Tls12 = (SecurityProtocolType)_Tls12;

        /// <summary>
        /// Tls12 설정을 합니다.
        /// </summary>
        /// <exception cref="System.NotSupportedException">
        /// Tls12 속성을 설정하기 위해 지정된 값이 유효한 SecurityProtocolType 열거형 값이 아닌 경우
        /// <para>윈도우 업데이트가 제대로 되어 있지 않을경우 발생 할 수 있다.</para>
        /// </exception>
        public static void SetTls12()
        {
            SecurityProtocolType type =
                ServicePointManager.SecurityProtocol |
                SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                Tls11 | Tls12;

            ServicePointManager.SecurityProtocol = type;
        }
    }
}
