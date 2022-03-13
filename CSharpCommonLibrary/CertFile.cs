using System;
using System.Security.Cryptography.X509Certificates;

namespace CommonLibrary
{
    public enum CertFileStore
    {
        None,

        /// <summary>
        /// 인증서 추가
        /// </summary>
        AddCert,

        /// <summary>
        /// 인증서가 존재 할때
        /// </summary>
        ExistsCert,

        /// <summary>
        /// 인증서 추가중 오류 발생
        /// </summary>
        ErrorCert,
    }

    /// <summary>
    /// 인증서 파일
    /// </summary>
    public class CertFile
    {
        /// <summary>
        /// 인증서 파일을 추가합니다.
        /// </summary>
        /// <param name="storeName"></param>
        /// <param name="fileName"></param>
        /// <param name="password"></param>
        /// <param name="outException"></param>
        /// <returns></returns>
        public static CertFileStore AddCrtFile(StoreName storeName, string fileName, string password, out Exception outException)
        {
            // TODO: 브라우저 실행 중일 때 추가 안되는 현상 개선 필요
            CertFileStore fileStore = CertFileStore.None;

            try
            {
                StoreLocation storeLocation = (Toolkit.IsAdministrator()) ? StoreLocation.LocalMachine : StoreLocation.CurrentUser;
                X509Store store = new X509Store(storeName, storeLocation);
                store.Open(OpenFlags.ReadWrite);

                X509Certificate2 cert = new X509Certificate2(fileName, password);
                bool contains = store.Certificates.Contains(cert);
                if (!contains)
                {
                    store.Add(cert);
                    fileStore = CertFileStore.AddCert;
                }
                else
                {
                    fileStore = CertFileStore.ExistsCert;
                }

                store.Close();

                outException = null;
                return fileStore;
            }
            catch (Exception ex)
            {
                outException = ex;
                fileStore = CertFileStore.ErrorCert;
                return fileStore;
            }
        }
    }
}
