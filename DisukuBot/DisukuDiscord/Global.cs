namespace DisukuBot.DisukuDiscord
{
    public static class Global
    {
        //TODO: Figure out why their are now two of these. (See: DisukuData (Global.cs))

        public static string ResourcesFolder { get; set; } = "./Resources";
        public static string ConfigPath { get; set; } = $"{ResourcesFolder}/config.json";
    }
}
