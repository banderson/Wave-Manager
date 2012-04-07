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
            this._btnBrowse = new System.Windows.Forms.Button();
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
            // FileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.Controls.Add(this._btnBrowse);
            this.Name = "FileView";
            this.Size = new System.Drawing.Size(204, 459);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _btnBrowse;
    }
}
