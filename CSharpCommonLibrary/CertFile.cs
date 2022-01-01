using System;
using System.Security.Cryptography.X509Certificates;

namespace CommonLibrary
{
    public enum CertFileStore
    {
        None,

        AddCert,

        ExistsCert,

        ErrorCert,
    }

    public class CertFile
    {
        public static CertFileStore AddCrtFile(bool isAdministrator, StoreName storeName, string fileName, string password, out Exception outException)
        {
            CertFileStore fileStore = CertFileStore.None;

            try
            {
                if (isAdministrator)
                {
                    if (!Toolkit.IsAdministrator())
                    {
                        throw new InvalidOperationException("It is not currently running in administrator mode.");
                    }
                }
                StoreLocation storeLocation = (isAdministrator) ? StoreLocation.LocalMachine : StoreLocation.CurrentUser;
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
