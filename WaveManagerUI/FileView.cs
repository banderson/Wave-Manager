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
            WaveManagerBusiness.WaveManager.FileOpened += LoadFolder;
        }

        private void LoadFolder(WaveFile file)
        {
            var directory = Path.GetDirectoryName(file.filePath);
            MessageBox.Show("Load the folder: " + directory);
        }



        private void OnDragDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show(e.Data.ToString());
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {

        }
    }
}
