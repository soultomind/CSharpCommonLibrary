using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CommonLibrary.Utilities
{
    public class ScreenUtility
    {
        public static int ScreenLength = Screen.AllScreens.Length;
        private static Screen[] _AsscendingScreens;
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

        public static bool IsValidIndex(int screenIndex)
        {
            if ((screenIndex >= 0) &&
                (Screen.AllScreens.Length > screenIndex))
            {
                return true;
            }

            return false;
        }

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

        public static Screen ContainsPointScreenWorkingArea(Point point)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Contains(point))
                {
                    return screen;
                }
            }
            return null;
        }

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
