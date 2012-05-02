namespace WaveManagerUI
{
    partial class MemoryMeter
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
            this._bar = new System.Windows.Forms.ProgressBar();
            this._tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // _bar
            // 
            this._bar.Dock = System.Windows.Forms.DockStyle.Fill;
            this._bar.Location = new System.Drawing.Point(0, 0);
            this._bar.Name = "_bar";
            this._bar.Size = new System.Drawing.Size(159, 75);
            this._bar.TabIndex = 1;
            this._bar.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this._bar.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            this._bar.MouseHover += new System.EventHandler(this.OnMouseEnter);
            // 
            // _tooltip
            // 
            this._tooltip.ToolTipTitle = "Memory Usage";
            // 
            // MemoryMeter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._bar);
            this.Name = "MemoryMeter";
            this.Size = new System.Drawing.Size(159, 75);
            this.Load += new System.EventHandler(this.OnLoad);
            this.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            this.MouseHover += new System.EventHandler(this.OnMouseEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar _bar;
        private System.Windows.Forms.ToolTip _tooltip;
    }
}
