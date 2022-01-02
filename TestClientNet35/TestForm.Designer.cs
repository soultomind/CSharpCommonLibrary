﻿
namespace TestNet32
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
            this._ButtonMouseOperation = new System.Windows.Forms.Button();
            this._ButtonShowScreenIndex = new System.Windows.Forms.Button();
            this._PictureBoxScreenCapture = new System.Windows.Forms.PictureBox();
            this._ButtonStartAndStopScreenCapture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._PictureBoxScreenCapture)).BeginInit();
            this.SuspendLayout();
            // 
            // _ButtonMouseOperation
            // 
            this._ButtonMouseOperation.Location = new System.Drawing.Point(24, 28);
            this._ButtonMouseOperation.Name = "_ButtonMouseOperation";
            this._ButtonMouseOperation.Size = new System.Drawing.Size(128, 27);
            this._ButtonMouseOperation.TabIndex = 0;
            this._ButtonMouseOperation.Text = "마우스 제어 시작";
            this._ButtonMouseOperation.UseVisualStyleBackColor = true;
            this._ButtonMouseOperation.Click += new System.EventHandler(this.ButtonMouseOperation_Click);
            // 
            // _ButtonShowScreenIndex
            // 
            this._ButtonShowScreenIndex.Location = new System.Drawing.Point(24, 61);
            this._ButtonShowScreenIndex.Name = "_ButtonShowScreenIndex";
            this._ButtonShowScreenIndex.Size = new System.Drawing.Size(128, 27);
            this._ButtonShowScreenIndex.TabIndex = 1;
            this._ButtonShowScreenIndex.Text = "스크린 인덱스 출력";
            this._ButtonShowScreenIndex.UseVisualStyleBackColor = true;
            this._ButtonShowScreenIndex.Click += new System.EventHandler(this.ButtonShowScreenIndex_Click);
            // 
            // _PictureBoxScreenCapture
            // 
            this._PictureBoxScreenCapture.Location = new System.Drawing.Point(415, 270);
            this._PictureBoxScreenCapture.Name = "_PictureBoxScreenCapture";
            this._PictureBoxScreenCapture.Size = new System.Drawing.Size(373, 243);
            this._PictureBoxScreenCapture.TabIndex = 2;
            this._PictureBoxScreenCapture.TabStop = false;
            // 
            // _ButtonStartAndStopScreenCapture
            // 
            this._ButtonStartAndStopScreenCapture.Location = new System.Drawing.Point(24, 94);
            this._ButtonStartAndStopScreenCapture.Name = "_ButtonStartAndStopScreenCapture";
            this._ButtonStartAndStopScreenCapture.Size = new System.Drawing.Size(128, 27);
            this._ButtonStartAndStopScreenCapture.TabIndex = 3;
            this._ButtonStartAndStopScreenCapture.Text = "스크린 캡쳐 시작";
            this._ButtonStartAndStopScreenCapture.UseVisualStyleBackColor = true;
            this._ButtonStartAndStopScreenCapture.Click += new System.EventHandler(this.ButtonStartAndStopScreenCapture_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 525);
            this.Controls.Add(this._ButtonStartAndStopScreenCapture);
            this.Controls.Add(this._PictureBoxScreenCapture);
            this.Controls.Add(this._ButtonShowScreenIndex);
            this.Controls.Add(this._ButtonMouseOperation);
            this.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this._PictureBoxScreenCapture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _ButtonMouseOperation;
        private System.Windows.Forms.Button _ButtonShowScreenIndex;
        private System.Windows.Forms.PictureBox _PictureBoxScreenCapture;
        private System.Windows.Forms.Button _ButtonStartAndStopScreenCapture;
    }
}