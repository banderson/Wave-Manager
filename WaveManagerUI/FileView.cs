using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WaveDataContracts;
using System.IO;
using WaveManagerBusiness;

namespace WaveManagerUI
{
    public partial class FileView : UserControl
    {
        AppSettings _settings = WaveManagerBusiness.WaveManager.GetSettings();

        public FileView()
        {
            InitializeComponent();
            this.AllowDrop = true;
            WaveManagerBusiness.WaveManager.RepaintFileList += InitPanel;
            _fileList.ImageList = _fileListIcons;
            _fileList.BackColor = _settings.bgColor;
            _fileList.Font = _settings.font;
            _fileList.ForeColor = _settings.textColor;
        }

        private void InitPanel()
        {
            _fileList.BackColor = _settings.bgColor;
            _fileList.Font = _settings.font;
            _fileList.ForeColor = _settings.textColor;
            RedrawFiles();
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show(e.Data.ToString());
        }

        private void RedrawFiles()
        {
            _fileList.Nodes.Clear();

            foreach (string dir in WaveManagerBusiness.WaveManager.GetDirectories())
            {
                PaintDirectoryNodes(dir);
            }
        }

        private void PaintDirectoryNodes(string dir)
        {
            //File d = File.
            if (Directory.Exists(dir))
            {
                var dirNode = AddDirectoryNode(dir);
                foreach (string file in Directory.GetFiles(dir).Where(x => Path.GetExtension(x).Equals(".wav")))
                {
                    // TODO: how do we avoid this, and just lazy load the wave file objects as needed?
                    //      Maybe: just add the key with a null object and then update the object when it's actually opened
                    AddFileNode(file, dirNode);
                }
                dirNode.ExpandAll();
            }
        }

        private TreeNode AddDirectoryNode(string folderName)
        {
            var node = _fileList.Nodes.Add(folderName);
            node.ImageIndex = node.SelectedImageIndex = 0;

            return node;
        }

        private TreeNode AddFileNode(string fileName, TreeNode parent)
        {
            var node = parent.Nodes.Add(Path.GetFileName(fileName));
            node.ImageIndex = node.SelectedImageIndex = 1;
            // store the full file path so we can load the file when clicked
            node.Tag = fileName;

            return node;
        }

        private void OnDblClick(object sender, EventArgs e)
        {
            if (_fileList.SelectedNode.Tag != null)
            {
                WaveManagerBusiness.WaveManager.OpenFile(_fileList.SelectedNode.Tag.ToString());
            }
        }

        private void OnFontChangeClick(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            dlg.ShowColor = true;
            dlg.Color = _settings.textColor;
            dlg.Font = _settings.font;

            // If the file name is not an empty string open it for saving.
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                WaveManagerBusiness.WaveManager.UpdateSettings(s => s.font = dlg.Font);
                WaveManagerBusiness.WaveManager.UpdateSettings(s => s.textColor = dlg.Color);
            }

            InitPanel();
        }

        private void OnBGChangeClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = _settings.bgColor;

            // If the file name is not an empty string open it for saving.
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                WaveManagerBusiness.WaveManager.UpdateSettings(s => s.bgColor = dlg.Color);
            }

            InitPanel();
        }

        private void OnRightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _settingsContext.Show(_fileList, e.X, e.Y);
            }
        }
    }
}
