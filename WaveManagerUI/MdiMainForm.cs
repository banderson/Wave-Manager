using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WaveManagerUI
{
    public partial class MdiMainForm : Form
    {
        public MdiMainForm()
        {
            InitializeComponent();
        }

        private void OnNewGraphClick(object sender, EventArgs e)
        {
            MdiForm graphForm = new MdiForm();
            graphForm.MdiParent = this;
            graphForm.Show();
        }

        private void OnAboutClick(object sender, EventArgs e)
        {
            AboutForm _aboutForm = new AboutForm();
            _aboutForm.ShowDialog();
        }

        private void OnHelpIndexClick(object sender, EventArgs e)
        {
            MessageBox.Show("Help:Index not required for this project");
        }
    }
}
