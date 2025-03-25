
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
            this.SuspendLayout();
            // 
            // buttonShowNormalScreensDlg
            // 
            this.buttonShowNormalScreensDlg.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowNormalScreensDlg.Location = new System.Drawing.Point(12, 12);
            this.buttonShowNormalScreensDlg.Name = "buttonShowNormalScreensDlg";
            this.buttonShowNormalScreensDlg.Size = new System.Drawing.Size(176, 40);
            this.buttonShowNormalScreensDlg.TabIndex = 0;
            this.buttonShowNormalScreensDlg.Text = "ShowNormalScreensDlg";
            this.buttonShowNormalScreensDlg.UseVisualStyleBackColor = true;
            this.buttonShowNormalScreensDlg.Click += new System.EventHandler(this.ButtonShowNormalScreensDlg_Click);
            // 
            // buttonShowAsscendingScreensDlg
            // 
            this.buttonShowAsscendingScreensDlg.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonShowAsscendingScreensDlg.Location = new System.Drawing.Point(12, 58);
            this.buttonShowAsscendingScreensDlg.Name = "buttonShowAsscendingScreensDlg";
            this.buttonShowAsscendingScreensDlg.Size = new System.Drawing.Size(176, 40);
            this.buttonShowAsscendingScreensDlg.TabIndex = 1;
            this.buttonShowAsscendingScreensDlg.Text = "ShowAsscendingScreensDlg";
            this.buttonShowAsscendingScreensDlg.UseVisualStyleBackColor = true;
            this.buttonShowAsscendingScreensDlg.Click += new System.EventHandler(this.ButtonShowAsscendingScreensDlg_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 525);
            this.Controls.Add(this.buttonShowAsscendingScreensDlg);
            this.Controls.Add(this.buttonShowNormalScreensDlg);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestForm_FormClosed);
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.Shown += new System.EventHandler(this.TestForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonShowNormalScreensDlg;
        private System.Windows.Forms.Button buttonShowAsscendingScreensDlg;
    }
}