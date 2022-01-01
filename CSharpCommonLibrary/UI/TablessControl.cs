using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommonLibrary.UI
{
    public class TablessControl : TabControl
    {
        public const int TCM_ADJUSTRECT = 0x1328;

        public bool IsTabless
        {
            get; set;
        }

        protected override void WndProc(ref Message m)
        {
            if (IsTabless)
            {
                if ((m.Msg == TCM_ADJUSTRECT) && !DesignMode)
                {
                    m.Result = (IntPtr)1;
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}
