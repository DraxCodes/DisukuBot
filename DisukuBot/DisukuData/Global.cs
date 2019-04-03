﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DisukuData
{
    public static class Global
    {
        public static string ResourcesFolder { get; set; } = "./Resources";
        public static string ConfigPath { get; set; } = $"{ResourcesFolder}/config.json";
    }
}