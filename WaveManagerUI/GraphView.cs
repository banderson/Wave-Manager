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
    public partial class GraphView : UserControl
    {
        public WaveFile Wave;

        public GraphView()
        {
            InitializeComponent();
            try
            {
                Wave = WaveManagerBusiness.WaveManager.Load(@"C:\Users\Ben\Desktop\Waves\carbrake.wav");
            }
            catch (FileNotFoundException ex)
            {
                //swallow it...
                Wave = new WaveFile();
            }
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // setup the scrolling mechanism
            AutoScrollMinSize = new Size(Wave.NumberOfSamples, 255);
            g.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

            // plot the wave file data
            if (WaveManagerBusiness.WaveManager.IsValid(Wave))
            {
                for (int i = 0; i < Wave.NumberOfSamples - 1; i++)
                {
                    g.DrawLine(Pens.Black, i, Wave.Data[i], i + 1, Wave.Data[i + 1]);
                }
            }
            else
            {
                Font myFont = new Font("Times New Roman", 16);
                g.DrawString("Invalid file: "+ Wave.fileName, myFont, Brushes.Black, 10, 10);
            }

        }
    }
}
