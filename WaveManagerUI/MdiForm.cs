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
    }
}
