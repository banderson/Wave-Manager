using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveDataContracts;

namespace WaveManagerDataAccess
{
    public static class WaveFileRepository
    {
        static Dictionary<string, WaveFile> _data;

        static WaveFileRepository()
        {
            _data = new Dictionary<string, WaveFile>();
        }

        public static void AddOrUpdateFile(string fileName, WaveFile file)
        {
            _data[fileName] = file;
        }

        public static void RemoveFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
                _data.Remove(fileName);
        }

        public static bool Contains(string fileName)
        {
            return _data.ContainsKey(fileName);
        }

        public static IEnumerable<WaveFile> GetFilesInDirectory(string directoryPath)
        {
            var results = FindAll(x => x.filePath.StartsWith(directoryPath));

            return results;
        }

        public static WaveFile Find(string fileName)
        {
            return (_data.ContainsKey(fileName)) 
                ? _data[fileName] 
                : null;
        }

        public static IEnumerable<WaveFile> FindAll()
        {
            return _data.Values;
        }

        public static IEnumerable<WaveFile> FindAll(Func<WaveFile, bool> query)
        {
            return FindAll().Where(query);
        }
    }
}
