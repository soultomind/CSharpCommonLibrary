using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommonLibrary.Extensions;

namespace CommonLibrary.UI
{
    public partial class ScreenIndexDialog : Form
    {
        /// <summary>
        /// 스크린 인덱스 정보 출력 
        /// </summary>
        public ShowScreenIndex ShowScreenIndex { get; set; } = ShowScreenIndex.Normal;

        public const int LocationOffSet = 50;

        /// <summary>
        /// 스크린 다이얼로그 활성화되는 기본 위치에서 추가적으로 더 이동시키는 오프셋
        /// </summary>
        public readonly int OffSet;

        /// <summary>
        /// 스크린(디스플레이) 이름
        /// </summary>
        public string ScreenName { get; private set; } = String.Empty;
        /// <summary>
        /// 스크린 인덱스
        /// <para><see cref="System.Windows.Forms.Screen.AllScreens"/> 배열에서 특정 요소 인덱스를 뜻합니다.</para>
        /// </summary>
        public int ScreenIndex { get; private set; } = -1;

        /// <summary>
        /// 스크린 전체 영역
        /// <para><see cref="System.Windows.Forms.Screen.AllScreens"/>배열에서 특정 요소 크기를 뜻합니다.</para>
        /// </summary>
        public Rectangle ScreenBounds { get; private set; } = Rectangle.Empty;

        /// <summary>
        /// 스크린 작업표시줄 제외한 영역
        /// <para><see cref="System.Windows.Forms.Screen.AllScreens"/>배열에서 특정 요소 크기를 뜻합니다.</para>
        /// </summary>
        public Rectangle ScreenWorkingArea { get; private set; } = Rectangle.Empty;

        /// <summary>
        /// 스크린 정보
        /// </summary>
        public Screen Screen { get; private set; }

        /// <summary>
        /// 창이 종료되는 초
        /// <para>기본값=3초</para>
        /// </summary>
        public int CloseTimeSeconds
        {
            get { return closeTimeSeconds; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }

                if (value > 0)
                {
                    closeTimeSeconds = value;
                }
            }
        }
        private int closeTimeSeconds = 3;

        private int currentTimeSeconds = 1;

        public AspectRatio AspectRatio { get; private set; }

        internal ScreenIndexDialog(int screenIndex, Screen screen, ScreenIndexDialogColor dialogColor)
            : this(screenIndex, screen, dialogColor, LocationOffSet)
        {

        }

        internal ScreenIndexDialog(int screenIndex, Screen screen, ScreenIndexDialogColor dialogColor, int locationOffSet)
        {
            InitializeComponent();

            InitializeScreen(String.Empty, screenIndex, screen);

            InitializeScreenComponentColor(dialogColor);

            OffSet = locationOffSet;
            Location = new Point(ScreenBounds.Location.X + OffSet, ScreenBounds.Location.Y + OffSet);

            timer.Start();
        }

        /// <summary>
        /// 컬러정보 까지 받는 생성자
        /// </summary>
        /// <param name="screenName"></param>
        /// <param name="screenIndex">스크린 인덱스</param>
        /// <param name="screenBounds">스크린 전체 영역</param>
        /// <param name="dialogColor">다이얼로그 컬러정보</param>
        internal ScreenIndexDialog(string screenName, int screenIndex, Screen screen, ScreenIndexDialogColor dialogColor)
            : this(screenName, screenIndex, screen, dialogColor, LocationOffSet)
        {

        }

        /// <summary>
        /// 컬러정보 까지 받는 생성자
        /// </summary>
        /// <param name="screenName"></param>
        /// <param name="screenIndex">스크린 인덱스</param>
        /// <param name="screenBounds">스크린 전체 영역</param>
        /// <param name="dialogColor">다이얼로그 컬러정보</param>
        internal ScreenIndexDialog(string screenName, int screenIndex, Screen screen, ScreenIndexDialogColor dialogColor, int locationOffSet)
        {
            InitializeComponent();

            InitializeScreen(screenName, screenIndex, screen);

            InitializeScreenComponentColor(dialogColor);

            OffSet = locationOffSet;
            Location = new Point(ScreenBounds.Location.X + OffSet, ScreenBounds.Location.Y + OffSet);

            timer.Start();
        }



        public static ScreenIndexDialog NewNormal(int screenIndex, Screen screen, ScreenIndexDialogColor dialogColor)
        {
            ScreenIndexDialog dialog = new ScreenIndexDialog(screenIndex, screen, dialogColor);
            return dialog;
        }

        public static ScreenIndexDialog NewNormal(int screenIndex, Screen screen, ScreenIndexDialogColor dialogColor, int locationOffSet)
        {
            ScreenIndexDialog dialog = new ScreenIndexDialog(screenIndex, screen, dialogColor, locationOffSet);
            return dialog;
        }

        public static ScreenIndexDialog NewNormal(string screenName, int screenIndex, Screen screen, ScreenIndexDialogColor dialogColor)
        {
            ScreenIndexDialog dialog = new ScreenIndexDialog(screenName, screenIndex, screen, dialogColor);
            return dialog;
        }

