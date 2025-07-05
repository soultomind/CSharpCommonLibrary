namespace TestClientNet
{
    partial class TestForm
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
            this.buttonShowNormalScreensDlg = new System.Windows.Forms.Button();
            this.buttonShowAsscendingScreensDlg = new System.Windows.Forms.Button();
            this.labelColorInfo = new System.Windows.Forms.Label();
            this.textBoxHexColor = new System.Windows.Forms.TextBox();
            this.buttonCheckColor = new System.Windows.Forms.Button();
            this.groupBoxColor = new System.Windows.Forms.GroupBox();
            this.groupBoxScreen = new System.Windows.Forms.GroupBox();
            this.groupBoxColor.SuspendLayout();
            this.groupBoxScreen.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonShowNormalScreensDlg
            // 
            this.buttonShowNormalScreensDlg.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowNormalScreensDlg.Location = new System.Drawing.Point(11, 26);
            this.buttonShowNormalScreensDlg.Name = "buttonShowNormalScreensDlg";
            this.buttonShowNormalScreensDlg.Size = new System.Drawing.Size(176, 40);
            this.buttonShowNormalScreensDlg.TabIndex = 1;
            this.buttonShowNormalScreensDlg.Text = "ShowNormalScreensDlg";
            this.buttonShowNormalScreensDlg.UseVisualStyleBackColor = true;
            this.buttonShowNormalScreensDlg.Click += new System.EventHandler(this.ButtonShowNormalScreensDlg_Click);
            // 
            // buttonShowAsscendingScreensDlg
            // 
            this.buttonShowAsscendingScreensDlg.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowAsscendingScreensDlg.Location = new System.Drawing.Point(11, 72);
            this.buttonShowAsscendingScreensDlg.Name = "buttonShowAsscendingScreensDlg";
            this.buttonShowAsscendingScreensDlg.Size = new System.Drawing.Size(176, 40);
            this.buttonShowAsscendingScreensDlg.TabIndex = 2;
            this.buttonShowAsscendingScreensDlg.Text = "ShowAsscendingScreensDlg";
            this.buttonShowAsscendingScreensDlg.UseVisualStyleBackColor = true;
            this.buttonShowAsscendingScreensDlg.Click += new System.EventHandler(this.ButtonShowAsscendingScreensDlg_Click);
            // 
            // labelColorInfo
            // 
            this.labelColorInfo.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.labelColorInfo.Location = new System.Drawing.Point(27, 60);
            this.labelColorInfo.Name = "labelColorInfo";
            this.labelColorInfo.Size = new System.Drawing.Size(486, 23);
            this.labelColorInfo.TabIndex = 6;
            this.labelColorInfo.Text = "Color 정보";
            this.labelColorInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxHexColor
            // 
            this.textBoxHexColor.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.textBoxHexColor.Location = new System.Drawing.Point(27, 30);
            this.textBoxHexColor.Name = "textBoxHexColor";
            this.textBoxHexColor.Size = new System.Drawing.Size(120, 23);
            this.textBoxHexColor.TabIndex = 4;
            this.textBoxHexColor.Text = "#FF0000";
            // 
            // buttonCheckColor
            // 
            this.buttonCheckColor.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.buttonCheckColor.Location = new System.Drawing.Point(153, 30);
            this.buttonCheckColor.Name = "buttonCheckColor";
            this.buttonCheckColor.Size = new System.Drawing.Size(50, 23);
            this.buttonCheckColor.TabIndex = 5;
            this.buttonCheckColor.Text = "확인";
            this.buttonCheckColor.UseVisualStyleBackColor = true;
            this.buttonCheckColor.Click += new System.EventHandler(this.ButtonCheckColor_Click);
            // 
            // groupBoxColor
            // 
            this.groupBoxColor.Controls.Add(this.labelColorInfo);
            this.groupBoxColor.Controls.Add(this.buttonCheckColor);
            this.groupBoxColor.Controls.Add(this.textBoxHexColor);
            this.groupBoxColor.Location = new System.Drawing.Point(12, 176);
            this.groupBoxColor.Name = "groupBoxColor";
            this.groupBoxColor.Size = new System.Drawing.Size(776, 184);
            this.groupBoxColor.TabIndex = 3;
            this.groupBoxColor.TabStop = false;
            this.groupBoxColor.Text = "Color";
            // 
            // groupBoxScreen
            // 
            this.groupBoxScreen.Controls.Add(this.buttonShowAsscendingScreensDlg);
            this.groupBoxScreen.Controls.Add(this.buttonShowNormalScreensDlg);
            this.groupBoxScreen.Location = new System.Drawing.Point(12, 12);
            this.groupBoxScreen.Name = "groupBoxScreen";
            this.groupBoxScreen.Size = new System.Drawing.Size(776, 158);
            this.groupBoxScreen.TabIndex = 0;
            this.groupBoxScreen.TabStop = false;
            this.groupBoxScreen.Text = "Screen";
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 525);
            this.Controls.Add(this.groupBoxScreen);
            this.Controls.Add(this.groupBoxColor);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestForm_FormClosed);
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.Shown += new System.EventHandler(this.TestForm_Shown);
            this.groupBoxColor.ResumeLayout(false);
            this.groupBoxColor.PerformLayout();
            this.groupBoxScreen.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonShowNormalScreensDlg;
        private System.Windows.Forms.Button buttonShowAsscendingScreensDlg;
        private System.Windows.Forms.Label labelColorInfo;
        private System.Windows.Forms.TextBox textBoxHexColor;
        private System.Windows.Forms.Button buttonCheckColor;
        private System.Windows.Forms.GroupBox groupBoxColor;
        private System.Windows.Forms.GroupBox groupBoxScreen;
    }
}