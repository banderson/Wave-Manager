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
using System.Runtime.InteropServices;

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

            InitVolumneControl();
        }

        // these are the PInvoke calls to get/set the volume control
        // NOTE: much of this is adapted from http://www.geekpedia.com/tutorial176_Get-and-set-the-wave-sound-volume.html
        [DllImport("winmm.dll")]
        public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);
        [DllImport("winmm.dll")]
        public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);

        private void InitVolumneControl()
        {
            uint vol = 0;
            waveOutGetVolume(IntPtr.Zero, out vol);
            // Calculate the volume
            ushort CalcVol = (ushort)(vol & 0x0000ffff);
            // Get the volume on a scale of 1 to 10 (to fit the trackbar)
            _volumeControl.Value = CalcVol / (ushort.MaxValue / 10);
        }

        private void OnVolumeChange(object sender, EventArgs e)
        {
            // Calculate the volume that's being set
            int NewVolume = ((ushort.MaxValue / 10) * _volumeControl.Value);
            // Set the same volume for both the left and the right channels
            uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
            // Set the volume
            waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
        }

        // END of volume control

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
