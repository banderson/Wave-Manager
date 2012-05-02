namespace WaveManagerUI
{
    partial class FileView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileView));
            this._btnBrowse = new System.Windows.Forms.Button();
            this._fileList = new System.Windows.Forms.TreeView();
            this._fileListIcons = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // _btnBrowse
            // 
            this._btnBrowse.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._btnBrowse.Location = new System.Drawing.Point(0, 416);
            this._btnBrowse.Name = "_btnBrowse";
            this._btnBrowse.Size = new System.Drawing.Size(204, 43);
            this._btnBrowse.TabIndex = 0;
            this._btnBrowse.Text = "Browse";
            this._btnBrowse.UseVisualStyleBackColor = true;
            // 
            // _fileList
            // 
            this._fileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._fileList.Location = new System.Drawing.Point(0, 0);
            this._fileList.Name = "_fileList";
            this._fileList.Size = new System.Drawing.Size(204, 416);
            this._fileList.TabIndex = 1;
            this._fileList.DoubleClick += new System.EventHandler(this.OnDblClick);
            // 
            // _fileListIcons
            // 
            this._fileListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_fileListIcons.ImageStream")));
            this._fileListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this._fileListIcons.Images.SetKeyName(0, "Folder-close.ico");
            this._fileListIcons.Images.SetKeyName(1, "Wav.ico");
            // 
            // FileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Controls.Add(this._fileList);
            this.Controls.Add(this._btnBrowse);
            this.Name = "FileView";
            this.Size = new System.Drawing.Size(204, 459);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btnBrowse;
        private System.Windows.Forms.TreeView _fileList;
        private System.Windows.Forms.ImageList _fileListIcons;
    }
}
