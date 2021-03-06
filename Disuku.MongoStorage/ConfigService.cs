using Newtonsoft.Json;
using System.IO;

namespace Disuku.MongoStorage
{
    public static class ConfigService
    {
        public const string ConfigPath = "Resources/MongoConfig.json";
        public static Conf GetConfig()
        {
            var json = File.ReadAllText(ConfigPath);
            return JsonConvert.DeserializeObject<Conf>(json);
        }
    }

    public class Conf
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Ip { get; set; }
        public int Port { get; set; }
    }
}