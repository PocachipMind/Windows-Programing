namespace AsyncFileIOWinForm
{
    partial class MainForm
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
            this.lblSource = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.btnFindSource = new System.Windows.Forms.Button();
            this.btnFindTarget = new System.Windows.Forms.Button();
            this.txtTarget = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAsyncCopy = new System.Windows.Forms.Button();
            this.pbCopy = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSyncCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSource
            // 
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(11, 21);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(64, 15);
            this.lblSource.TabIndex = 0;
            this.lblSource.Text = "Source :";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(79, 16);
            this.txtSource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(245, 25);
            this.txtSource.TabIndex = 1;
            // 
            // btnFindSource
            // 
            this.btnFindSource.Location = new System.Drawing.Point(331, 16);
            this.btnFindSource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFindSource.Name = "btnFindSource";
            this.btnFindSource.Size = new System.Drawing.Size(43, 29);
            this.btnFindSource.TabIndex = 2;
            this.btnFindSource.Text = "...";
            this.btnFindSource.UseVisualStyleBackColor = true;
            this.btnFindSource.Click += new System.EventHandler(this.btnFindSource_Click);
            // 
            // btnFindTarget
            // 
            this.btnFindTarget.Location = new System.Drawing.Point(331, 50);
            this.btnFindTarget.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFindTarget.Name = "btnFindTarget";
            this.btnFindTarget.Size = new System.Drawing.Size(43, 29);
            this.btnFindTarget.TabIndex = 5;
            this.btnFindTarget.Text = "...";
            this.btnFindTarget.UseVisualStyleBackColor = true;
            this.btnFindTarget.Click += new System.EventHandler(this.btnFindTarget_Click);
            // 
            // txtTarget
            // 
            this.txtTarget.Location = new System.Drawing.Point(79, 50);
            this.txtTarget.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTarget.Name = "txtTarget";
            this.txtTarget.Size = new System.Drawing.Size(245, 25);
            this.txtTarget.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Target :";
            // 
            // btnAsyncCopy
            // 
            this.btnAsyncCopy.Location = new System.Drawing.Point(14, 108);
            this.btnAsyncCopy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAsyncCopy.Name = "btnAsyncCopy";
            this.btnAsyncCopy.Size = new System.Drawing.Size(102, 29);
            this.btnAsyncCopy.TabIndex = 6;
            this.btnAsyncCopy.Text = "Async Copy";
            this.btnAsyncCopy.UseVisualStyleBackColor = true;
            this.btnAsyncCopy.Click += new System.EventHandler(this.btnAsyncCopy_Click);
            // 
            // pbCopy
            // 
            this.pbCopy.Location = new System.Drawing.Point(14, 160);
            this.pbCopy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbCopy.Name = "pbCopy";
            this.pbCopy.Size = new System.Drawing.Size(361, 29);
            this.pbCopy.TabIndex = 7;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(270, 108);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 29);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSyncCopy
            // 
            this.btnSyncCopy.Location = new System.Drawing.Point(142, 108);
            this.btnSyncCopy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSyncCopy.Name = "btnSyncCopy";
            this.btnSyncCopy.Size = new System.Drawing.Size(102, 29);
            this.btnSyncCopy.TabIndex = 9;
            this.btnSyncCopy.Text = "Sync Copy";
            this.btnSyncCopy.UseVisualStyleBackColor = true;
            this.btnSyncCopy.Click += new System.EventHandler(this.btnSyncCopy_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 225);
            this.Controls.Add(this.btnSyncCopy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pbCopy);
            this.Controls.Add(this.btnAsyncCopy);
            this.Controls.Add(this.btnFindTarget);
            this.Controls.Add(this.txtTarget);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFindSource);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.lblSource);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Async File Copy";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.Button btnFindSource;
        private System.Windows.Forms.Button btnFindTarget;
        private System.Windows.Forms.TextBox txtTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAsyncCopy;
        private System.Windows.Forms.ProgressBar pbCopy;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSyncCopy;
    }
}

