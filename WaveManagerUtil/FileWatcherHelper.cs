using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

namespace WaveManagerUtil
{
    public static class FileWatcherHelper
    {
        // based on documentation @ http://msdn.microsoft.com/en-us/library/system.io.filesystemwatcher.aspx
        public static FileSystemWatcher GetWatcher(string directory, FileSystemEventHandler OnChanged)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = directory;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter = "*.wav";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnChanged);

            // Begin watching.
            watcher.EnableRaisingEvents = true;

            return watcher;
        }
    }
}
