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
            this.graphView1 = new WaveManagerUI.GraphView();
            this.SuspendLayout();
            // 
            // graphView1
            // 
            this.graphView1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.graphView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphView1.Location = new System.Drawing.Point(0, 0);
            this.graphView1.Name = "graphView1";
            this.graphView1.Size = new System.Drawing.Size(555, 441);
            this.graphView1.TabIndex = 0;
            // 
            // MdiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 441);
            this.Controls.Add(this.graphView1);
            this.Name = "MdiForm";
            this.Text = "Graph";
            this.ResumeLayout(false);

        }

        #endregion

        private GraphView graphView1;
    }
}