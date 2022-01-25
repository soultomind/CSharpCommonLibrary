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
