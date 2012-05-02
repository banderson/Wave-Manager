using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WaveDataContracts;
using System.IO;

namespace WaveManagerUI
{
    public partial class MdiMainForm : Form
    {
        public MdiMainForm()
        {
            InitializeComponent();
            this.AllowDrop = true;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            WaveManagerBusiness.WaveManager.FileOpened += OpenExistingFile;
        }

        private void OnNewGraphClick(object sender, EventArgs e)
        {
            MdiForm graphForm = new MdiForm();
            graphForm.MdiParent = this;
            graphForm.Show();
        }

        private void OnOpenClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "WAVE Files|*.wav";
            dlg.Title = "Open Wave File";

            // If the file name is not an empty string open it for saving.
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //KeyPassMgr.OpenDocument(dlg.FileName);
                    WaveFile file = WaveManagerBusiness.WaveManager.OpenFile(dlg.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid file, please choose a file compatible with this application.");
                }
            }
        }

        protected void OpenExistingFile(WaveFile file)
        {
            MdiForm graphForm = new MdiForm(file);
            graphForm.MdiParent = this;
            graphForm.Show();

            // start tracking the current directory
            var directory = Path.GetDirectoryName(file.filePath);
            WaveManagerBusiness.WaveManager.AddDirectory(directory);

            // redraw the file list in the left panel
            WaveManagerBusiness.WaveManager.FireRepaintFileList();
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

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            WaveFile f = new WaveFile();
            foreach (string file in files)
            {
                f = WaveManagerBusiness.WaveManager.OpenFile(file);
            }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void OnToolbarToggle(object sender, EventArgs e)
        {
            _toolbarToggle.Checked = !_toolbarToggle.Checked;
            _toolbar.Visible = _toolbarToggle.Checked;
        }

        private void OnStatusToggle(object sender, EventArgs e)
        {
            _statusToggle.Checked = !_statusToggle.Checked;
            _statusBar.Visible = _statusToggle.Checked;
        }

        private void OnViewModeToggle(object sender, EventArgs e)
        {
            WaveManagerBusiness.WaveManager.FireViewModeChanged();
        }
    }
}
