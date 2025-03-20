using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using CommonLibrary.Reflection;
using System.Reflection;

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
    }
}
