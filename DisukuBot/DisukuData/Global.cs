using System;
using System.Collections.Generic;
using System.Text;

namespace DisukuData
{
    public static class Global
    {
        public static string ResourcesFolder { get; private set; } = "./Resources";
        public static string BotConfigPath { get; private set; } = $"{ResourcesFolder}/config.json";
        public static string TmdbConfigPath { get; private set; } = $"{ResourcesFolder}/tmdbConfig.json";
    }
}
