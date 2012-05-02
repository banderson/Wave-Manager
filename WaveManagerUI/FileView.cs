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

namespace WaveManagerUI
{
    public partial class FileView : UserControl
    {
        public FileView()
        {
            InitializeComponent();
            this.AllowDrop = true;
            WaveManagerBusiness.WaveManager.RepaintFileList += LoadFolder;
        }

        private void LoadFolder()
        {
            RedrawFiles();
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show(e.Data.ToString());
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {

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
            }
        }

        private TreeNode AddDirectoryNode(string folderName)
        {
            var node = _fileList.Nodes.Add(folderName);
            _fileList.SelectedNode = node;

            return node;
        }

        private TreeNode AddFileNode(string fileName, TreeNode parent)
        {
            var node = parent.Nodes.Add(Path.GetFileName(fileName));
            // store the full file path so we can load the file when clicked
            node.Tag = fileName;

            return node;
        }

        private void OnDblClick(object sender, EventArgs e)
        {
            WaveManagerBusiness.WaveManager.OpenFile(_fileList.SelectedNode.Tag.ToString());
        }
    }
}
