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

        public ScreenIndexDialog(int index, Rectangle rectangle)
        {
            InitializeComponent();

            Location = rectangle.Location;

            _LabelScreenIndex.Text = "" + index;
            _LabelBounds.Text = rectangle.ToString();

            _TimerShowPosition.Start();
        }

        private bool IsPrimaryScreen(int index)
        {
            return index == (ScreenUtility.GetPrimaryScreenIndex() + 1);
        }

        public Label LabelScreenIndex
        {
            get { return _LabelScreenIndex; }
        }

        public Label LabelBounds
        {
            get { return _LabelBounds; }
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
    }
}
