using DisukuBot.DisukuDiscord;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DisukuBot.DisukuData
{
    public class DisukuJsonDataService 
    {
        /// <summary>
        /// Retreives data from a json file.
        /// </summary>
        /// <typeparam name="T">The Type you want to retrieve.</typeparam>
        /// <param name="path">The path to the file you want to load.</param>
        /// <returns></returns>
        public Task<T> Retreive<T>(string path)
        {
            if (!Exists(path))
                throw new FileNotFoundException("Json path not found (Did you forget to create it)", path);
            var rawData = GetRawData(path);
            return Task.FromResult(JsonConvert.DeserializeObject<T>(rawData));
        }

        /// <summary>
        /// Saves JSON data into a specified path.
        /// </summary>
        /// <param name="obj">The Object you wish to serialize and save.</param>
        /// <param name="path">Where you want to save to.</param>
        /// <returns></returns>
        public async Task Save(object obj, string path)
        {
            var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            if (!Exists(path))
                CreateFile(path);
            await File.WriteAllTextAsync(path, json);
        }

        /// <summary>
        /// Checks if the specified path exists.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns></returns>
        public bool Exists(string path)
            => File.Exists(path);

        private void CreateFile(string path)
        {
            if (!Directory.Exists(Global.ResourcesFolder))
                Directory.CreateDirectory(Global.ResourcesFolder);
            if (Exists(path))
                File.Create(path);
        }

        private string GetRawData(string path)
            => File.ReadAllText(path);
    }
}
