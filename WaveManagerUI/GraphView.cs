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
using WaveManagerUtil;
using WaveManagerBusiness;

namespace WaveManagerUI
{
    public partial class GraphView : UserControl
    {
        public WaveFile Wave;
        public RenderStyle RenderStrategy { get; set; }

        public enum RenderStyle
        {
            Standard,
            Full
        }

        public GraphView()
        {
            InitializeComponent();
            RenderStrategy = RenderStyle.Standard;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            WaveManagerBusiness.WaveManager.ViewModeChanged += AdjustViewMode;
            WaveManagerBusiness.WaveManager.AppSettingsChanged += RePaint;
            WaveManagerBusiness.WaveManager.CurrentWindowModified += RePaintCurrent;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (Wave == null || !Wave.IsValid()) return;

            AppSettings settings = WaveManagerBusiness.WaveManager.GetSettings();

            this.BackColor = settings.canvasColor;

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); 
            SetStyle(ControlStyles.OptimizedDoubleBuffer,true); 
            SetStyle(ControlStyles.UserPaint, true);

            Graphics g = e.Graphics;

            // the maximum sample in the set
            int maxValue = Wave.Data.Max();

            // setup the scrolling mechanism
            switch (RenderStrategy)
            {
                case RenderStyle.Standard:
                    AutoScrollMinSize = new Size(Wave.NumberOfSamples, maxValue);
                    g.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
                    break;
                case RenderStyle.Full:
                    AutoScrollMinSize = ClientRectangle.Size;
                    float xScaleFactor = (float)ClientRectangle.Size.Width / Wave.NumberOfSamples;
                    float yScaleFactor = (float)ClientRectangle.Size.Height / maxValue;
                    g.ScaleTransform(xScaleFactor, yScaleFactor);
                    break;
            }

            // plot the wave file data
            if (WaveManagerBusiness.WaveManager.IsValid(Wave))
            {
                Pen p = new Pen(settings.lineColor, settings.lineWidth);
                for (int i = 0; i < Wave.NumberOfSamples - 1; i++)
                {
                    g.DrawLine(p, i, Wave.Data[i], i + 1, Wave.Data[i + 1]);
                }
            }
            else
            {
                Font myFont = new Font("Times New Roman", 16);
                g.DrawString("Invalid file: "+ Wave.fileName, myFont, Brushes.Black, 10, 10);
            }
        }

        private void AdjustViewMode()
        {
            // this should only apply to the currently selected window
            if (Wave != WaveManagerBusiness.WaveManager.GetActiveFile())
                return;

            // toggle the render mode
            RenderStrategy = (RenderStrategy == RenderStyle.Full)
                                ? RenderStyle.Standard
                                : RenderStyle.Full;
            RePaint();
        }

        private void RePaintCurrent()
        {
            // this should only apply to the currently selected window
            if (Wave != WaveManagerBusiness.WaveManager.GetActiveFile())
                return;

            RePaint();
        }

        private void RePaint()
        {
            Invalidate();
        }
    }
}
