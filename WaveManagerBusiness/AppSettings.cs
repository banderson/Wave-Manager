using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Drawing;

namespace WaveManagerBusiness
{
    [KnownType(typeof(FontStyle))]
    [KnownType(typeof(GraphicsUnit))]
    [Serializable]
    public class AppSettings
    {
        public Color bgColor;
        public Color textColor;
        public Font font;

        public Color canvasColor;
        public Color lineColor;
        public short lineWidth;

        public AppSettings()
        {
            bgColor = Color.Azure;
            textColor = Color.Black;
            font = new Font("Helvetica", 12.0f);

            canvasColor = Color.CornflowerBlue;
            lineColor = Color.BlanchedAlmond;
        }

        public void Modulate()
        {

        }
    }
}