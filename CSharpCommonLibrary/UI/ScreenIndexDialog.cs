using CommonLibrary.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CommonLibrary.UI
{
    /// <summary>
    /// <see cref="System.Windows.Forms.Screen"/> 인덱스 출력 다이얼로그
    /// </summary>
    public partial class ScreenIndexDialog : Form
    {
        public int ScreenIndex
        {
            get { return _screenIndex; }
            private set { _screenIndex = value; }
        }
        private int _screenIndex;

        /// <summary>
        /// 다이얼로그가 보여지는 시간(초)
        /// </summary>
        public int ShowPositionSecond
        {
            get { return _showPositionSecond; }
            set
            {
                if (6 > value && value > 0)
                {
                    _showPositionSecond = value;
                }
                else
                {
                    throw new ArgumentException("Usage Value 1 - 5");
                }
            }
        }
        private int _showPositionSecond = 3;

        private int _currentShowPositionSecond = 0;

        public ScreenIndexDialogColor DialogColor { get; set; } = new ScreenIndexDialogColor();

        /// <summary>
        /// 생성자 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="rectangle"></param>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="index"/>값이 0보다 작거나 <see cref="System.Windows.Forms.Screen.AllScreens"/>.Length 값과 같거나 클때
        /// </exception>
        public ScreenIndexDialog(int index, Rectangle rectangle)
        {
            if (index < 0 || index > Screen.AllScreens.Length)
            {
                throw new ArgumentException(nameof(index));
            }

            InitializeComponent();

            Location = rectangle.Location;

            ScreenIndex = index;

            _LabelScreenIndex.Text = "" + index;
            _LabelBounds.Text = rectangle.ToString();

            SetColorInfo(DialogColor, index);

            _TimerShowPosition.Start();
        }

        /// <summary>
        /// 생성자 
        /// <para><paramref name="index"/> 값은 1부터 시작입니다.</para>
        /// </summary>
        /// <param name="index"></param>
        /// <param name="rectangle"></param>
        /// <param name="colorInfo">스크린 컬러정보</param>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="index"/>값이 0보다 작거나 <see cref="System.Windows.Forms.Screen.AllScreens"/>.Length 값과 같거나 클때
        /// </exception>
        public ScreenIndexDialog(int index, Rectangle rectangle, ScreenIndexDialogColor colorInfo)
        {
            if (index < 0 || index > Screen.AllScreens.Length)
            {
                throw new ArgumentException(nameof(index));
            }

            InitializeComponent();

            Location = rectangle.Location;

            ScreenIndex = index;

            _LabelScreenIndex.Text = (index + 1).ToString();
            _LabelBounds.Text = rectangle.ToString();

            SetColorInfo(colorInfo, index);

            _TimerShowPosition.Start();
        }

        private void SetColorInfo(ScreenIndexDialogColor dialogColor, int index)
        {
            DialogColor = dialogColor;
            if (IsMainScreen(index))
            {
                _LabelBounds.BackColor = dialogColor.MainBackColor;
                _LabelBounds.ForeColor = dialogColor.MainForeColor;
                _LabelScreenIndex.BackColor = dialogColor.MainBackColor;
                _LabelScreenIndex.ForeColor = dialogColor.MainForeColor;
            }
            else
            {
                _LabelBounds.BackColor = dialogColor.SubBackColor;
                _LabelBounds.ForeColor = dialogColor.SubForeColor;
                _LabelScreenIndex.BackColor = dialogColor.SubBackColor;
                _LabelScreenIndex.ForeColor = dialogColor.SubForeColor;
            }
        }

        private void ShowPositionTimer_Tick(object sender, EventArgs e)
        {
            _currentShowPositionSecond++;
            if (_currentShowPositionSecond == _showPositionSecond)
            {
                _TimerShowPosition.Stop();
                this.Dispose();
            }
        }

        /// <summary>
        /// 주 모니터 스크린 여부값을 반환합니다.
        /// <para><paramref name="index"/> 값은 1부터 시작합니다.</para>
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsMainScreen(int index)
        {
            return index == (ScreenUtility.GetPrimaryScreenIndex() + 1);
        }

        /// <summary>
        /// 디스플레이 번호 표현 라벨
        /// </summary>
        public Label LabelScreenIndex
        {
            get { return _LabelScreenIndex; }
        }

        /// <summary>
        /// 디스플레이 위치 및 크기 표현 라벨
        /// </summary>
        public Label LabelBounds
        {
            get { return _LabelBounds; }
        }
    }

    /// <summary>
    /// 스크린 컬러 정보
    /// </summary>
    public class ScreenIndexDialogColor
    {
        /// <summary>
        /// 메인 스크린(주모니터) 배경 색
        /// </summary>
        public Color MainBackColor { get; set; } = Color.Red;
        /// <summary>
        /// 메인 스크린(주모니터) 글자 색
        /// </summary>
        public Color MainForeColor { get; set; } = Color.White;

        /// <summary>
        /// 서브 스크린 배경 색
        /// </summary>
        public Color SubBackColor { get; set; } = Color.DarkGray;
        /// <summary>
        /// 서브 스크린 글자 색
        /// </summary>
        public Color SubForeColor { get; set; } = Color.White;
    }
}
