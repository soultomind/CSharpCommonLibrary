using CommonLibrary.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonLibrary
{
    public class ScreenManager
    {
        public const int DefaultCloseTimeoutSecond = 5;

        /// <summary>
        /// 유효하지 않는 스크린 인덱스
        /// </summary>
        public const int InvalidSceenIndex = -1;

        /// <summary>
        /// 현재 스크린 수
        /// <para><see cref="Screen.AllScreens"/>.Length</para>
        /// </summary>
        public static int AllScreenLength
        {
            get { return Screen.AllScreens.Length; }
        }

        /// <summary>
        /// 현재 스크린 수가 하나인지 여부를 나타냅니다.
        /// </summary>
        public static bool SingleScreen
        {
            get { return 1 == Screen.AllScreens.Length; }
        }

        /// <summary>
        /// <see cref="Screen.AllScreens"/> 배열을 반환합니다.
        /// </summary>
        public static Screen[] NormalScreens
        {
            get { return Screen.AllScreens; }
        }

        /// <summary>
        /// X 좌표순으로 오름차순 정렬된 <see cref="Screen"/> 배열을 반환합니다.
        /// </summary>
        public static Screen[] AsscendingScreens
        {
            get
            {
                List<Screen> list = new List<Screen>();
                list.AddRange(Screen.AllScreens);

                int count = list.Count;
                Screen temp = null;
                for (int i = 0; i < count - 1; i++)
                {
                    for (int j = i + 1; j < count; j++)
                    {
                        if (list[i].WorkingArea.X > list[j].WorkingArea.X)
                        {
                            temp = list[i];
                            list[i] = list[j];
                            list[j] = temp;
                        }
                    }
                }
                return list.ToArray();
            }
        }

        /// <summary>
        /// <paramref name="screenIndex"/> 값이 유효한 인덱스 값인지 여부를 반환합니다.
        /// </summary>
        /// <param name="screenIndex"></param>
        /// <returns></returns>
        public static bool IsValidIndex(int screenIndex)
        {
            int length = AllScreenLength;
            if (screenIndex >= 0 && length > screenIndex)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 주 모니터 디스플레이 인덱스 값을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static int GetPrimaryIndex()
        {
            int primaryScreenIndex = GetIndexByScreen(Screen.PrimaryScreen);
            return primaryScreenIndex;
        }

        /// <summary>
        /// <paramref name="screen"/>에 인덱스값을 반환합니다.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static int GetIndexByScreen(Screen screen)
        {
            int screenIndex = InvalidSceenIndex;

            Screen[] screens = NormalScreens;
            int length = screens.Length;
            for (int index = 0; index < length; index++)
            {
                if (screen.Equals(screens[index]))
                {
                    screenIndex = index;
                    break;
                }
            }

            return screenIndex;
        }

        /// <summary>
        /// <paramref name="screenIndex"/> 값에 해당하는 <see cref="Screen"/> 을 반환합니다.
        /// </summary>
        /// <param name="screenIndex"></param>
        /// <returns></returns>
        public static Screen GetScreenByIndex(int screenIndex)
        {
            if (screenIndex == InvalidSceenIndex)
            {
                throw new ArgumentException(nameof(screenIndex) + " == " + nameof(InvalidSceenIndex));
            }

            if (!IsValidIndex(screenIndex))
            {
                throw new ArgumentException(nameof(screenIndex));
            }

            Screen screen = null;

            Screen[] screens = NormalScreens;
            int length = screens.Length;
            for (int i = 0; i < length; i++)
            {
                if (i == screenIndex)
                {
                    screen = screens[i];
                    break;
                }
            }
            return screen;
        }

        /// <summary>
        /// <paramref name="screens"/> 배열에서 <paramref name="screen"/> 오브젝트의 인덱스 값을 반환합니다.
        /// </summary>
        /// <param name="screens"></param>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static int GetIndexByScreensInScreen(Screen[] screens, Screen screen)
        {
            int screenIndex = InvalidSceenIndex;

            int length = screens.Length;
            for (int index = 0; index < length; index++)
            {
                if (screens.Equals(screen))
                {
                    screenIndex = index;
                    break;
                }
            }

            return screenIndex;
        }

        /// <summary>
        /// <paramref name="screenIndex"/> 값이 정렬된 스크린 기준으로 첫번째 스크린인지 마지막 스크린인지 여부를 나타냅니다.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static bool IsAsscendingScreenFirstOrLastScreen(int screenIndex)
        {
            Screen screen = GetScreenByIndex(screenIndex);
            return IsAsscendingScreenFirstOrLastScreen(screen);
        }

        /// <summary>
        /// <paramref name="screen"/> 값이 정렬된 스크린 기준으로 첫번째 스크린인지 마지막 스크린인지 여부를 나타냅니다.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static bool IsAsscendingScreenFirstOrLastScreen(Screen screen)
        {
            Screen[] asscendingScreens = AsscendingScreens;
            Screen leftScreen = asscendingScreens[0];
            Screen rightScreen = asscendingScreens[asscendingScreens.Length - 1];
            if (leftScreen.Equals(screen) || rightScreen.Equals(screen))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// <paramref name="thisScreen"/> 스크린 값과 <paramref name="otherScreen"/> 스크린 값이 서로 이웃하고 있는지 여부를 나타냅니다.
        /// </summary>
        /// <param name="thisScreen"></param>
        /// <param name="otherScreen"></param>
        /// <returns></returns>
        public static bool IsLeftOrRightNearScreen(Screen thisScreen, Screen otherScreen)
        {
            if (IsLeftScreen(thisScreen, otherScreen))
            {
                if (thisScreen.Bounds.Left == (otherScreen.Bounds.Left + otherScreen.Bounds.Width))
                {
                    return true;
                }
                return false;
            }
            else
            {
                if ((thisScreen.Bounds.Left + thisScreen.Bounds.Width) == otherScreen.Bounds.Left)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// <paramref name="thisScreen"/> 스크린 값이 <paramref name="otherScreen"/> 스크린 값보다 오른쪽에 있는지 여부를 나타냅니다.
        /// </summary>
        /// <param name="thisScreen"></param>
        /// <param name="otherScreen"></param>
        /// <returns></returns>
        public static bool IsRightScreen(Screen thisScreen, Screen otherScreen)
        {
            if (thisScreen.Bounds.Left < otherScreen.Bounds.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// <paramref name="thisScreen"/> 스크린 값이 <paramref name="otherScreen"/> 스크린 값보다 왼쪽에 있는지 여부를 나타냅니다.
        /// </summary>
        /// <param name="thisScreen">기준이 되는 스크린</param>
        /// <param name="otherScreen">다른 스크린</param>
        /// <returns></returns>
        public static bool IsLeftScreen(Screen thisScreen, Screen otherScreen)
        {
            if (thisScreen.Bounds.Left > otherScreen.Bounds.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// <see cref="AsscendingScreens"/> 오름차순 정렬된 스크린에서 <paramref name="screen"/> 이 가장 첫번째 스크린인지 여부를 나타냅니다.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static bool IsFirstScreen(Screen screen)
        {
            Screen[] screens = AsscendingScreens;
            return screens[0].Equals(screen);
        }

        /// <summary>
        /// <see cref="AsscendingScreens"/> 오름차순 정렬된 스크린에서 <paramref name="screen"/> 이 가장 마지막 스크린인지 여부를 나타냅니다.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static bool IsLastScreen(Screen screen)
        {
            Screen[] screens = AsscendingScreens;
            return screens[screens.Length - 1].Equals(screen);
        }

        /// <summary>
        /// 스크린 정보를 출력하는 다이얼로그를 보여줍니다. (정렬이 안된 상태)
        /// </summary>
        /// <param name="closeTimeSeconds">다이얼로그 종료 시간(초단위)</param>
        public static void ShowNormalScreensDlg(int closeTimeSeconds = 3)
        {
            if (closeTimeSeconds <= 0)
            {
                throw new ArgumentNullException(nameof(closeTimeSeconds));
            }

            Screen[] screens = NormalScreens;
            int length = screens.Length;
            for (int screenIndex = 0; screenIndex < length; screenIndex++)
            {
                Screen screen = screens[screenIndex];

                ScreenIndexDialog dlg = null;
                if (screen.Primary)
                {
                    dlg = ScreenIndexDialog.NewNormal(screenIndex, screen, ScreenIndexDialogColor.Primary);
                }
                else
                {
                    dlg = ScreenIndexDialog.NewNormal(screenIndex, screen, ScreenIndexDialogColor.Default);
                }
                dlg.CloseTimeSeconds = closeTimeSeconds;
                dlg.Show();
            }
        }

        public static void ShowAsscendingScreensDlg(int closeTimeSeconds = 3)
        {
            if (closeTimeSeconds <= 0)
            {
                throw new ArgumentNullException(nameof(closeTimeSeconds));
            }

            Screen[] screens = AsscendingScreens;
            int length = screens.Length;
            for (int screenIndex = 0; screenIndex < length; screenIndex++)
            {
                Screen screen = screens[screenIndex];

                ScreenIndexDialog dlg = null;
                if (screen.Primary)
                {
                    dlg = ScreenIndexDialog.NewAsscending(screenIndex, screen, ScreenIndexDialogColor.Primary);
                }
                else
                {
                    dlg = ScreenIndexDialog.NewAsscending(screenIndex, screen, ScreenIndexDialogColor.Default);
                }

                dlg.CloseTimeSeconds = closeTimeSeconds;
                dlg.Show();
            }
        }
    }
}
