using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WaveDataContracts
{
    public class WaveFile
    {
        public const int HEADER_SIZE = 40;
        public const string HEADER_PREFIX = "RIFF";

        public byte[] Header { get; set; }
        public byte[] Data { get; set; }
        public int NumberOfSamples { get; set; }

        public string fileName { get; set; }
        public string filePath { get; set; }

        public WaveFile()
        {
            NumberOfSamples = 0;
            Header = new byte[HEADER_SIZE];
        }
    }
}
