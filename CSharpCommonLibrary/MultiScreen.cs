﻿using CommonLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary
{
    /// <summary>
    /// <see cref="System.Windows.Forms.Screen"/> 관련 모듈 클래스
    /// <para>듀얼 모니터 이상일 때 사용합니다.</para>
    /// </summary>
    public class MultiScreen
    {
        public const int InvalidScreenIndex = -1;
        /// <summary>
        /// 타겟이 될 <see cref="System.Windows.Forms.Screen"/> 인덱스
        /// </summary>
        public int TargetScreenIndex { get; set; }
        /// <summary>
        /// 타겟이 될 <see cref="System.Windows.Forms.Screen"/> 스크린 Bounds 사이즈
        /// </summary>
        public Size TargetScreenBoundsSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetScreenIndex">타겟이 되는 스크린 인덱스</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        public MultiScreen(int targetScreenIndex)
        {
            if (Screen.AllScreens.Length == 1)
            {
                throw new InvalidOperationException("Screen.AllScreens.Length==1");
            }

            if (!ScreenUtility.IsValidIndex(targetScreenIndex))
            {
                throw new ArgumentException(nameof(targetScreenIndex));
            }

            TargetScreenIndex = targetScreenIndex;
            TargetScreenBoundsSize = Size.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetScreenBoundsSize">타겟이 되는 스크린 사이즈(Bounds)</param>
        public MultiScreen(Size targetScreenBoundsSize)
        {
            if (Screen.AllScreens.Length == 1)
            {
                throw new InvalidOperationException("Screen.AllScreens.Length==1");
            }

            if (!ScreenUtility.EqualsScreenBoundsSize(targetScreenBoundsSize))
            {
                throw new ArgumentException(nameof(targetScreenBoundsSize));
            }

            TargetScreenBoundsSize = targetScreenBoundsSize;
            TargetScreenIndex = InvalidScreenIndex;
        }

        /// <summary>
        /// 타겟 스크린
        /// </summary>
        public Screen TargetScreen
        {
            get
            {
                Screen screen = null;
                if (TargetScreenIndex == InvalidScreenIndex)
                {
                    screen = ScreenUtility.GetTargetScreen(TargetScreenBoundsSize);
                }
                else
                {
                    screen = ScreenUtility.GetTargetScreen(TargetScreenIndex);
                }
                return screen;
            }
        }
    }
}
