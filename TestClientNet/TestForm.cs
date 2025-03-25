using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using CommonLibrary.Reflection;
using System.Reflection;
using CommonLibrary;

namespace TestClientNet
{
    public partial class TestForm : Form
    {

        public TestForm()
        {
            InitializeComponent();
        }


        private void TestForm_Load(object sender, EventArgs e)
        {
            DateTime outDateTime = DateTime.MinValue;
            Assembly assembly = Assembly.GetEntryAssembly();
            if (AssemblyBuildInfo.TryGetBuildDate(assembly, out outDateTime))
            {
                Text = String.Format("{0} {1}", Text, outDateTime);
            }
        }

        private void TestForm_Shown(object sender, EventArgs e)
        {

        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void ButtonShowNormalScreensDlg_Click(object sender, EventArgs e)
        {
            ScreenManager.ShowNormalScreensDlg(5);
        }

        private void ButtonShowAsscendingScreensDlg_Click(object sender, EventArgs e)
        {
            ScreenManager.ShowAsscendingScreensDlg(5);
        }
    }
}
