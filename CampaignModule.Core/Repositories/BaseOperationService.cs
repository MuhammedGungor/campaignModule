using CampaignModule.Utilities;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CampaignModule.Core.Repositories
{
    public abstract class BaseOperationService
    {
        protected virtual async Task<string> ReadJson(string storePath)
        {
            if (!FileHelper.GetInstance().CheckFileExist(storePath))
                File.Create(storePath).Close();

            var jsonString = string.Empty;

            byte[] fileContents = File.ReadAllBytes(storePath);
            
            using (MemoryStream memoryStream = new MemoryStream(fileContents))
            {
                using (StreamReader textReader = new StreamReader(memoryStream))
                {
                    jsonString = await textReader.ReadToEndAsync();
                    textReader.Close();                    
                }

                memoryStream.Flush();
            }

            return jsonString;
        }

        protected virtual async Task<bool> WriteJson(string storePath, string jsonData)
        {
            try
            {
                var fullPath = FileHelper.GetInstance().GetCombinedStoreFolderPath(storePath);

                //File.CreateText(fullPath).Close();

                using (StreamWriter tw = new StreamWriter(fullPath))
                {
                    await tw.WriteAsync(jsonData);
                    tw.Close();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        protected virtual async Task<TEntity> GetValuesFromFolder<TEntity>(string storePath)
        {
            var fullPath = FileHelper.GetInstance().GetCombinedStoreFolderPath(storePath);

            var json = await ReadJson(fullPath);

            var values = JsonConvert.DeserializeObject<TEntity>(json);

            return values;
        }
    }
}
