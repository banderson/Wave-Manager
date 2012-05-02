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
        /*** Events ***/
        public static event Events.FileOpenedEventHandler FileOpened;
        public static event Events.FileClosedEventHandler FileClosed;

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
                    // allocates array size, otherwise will crash 
                    file.Data = new byte[file.NumberOfSamples]; 
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

        public static WaveFile OpenFile(string fileName)
        {
            WaveFile file = null;

            try
            {
                file = Load(fileName);
            }
            catch (FileNotFoundException)
            {
                //swallow it...
            }

            FireFileOpened(file);

            return file;
        }

        public static void FireFileOpened(WaveFile file)
        {
            if (FileOpened != null)
                FileOpened.Invoke(file);
        }

        public static void FireFileClosed(WaveFile file)
        {
            if (FileClosed != null)
                FileClosed.Invoke(file);
        }

        public static bool IsValid(WaveFile file)
        {
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
