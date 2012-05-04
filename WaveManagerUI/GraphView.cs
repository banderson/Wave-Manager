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
using System.Drawing.Imaging;

namespace WaveManagerUI
{
    public partial class GraphView : UserControl
    {
        public WaveFile Wave;
        public RenderStyle RenderStrategy { get; set; }
        AppSettings settings;
        Pen pen;

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

            settings = WaveManagerBusiness.WaveManager.GetSettings();
            this.BackColor = settings.canvasColor;
            this.pen = new Pen(settings.lineColor, settings.lineWidth);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            RenderToGraphics(e.Graphics);
        }

        private void RenderToGraphics(Graphics canvas, bool print = false)
        {
            // bail now if there is no wave file to draw
            if (Wave == null || !Wave.IsValid())
            {
                MessageBox.Show("Invalid file! Please select a valid WAV file...");
                this.ParentForm.Close();
                WaveManagerBusiness.WaveManager.FireInvalidFileOpened(Wave);
                return;
            }

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);

            Graphics g = canvas;

            // the maximum sample in the set
            int maxValue = Wave.MaxDataPoint();

            // setup the scrolling mechanism
            switch (RenderStrategy)
            {
                case RenderStyle.Standard:
                    AutoScrollMinSize = new Size(Wave.NumberOfSamples, maxValue);
                    if (print == false)
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
                for (int i = 0; i < Wave.NumberOfSamples - 1; i++)
                {
                    canvas.DrawLine(pen, i, Wave.Data[i], i + 1, Wave.Data[i + 1]);
                }
            }
            else
            {
                Font myFont = new Font("Times New Roman", 16);
                g.DrawString("Invalid file: " + Wave.fileName, myFont, Brushes.Black, 10, 10);
            }
        }

        // TODO: figure out how to do this
        public void SaveToDisk(string fileName)
        {
            Bitmap offScreenBmp = null;
            switch (RenderStrategy)
            {
                case RenderStyle.Standard:
                    offScreenBmp = new Bitmap(Wave.NumberOfSamples, Wave.Data.Max()); 
                    break;
                case RenderStyle.Full:
                    offScreenBmp = new Bitmap(this.Width, this.Height); 
                    break;
            }
            
            Graphics offScreenGraphics = Graphics.FromImage(offScreenBmp); // do drawing in offScreenGC
            offScreenGraphics.Clear(this.settings.canvasColor);
            RenderToGraphics(offScreenGraphics, true);
            //clientGC.DrawImage(offScreenBmp , 0, 0);
            //// Save off Screen bitmap to a file 
            offScreenBmp.Save(fileName, ImageFormat.Png);
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
