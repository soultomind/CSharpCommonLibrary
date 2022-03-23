using CommonLibrary;
using CommonLibrary.UI;
using CommonLibrary.Utilities;
using CommonLibrary.Web;
using CommonLibrary.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace TestNet32
{
    public partial class TestForm : Form
    {
        private bool _mouseMovePrevent;
        private MouseManager _mouseManager;

        private bool _screenImage;
        private ScreenImageCapture _screenImageCapture;

        private WindowManager _windowManager;
        public TestForm()
        {
            InitializeComponent();

            _mouseManager = new MouseManager() { MouseMovePreventInterval = 1000, IsMouseMoveLastPreventPoint = true };
            _mouseManager.MouseMovePreventScreenIndex = ScreenUtility.GetFirstScreenIndexAndExceptPrimaryScreen();

            _screenImageCapture = new ScreenImageCapture(ScreenUtility.GetFirstScreenIndexAndExceptPrimaryScreen());
            _screenImageCapture.CreateScreenImageCapture += ScreenImageCapture_CreateScreenImageCapture;
        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mouseManager.StopMouseMovePrevent();
        }

        private void ButtonMouseMovePrevent_Click(object sender, EventArgs e)
        {
            _mouseMovePrevent = !_mouseMovePrevent;
            if (_mouseMovePrevent)
            {
                _mouseManager.StartMouseMovePrevent();
                _ButtonMouseMovePrevent.Text = "마우스 이동 제어 정지";
            }
            else
            {
                _mouseManager.StopMouseMovePrevent();
                _ButtonMouseMovePrevent.Text = "마우스 이동 제어 시작";
            }
        }

        private void ButtonShowScreenIndex_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < Screen.AllScreens.Length; index++)
            {
                Screen screen = Screen.AllScreens[index];
                ScreenIndexDialog dlg = new ScreenIndexDialog((index + 1), screen.Bounds, new DialogColor()
                {
                    MainBackColor = Color.Green, MainForeColor = Color.White,
                    SubBackColor = Color.Black, SubForeColor = Color.White
                });

                dlg.Show();
            }
        }

        private void ButtonProcessWindowHandleFixedLocation_Click(object sender, EventArgs e)
        {
            if (_windowManager == null)
            {
                _windowManager = new WindowManager();
                _windowManager.SetWindowPos += WindowManager_SetWindowPos;
                _windowManager.StartProcessHandleWindowMovePrevent();
                _ButtonProcessWindowHandleFixedLocation.Text = "창 제어 정지";
            }
            else
            {
                _windowManager.StopProcessHandleWindowMovePrevent();
                _windowManager = null;
                _ButtonProcessWindowHandleFixedLocation.Text = "창 제어 시작";
            }
        }

        private bool WindowManager_SetWindowPos(object sender, ProcessWndHandleWindowStateEventHandler e)
        {
            // 해당 이벤트핸들러에서 특정 프로세스 창에 대하여 윈도우 창 제어를 할지 여부를 처리한다.
            if (e.Process.ProcessName.Equals(""))
            {
                // 특정 프로세스 일때 특정 핸들의 타이틀값일때 해당 핸들도 처리 할 수 있다.
                return false;
            }
            else
            {
                // 일반적으로는 메인 윈도우 핸들만 창 제어를 처리한다.
                if (e.Process.MainWindowHandle == e.Handle)
                {
                    return true;
                }
                return false;
            }
        }

        private void ButtonStartAndStopScreenCapture_Click(object sender, EventArgs e)
        {
            _screenImage = !_screenImage;
            if (_screenImage)
            {
                _screenImageCapture.Start();
                _ButtonStartAndStopScreenCapture.Text = "스크린 캡쳐 정지";
            }
            else
            {
                _screenImageCapture.Stop();
                _ButtonStartAndStopScreenCapture.Text = "스크린 캡쳐 시작";
            }
        }

        private void ScreenImageCapture_CreateScreenImageCapture(object sender, ScreenImageCaptureEventArgs e)
        {
            if (e.Exception == null)
            {
                if (_PictureBoxScreenCapture.Image != null)
                {
                    _PictureBoxScreenCapture.Image.Dispose();

                }
                _PictureBoxScreenCapture.SizeMode = PictureBoxSizeMode.StretchImage;
                _PictureBoxScreenCapture.Image = e.Bitmap;
            }
        }

        private void _ButtonHttpToolkitTest_Click(object sender, EventArgs e)
        {
            Dictionary<string, string[]> parameter = new Dictionary<string, string[]>();
            parameter.Add("Test", new string[] { "Test1", "Test2" });
            parameter.Add("Hangeul", new string[] { Uri.EscapeUriString("한글") });
            
            /*
            Dictionary<string, string> parameter = new Dictionary<string, string>();
            parameter.Add("Test1", "Test1");
            parameter.Add("Test2", "Test2");
            parameter.Add("Hangeul", Uri.EscapeUriString("한글"));
            */
            HttpStatusCode statusCode = HttpStatusCode.OK;
            Exception exception = null;

            HttpToolkit httpToolkit = new HttpToolkit();
            httpToolkit.CreateHttpWebRequest += HttpToolkit_CreateHttpWebRequest;
            httpToolkit.CreateHttpWebResponse += HttpToolkit_CreateHttpWebResponse;
            string response = httpToolkit.GetResponseByPost(
                "http://localhost:8080/CSharpCommonLibrary/index.jsp", 
                parameter, 
                "UTF-8", 
                "UTF-8", 
                10000, 
                out statusCode,
                out exception
            );
        }

        private void HttpToolkit_CreateHttpWebRequest(object sender, CreateHttpWebRequestEventArgs e)
        {
            Toolkit.TraceWriteLine("HttpToolkit_CreateHttpWebRequest");
        }

        private void HttpToolkit_CreateHttpWebResponse(object sender, CreateHttpWebResponseEventArgs e)
        {
            Toolkit.TraceWriteLine("HttpToolkit_CreateHttpWebResponse");
        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }

        private void TestForm_Shown(object sender, EventArgs e)
        {

        }
    }
}
