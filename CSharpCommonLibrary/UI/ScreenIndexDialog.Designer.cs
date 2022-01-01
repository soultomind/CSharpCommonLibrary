
namespace CommonLibrary.UI
{
    partial class ScreenIndexDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._LabelScreenIndex = new System.Windows.Forms.Label();
            this._TimerShowPosition = new System.Windows.Forms.Timer(this.components);
            this._LabelBounds = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _LabelScreenIndex
            // 
            this._LabelScreenIndex.BackColor = System.Drawing.Color.LightSkyBlue;
            this._LabelScreenIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._LabelScreenIndex.Dock = System.Windows.Forms.DockStyle.Top;
            this._LabelScreenIndex.Font = new System.Drawing.Font("맑은 고딕", 54.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._LabelScreenIndex.ForeColor = System.Drawing.Color.White;
            this._LabelScreenIndex.Location = new System.Drawing.Point(0, 0);
            this._LabelScreenIndex.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._LabelScreenIndex.Name = "_LabelScreenIndex";
            this._LabelScreenIndex.Size = new System.Drawing.Size(280, 104);
            this._LabelScreenIndex.TabIndex = 0;
            this._LabelScreenIndex.Text = "1";
            this._LabelScreenIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _TimerShowPosition
            // 
            this._TimerShowPosition.Interval = 1000;
            this._TimerShowPosition.Tick += new System.EventHandler(this.ShowPositionTimer_Tick);
            // 
            // _LabelBounds
            // 
            this._LabelBounds.BackColor = System.Drawing.Color.LightSkyBlue;
            this._LabelBounds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._LabelBounds.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._LabelBounds.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this._LabelBounds.ForeColor = System.Drawing.Color.White;
            this._LabelBounds.Location = new System.Drawing.Point(0, 104);
            this._LabelBounds.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._LabelBounds.Name = "_LabelBounds";
            this._LabelBounds.Size = new System.Drawing.Size(280, 46);
            this._LabelBounds.TabIndex = 1;
            this._LabelBounds.Text = "Bounds";
            this._LabelBounds.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScreenPositionDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 150);
            this.Controls.Add(this._LabelBounds);
            this.Controls.Add(this._LabelScreenIndex);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(280, 150);
            this.MinimumSize = new System.Drawing.Size(280, 150);
            this.Name = "ScreenPositionDlg";
            this.Opacity = 0.7D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MonitorPosition";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _LabelScreenIndex;
        private System.Windows.Forms.Timer _TimerShowPosition;
        private System.Windows.Forms.Label _LabelBounds;
    }
}