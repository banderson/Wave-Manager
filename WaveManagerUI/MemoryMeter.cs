using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using WaveManagerUtil;

namespace WaveManagerUI
{
    public partial class MemoryMeter : UserControl
    {
        int actualMemoryInMegabytes = 0;

        public MemoryMeter()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            Recalculate();
        }

        public void Recalculate()
        {
            _bar.Value = getMemoryInMegaBytes();
        }

        public int getMemoryInMegaBytes()
        {
            long actualMemory = Process.GetCurrentProcess().PrivateMemorySize64;

            actualMemoryInMegabytes = actualMemory.toMegabytes();

            // return the lesser of the actual megabytes vs 100 megabytes (our arbitrary max)
            return NumericHelper.Lesser(actualMemoryInMegabytes, 100);
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            _tooltip.Show(actualMemoryInMegabytes.ToString() + "mb", _bar);
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            _tooltip.Hide(_bar);
        }
    }
}
