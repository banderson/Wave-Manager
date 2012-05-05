using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WaveDataContracts
{
    public class WaveFile : ICloneable
    {
        public const int HEADER_SIZE = 40;
        public const string HEADER_PREFIX = "RIFF";

        public byte[] Header { get; set; }
        public byte[] Data { get; set; }
        public int NumberOfSamples { get; set; }

        public string fileName { get; set; }
        public string filePath { get; set; }

        private Boolean _isModified = false;

        public WaveFile()
        {
            NumberOfSamples = 0;
            _maxDataPoint = 0;
            Header = new byte[HEADER_SIZE];
        }

        public WaveFile(string fileName)
            : this()
        {
            Load(fileName);
        }

        public void Load(string fileName)
        {
            this.fileName = fileName;
            this.filePath = fileName;

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                br.Read(this.Header, 0, WaveFile.HEADER_SIZE);
                this.NumberOfSamples = br.ReadInt32();
                try
                {
                    // allocates array size, otherwise will crash 
                    this.Data = new byte[this.NumberOfSamples];
                }
                catch (Exception)
                {
                    // don't load any data...
                    this.NumberOfSamples = 0;
                    this.Data = new byte[0];
                }
                br.Read(this.Data, 0, this.NumberOfSamples);

                // save the maximum data point
                if (Data.Length > 0)
                    _maxDataPoint = Data.Max();
            }
        }

        public void Save()
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            using (BinaryWriter br = new BinaryWriter(fs))
            {
                br.Write(this.Header, 0, WaveFile.HEADER_SIZE);
                br.Write(this.NumberOfSamples);
                br.Write(this.Data);
            }

            this._isModified = false;
        }

        public void SaveAs(string fileName)
        {
            // assign the new file name
            this.fileName = this.filePath = fileName;

            Save();
        }

        public void Modulate()
        {
            if (!IsValid())
                return;

            for (int i = 0; i < NumberOfSamples - 1; i++)
            {
                Data[i] = (byte)(Math.Sin(i+3.2f)*20 + Data[i]);
            }
        }

        public void Flip()
        {
            if (!IsValid())
                return;

            for (int i = 0; i < NumberOfSamples - 1; i++)
            {
                Data[i] = (byte)(255 - Data[i]);
            }
        }

        public Boolean IsValid()
        {            // first 4-bytes from header (there are probably many other better ways to do this...)
            var test = "";
            for (int i = 0; i < 4; i++)
            {
                test += (char)Header[i];
            }

            // check if the first 4 bytes of the header are "RIFF"
            return test == WaveFile.HEADER_PREFIX;
        }

        public Boolean IsEmpty()
        {
            return NumberOfSamples == 0;
        }

        public bool IsModified()
        {
            return _isModified;
        }

        public void MarkAsModified()
        {
            _isModified = true;
        }

        int _maxDataPoint;
        public int MaxDataPoint()
        {
            if (Data.Length == 0)
                return 0;

            return _maxDataPoint;
        }

        public void ClearData()
        {
            NumberOfSamples = 0;
            Data = new byte[0];
        }

        public object Clone()
        {
            var copy = new WaveFile();
            copy.fileName = fileName;
            copy.filePath = filePath;
            copy.NumberOfSamples = NumberOfSamples;
            copy.Header = new byte[WaveFile.HEADER_SIZE];
            copy.Data = new byte[NumberOfSamples];

            // copy the header data
            for (int i = 0; i < WaveFile.HEADER_SIZE; i++)
            {
                copy.Header[i] = this.Header[i];
            }

            // copy the wave data
            for (int i = 0; i < NumberOfSamples; i++)
            {
                copy.Data[i] = this.Data[i];
            }

            return copy;
        }
    }
}
