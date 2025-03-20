using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Win32
{
    public class WmiEnvironment
    {
        internal static readonly string ROOT_SCOPE = "root\\WMI";
        public static ManagementObjectSearcher CreateManagementObjectSearcher(string scope, string queryString)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope,queryString);
            return searcher;
        }
        public static int GetDisplayCount()
        {
            ManagementObjectSearcher searcher = CreateManagementObjectSearcher(
                    ROOT_SCOPE,
                    "SELECT * FROM WmiMonitorBasicDisplayParams"
            );

            var info = new List<string>();
            foreach (ManagementObject queryObj in searcher.Get())
            {
                info.Add(queryObj["InstanceName"].ToString());
            }

            return info.Count;
        }
    }
}
