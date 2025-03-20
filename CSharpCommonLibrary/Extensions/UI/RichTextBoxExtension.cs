using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonLibrary.Extensions.UI
{
    public static class RichTextBoxExtension
    {
        public static void AppendText(this RichTextBox @this, string text, Color color)
        {
            @this.SelectionStart = @this.TextLength;
            @this.SelectionLength = 0;

            @this.SelectionColor = color;
            @this.AppendText(text);
            @this.SelectionColor = @this.ForeColor;
        }
    }
}
