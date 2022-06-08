
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
            this._ButtonMouseMovePrevent = new System.Windows.Forms.Button();
            this._ButtonShowScreenIndex = new System.Windows.Forms.Button();
            this._PictureBoxImageCapture = new System.Windows.Forms.PictureBox();
            this._ButtonStartAndStopScreenCapture = new System.Windows.Forms.Button();
            this._ButtonHttpToolkitTest = new System.Windows.Forms.Button();
            this._RichTextBoxHttpToolkitTest = new System.Windows.Forms.RichTextBox();
            this._ButtonProcessWindowHandleFixedLocation = new System.Windows.Forms.Button();
            this._ButtonMoveCursorPoint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._PictureBoxImageCapture)).BeginInit();
            this.SuspendLayout();
            // 
            // _ButtonMouseMovePrevent
            // 
            this._ButtonMouseMovePrevent.Location = new System.Drawing.Point(24, 28);
            this._ButtonMouseMovePrevent.Name = "_ButtonMouseMovePrevent";
            this._ButtonMouseMovePrevent.Size = new System.Drawing.Size(128, 27);
            this._ButtonMouseMovePrevent.TabIndex = 0;
            this._ButtonMouseMovePrevent.Text = "마우스 이동 제어 시작";
            this._ButtonMouseMovePrevent.UseVisualStyleBackColor = true;
            this._ButtonMouseMovePrevent.Click += new System.EventHandler(this.ButtonMouseMovePrevent_Click);
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
            this._PictureBoxImageCapture.Location = new System.Drawing.Point(415, 282);
            this._PictureBoxImageCapture.Name = "_PictureBoxScreenCapture";
            this._PictureBoxImageCapture.Size = new System.Drawing.Size(373, 231);
            this._PictureBoxImageCapture.TabIndex = 2;
            this._PictureBoxImageCapture.TabStop = false;
            // 
            // _ButtonStartAndStopScreenCapture
            // 
            this._ButtonStartAndStopScreenCapture.Location = new System.Drawing.Point(415, 249);
            this._ButtonStartAndStopScreenCapture.Name = "_ButtonStartAndStopScreenCapture";
            this._ButtonStartAndStopScreenCapture.Size = new System.Drawing.Size(128, 27);
            this._ButtonStartAndStopScreenCapture.TabIndex = 3;
            this._ButtonStartAndStopScreenCapture.Text = "스크린 캡쳐 시작";
            this._ButtonStartAndStopScreenCapture.UseVisualStyleBackColor = true;
            this._ButtonStartAndStopScreenCapture.Click += new System.EventHandler(this.ButtonStartAndStopScreenCapture_Click);
            // 
            // _ButtonHttpToolkitTest
            // 
            this._ButtonHttpToolkitTest.Location = new System.Drawing.Point(415, 12);
            this._ButtonHttpToolkitTest.Name = "_ButtonHttpToolkitTest";
            this._ButtonHttpToolkitTest.Size = new System.Drawing.Size(128, 27);
            this._ButtonHttpToolkitTest.TabIndex = 4;
            this._ButtonHttpToolkitTest.Text = "HttpToolkit 테스트";
            this._ButtonHttpToolkitTest.UseVisualStyleBackColor = true;
            this._ButtonHttpToolkitTest.Click += new System.EventHandler(this._ButtonHttpToolkitTest_Click);
            // 
            // _RichTextBoxHttpToolkitTest
            // 
            this._RichTextBoxHttpToolkitTest.Location = new System.Drawing.Point(415, 45);
            this._RichTextBoxHttpToolkitTest.Name = "_RichTextBoxHttpToolkitTest";
            this._RichTextBoxHttpToolkitTest.Size = new System.Drawing.Size(373, 165);
            this._RichTextBoxHttpToolkitTest.TabIndex = 5;
            this._RichTextBoxHttpToolkitTest.Text = "";
            // 
            // _ButtonProcessWindowHandleFixedLocation
            // 
            this._ButtonProcessWindowHandleFixedLocation.Location = new System.Drawing.Point(24, 94);
            this._ButtonProcessWindowHandleFixedLocation.Name = "_ButtonProcessWindowHandleFixedLocation";
            this._ButtonProcessWindowHandleFixedLocation.Size = new System.Drawing.Size(128, 27);
            this._ButtonProcessWindowHandleFixedLocation.TabIndex = 6;
            this._ButtonProcessWindowHandleFixedLocation.Text = "창 제어 시작";
            this._ButtonProcessWindowHandleFixedLocation.UseVisualStyleBackColor = true;
            this._ButtonProcessWindowHandleFixedLocation.Click += new System.EventHandler(this.ButtonProcessWindowHandleFixedLocation_Click);
            // 
            // _ButtonMoveCursorPoint
            // 
            this._ButtonMoveCursorPoint.Location = new System.Drawing.Point(158, 28);
            this._ButtonMoveCursorPoint.Name = "_ButtonMoveCursorPoint";
            this._ButtonMoveCursorPoint.Size = new System.Drawing.Size(144, 27);
            this._ButtonMoveCursorPoint.TabIndex = 7;
            this._ButtonMoveCursorPoint.Text = "마우스 특정위치로 이동";
            this._ButtonMoveCursorPoint.UseVisualStyleBackColor = true;
            this._ButtonMoveCursorPoint.Click += new System.EventHandler(this.ButtonMoveCursorPoint_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 525);
            this.Controls.Add(this._ButtonMoveCursorPoint);
            this.Controls.Add(this._ButtonProcessWindowHandleFixedLocation);
            this.Controls.Add(this._RichTextBoxHttpToolkitTest);
            this.Controls.Add(this._ButtonHttpToolkitTest);
            this.Controls.Add(this._ButtonStartAndStopScreenCapture);
            this.Controls.Add(this._PictureBoxImageCapture);
            this.Controls.Add(this._ButtonShowScreenIndex);
            this.Controls.Add(this._ButtonMouseMovePrevent);
            this.Font = new System.Drawing.Font("나눔고딕", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TestForm_FormClosed);
            this.Load += new System.EventHandler(this.TestForm_Load);
            this.Shown += new System.EventHandler(this.TestForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this._PictureBoxImageCapture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _ButtonMouseMovePrevent;
        private System.Windows.Forms.Button _ButtonShowScreenIndex;
        private System.Windows.Forms.PictureBox _PictureBoxImageCapture;
        private System.Windows.Forms.Button _ButtonStartAndStopScreenCapture;
        private System.Windows.Forms.Button _ButtonHttpToolkitTest;
        private System.Windows.Forms.RichTextBox _RichTextBoxHttpToolkitTest;
        private System.Windows.Forms.Button _ButtonProcessWindowHandleFixedLocation;
        private System.Windows.Forms.Button _ButtonMoveCursorPoint;
    }
}