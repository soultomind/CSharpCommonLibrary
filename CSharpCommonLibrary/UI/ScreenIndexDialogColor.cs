using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.UI
{
    public class ScreenIndexDialogColor
    {
        /// <summary>
        /// 기본 컬러정보
        /// </summary>
        public static ScreenIndexDialogColor Default;

        /// <summary>
        /// 주스크린 컬러정보
        /// </summary>
        public static ScreenIndexDialogColor Primary;

        static ScreenIndexDialogColor()
        {
            Default = new ScreenIndexDialogColor();

            Primary = new ScreenIndexDialogColor();
            Primary.SetAllColor(Color.FromArgb(255, 252, 94, 32));
        }

        private ScreenIndexDialogColor() { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="screenDlgBackColor"></param>
        /// <param name="screenIndexLabelBackColor"></param>
        /// <param name="screenIndexLabelForeColor"></param>
        /// <param name="screenBoundsLabelBackColor"></param>
        /// <param name="screenBoundsLabelForeColor"></param>
        public ScreenIndexDialogColor(Color screenDlgBackColor, Color screenIndexLabelBackColor, Color screenIndexLabelForeColor, Color screenBoundsLabelBackColor, Color screenBoundsLabelForeColor)
        {
            ScreenDlgBackColor = screenDlgBackColor;
            ScreenIndexLabelBackColor = screenIndexLabelBackColor;
            ScreenIndexLabelForeColor = screenIndexLabelForeColor;

            ScreenBoundsLabelBackColor = screenBoundsLabelBackColor;
            ScreenBoundsLabelForeColor = screenBoundsLabelForeColor;
        }

        public void SetAllColor(Color color)
        {
            ScreenDlgBackColor = color;
            ScreenIndexExLabelBackColor = color;
            ScreenIndexLabelBackColor = color;
            ScreenAspectRatioLabelBackColor = color;
            ScreenBoundsLabelBackColor = color;
        }

        /// <summary>
        /// 스크린 인덱스 다이얼로그 백그라운드 컬러
        /// </summary>
        public Color ScreenDlgBackColor { get; private set; } = Color.FromArgb(255, 17, 17, 17);

        /// <summary>
        /// 스크린(디스플레이) 이름 라벨 백그라운드 컬러
        /// </summary>
        public Color ScreenNameLabelBackColor { get; private set; } = Color.FromArgb(255, 17, 17, 17);

        /// <summary>
        /// 스크린(디스플레이) 이름 라벨 폰트 컬러
        /// </summary>
        public Color ScreenNameLabelForeColor { get; private set; } = Color.White;

        /// <summary>
        /// 스크린 인덱스 확장(정렬된 상태로 보여줄때 기존 인덱스 정보) 라벨 백그라운드 컬러
        /// </summary>
        public Color ScreenIndexExLabelBackColor { get; private set; } = Color.FromArgb(255, 17, 17, 17);

        /// <summary>
        /// 스크린 인덱스 확장(정렬된 상태로 보여줄때 기존 인덱스 정보) 라벨 폰트 컬러
        /// </summary>
        public Color ScreenIndexExLabelForeColor { get; private set; } = Color.White;

        /// <summary>
        /// 스크린 인덱스 라벨 백그라운드 컬러
        /// </summary>
        public Color ScreenIndexLabelBackColor { get; private set; } = Color.FromArgb(255, 17, 17, 17);

        /// <summary>
        /// 스크린 인덱스 라벨 폰트 컬러
        /// </summary>
        public Color ScreenIndexLabelForeColor { get; private set; } = Color.White;

        /// <summary>
        /// 스크린 화면비율 라벨 백그라운드 컬러
        /// </summary>
        public Color ScreenAspectRatioLabelBackColor { get; private set; } = Color.FromArgb(255, 17, 17, 17);

        /// <summary>
        /// 스크린 화면비율 라벨 폰트 컬러
        /// </summary>
        public Color ScreenAspectRatioLabelForeColor { get; private set; } = Color.White;

        /// <summary>
        /// 스크린 영역 전체 크기 라벨 백그라운드 컬러
        /// </summary>
        public Color ScreenBoundsLabelBackColor { get; private set; } = Color.FromArgb(255, 17, 17, 17);

        /// <summary>
        /// 스크린 영역 전체 크기 라벨 폰트 컬러
        /// </summary>
        public Color ScreenBoundsLabelForeColor { get; private set; } = Color.White;

        /// <summary>
        /// 스크린 작업표시줄 제외한 영역 전체 크기 라벨 백그라운드 컬러
        /// </summary>
        public Color ScreenWorkingAreaLabelBackColor { get; private set; } = Color.FromArgb(255, 17, 17, 17);

        /// <summary>
        /// 스크린 작업표시줄 제외한 영역 전체 크기 라벨 폰트 컬러
        /// </summary>
        public Color ScreenWorkingAreaLabelForeColor { get; private set; } = Color.White;
    }
}
