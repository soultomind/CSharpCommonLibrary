using CommonLibrary.Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class EdgeWebView2Runtime
    {
        /// <summary>
        /// 64비트 운영체제 여부
        /// </summary>
        public static readonly bool Is64BitOperatingSystem = Environment.Is64BitOperatingSystem;
        /// <summary>
        /// 현재 프로세스가 64비트 여부
        /// </summary>
        public static readonly bool Is64BitProcess = Environment.Is64BitProcess;

        /// <summary>
        /// Edge WebView2 레지스트리 Guid 값입니다.
        /// </summary>
        public static readonly string ClientsSubId = "{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}";

        public event EventHandler<ExternalLoggingEventArgs> ExternalLogging;
        public LogLevel LogLevel { get; set; } = LogLevel.Debug;

        public EdgeWebView2Runtime()
        {

        }

        internal void OnExternalLogging(ExternalLoggingEventArgs e)
        {
            ExternalLogging?.Invoke(this, e);
        }

        internal bool IsEnabled(LogLevel logLevel)
        {
            return LogLevel.IsEnabled(logLevel);
        }

        private void Trace(string message)
        {
            if (!IsEnabled(LogLevel.Trace))
            {
                return;
            }

            if (ExternalLogging != null)
            {
                OnExternalLogging(new ExternalLoggingEventArgs(message)
                {
                    Level = LogLevel.Trace
                });
            }
        }

        private void Debug(string message)
        {
            if (!IsEnabled(LogLevel.Debug))
            {
                return;
            }

            if (ExternalLogging != null)
            {
                OnExternalLogging(new ExternalLoggingEventArgs(message)
                {
                    Level = LogLevel.Debug
                });
            }
        }

        private void Error(Exception exception)
        {
            if (!IsEnabled(LogLevel.Error))
            {
                return;
            }

            if (ExternalLogging != null)
            {
                OnExternalLogging(new ExternalLoggingEventArgs(exception)
                {
                    Level = LogLevel.Error
                });
            }
        }

        /// <summary>
        /// 현재 플랫폼에 맞는 ClientSubId 레지스트리키 경로를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public string GetFlatformTargetRegistryClientsSubIdKey()
        {
            string name = RegistryUtility.GetLocalMachineBaseKey(Is64BitOperatingSystem, Is64BitProcess) +
                @"\Microsoft\EdgeUpdate\Clients\" + ClientsSubId;
            return name;
        }

        /// <summary>
        /// Edge WebView2 런타임이 설치되어 있는지 여부를 반환합니다.
        /// </summary>
        /// <returns>설치 여부</returns>
        public bool InstalledEdgeWebView2LoaclMachine()
        {
            Trace("Start InstalledEdgeWebView2LoaclMachine");
            bool bInstalled = false;

            string subKey = GetFlatformTargetRegistryClientsSubIdKey();
            Debug("SubKey=" + subKey);
            using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subKey))
            {
                bInstalled = (registryKey != null);
                if (bInstalled)
                {
                    Debug("===== Registry Information ======");
                    foreach (var name in registryKey.GetValueNames())
                    {
                        object value = registryKey.GetValue(name);
                        string text = String.Format("{0}={1}", name, value);

                        Debug(text);
                    }
                    Debug("===== Registry Information ======");
                }
            }
            Trace("Finish InstalledEdgeWebView2LoaclMachine");
            return bInstalled;
        }
    }
}
