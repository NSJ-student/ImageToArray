namespace ImageToArray
{
    partial class ImageToArray
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
			this.tbImagePath = new System.Windows.Forms.TextBox();
			this.btnImageLoad = new System.Windows.Forms.Button();
			this.rtbArrayCode = new System.Windows.Forms.RichTextBox();
			this.btnConvert = new System.Windows.Forms.Button();
			this.btnShowImage = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.tbSaveName = new System.Windows.Forms.TextBox();
			this.rb565RGB = new System.Windows.Forms.RadioButton();
			this.rbNormalRGB = new System.Windows.Forms.RadioButton();
			this.chkSwap = new System.Windows.Forms.CheckBox();
			this.lblLine = new System.Windows.Forms.Label();
			this.pbProgress = new System.Windows.Forms.ProgressBar();
			this.SuspendLayout();
			// 
			// tbImagePath
			// 
			this.tbImagePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tbImagePath.Location = new System.Drawing.Point(136, 21);
			this.tbImagePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tbImagePath.Name = "tbImagePath";
			this.tbImagePath.ReadOnly = true;
			this.tbImagePath.Size = new System.Drawing.Size(219, 18);
			this.tbImagePath.TabIndex = 0;
			// 
			// btnImageLoad
			// 
			this.btnImageLoad.Location = new System.Drawing.Point(14, 15);
			this.btnImageLoad.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnImageLoad.Name = "btnImageLoad";
			this.btnImageLoad.Size = new System.Drawing.Size(115, 29);
			this.btnImageLoad.TabIndex = 1;
			this.btnImageLoad.Text = "Load Image";
			this.btnImageLoad.UseVisualStyleBackColor = true;
			this.btnImageLoad.Click += new System.EventHandler(this.btnImageLoad_Click);
			// 
			// rtbArrayCode
			// 
			this.rtbArrayCode.Location = new System.Drawing.Point(14, 88);
			this.rtbArrayCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.rtbArrayCode.Name = "rtbArrayCode";
			this.rtbArrayCode.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.rtbArrayCode.Size = new System.Drawing.Size(341, 176);
			this.rtbArrayCode.TabIndex = 2;
			this.rtbArrayCode.Text = "";
			// 
			// btnConvert
			// 
			this.btnConvert.Location = new System.Drawing.Point(14, 272);
			this.btnConvert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.Size = new System.Drawing.Size(86, 29);
			this.btnConvert.TabIndex = 3;
			this.btnConvert.Text = "Convert";
			this.btnConvert.UseVisualStyleBackColor = true;
			this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
			// 
			// btnShowImage
			// 
			this.btnShowImage.Location = new System.Drawing.Point(14, 51);
			this.btnShowImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnShowImage.Name = "btnShowImage";
			this.btnShowImage.Size = new System.Drawing.Size(115, 29);
			this.btnShowImage.TabIndex = 4;
			this.btnShowImage.Text = "Show Image";
			this.btnShowImage.UseVisualStyleBackColor = true;
			this.btnShowImage.Click += new System.EventHandler(this.btnShowImage_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(270, 51);
			this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(86, 29);
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// tbSaveName
			// 
			this.tbSaveName.Location = new System.Drawing.Point(136, 54);
			this.tbSaveName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tbSaveName.Name = "tbSaveName";
			this.tbSaveName.Size = new System.Drawing.Size(127, 25);
			this.tbSaveName.TabIndex = 6;
			// 
			// rb565RGB
			// 
			this.rb565RGB.AutoSize = true;
			this.rb565RGB.Checked = true;
			this.rb565RGB.Location = new System.Drawing.Point(106, 276);
			this.rb565RGB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.rb565RGB.Name = "rb565RGB";
			this.rb565RGB.Size = new System.Drawing.Size(88, 19);
			this.rb565RGB.TabIndex = 7;
			this.rb565RGB.TabStop = true;
			this.rb565RGB.Text = "565 RGB";
			this.rb565RGB.UseVisualStyleBackColor = true;
			this.rb565RGB.CheckedChanged += new System.EventHandler(this.rb565RGB_CheckedChanged);
			// 
			// rbNormalRGB
			// 
			this.rbNormalRGB.AutoSize = true;
			this.rbNormalRGB.Location = new System.Drawing.Point(193, 276);
			this.rbNormalRGB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.rbNormalRGB.Name = "rbNormalRGB";
			this.rbNormalRGB.Size = new System.Drawing.Size(88, 19);
			this.rbNormalRGB.TabIndex = 8;
			this.rbNormalRGB.Text = "888 RGB";
			this.rbNormalRGB.UseVisualStyleBackColor = true;
			this.rbNormalRGB.CheckedChanged += new System.EventHandler(this.rbNormalRGB_CheckedChanged);
			// 
			// chkSwap
			// 
			this.chkSwap.AutoSize = true;
			this.chkSwap.Location = new System.Drawing.Point(291, 276);
			this.chkSwap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.chkSwap.Name = "chkSwap";
			this.chkSwap.Size = new System.Drawing.Size(65, 19);
			this.chkSwap.TabIndex = 9;
			this.chkSwap.Text = "Swap";
			this.chkSwap.UseVisualStyleBackColor = true;
			// 
			// lblLine
			// 
			this.lblLine.AutoSize = true;
			this.lblLine.Location = new System.Drawing.Point(272, 279);
			this.lblLine.Name = "lblLine";
			this.lblLine.Size = new System.Drawing.Size(14, 15);
			this.lblLine.TabIndex = 10;
			this.lblLine.Text = "|";
			// 
			// pbProgress
			// 
			this.pbProgress.Location = new System.Drawing.Point(136, 54);
			this.pbProgress.Name = "pbProgress";
			this.pbProgress.Size = new System.Drawing.Size(127, 25);
			this.pbProgress.TabIndex = 11;
			// 
			// ImageToArray
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(369, 312);
			this.Controls.Add(this.pbProgress);
			this.Controls.Add(this.lblLine);
			this.Controls.Add(this.chkSwap);
			this.Controls.Add(this.rbNormalRGB);
			this.Controls.Add(this.rb565RGB);
			this.Controls.Add(this.tbSaveName);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnShowImage);
			this.Controls.Add(this.btnConvert);
			this.Controls.Add(this.rtbArrayCode);
			this.Controls.Add(this.btnImageLoad);
			this.Controls.Add(this.tbImagePath);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.Name = "ImageToArray";
			this.Text = "ImageToArray";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbImagePath;
        private System.Windows.Forms.Button btnImageLoad;
        private System.Windows.Forms.RichTextBox rtbArrayCode;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnShowImage;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox tbSaveName;
		private System.Windows.Forms.RadioButton rb565RGB;
		private System.Windows.Forms.RadioButton rbNormalRGB;
		private System.Windows.Forms.CheckBox chkSwap;
		private System.Windows.Forms.Label lblLine;
		private System.Windows.Forms.ProgressBar pbProgress;
    }
}

