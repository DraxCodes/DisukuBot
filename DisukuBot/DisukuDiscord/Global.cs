using DisukuData.Entities;

namespace DisukuBot.DisukuDiscord
{
    public static class Global
    {
        public static string ResourcesFolder { get; set; } = "./Resources";
        public static string ConfigPath { get; set; } = $"{ResourcesFolder}/config.json";
        public static string TmdbConfigPath { get; private set; } = $"{ResourcesFolder}/tmdbConfig.json";
    }
}
