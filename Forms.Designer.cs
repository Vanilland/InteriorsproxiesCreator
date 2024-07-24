namespace InteriorsproxiesCreator
{
    partial class Forms
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.resourcefolder = new System.Windows.Forms.TextBox();
            this.doStartButton = new System.Windows.Forms.Button();
            this.openDirDialog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "resourcesフォルダを選択";
            // 
            // resourcefolder
            // 
            this.resourcefolder.AllowDrop = true;
            this.resourcefolder.Location = new System.Drawing.Point(13, 29);
            this.resourcefolder.Name = "resourcefolder";
            this.resourcefolder.Size = new System.Drawing.Size(440, 19);
            this.resourcefolder.TabIndex = 1;
            this.resourcefolder.DragDrop += new System.Windows.Forms.DragEventHandler(this.resourcefolder_DragDrop);
            this.resourcefolder.DragEnter += new System.Windows.Forms.DragEventHandler(this.resourcefolder_DragEnter);
            // 
            // doStartButton
            // 
            this.doStartButton.Location = new System.Drawing.Point(13, 55);
            this.doStartButton.Name = "doStartButton";
            this.doStartButton.Size = new System.Drawing.Size(93, 25);
            this.doStartButton.TabIndex = 2;
            this.doStartButton.Text = "開始";
            this.doStartButton.UseVisualStyleBackColor = true;
            this.doStartButton.Click += new System.EventHandler(this.doStartButton_Click);
            // 
            // openDirDialog
            // 
            this.openDirDialog.Location = new System.Drawing.Point(460, 29);
            this.openDirDialog.Name = "openDirDialog";
            this.openDirDialog.Size = new System.Drawing.Size(75, 23);
            this.openDirDialog.TabIndex = 3;
            this.openDirDialog.Text = "参照";
            this.openDirDialog.UseVisualStyleBackColor = true;
            this.openDirDialog.Click += new System.EventHandler(this.openDirDialog_Click);
            // 
            // Forms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 89);
            this.Controls.Add(this.openDirDialog);
            this.Controls.Add(this.doStartButton);
            this.Controls.Add(this.resourcefolder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Forms";
            this.Text = "InteriorsproxiesCreator";
            this.Load += new System.EventHandler(this.Forms_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox resourcefolder;
        private System.Windows.Forms.Button doStartButton;
        private System.Windows.Forms.Button openDirDialog;
    }
}

