namespace WaveManagerUI
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this._okButton = new System.Windows.Forms.Button();
            this._copyrightLabel = new System.Windows.Forms.Label();
            this._aboutProductLabel = new System.Windows.Forms.Label();
            this._companyNameLabel = new System.Windows.Forms.Label();
            this._productVersionLabel = new System.Windows.Forms.Label();
            this._productNameLabel = new System.Windows.Forms.Label();
            this._aboutProductText = new System.Windows.Forms.TextBox();
            this._logoImage = new System.Windows.Forms.PictureBox();
            this._lblUrl = new System.Windows.Forms.Label();
            this._linkURL = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this._logoImage)).BeginInit();
            this.SuspendLayout();
            // 
            // _okButton
            // 
            this._okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._okButton.Location = new System.Drawing.Point(284, 333);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(123, 28);
            this._okButton.TabIndex = 15;
            this._okButton.Text = "Close";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this.OnCloseButtonClick);
            // 
            // _copyrightLabel
            // 
            this._copyrightLabel.AutoSize = true;
            this._copyrightLabel.Location = new System.Drawing.Point(153, 76);
            this._copyrightLabel.Name = "_copyrightLabel";
            this._copyrightLabel.Size = new System.Drawing.Size(51, 13);
            this._copyrightLabel.TabIndex = 14;
            this._copyrightLabel.Text = "Copyright";
            // 
            // _aboutProductLabel
            // 
            this._aboutProductLabel.AutoSize = true;
            this._aboutProductLabel.Location = new System.Drawing.Point(153, 162);
            this._aboutProductLabel.Name = "_aboutProductLabel";
            this._aboutProductLabel.Size = new System.Drawing.Size(78, 13);
            this._aboutProductLabel.TabIndex = 13;
            this._aboutProductLabel.Text = "About Product:";
            // 
            // _companyNameLabel
            // 
            this._companyNameLabel.AutoSize = true;
            this._companyNameLabel.Location = new System.Drawing.Point(153, 98);
            this._companyNameLabel.Name = "_companyNameLabel";
            this._companyNameLabel.Size = new System.Drawing.Size(82, 13);
            this._companyNameLabel.TabIndex = 12;
            this._companyNameLabel.Text = "Company Name";
            // 
            // _productVersionLabel
            // 
            this._productVersionLabel.AutoSize = true;
            this._productVersionLabel.Location = new System.Drawing.Point(153, 46);
            this._productVersionLabel.Name = "_productVersionLabel";
            this._productVersionLabel.Size = new System.Drawing.Size(82, 13);
            this._productVersionLabel.TabIndex = 11;
            this._productVersionLabel.Text = "Product Version";
            // 
            // _productNameLabel
            // 
            this._productNameLabel.AutoSize = true;
            this._productNameLabel.Location = new System.Drawing.Point(153, 23);
            this._productNameLabel.Name = "_productNameLabel";
            this._productNameLabel.Size = new System.Drawing.Size(75, 13);
            this._productNameLabel.TabIndex = 10;
            this._productNameLabel.Text = "Product Name";
            // 
            // _aboutProductText
            // 
            this._aboutProductText.Enabled = false;
            this._aboutProductText.Location = new System.Drawing.Point(156, 178);
            this._aboutProductText.Multiline = true;
            this._aboutProductText.Name = "_aboutProductText";
            this._aboutProductText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._aboutProductText.Size = new System.Drawing.Size(251, 149);
            this._aboutProductText.TabIndex = 9;
            // 
            // _logoImage
            // 
            this._logoImage.Image = ((System.Drawing.Image)(resources.GetObject("_logoImage.Image")));
            this._logoImage.Location = new System.Drawing.Point(23, 23);
            this._logoImage.Name = "_logoImage";
            this._logoImage.Size = new System.Drawing.Size(110, 317);
            this._logoImage.TabIndex = 8;
            this._logoImage.TabStop = false;
            // 
            // _lblUrl
            // 
            this._lblUrl.AutoSize = true;
            this._lblUrl.Location = new System.Drawing.Point(153, 121);
            this._lblUrl.Name = "_lblUrl";
            this._lblUrl.Size = new System.Drawing.Size(0, 13);
            this._lblUrl.TabIndex = 16;
            // 
            // _linkURL
            // 
            this._linkURL.AutoSize = true;
            this._linkURL.Location = new System.Drawing.Point(153, 119);
            this._linkURL.Name = "_linkURL";
            this._linkURL.Size = new System.Drawing.Size(64, 13);
            this._linkURL.TabIndex = 17;
            this._linkURL.TabStop = true;
            this._linkURL.Text = "company url";
            this._linkURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkClick);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 371);
            this.Controls.Add(this._linkURL);
            this.Controls.Add(this._lblUrl);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._copyrightLabel);
            this.Controls.Add(this._aboutProductLabel);
            this.Controls.Add(this._companyNameLabel);
            this.Controls.Add(this._productVersionLabel);
            this.Controls.Add(this._productNameLabel);
            this.Controls.Add(this._aboutProductText);
            this.Controls.Add(this._logoImage);
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AboutForm";
            this.Load += new System.EventHandler(this.OnAboutFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this._logoImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Label _copyrightLabel;
        private System.Windows.Forms.Label _aboutProductLabel;
        private System.Windows.Forms.Label _companyNameLabel;
        private System.Windows.Forms.Label _productVersionLabel;
        private System.Windows.Forms.Label _productNameLabel;
        private System.Windows.Forms.TextBox _aboutProductText;
        private System.Windows.Forms.PictureBox _logoImage;
        private System.Windows.Forms.Label _lblUrl;
        private System.Windows.Forms.LinkLabel _linkURL;

    }
}