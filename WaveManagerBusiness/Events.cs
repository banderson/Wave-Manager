using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveDataContracts;

namespace WaveManagerBusiness
{
    public static partial class WaveManager
    {
        /*** Event Types ***/
        public delegate void FileOpenedEventHandler(WaveFile file);
        public delegate void FileClosedEventHandler(WaveFile file);
        public delegate void WindowSelectedEventHandler(WaveFile file);
        public delegate void RepaintFileListEventHandler();
        public delegate void ViewModeChangeEventHandler();
        public delegate void AppSettingsChangedEventHandler();
        public delegate void ConfigSettingsChangedEventHandler();
        public delegate void CurrentWindowModifiedEventHandler();
        public delegate void DirectoryModifiedEventHandler();

        public delegate void FileSavedEventHandler();
        public delegate void FileSaveAsEventHandler(WaveFile oldFile, WaveFile newFile);
        public delegate void FileCutEventHandler();
        public delegate void FileCopiedEventHandler();
        public delegate void FilePastedEventHandler();
        public delegate void FileDeletedEventHandler();


        /*** Events ***/
        public static event FileOpenedEventHandler FileOpened;
        public static event FileClosedEventHandler FileClosed;
        public static event WindowSelectedEventHandler WindowSelected;
        public static event RepaintFileListEventHandler RepaintFileList;
        public static event ViewModeChangeEventHandler ViewModeChanged;
        public static event AppSettingsChangedEventHandler AppSettingsChanged;
        public static event ConfigSettingsChangedEventHandler ConfigSettingsChanged;
        public static event CurrentWindowModifiedEventHandler CurrentWindowModified;
        public static event FileSavedEventHandler FileSaved;
        public static event FileSaveAsEventHandler FileSaveAs;
        public static event FileCutEventHandler FileCut;
        public static event FileCopiedEventHandler FileCopied;
        public static event FilePastedEventHandler FilePasted;
        public static event FileDeletedEventHandler FileDeleted;
        public static event DirectoryModifiedEventHandler DirectoryModified;


        /*** Invoke Event Callbacks ***/

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

        public static void FireViewModeChanged()
        {
            if (ViewModeChanged != null)
                ViewModeChanged.Invoke();
        }

        public static void FireAppSettingsChanged()
        {
            if (AppSettingsChanged != null)
                AppSettingsChanged.Invoke();
        }

        public static void FireConfigSettingsChanged()
        {
            if (ConfigSettingsChanged != null)
                ConfigSettingsChanged.Invoke();
        }

        public static void FireCurrentWindowModified()
        {
            if (CurrentWindowModified != null)
                CurrentWindowModified.Invoke();
        }

        public static void FireFileSaved()
        {
            if (FileSaved != null)
                FileSaved.Invoke();
        }

        public static void FireFileSaveAs(WaveFile oldFile, WaveFile newFile)
        {
            if (FileSaveAs != null)
                FileSaveAs.Invoke(oldFile, newFile);
        }

        public static void FireFileCopied()
        {
            if (FileCopied != null)
                FileCopied.Invoke();
        }

        public static void FireFileCut()
        {
            if (FileCut != null)
                FileCut.Invoke();
        }

        public static void FireFilePasted()
        {
            if (FilePasted != null)
                FilePasted.Invoke();
        }

        public static void FireFileDeleted()
        {
            if (FileDeleted != null)
                FileDeleted.Invoke();
        }

        public static void FireDirectoryModified()
        {
            if (DirectoryModified != null)
                DirectoryModified.Invoke();
        }
    }
}
