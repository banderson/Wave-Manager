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
using System.Drawing.Printing;

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
            if (print == false)
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
            //// Save off Screen bitmap to a file 
            offScreenBmp.Save(fileName, ImageFormat.Png);
        }

        // TODO: is there any way around this horrible global?
        int _pageNumber = 0;
        public void PrintStandard(PrintPageEventArgs printArgs)
        {
            var g = printArgs.Graphics;
            // move to the starting point, factoring in margins
            Point origin = printArgs.MarginBounds.Location;
            g.TranslateTransform(origin.X, origin.Y);

            // paint the bg color
            g.Clear(this.settings.canvasColor);

            // get the effective width of the page
            int pageWidth = printArgs.MarginBounds.Width;

            // calculate the number of pages (+1 to hold the remainder)
            int totalPages = Wave.NumberOfSamples / pageWidth + 1;
            int offsetX = 0;

            // draw the points
            for (int i = (_pageNumber*pageWidth); i < Wave.NumberOfSamples - 1; i++)
            {
                offsetX = i % pageWidth;

                g.DrawLine(pen, offsetX, Wave.Data[i], offsetX + 1, Wave.Data[i + 1]);

                if (offsetX >= pageWidth-1)
                {
                    printArgs.HasMorePages = true;
                    offsetX = 0;
                    _pageNumber++;
                    return;
                }
                else
                {
                    printArgs.HasMorePages = false;
                }
            }

            // yikes...
            _pageNumber = 0;
            printArgs.HasMorePages = false;
        }

        public void PrintFull(PrintPageEventArgs printArgs)
        {
            var g = printArgs.Graphics;

            // move to the starting point, factoring in margins
            Point origin = printArgs.MarginBounds.Location; 
            g.TranslateTransform(origin.X, origin.Y);

            // paint the bg color
            g.Clear(this.settings.canvasColor);

            // the maximum sample in the set
            int maxValue = Wave.MaxDataPoint();

            // scale the graphic to fit page dimensions
            float xScaleFactor = (float)printArgs.MarginBounds.Width / Wave.NumberOfSamples;
            float yScaleFactor = (float)printArgs.MarginBounds.Height / maxValue;
            g.ScaleTransform(xScaleFactor, yScaleFactor);

            // draw the points
            for (int i = 0; i < Wave.NumberOfSamples - 1; i++)
            {
                g.DrawLine(pen, i, Wave.Data[i], i + 1, Wave.Data[i + 1]);
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
