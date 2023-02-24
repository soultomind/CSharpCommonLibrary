using CommonLibrary.Utilities;
using CommonLibrary.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary
{
    #region Event

    public class ImageCaptureEventArgs : EventArgs
    {
        public Exception Exception { get; private set; }
        public Bitmap Bitmap { get; private set; }

        public ImageCaptureEventArgs(Bitmap bitmap)
        {
            Bitmap = bitmap;
        }

        public ImageCaptureEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }

    public delegate void CreateImageCaptureEventHandler(object sender, ImageCaptureEventArgs e);

    #endregion

    /// <summary>
    /// <see cref="System.Windows.Forms.Screen"/> 이미지 캡쳐 클래스
    /// </summary>
    public class ImageCapture
    {
        /// <summary>
        /// 생성된 이미지가 전달되는 이벤트 핸들러
        /// </summary>
        public event CreateImageCaptureEventHandler CreateImageCapture;

        #region Screen 방식
        /// <summary>
        /// 유효하지 않는 스크린 인덱스
        /// </summary>
        public const int InvalidScreenIndex = -1;
        
        /// <summary>
        /// <see cref="System.Windows.Forms.Screen"/> 인덱스
        /// </summary>
        public int TargetScreenIndex { get; private set; }
        public Size TargetScreenBoundsSize { get; private set; }
        public Screen TargetScreen { get; private set; }
        #endregion

        #region Handle 방식
        public IntPtr TargetHandle { get; set; }
        #endregion

        public bool IsStart
        {
            get { return _start; }
            private set { _start = value; }
        }
        private bool _start;

        private int _interval;
        private Timer _Timer;
        public ImageCapture(int targetScreenIndex, int interval = 500)
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

        public ImageCapture(Size targetScreenBoundsSize, int interval = 500)
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

        public ImageCapture(IntPtr targetHandle, int interval = 500)
        {
            TargetHandle = targetHandle;
            _interval = interval;
        }

        private Image CaptureWindow(IntPtr handle)
        {
            // get te hDC of the target window
            IntPtr hdcSrc = User32.GetWindowDC(handle);
            // get the size
            RECT windowRect = new RECT();
            User32.GetWindowRect(handle, out windowRect);
            int width = windowRect.Right - windowRect.Left;
            int height = windowRect.Bottom - windowRect.Top;
            // create a device context we can copy to
            IntPtr hdcDest = Gdi32.CreateCompatibleDC(hdcSrc);
            // create a bitmap we can copy it to,
            // using GetDeviceCaps to get the width/height
            IntPtr hBitmap = Gdi32.CreateCompatibleBitmap(hdcSrc, width, height);
            // select the bitmap object
            IntPtr hOld = Gdi32.SelectObject(hdcDest, hBitmap);
            // bitblt over
            Gdi32.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, Gdi32.SRCCOPY);
            // restore selection
            Gdi32.SelectObject(hdcDest, hOld);
            // clean up 
            Gdi32.DeleteDC(hdcDest);
            User32.ReleaseDC(handle, hdcSrc);
            // get a .NET image object for it
            Image img = Image.FromHbitmap(hBitmap);
            // free up the Bitmap object
            Gdi32.DeleteObject(hBitmap);
            return img;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                Bitmap bitmap = null;
                if (TargetHandle != IntPtr.Zero)
                {
                    bitmap = (Bitmap)CaptureWindow(TargetHandle);
                }
                else
                {
                    Rectangle bounds = TargetScreen.Bounds;
                    bitmap = new Bitmap(bounds.Width, bounds.Height);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
                    }
                }

                CreateImageCapture?.Invoke(this, new ImageCaptureEventArgs(bitmap));
            }
            catch (Exception ex)
            {
                CreateImageCapture?.Invoke(this, new ImageCaptureEventArgs(ex));
            }
        }

        /// <summary>
        /// 리소스를 닫습니다.
        /// <para>타이머를 다시 시작하려면 <see cref="CommonLibrary.ImageCapture.Start"/> 메서드를 호출합니다.</para>
        /// </summary>
        public void Close()
        {
            Stop();

            _Timer.Dispose();
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
