﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
            }
        }
    }
}
