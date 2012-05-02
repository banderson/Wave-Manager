namespace WaveManagerUI
{
    partial class StatusBar
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
            this._layoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._errorList = new System.Windows.Forms.ComboBox();
            this._wavesCount = new System.Windows.Forms.Label();
            this._samplesCount = new System.Windows.Forms.Label();
            this._volumeControl = new System.Windows.Forms.TrackBar();
            this._memoryMeter = new WaveManagerUI.MemoryMeter();
            this._layoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._volumeControl)).BeginInit();
            this.SuspendLayout();
            // 
            // _layoutPanel
            // 
            this._layoutPanel.ColumnCount = 5;
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.16931F));
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.83069F));
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this._layoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this._layoutPanel.Controls.Add(this._errorList, 1, 0);
            this._layoutPanel.Controls.Add(this._wavesCount, 2, 0);
            this._layoutPanel.Controls.Add(this._samplesCount, 3, 0);
            this._layoutPanel.Controls.Add(this._volumeControl, 4, 0);
            this._layoutPanel.Controls.Add(this._memoryMeter, 0, 0);
            this._layoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._layoutPanel.Location = new System.Drawing.Point(0, 0);
            this._layoutPanel.Name = "_layoutPanel";
            this._layoutPanel.RowCount = 1;
            this._layoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._layoutPanel.Size = new System.Drawing.Size(782, 36);
            this._layoutPanel.TabIndex = 0;
            // 
            // _errorList
            // 
            this._errorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._errorList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._errorList.FormattingEnabled = true;
            this._errorList.Location = new System.Drawing.Point(71, 3);
            this._errorList.Name = "_errorList";
            this._errorList.Size = new System.Drawing.Size(485, 21);
            this._errorList.TabIndex = 1;
            // 
            // _wavesCount
            // 
            this._wavesCount.AutoSize = true;
            this._wavesCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._wavesCount.Location = new System.Drawing.Point(562, 0);
            this._wavesCount.Name = "_wavesCount";
            this._wavesCount.Size = new System.Drawing.Size(77, 36);
            this._wavesCount.TabIndex = 2;
            this._wavesCount.Text = "Waves:  0";
            this._wavesCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _samplesCount
            // 
            this._samplesCount.AutoSize = true;
            this._samplesCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._samplesCount.Location = new System.Drawing.Point(645, 0);
            this._samplesCount.Name = "_samplesCount";
            this._samplesCount.Size = new System.Drawing.Size(51, 36);
            this._samplesCount.TabIndex = 3;
            this._samplesCount.Text = "Samples: 0";
            this._samplesCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _volumeControl
            // 
            this._volumeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._volumeControl.Location = new System.Drawing.Point(702, 3);
            this._volumeControl.Name = "_volumeControl";
            this._volumeControl.Size = new System.Drawing.Size(77, 30);
            this._volumeControl.TabIndex = 4;
            // 
            // _memoryMeter
            // 
            this._memoryMeter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._memoryMeter.Enabled = false;
            this._memoryMeter.Location = new System.Drawing.Point(3, 3);
            this._memoryMeter.Name = "_memoryMeter";
            this._memoryMeter.Size = new System.Drawing.Size(62, 30);
            this._memoryMeter.TabIndex = 5;
            // 
            // StatusBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._layoutPanel);
            this.Name = "StatusBar";
            this.Size = new System.Drawing.Size(782, 36);
            this.Load += new System.EventHandler(this.OnLoad);
            this._layoutPanel.ResumeLayout(false);
            this._layoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._volumeControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel _layoutPanel;
        private System.Windows.Forms.ComboBox _errorList;
        private System.Windows.Forms.Label _wavesCount;
        private System.Windows.Forms.Label _samplesCount;
        private System.Windows.Forms.TrackBar _volumeControl;
        private MemoryMeter _memoryMeter;
    }
}
