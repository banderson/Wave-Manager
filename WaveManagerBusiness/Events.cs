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


        /*** Events ***/
        public static event FileOpenedEventHandler FileOpened;
        public static event FileClosedEventHandler FileClosed;
        public static event WindowSelectedEventHandler WindowSelected;
        public static event RepaintFileListEventHandler RepaintFileList;
        public static event ViewModeChangeEventHandler ViewModeChanged;
        public static event AppSettingsChangedEventHandler AppSettingsChanged;
        public static event ConfigSettingsChangedEventHandler ConfigSettingsChanged;
        public static event CurrentWindowModifiedEventHandler CurrentWindowModified;


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
    }
}
