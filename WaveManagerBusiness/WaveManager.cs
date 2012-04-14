using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveDataContracts;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace WaveManagerBusiness
{
    public static class WaveManager
    {
        public static WaveFile Load(string fileName)
        {
            WaveFile file = new WaveFile();
            file.fileName = fileName;
            file.filePath = fileName;

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                br.Read(file.Header, 0, WaveFile.HEADER_SIZE);
                file.NumberOfSamples = br.ReadInt32();
                try
                {
                    file.Data = new byte[file.NumberOfSamples]; // allocates array size, otherwise will crash 
                }
                catch (Exception)
                {
                    // don't load any data...
                    file.NumberOfSamples = 0;
                    file.Data = new byte[0];
                }
                br.Read(file.Data, 0, file.NumberOfSamples);
            }

            return file;
        }

        public static bool IsValid(string fileName)
        {
            var file = WaveManager.Load(fileName);

            return IsValid(file);
        }

        public static bool IsValid(WaveFile file)
        {
            // credit: http://stackoverflow.com/questions/472906/net-string-to-byte-array-c-sharp
            //BinaryFormatter bfx = new BinaryFormatter();
            //MemoryStream msx = new MemoryStream();
            //msx.Write(file.Header, 0, 2);
            //msx.Seek(0, 0);

            // first 4-bytes from header (there are probably many other better ways to do this...)
            var test = "";
            for (int i = 0; i < 4; i++)
            {
                test += (char)file.Header[i];
            }

            // check if the first 4 bytes of the header are "RIFF"
            return test == WaveFile.HEADER_PREFIX;
        }
    }
}
