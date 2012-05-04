using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WaveDataContracts;

namespace WaveManagerUI
{
    public partial class MdiForm : Form
    {
        public WaveFile Wave { get; set; }

        public MdiForm() : this(null)
            {}
        
        public MdiForm(WaveFile file)
        {
            InitializeComponent();
            Initialize(file);
        }

        public void Initialize(WaveFile file)
        {
            this.Wave = file;
            this._graphView.Wave = file;
            if (Wave != null)
            {
                this.Text = Wave.fileName;
            }
            else
            {
                this.Text = "(New File)";
            }
        }

        public void ReInitialize(WaveFile file)
        {
            Initialize(file);
        }

        private void MdiForm_Load(object sender, EventArgs e)
        {

        }

        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            WaveManagerBusiness.WaveManager.FireFileClosed(Wave);
        }

        private void OnFocus(object sender, EventArgs e)
        {
            WaveManagerBusiness.WaveManager.FireWindowSelected(this.Wave);
        }

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            if (Wave.IsModified())
            {
                DialogResult r = MessageBox.Show("Save changes to "+ Wave.fileName +"?", "Save", MessageBoxButtons.YesNoCancel);
                if (r == DialogResult.Yes)
                    WaveManagerBusiness.WaveManager.Save(Wave);
                else if (r == DialogResult.Cancel)
                    e.Cancel = true; // leave the file open
            }
        }
    }
}
