using CampaignModule.Domain.Base;
using CampaignModule.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CampaignModule.Core.Repositories
{
    public abstract class BaseOperationService
    {
        protected virtual async Task<string> ReadJson(string storePath)
        {
            if (!FileHelper.GetInstance().CheckFileExist(storePath))
                File.Create(storePath);

            var jsonString = string.Empty;

            byte[] fileContents = File.ReadAllBytes(storePath);
            using (MemoryStream memoryStream = new MemoryStream(fileContents))
            {
                using (TextReader textReader = new StreamReader(memoryStream))
                {
                    jsonString = await textReader.ReadToEndAsync();
                }
            }

            //using (var fs = new FileStream(storePath, FileMode.Open, FileAccess.Read))
            //{
            //    using (var sr = new StreamReader(fs, Encoding.UTF8))
            //    {
            //        jsonString = await sr.ReadToEndAsync();

            //        sr.Close();
            //    }

            //    fs.Close();
            //}


            //using (var ms = new MemoryStream())
            //{
            //    ms.Position = 0;
            //    using var sr = new StreamReader(ms);
            //    jsonString = await sr.ReadToEndAsync();
            //}

            return jsonString;
        }

        protected virtual async Task<bool> WriteJson(string storePath, string jsonData)
        {
            try
            {
                var fullPath = FileHelper.GetInstance().GetCombinedStoreFolderPath(storePath);

                using (StreamWriter tw = new StreamWriter(fullPath))
                {
                    await tw.WriteAsync(jsonData); 
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
