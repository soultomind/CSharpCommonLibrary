using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    internal class DEV_BROADCAST_DEVICEINTERFACE
    {
        internal Int32 Size;
        internal Int32 DeviceType;
        internal Int32 Reserved;
        internal Guid ClassGuid;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        internal char[] Name;
    }

    public class DeviceInterfaceMessageFilter : IMessageFilter
    {
        public readonly uint WM_DEVICECHANGE = 0x0219;
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_DEVICECHANGE)
            {
                return true;
            }
            return false;
        }
    }

    public class DeviceInterface
    {
        // USB디바이스 감지용 외부함수 등록
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr RegisterDeviceNotification(IntPtr recipient, IntPtr notificationFilter, int flags);

        public const uint WM_DEVICECHANGE = 0x0219;

        // 새 장치가 발견됨
        public const int DBT_DEVICEARRIVAL = 0x8000;
        // 장치가 제거됨
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004;

        private IMessageFilter MessageFilter { get; set; }

        public string Vid { get; set; } = String.Empty;
        public string Pid { get; set; } = String.Empty;
        public DeviceInterface(string vid, string pid)
        {
            Vid = vid;
            Pid = pid;
        }

        public bool Initialize(IntPtr handle, out IntPtr deviceHandle)
        {
            Application.AddMessageFilter(MessageFilter = new DeviceInterfaceMessageFilter());

            const int DBT_DEVTYP_DEVICEINTERFACE = 0x00000005;
            const Int32 DEVICE_NOTIFY_WINDOW_HANDLE = 0;
            Guid classGuid = new Guid(""); // USB 장치의 GUID

            DEV_BROADCAST_DEVICEINTERFACE di = new DEV_BROADCAST_DEVICEINTERFACE();
            IntPtr devBroadcastDeviceInterfaceBuffer = IntPtr.Zero;
            IntPtr deviceNotificationHandle = IntPtr.Zero;
            Int32 size = 0;
            // Set the parameters in the DEV_BROADCAST_DEVICEINTERFACE structure.

            // Set the size.
            size = Marshal.SizeOf(di);
            di.Size = size;

            // Request to receive notifications about a class of devices.

            di.DeviceType = DBT_DEVTYP_DEVICEINTERFACE;

            di.Reserved = 0;

            // Specify the interface class to receive notifications about.
            di.ClassGuid = classGuid;

            // Allocate memory for the buffer that holds the DEV_BROADCAST_DEVICEINTERFACE structure.

            devBroadcastDeviceInterfaceBuffer = Marshal.AllocHGlobal(size);

            // Copy the DEV_BROADCAST_DEVICEINTERFACE structure to the buffer.
            // Set fDeleteOld True to prevent memory leaks.

            Marshal.StructureToPtr(di, devBroadcastDeviceInterfaceBuffer, true);

            // RegisterDeviceNotification : USB장치에 대해 추가정보(이름 정보 등)를 가져오도록 등록
            deviceNotificationHandle = RegisterDeviceNotification(handle, devBroadcastDeviceInterfaceBuffer, DEVICE_NOTIFY_WINDOW_HANDLE);
            deviceHandle = deviceNotificationHandle;

            if (deviceNotificationHandle != IntPtr.Zero)
            {
                return true;
            }
            return false;
        }

        public void Destory()
        {
            Application.RemoveMessageFilter(MessageFilter);
            MessageFilter = null;
        }

        public void Process(ref Message m)
        {
            if (m.Msg == WM_DEVICECHANGE)
            {
                switch (m.WParam.ToInt32())
                {
                    case DBT_DEVICEARRIVAL:
                    case DBT_DEVICEREMOVECOMPLETE:
                        DEV_BROADCAST_DEVICEINTERFACE di;
                        di = (DEV_BROADCAST_DEVICEINTERFACE)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_DEVICEINTERFACE));

                        if (di.Name != null && di.Name.Length > 0)
                        {
                            string deviceName = new string(di.Name).Trim();
                            deviceName = deviceName.Replace("\0", String.Empty);
                            if (deviceName.Contains(Vid) && deviceName.Contains(Pid))
                            {
                                if (m.WParam.ToInt32() == DBT_DEVICEARRIVAL)
                                {
                                    // 장치 연결
                                }
                                else if (m.WParam.ToInt32() == DBT_DEVICEREMOVECOMPLETE)
                                {
                                    // 장치 연결 해제
                                }
                            }
                            break;
                        }
                        break;
                }
            }
        }
    }
}
