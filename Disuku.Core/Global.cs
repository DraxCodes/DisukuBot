namespace Disuku.Core
{
    public static class Global
    {
        public static string ResourcesFolder { get; set; } = "./Resources";
        public static string TmdbConfigPath { get; private set; } = $"{ResourcesFolder}/tmdbConfig.json";
    }
}
