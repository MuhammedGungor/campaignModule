using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CampaignModule.Utilities
{
    public class FileHelper
    {
        private static FileHelper instance;

        protected FileHelper()
        {
        }

        public static FileHelper GetInstance()
        {
            // Uses lazy initialization.

            if (instance == null)
            {
                instance = new FileHelper();
            }

            return instance;
        }
        public bool CheckFileExist(string path)
        {
            return File.Exists(path);
        }

        public string GetCombinedStoreFolderPath(string storePath)
        {
            var appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            
            var storeFolderPath = Path.Combine(appDataFolder, Constants.General.StoreDirectoryName);
            
            return storeFolderPath + storePath;
        }
    }
}
