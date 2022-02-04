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
    public class ScreenImageCapture
    {
        public const int InvalidScreenIndex = -1;

        public event CreateScreenImageCaptureEventHandler CreateScreenImageCapture;
        /// <summary>
        /// <see cref="System.Windows.Forms.Screen"/> 인덱스
        /// </summary>
        public int TargetScreenIndex { get; set; }
        public Size TargetScreenBoundsSize { get; set; }
        public Screen TargetScreen { get; private set; }
        public int Interval { get; set; }
        public bool IsStart { get; private set; }
        private Timer _Timer;
        public ScreenImageCapture(int targetScreenIndex, int interval = 500)
        {
            if (!ScreenUtility.IsValidIndex(targetScreenIndex))
            {
                throw new ArgumentException("Usage " + nameof(targetScreenIndex) + "=0-" + (Screen.AllScreens.Length - 1));
            }

            if (!(interval >= 500 && interval <= 3000))
            {
                throw new ArgumentException("Usage " + nameof(interval) + "=500-3000");
            }

            TargetScreenIndex = targetScreenIndex;
            TargetScreen = new MultiScreenManager(TargetScreenIndex).TargetScreen;
            if (TargetScreen == null)
            {
                throw new InvalidOperationException("TargetScreen is null");
            }

            TargetScreenBoundsSize = Size.Empty;
            Interval = interval;
        }

        public ScreenImageCapture(Size targetScreenBoundsSize, int interval = 500)
        {
            if (!ScreenUtility.EqualsScreenBoundsSize(targetScreenBoundsSize))
            {
                throw new ArgumentException(nameof(targetScreenBoundsSize));
            }

            if (!(interval >= 500 && interval <= 3000))
            {
                throw new ArgumentException("Usage " + nameof(interval) + "=500-3000");
            }

            TargetScreenBoundsSize = targetScreenBoundsSize;
            TargetScreen = new MultiScreenManager(TargetScreenBoundsSize).TargetScreen;
            if (TargetScreen == null)
            {
                throw new InvalidOperationException("TargetScreen is null");
            }

            TargetScreenIndex = InvalidScreenIndex;
            Interval = interval;
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

        public void Close()
        {
            Stop();
            _Timer = null;
        }

        public void Start()
        {
            if (_Timer == null)
            {
                _Timer = new Timer();
                _Timer.Interval = Interval;
                _Timer.Tick += Timer_Tick;
            }

            _Timer.Start();
        }

        public void Stop()
        {
            if (_Timer != null)
            {
                _Timer.Stop();
            }
        }
    }
}
