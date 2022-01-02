﻿using CommonLibrary;
using CommonLibrary.UI;
using CommonLibrary.Utilities;
using CSharpCommonLibrary;
using System;
using System.Windows.Forms;

namespace TestNet32
{
    public partial class TestForm : Form
    {
        private bool _mouseOperation;
        private MouseOperationManager _mouseOperationManager;

        private bool _screenImage;
        private ScreenImageCapture _screenImageCapture;
        public TestForm()
        {
            InitializeComponent();

            _mouseOperationManager = new MouseOperationManager() { MouseMovePreventInterval = 2000 };
            _mouseOperationManager.MouseMovePreventScreenIndex = ScreenUtility.GetFirstScreenIndexAndExceptPrimaryScreen();

            _screenImageCapture = new ScreenImageCapture(ScreenUtility.GetFirstScreenIndexAndExceptPrimaryScreen());
            _screenImageCapture.CreateScreenImageCapture += ScreenImageCapture_CreateScreenImageCapture;
        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mouseOperationManager.MouseMovePreventStop();
        }

        private void ButtonMouseOperation_Click(object sender, EventArgs e)
        {
            _mouseOperation = !_mouseOperation;
            if (_mouseOperation)
            {
                _mouseOperationManager.MouseMovePreventStart();
                _ButtonMouseOperation.Text = "마우스 제어 정지";
            }
            else
            {
                _mouseOperationManager.MouseMovePreventStop();
                _ButtonMouseOperation.Text = "마우스 제어 시작";
            }
        }

        private void ButtonShowScreenIndex_Click(object sender, EventArgs e)
        {
            for (int index = 0; index < Screen.AllScreens.Length; index++)
            {
                Screen screen = Screen.AllScreens[index];
                ScreenIndexDialog dlg = new ScreenIndexDialog((index + 1), screen.Bounds);
                dlg.Show();
            }
        }

        private void ButtonStartAndStopScreenCapture_Click(object sender, EventArgs e)
        {
            _screenImage = !_screenImage;
            if (_screenImage)
            {
                _screenImageCapture.Start();
            }
            else
            {
                _screenImageCapture.Stop();
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
    }
}
