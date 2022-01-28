using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CommonLibrary.Utilities
{
    /// <summary>
    /// 스크린(디스플레이) 관련 유틸
    /// </summary>
    public class ScreenUtility
    {
        private static Screen[] _AsscendingScreens;

        /// <summary>
        /// X 좌표순으로 오름차순 정렬된 스크린 배열
        /// </summary>
        public static Screen[] AsscendingScreens
        {
            get
            {
                if (_AsscendingScreens == null)
                {
                    List<Screen> list = new List<Screen>();
                    list.AddRange(Screen.AllScreens);

                    Screen temp = null;
                    for (int i = 0; i < list.Count - 1; i++)
                    {
                        for (int j = i + 1; j < list.Count; j++)
                        {
                            if (list[i].WorkingArea.X > list[j].WorkingArea.X)
                            {
                                temp = list[i];
                                list[i] = list[j];
                                list[j] = temp;
                            }
                        }
                    }

                    _AsscendingScreens = list.ToArray();
                }

                return _AsscendingScreens;
            }
        }

        /// <summary>
        /// <paramref name="screenIndex"/> 값이 유효한 인덱스 값인지 여부를 반환합니다.
        /// </summary>
        /// <param name="screenIndex"></param>
        /// <returns></returns>
        public static bool IsValidIndex(int screenIndex)
        {
            if ((screenIndex >= 0) &&
                (Screen.AllScreens.Length > screenIndex))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 주 모니터 디스플레이 인덱스 값을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static int GetPrimaryScreenIndex()
        {
            int primaryScreenIndex = 0;
            for (int index = 0; index < Screen.AllScreens.Length; index++)
            {
                if (Screen.AllScreens[index].Equals(Screen.PrimaryScreen))
                {
                    primaryScreenIndex = index;
                    break;
                }
            }
            return primaryScreenIndex;
        }

        /// <summary>
        /// 주 모니터를 제외한 첫번째 디스플레이 인덱스 값을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static int GetFirstScreenIndexAndExceptPrimaryScreen()
        {
            int targetScreenIndex = 0;
            for (int index = 0; index < Screen.AllScreens.Length; index++)
            {
                if (!Screen.AllScreens[index].Equals(Screen.PrimaryScreen))
                {
                    targetScreenIndex = index;
                    break;
                }
            }
            return targetScreenIndex;
        }

        /// <summary>
        /// 사이즈와 같은 작업표시줄 제외한 스크린 사이즈가 있는지 여부
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static bool EqualsScreenWorkingAreaSize(Size size)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Width == size.Width &&
                    screen.WorkingArea.Height == size.Height)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 사이즈와 같은 작업표시줄 포함한 스크린 사이즈가 있는지 여부
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static bool EqualsScreenBoundsSize(Size size)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Bounds.Width == size.Width &&
                    screen.Bounds.Height == size.Height)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// <paramref name="point"/>가 위치해 있는 스크린(디스플레이) 값을 반환합니다.
        /// <para>못 찾을 경우 Null 반환합니다.</para>
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Screen ContainsPointScreenBounds(Point point)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Bounds.Contains(point))
                {
                    return screen;
                }
            }
            return null;
        }

        /// <summary>
        /// <paramref name="targetScreenIndex"/> 값에 해당하는 스크린(디스플레이) 값을 반환합니다.
        /// <para>못 찾을 경우 Null 반환합니다.</para>
        /// </summary>
        /// <param name="targetScreenIndex"></param>
        /// <returns></returns>
        public static Screen GetTargetScreen(int targetScreenIndex)
        {
            for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
            {
                if (targetScreenIndex == screenIndex)
                {
                    return Screen.AllScreens[screenIndex];
                }
            }
            return null;
        }

        /// <summary>
        /// <paramref name="targetResolutionDisplayBoundsSize"/> 값과 같은 스크린(디스플레이) 값을 반환합니다.
        /// <para>못 찾을 경우 Null 반환합니다.</para>
        /// </summary>
        /// <param name="targetResolutionDisplayBoundsSize"></param>
        /// <returns></returns>
        public static Screen GetTargetScreen(Size targetResolutionDisplayBoundsSize)
        {
            for (int screenIndex = 0; screenIndex < Screen.AllScreens.Length; screenIndex++)
            {
                Screen screen = Screen.AllScreens[screenIndex];
                if (screen.Bounds.Width == targetResolutionDisplayBoundsSize.Width &&
                    screen.Bounds.Height == targetResolutionDisplayBoundsSize.Height)
                {
                    return Screen.AllScreens[screenIndex];
                }
            }
            return null;
        }
    }
}
