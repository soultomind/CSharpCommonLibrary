using CommonLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary
{
    public delegate void CreateScreenImageCaptureEventHandler(object sender, ScreenImageCaptureEventArgs e);
    public class ScreenImageCaptureEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }
        public Bitmap Bitmap { get; private set; }

        public ScreenImageCaptureEventArgs(Bitmap bitmap)
        {
            Bitmap = bitmap;
        }

        public ScreenImageCaptureEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
    /// <summary>
    /// <see cref="System.Windows.Forms.Screen"/> 이미지 캡쳐 클래스
    /// </summary>
    public class ScreenImageCapture
    {
        /// <summary>
        /// 유효하지 않는 스크린 인덱스
        /// </summary>
        public const int InvalidScreenIndex = -1;

        public event CreateScreenImageCaptureEventHandler CreateScreenImageCapture;
        /// <summary>
        /// <see cref="System.Windows.Forms.Screen"/> 인덱스
        /// </summary>
        public int TargetScreenIndex { get; set; }
        public Size TargetScreenBoundsSize { get; set; }
        public Screen TargetScreen { get; private set; }

        public bool IsStart
        {
            get { return _start; }
            private set { _start = value; }
        }
        private bool _start;

        private int _interval;
        private Timer _Timer;
        public ScreenImageCapture(int targetScreenIndex, int interval = 500)
        {
            if (!ScreenUtility.IsValidIndex(targetScreenIndex))
            {
                throw new ArgumentException(nameof(targetScreenIndex) + " 값 범위는 0-" + (Screen.AllScreens.Length - 1) + " 입니다.");
            }

            if (!(interval >= 500 && interval <= 3000))
            {
                throw new ArgumentException(nameof(interval) + " 값 범위는 500-3000 입니다.");
            }

            TargetScreenIndex = targetScreenIndex;
            TargetScreen = new MultiScreen(TargetScreenIndex).TargetScreen;
            if (TargetScreen == null)
            {
                throw new InvalidOperationException("TargetScreen 이 null 입니다.");
            }

            TargetScreenBoundsSize = Size.Empty;
            _interval = interval;
        }

        public ScreenImageCapture(Size targetScreenBoundsSize, int interval = 500)
        {
            if (!ScreenUtility.EqualsScreenBoundsSize(targetScreenBoundsSize))
            {
                throw new ArgumentException(nameof(targetScreenBoundsSize));
            }

            if (!(interval >= 500 && interval <= 3000))
            {
                throw new ArgumentException(nameof(interval) + " 값 범위는 500-3000 입니다.");
            }

            TargetScreenBoundsSize = targetScreenBoundsSize;
            TargetScreen = new MultiScreen(TargetScreenBoundsSize).TargetScreen;
            if (TargetScreen == null)
            {
                throw new InvalidOperationException("TargetScreen 이 null 입니다.");
            }

            TargetScreenIndex = InvalidScreenIndex;
            _interval = interval;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Rectangle bounds = TargetScreen.Bounds;
                Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
                }

                CreateScreenImageCapture?.Invoke(this, new ScreenImageCaptureEventArgs(bitmap));
            }
            catch (Exception ex)
            {
                CreateScreenImageCapture?.Invoke(this, new ScreenImageCaptureEventArgs(ex));
            }
        }

        /// <summary>
        /// 리소스를 닫습니다.
        /// <para>타이머를 다시 시작하려면 <see cref="CommonLibrary.ScreenImageCapture.Start"/> 메서드를 호출합니다.</para>
        /// </summary>
        public void Close()
        {
            Stop();
            _Timer = null;
        }

        /// <summary>
        /// 타이머를 시작합니다.
        /// </summary>
        public void Start()
        {
            if (_Timer == null)
            {
                _Timer = new Timer();
                _Timer.Interval = _interval;
                _Timer.Tick += Timer_Tick;
            }

            _Timer.Start();
            _start = true;
        }

        /// <summary>
        /// 타이머를 정지합니다.
        /// </summary>
        public void Stop()
        {
            if (_Timer != null)
            {
                _Timer.Stop();
            }

            _start = false;
        }
    }
}
