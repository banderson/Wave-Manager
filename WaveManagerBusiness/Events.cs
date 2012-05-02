using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveDataContracts;

namespace WaveManagerBusiness
{
    public class Events
    {
        public delegate void FileOpenedEventHandler(WaveFile file);
        public delegate void FileClosedEventHandler(WaveFile file);
        public delegate void WindowSelectedEventHandler(WaveFile file);
        public delegate void RepaintFileList();
    }
}
