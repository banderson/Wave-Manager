using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveDataContracts;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using WaveManagerDataAccess;
using System.Drawing.Printing;
using WaveManagerUtil;
using System.ComponentModel;
using System.Drawing;

namespace WaveManagerBusiness
{
    public static partial class WaveManager
    {

        static WaveManager()
        {
            FileClosed += RemoveOpenFile;
            WindowSelected += SetActiveFile;
            AppSettingsChanged += SerializeSettings;
            CurrentWindowModified += MarkAsModified;

            _settings = new AppSettings();
            _settingsFile = Path.GetFullPath(".") + Path.DirectorySeparatorChar + "settings.config";
            PageSettings = new PageSettings();

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

        public static PageSettings PageSettings;

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


        //static List<string> directories = new List<string>();
        static List<FileSystemWatcher> directories = new List<FileSystemWatcher>();
        public static List<string> GetDirectories()
        {
            return directories.Select<FileSystemWatcher, String>(fsw => fsw.Path).ToList();
        }
        public static void AddDirectory(string directory)
        {
            if (directories.Where(fsw => fsw.Path.Equals(directory)).Count() == 0)
            {
                directories.Add(FileWatcherHelper.GetWatcher(directory, OnDirectoryChanged));
            }
        }

        public static WaveFile Load(string fileName)
        {
            var wave = new WaveFile(fileName);

            // this will add or update the file reference in the repository
            AddOpenFile(wave);

            return wave;
        }

        public static Boolean IsCurrentDocumentBlank()
        {
            return ActiveFile == null || ActiveFile.IsEmpty();
        }

        // this indicates whether the file has changed since last save
        public static bool IsCurrentDocumentModified()
        {
            return ActiveFile != null && ActiveFile.IsModified();
        }

        // this indicates whether the file has changed since last save
        public static void MarkAsModified()
        {
            if (ActiveFile != null)
                ActiveFile.MarkAsModified();
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
            return file.IsValid();
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

        public static string GetActiveFilePath()
        {
            return (ActiveFile == null)
                ? null
                : ActiveFile.filePath;
        }

        public static void ModulateWave()
        {
            WaveFile file = GetActiveFile();

            file.Modulate();

            FireCurrentWindowModified();
        }

        public static void RotateWave()
        {
            WaveFile file = GetActiveFile();

            file.Flip();

            FireCurrentWindowModified();
        }

        public static void Save(WaveFile file)
        {
            file.Save();
            FireFileSaved();
        }

        public static void Save()
        {
            WaveFile file = GetActiveFile();
            Save(file);
        }

        public static WaveFile SaveAs(string fileName)
        {
            WaveFile file = GetActiveFile();
            // remove the reference to the old file in the repo
            RemoveOpenFile(file);
            // save the new file
            file.SaveAs(fileName);

            // add a reference to the new file in the repo
            var newFile = new WaveFile(fileName);
            AddOpenFile(newFile);

            FireFileSaveAs(file, newFile);
            FireWindowSelected(newFile);

            return newFile;
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

        public static void PrintPage(PrintPageEventArgs e)
        {
            int row = 1; // start on first row

            //// adjust for margins
            //Point origin = e.MarginBounds.Location;
            //e.Graphics.TranslateTransform(origin.X, origin.Y);

            //// print header row
            //PrintHeaderCell(e, 1, row, "Group");
            //PrintHeaderCell(e, 2, row, "Title");
            //PrintHeaderCell(e, 3, row, "User Name");
            //PrintHeaderCell(e, 4, row, "Password");
            //PrintHeaderCell(e, 5, row, "URL");
            //PrintHeaderCell(e, 6, row, "Notes");
            //row++;

            //// print seection for each group and its related keys
            //foreach (var group in KeyPassMgr.GetGroups())
            //{
            //    row++;
            //    PrintHeaderCell(e, 1, row, group.GroupName);

            //    for (int i = 0; i < group.Keys.Count; i++)
            //    {
            //        PrintDataCell(e, 2, row, group.Keys[i].Title);
            //        PrintDataCell(e, 3, row, group.Keys[i].UserName);
            //        PrintDataCell(e, 4, row, group.Keys[i].Password);
            //        PrintDataCell(e, 5, row, group.Keys[i].Url);
            //        PrintDataCell(e, 6, row, group.Keys[i].Notes);
            //        row++;
            //    }
            //}
        }

        private static void PrintCell(PrintPageEventArgs e, int column, int row, string text, Brush brush)
        {
            // dynamically determine the width of each column
            int cellHeight = 25;
            int cellWidth = 100;// (e.PageBounds.Width - e.PageSettings.Margins.Left - e.PageSettings.Margins.Top) / 6;

            // calculate the cell x/y location based on page dimensions
            int x = cellWidth * (column - 1), y = cellHeight * (row - 1);

            // draw cell
            var rect = new Rectangle(x, y, cellWidth, cellHeight);
            var g = e.Graphics;
            g.FillRectangle(brush, rect);
            g.DrawRectangle(Pens.Black, rect);

            // format text and add to cell
            Font messageFont = new Font("Segoe UI", 9, GraphicsUnit.Point);
            StringFormat mine = new StringFormat(StringFormat.GenericTypographic);
            mine.Trimming = StringTrimming.EllipsisCharacter;
            mine.Alignment = StringAlignment.Near;
            mine.LineAlignment = StringAlignment.Center;
            g.DrawString(" " + text, messageFont, Brushes.Black, rect, mine);
        }

        private static void PrintHeaderCell(PrintPageEventArgs e, int column, int row, string text)
        {
            PrintCell(e, column, row, text, Brushes.LightGray);
        }

        private static void PrintDataCell(PrintPageEventArgs e, int column, int row, string text)
        {
            PrintCell(e, column, row, text, Brushes.White);
        }
    }
}
