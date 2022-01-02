using CommonLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharpCommonLibrary
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
        public int ScreenIndex { get; set; }
        public Size ResolutionDisplaySize { get; set; }

        public int Interval { get; set; }
        public bool IsStart { get; private set; }
        private Timer _Timer;
        public ScreenImageCapture(int screenIndex, int interval = 500)
        {
            if (!ScreenUtility.IsValidIndex(screenIndex))
            {
                throw new ArgumentException("Usage " + nameof(screenIndex) + "=0-" + (Screen.AllScreens.Length - 1));
            }

            if (!(interval >= 500 && interval <= 3000))
            {
                throw new ArgumentException("Usage " + nameof(interval) + "=500-3000");
            }

            ScreenIndex = screenIndex;
            Interval = interval;
        }

        public ScreenImageCapture(Size resolutionDisplaySize, int interval = 500)
        {
            if (!ScreenUtility.EqualsScreenBoundsSize(resolutionDisplaySize))
            {
                throw new ArgumentException(nameof(resolutionDisplaySize));
            }

            if (!(interval >= 500 && interval <= 3000))
            {
                throw new ArgumentException("Usage " + nameof(interval) + "=500-3000");
            }

            ResolutionDisplaySize = resolutionDisplaySize;
            Interval = interval;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Bitmap bitmap = null;

                Rectangle bounds = Rectangle.Empty;
                if (ScreenIndex == InvalidScreenIndex)
                {
                    bounds = new MultiScreenManager(ResolutionDisplaySize).TargetScreen.Bounds;
                }
                else
                {
                    bounds = new MultiScreenManager(ScreenIndex).TargetScreen.Bounds;
                }

                bitmap = new Bitmap(bounds.Width, bounds.Height);
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
