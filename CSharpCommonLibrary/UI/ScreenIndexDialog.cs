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
            if (index < 0 || index >= Screen.AllScreens.Length)
            {
                throw new ArgumentException(nameof(index));
            }

            InitializeComponent();

            Location = rectangle.Location;

            _LabelScreenIndex.Text = "" + index;
            _LabelBounds.Text = rectangle.ToString();

            _TimerShowPosition.Start();
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

        private bool IsPrimaryScreen(int index)
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
}
