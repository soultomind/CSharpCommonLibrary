
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.labelScreenBounds = new System.Windows.Forms.Label();
            this.screenBoundsPanel = new System.Windows.Forms.Panel();
            this.labelScreenWorkingArea = new System.Windows.Forms.Label();
            this.screenIndexPanel = new System.Windows.Forms.Panel();
            this.labelScreenAspectRatio = new System.Windows.Forms.Label();
            this.labelScreenIndex = new System.Windows.Forms.Label();
            this.panelDisplayName = new System.Windows.Forms.Panel();
            this.labelScreenIndexEx = new System.Windows.Forms.Label();
            this.labelScreenName = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel.SuspendLayout();
            this.screenBoundsPanel.SuspendLayout();
            this.screenIndexPanel.SuspendLayout();
            this.panelDisplayName.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.labelScreenBounds, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.screenBoundsPanel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.screenIndexPanel, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.panelDisplayName, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 4;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(300, 250);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // labelScreenBounds
            // 
            this.labelScreenBounds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScreenBounds.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelScreenBounds.Location = new System.Drawing.Point(0, 212);
            this.labelScreenBounds.Margin = new System.Windows.Forms.Padding(0);
            this.labelScreenBounds.Name = "labelScreenBounds";
            this.labelScreenBounds.Size = new System.Drawing.Size(300, 38);
            this.labelScreenBounds.TabIndex = 0;
            this.labelScreenBounds.Text = "X=0,Y=0,Width=1080,Height=1920";
            this.labelScreenBounds.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // screenBoundsPanel
            // 
            this.screenBoundsPanel.Controls.Add(this.labelScreenWorkingArea);
            this.screenBoundsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenBoundsPanel.Location = new System.Drawing.Point(3, 178);
            this.screenBoundsPanel.Name = "screenBoundsPanel";
            this.screenBoundsPanel.Size = new System.Drawing.Size(294, 31);
            this.screenBoundsPanel.TabIndex = 1;
            // 
            // labelScreenWorkingArea
            // 
            this.labelScreenWorkingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScreenWorkingArea.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelScreenWorkingArea.Location = new System.Drawing.Point(0, 0);
            this.labelScreenWorkingArea.Margin = new System.Windows.Forms.Padding(0);
            this.labelScreenWorkingArea.Name = "labelScreenWorkingArea";
            this.labelScreenWorkingArea.Size = new System.Drawing.Size(294, 31);
            this.labelScreenWorkingArea.TabIndex = 3;
            this.labelScreenWorkingArea.Text = "X=0,Y=0,Width=1080,Height=1920";
            this.labelScreenWorkingArea.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelScreenWorkingArea.Visible = false;
            // 
            // screenIndexPanel
            // 
            this.screenIndexPanel.Controls.Add(this.labelScreenAspectRatio);
            this.screenIndexPanel.Controls.Add(this.labelScreenIndex);
            this.screenIndexPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenIndexPanel.Location = new System.Drawing.Point(3, 53);
            this.screenIndexPanel.Name = "screenIndexPanel";
            this.screenIndexPanel.Size = new System.Drawing.Size(294, 119);
            this.screenIndexPanel.TabIndex = 0;
            // 
            // labelScreenAspectRatio
            // 
            this.labelScreenAspectRatio.Font = new System.Drawing.Font("맑은 고딕", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelScreenAspectRatio.Location = new System.Drawing.Point(172, 11);
            this.labelScreenAspectRatio.Name = "labelScreenAspectRatio";
            this.labelScreenAspectRatio.Size = new System.Drawing.Size(113, 90);
            this.labelScreenAspectRatio.TabIndex = 2;
            this.labelScreenAspectRatio.Text = "AspectRatio";
            this.labelScreenAspectRatio.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // labelScreenIndex
            // 
            this.labelScreenIndex.Font = new System.Drawing.Font("맑은 고딕", 56.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelScreenIndex.Location = new System.Drawing.Point(5, 11);
            this.labelScreenIndex.Margin = new System.Windows.Forms.Padding(3);
            this.labelScreenIndex.Name = "labelScreenIndex";
            this.labelScreenIndex.Size = new System.Drawing.Size(152, 90);
            this.labelScreenIndex.TabIndex = 1;
            this.labelScreenIndex.Text = "1";
            this.labelScreenIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDisplayName
            // 
            this.panelDisplayName.Controls.Add(this.labelScreenIndexEx);
            this.panelDisplayName.Controls.Add(this.labelScreenName);
            this.panelDisplayName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDisplayName.Location = new System.Drawing.Point(3, 3);
            this.panelDisplayName.Name = "panelDisplayName";
            this.panelDisplayName.Size = new System.Drawing.Size(294, 44);
            this.panelDisplayName.TabIndex = 2;
            // 
            // labelScreenIndexEx
            // 
            this.labelScreenIndexEx.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelScreenIndexEx.Location = new System.Drawing.Point(205, 5);
            this.labelScreenIndexEx.Margin = new System.Windows.Forms.Padding(0);
            this.labelScreenIndexEx.Name = "labelScreenIndexEx";
            this.labelScreenIndexEx.Size = new System.Drawing.Size(80, 35);
            this.labelScreenIndexEx.TabIndex = 2;
            this.labelScreenIndexEx.Text = "1";
            this.labelScreenIndexEx.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelScreenIndexEx.Visible = false;
            // 
            // labelScreenName
            // 
            this.labelScreenName.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelScreenName.Location = new System.Drawing.Point(6, 5);
            this.labelScreenName.Margin = new System.Windows.Forms.Padding(0);
            this.labelScreenName.Name = "labelScreenName";
            this.labelScreenName.Size = new System.Drawing.Size(131, 35);
            this.labelScreenName.TabIndex = 1;
            this.labelScreenName.Text = "Monitor Name";
            this.labelScreenName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // ScreenIndexDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 250);
            this.Controls.Add(this.tableLayoutPanel);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ScreenIndexDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ScreenIndexDialog";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ScreenIndexDialog_FormClosed);
            this.Load += new System.EventHandler(this.ScreenIndexDialog_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.screenBoundsPanel.ResumeLayout(false);
            this.screenIndexPanel.ResumeLayout(false);
            this.panelDisplayName.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel screenIndexPanel;
        private System.Windows.Forms.Panel screenBoundsPanel;
        private System.Windows.Forms.Label labelScreenIndex;
        private System.Windows.Forms.Label labelScreenBounds;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Panel panelDisplayName;
        private System.Windows.Forms.Label labelScreenName;
        private System.Windows.Forms.Label labelScreenWorkingArea;
        private System.Windows.Forms.Label labelScreenAspectRatio;
        private System.Windows.Forms.Label labelScreenIndexEx;
    }
}