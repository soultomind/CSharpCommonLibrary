using CommonLibrary.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CommonLibrary.UI
{
    public partial class ScreenPositionDlg : Form
    {
        public int ShowPositionSecond
        {
            get { return _showPositionSecond; }
            set
            {
                if (5 < value && value > 0)
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

        public ScreenPositionDlg(int index, Rectangle rectangle)
        {
            InitializeComponent();

            Location = rectangle.Location;

            _LabelScreenIndex.Text = "" + index;
            _LabelBounds.Text = rectangle.ToString();
            if (IsPrimaryScreen(index))
            {
                _LabelScreenIndex.BackColor = Color.Red;
                _LabelBounds.BackColor = Color.Red;
            }

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
