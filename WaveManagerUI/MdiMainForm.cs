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
using WaveManagerBusiness;
using System.Media;

namespace WaveManagerUI
{
    public partial class MdiMainForm : Form
    {

        List<ToolStripItem> hideIfBlank, hideIfUnModified;

        public MdiMainForm()
        {
            InitializeComponent();
            this.AllowDrop = true;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            WaveManagerBusiness.WaveManager.FileOpened += OpenExistingFile;
            WaveManagerBusiness.WaveManager.WindowSelected += UpdateToolbarOptionsForCurrentWindow;
            WaveManagerBusiness.WaveManager.FileClosed += UpdateToolbarOptionsForCurrentWindow;
            WaveManagerBusiness.WaveManager.CurrentWindowModified += UpdateToolbarOptionsForCurrentWindow;

            hideIfBlank = new List<ToolStripItem> 
                                {   _btnCopy, _btnCut, _btnPaste, _btnDelete, _btnModule, 
                                    _btnRotate, _btnPlay, _btnPrint, _btnSave, _btnViewMode };

            hideIfUnModified = new List<ToolStripItem> { _btnSave };

            UpdateToolbarOptionsForCurrentWindow();
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

        private void OnBackgroundColorClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = WaveManagerBusiness.WaveManager.GetSettings().canvasColor;

            // If the file name is not an empty string open it for saving.
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                WaveManagerBusiness.WaveManager.UpdateSettings(s => s.canvasColor = dlg.Color);
            }
        }

        private void OnLineColorClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = WaveManagerBusiness.WaveManager.GetSettings().lineColor;

            // If the file name is not an empty string open it for saving.
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                WaveManagerBusiness.WaveManager.UpdateSettings(s => s.lineColor = dlg.Color);
            }
        }

        private void OnLineThicknessChange(object sender, EventArgs e)
        {
            // if the update was made, then update the app settings
            short width = 1;
            if (Int16.TryParse(sender.ToString(), out width))
                WaveManagerBusiness.WaveManager.UpdateSettings(s => s.lineWidth = width);
        }

        private void OnModulate(object sender, EventArgs e)
        {
            WaveManagerBusiness.WaveManager.ModulateWave();
        }

        private void OnRotateClick(object sender, EventArgs e)
        {
            WaveManagerBusiness.WaveManager.RotateWave();
        }

        private void UpdateToolbarOptionsForCurrentWindow(WaveFile file)
        {
            UpdateToolbarOptionsForCurrentWindow();
        }

        private void UpdateToolbarOptionsForCurrentWindow()
        {
            hideIfBlank.ForEach(b => b.Enabled = !WaveManagerBusiness.WaveManager.IsCurrentDocumentBlank());
            hideIfUnModified.ForEach(b => b.Enabled = WaveManagerBusiness.WaveManager.IsCurrentDocumentModified());
        }

        private void OnPlay(object sender, EventArgs e)
        {
            if (WaveManagerBusiness.WaveManager.ActiveFile.IsModified())
                MessageBox.Show("You must save the current file first.");

            // this should play the actively selected file
            new SoundPlayer(WaveManagerBusiness.WaveManager.ActiveFile.filePath).Play();
        }
    }
}
