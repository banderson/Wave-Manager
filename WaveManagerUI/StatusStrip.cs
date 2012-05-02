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
        }

        private void RefreshMemoryCounter(WaveFile file)
        {
            _memoryMeter.Recalculate();
        }
    }
}
