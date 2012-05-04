namespace WaveManagerUI
{
    partial class MdiForm
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
            this._graphView = new WaveManagerUI.GraphView();
            this.SuspendLayout();
            // 
            // _graphView
            // 
            this._graphView.BackColor = System.Drawing.Color.CornflowerBlue;
            this._graphView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._graphView.Location = new System.Drawing.Point(0, 0);
            this._graphView.Name = "_graphView";
            this._graphView.RenderStrategy = WaveManagerUI.GraphView.RenderStyle.Standard;
            this._graphView.Size = new System.Drawing.Size(884, 441);
            this._graphView.TabIndex = 0;
            // 
            // MdiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 441);
            this.Controls.Add(this._graphView);
            this.Name = "MdiForm";
            this.Text = "Graph";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.Load += new System.EventHandler(this.MdiForm_Load);
            this.Enter += new System.EventHandler(this.OnFocus);
            this.ResumeLayout(false);

        }

        #endregion

        private GraphView _graphView;
    }
}