        public static ScreenIndexDialog NewNormal(string screenName, int screenIndex, Screen screen, ScreenIndexDialogColor dialogColor, int locationOffSet)
        {
            ScreenIndexDialog dialog = new ScreenIndexDialog(screenName, screenIndex, screen, dialogColor, locationOffSet);
            return dialog;
        }

        public static ScreenIndexDialog NewAsscending(int screenIndex, Screen screen, ScreenIndexDialogColor dialogColor)
        {
            ScreenIndexDialog dialog = new ScreenIndexDialog(screenIndex, screen, dialogColor);
            dialog.ShowScreenIndex = ShowScreenIndex.Asscending;
            return dialog;
        }

        public static ScreenIndexDialog NewAsscending(int screenIndex, Screen screen, ScreenIndexDialogColor dialogColor, int locationOffSet)
        {
            ScreenIndexDialog dialog = new ScreenIndexDialog(screenIndex, screen, dialogColor, locationOffSet);
            dialog.ShowScreenIndex = ShowScreenIndex.Asscending;
            return dialog;
        }

        public static ScreenIndexDialog NewAsscending(string screenName, int screenIndex, Screen screen, ScreenIndexDialogColor dialogColor)
        {
            ScreenIndexDialog dialog = new ScreenIndexDialog(screenName, screenIndex, screen, dialogColor);
            dialog.ShowScreenIndex = ShowScreenIndex.Asscending;
            return dialog;
        }

        public static ScreenIndexDialog NewAsscending(string screenName, int screenIndex, Screen screen, ScreenIndexDialogColor dialogColor, int locationOffSet)
        {
            ScreenIndexDialog dialog = new ScreenIndexDialog(screenName, screenIndex, screen, dialogColor, locationOffSet);
            dialog.ShowScreenIndex = ShowScreenIndex.Asscending;
            return dialog;
        }

        private void InitializeScreen(string screenName, int screenIndex, Screen screen)
        {
            ScreenName = screenName;
            if (String.IsNullOrEmpty(ScreenName))
            {
                labelScreenName.Visible = false;
            }

            ScreenIndex = screenIndex;
            AspectRatio = screen.ToAspectRatio();
            ScreenBounds = screen.Bounds;
            ScreenWorkingArea = screen.WorkingArea;
            Screen = screen;

            labelScreenName.Text = screenName;
            labelScreenIndex.Text = (ScreenIndex + 1).ToString();
            labelScreenAspectRatio.Text = AspectRatio.ToString();
            labelScreenBounds.Text = ScreenBounds.ToString();
            labelScreenWorkingArea.Text = ScreenWorkingArea.ToString();
        }

        private void InitializeScreenComponentColor(ScreenIndexDialogColor dialogColor)
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            BackColor = dialogColor.ScreenDlgBackColor;

            labelScreenName.BackColor = dialogColor.ScreenNameLabelBackColor;
            labelScreenName.ForeColor = dialogColor.ScreenNameLabelForeColor;

            labelScreenIndexEx.BackColor = dialogColor.ScreenIndexExLabelBackColor;
            labelScreenIndexEx.ForeColor = dialogColor.ScreenIndexExLabelForeColor;

            labelScreenIndex.BackColor = dialogColor.ScreenIndexLabelBackColor;
            labelScreenIndex.ForeColor = dialogColor.ScreenIndexLabelForeColor;

            labelScreenAspectRatio.BackColor = dialogColor.ScreenAspectRatioLabelBackColor;
            labelScreenAspectRatio.ForeColor = dialogColor.ScreenAspectRatioLabelForeColor;

            labelScreenBounds.BackColor = dialogColor.ScreenBoundsLabelBackColor;
            labelScreenBounds.ForeColor = dialogColor.ScreenBoundsLabelForeColor;

            labelScreenWorkingArea.BackColor = dialogColor.ScreenBoundsLabelBackColor;
            labelScreenWorkingArea.ForeColor = dialogColor.ScreenWorkingAreaLabelForeColor;
        }

        public Label LabelScreenName
        {
            get { return labelScreenName; }
        }

        public Label LabelScreenIndexEx
        {
            get { return labelScreenIndexEx; }
        }

        public Label LabelScreenIndex
        {
            get { return labelScreenIndex; }
        }

        public Label LabelScreenAspectRatio
        {
            get { return labelScreenAspectRatio; }
        }

        public Label LabelScreenBounds
        {
            get { return labelScreenBounds; }
        }

        public Label LabelScreenWorkingArea
        {
            get { return labelScreenWorkingArea; }
        }

        private void ScreenIndexDialog_Load(object sender, EventArgs e)
        {
            if (ShowScreenIndex == ShowScreenIndex.Asscending)
            {
                LabelScreenIndexEx.Visible = true;
                LabelScreenIndexEx.Text = (ScreenManager.GetIndexByScreen(Screen) + 1).ToString();
            }
        }

        private void ScreenIndexDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (CloseTimeSeconds == currentTimeSeconds)
            {
                Close();
            }

            currentTimeSeconds++;
        }
    }
}
