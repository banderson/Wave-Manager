﻿using System;
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
    public static partial class WaveManager
    {

        static WaveManager()
        {
            FileClosed += RemoveOpenFile;
            WindowSelected += SetActiveFile;
            AppSettingsChanged += SerializeSettings;

            _settings = new AppSettings();
            _settingsFile = Path.GetFullPath(".") + Path.DirectorySeparatorChar + "settings.config";

            if (File.Exists(_settingsFile))
            {
                _settings = DeserializeSettings();
            }
        }

        static AppSettings _settings;
        static string _settingsFile;
        public static AppSettings GetSettings()
        {
            return _settings;
        }
        public static void UpdateSettings(Action<AppSettings> lambda)
        {
            lambda.Invoke(_settings);
            FireAppSettingsChanged();
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
            var wave = new WaveFile(fileName);

            // this will add or update the file reference in the repository
            AddOpenFile(wave);

            return wave;
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

        private static void SerializeSettings()
        {
            using (var fileStream = new FileStream(_settingsFile, FileMode.Create))
            {
                // serialize file to disk
                var bf = new BinaryFormatter();
                bf.Serialize(fileStream, _settings);
            }
        }

        private static AppSettings DeserializeSettings()
        {
            using (var fileStream = new FileStream(_settingsFile, FileMode.Open))
            {
                var bf = new BinaryFormatter();
                _settings = (AppSettings)bf.Deserialize(fileStream);
            }

            return _settings;
        }
    }
}
