using CommonLibrary.Win32;
using System;
using System.Globalization;

namespace CommonLibrary
{
    /// <summary>
    /// 작업표시줄 관련 클래스
    /// </summary>
    public class Taskbar
    {
        /// <summary>
        /// 트레이 영역을 새로고침 합니다.
        /// </summary>
        public static void RefreshTrayArea()
        {
            string notificationAreaHandleName = String.Empty;
            string userPromotedNotificationAreaHandleName = String.Empty;
            string overflowNotificationAreaHandleName = String.Empty;

            CultureInfo ci = CultureInfo.InstalledUICulture;
            if (ci.Name.Equals("ko-KR"))
            {
                notificationAreaHandleName = "지정 알림 영역";
                userPromotedNotificationAreaHandleName = "사용자 지정 알림 영역";
                overflowNotificationAreaHandleName = "오버플로 알림 영역";
            }
            else
            {
                notificationAreaHandleName = "Notification Area"; 
                userPromotedNotificationAreaHandleName = "User Promoted Notification Area";
                overflowNotificationAreaHandleName = "Overflow Notification Area";
            }

            IntPtr shellTrayWndHandle = User32.FindWindow("Shell_TrayWnd", null);
            IntPtr trayNotifyWndHandle = User32.FindWindowEx(shellTrayWndHandle, IntPtr.Zero, "TrayNotifyWnd", null);
            IntPtr sysPagerHandle = User32.FindWindowEx(trayNotifyWndHandle, IntPtr.Zero, "SysPager", null);
            IntPtr notificationAreaHandle = User32.FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", notificationAreaHandleName);
            if (notificationAreaHandle == IntPtr.Zero)
            {
                notificationAreaHandle = User32.FindWindowEx(sysPagerHandle, IntPtr.Zero, "ToolbarWindow32", userPromotedNotificationAreaHandleName);
                IntPtr notifyIconOverflowWindowHandle = User32.FindWindow("NotifyIconOverflowWindow", null);
                IntPtr overflowNotificationAreaHandle = User32.FindWindowEx(notifyIconOverflowWindowHandle, IntPtr.Zero, "ToolbarWindow32", overflowNotificationAreaHandleName);
                if (overflowNotificationAreaHandle != IntPtr.Zero)
                {
                    RefreshTrayArea(overflowNotificationAreaHandle);
                }
            }

            if (notificationAreaHandle != IntPtr.Zero)
            {
                RefreshTrayArea(notificationAreaHandle);
            }
        }

        private static void RefreshTrayArea(IntPtr windowHandle)
        {
            RECT rect;
            User32.GetClientRect(windowHandle, out rect);
            for (int x = 0; x < rect.Right; x += 5)
            {
                for (int y = 0; y < rect.Bottom; y += 5)
                {
                    User32.SendMessage(windowHandle, WinMessage.WM_MOUSEMOVE, 0, (y << 16) + x);
                }
            }
        }
    }
}
