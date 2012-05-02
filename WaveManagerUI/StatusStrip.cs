using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WaveDataContracts;
using WaveManagerBusiness;

namespace WaveManagerUI
{
    public partial class StatusBar : UserControl
    {
        public StatusBar()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            WaveManagerBusiness.WaveManager.FileOpened += RefreshMemoryCounter;
            WaveManagerBusiness.WaveManager.FileClosed += RefreshMemoryCounter;

            WaveManagerBusiness.WaveManager.FileOpened += UpdateFileCount;
            WaveManagerBusiness.WaveManager.FileClosed += UpdateFileCount;

            WaveManagerBusiness.WaveManager.FileClosed += UpdateSampleCount;
            WaveManagerBusiness.WaveManager.WindowSelected += UpdateSampleCount;
        }

        private void UpdateFileCount(WaveFile file)
        {
            _wavesCount.Text = "Waves: "+ WaveManagerBusiness.WaveManager.GetOpenFilesCount().ToString();
        }

        private void UpdateSampleCount(WaveFile file)
        {
            _samplesCount.Text = "Samples: " + WaveManagerBusiness.WaveManager.GetActiveFile().NumberOfSamples.ToString();
        }

        private void RefreshMemoryCounter(WaveFile file)
        {
            _memoryMeter.Recalculate();
        }
    }
}
