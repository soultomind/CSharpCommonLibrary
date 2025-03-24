using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class ScreenManager
    {
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
    }
}
