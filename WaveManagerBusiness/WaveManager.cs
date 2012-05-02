using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveDataContracts;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using WaveManagerDataAccess;

namespace WaveManagerBusiness
{
    public static class WaveManager
    {
        /*** Events ***/
        public static event Events.FileOpenedEventHandler FileOpened;
        public static event Events.FileClosedEventHandler FileClosed;
        public static event Events.WindowSelectedEventHandler WindowSelected;
        public static event Events.RepaintFileList RepaintFileList;

        static WaveManager()
        {
            FileClosed += RemoveOpenFile;
            WindowSelected += SetActiveFile;
        }

        public static WaveFile ActiveFile;

        static List<WaveFile> openFiles = new List<WaveFile>();
        public static int GetOpenFilesCount()
        {
            return WaveFileRepository.FindAll().Count();
        }
        private static void AddOpenFile(WaveFile file)
        {
            // not: even if it already exists, the reference will just be updated
            WaveFileRepository.AddOrUpdateFile(file.filePath, file);
        }
        private static void RemoveOpenFile(WaveFile file)
        {
            if (file != null)
            {
                WaveFileRepository.RemoveFile(file.filePath);
            }
            ActiveFile = null;
        }


        static List<string> directories = new List<string>();
        public static List<string> GetDirectories()
        {
            return directories;
        }
        public static void AddDirectory(string directory)
        {
            if (!directories.Contains(directory))
            {
                directories.Add(directory);
            }
        }

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

            // this will add or update the file reference in the repository
            AddOpenFile(file);

            return file;
        }

        public static WaveFile FindFile(string fileName)
        {
            return WaveFileRepository.Find(fileName);
        }

        public static WaveFile OpenFile(string fileName)
        {
            WaveFile file = WaveFileRepository.Find(fileName);

            // if the file is not yet loaded, then get it from disk
            if (file == null)
            {
                try
                {
                    file = Load(fileName);
                }
                catch (FileNotFoundException) { /* swallow it... */ }
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

        public static void FireWindowSelected(WaveFile file)
        {
            if (WindowSelected != null)
                WindowSelected.Invoke(file);
        }

        public static void FireRepaintFileList()
        {
            if (RepaintFileList != null)
                RepaintFileList.Invoke();
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

        public static void SetActiveFile(WaveFile file)
        {
            ActiveFile = file;
        }

        public static WaveFile GetActiveFile()
        {
            return (ActiveFile == null)
                ? new WaveFile()
                : ActiveFile;
        }
    }
}
