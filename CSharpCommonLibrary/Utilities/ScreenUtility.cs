using System.Collections.Generic;
using System.Windows.Forms;

namespace CommonLibrary.Utilities
{
    public class ScreenUtility
    {
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
    }
}
