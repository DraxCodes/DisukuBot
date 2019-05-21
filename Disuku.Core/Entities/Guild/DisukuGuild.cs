using System;
using System.Collections.Generic;

namespace Disuku.Core.Entities
{
    public class DisukuGuild
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> TextChannels { get; set; }
        public IEnumerable<string> VoiceChannels { get; set; }
        public DateTime CreationDate { get; set; }
        public int TextChannelCount { get; set; }
        public int VoiceChannelCount { get; set; }
        public int MemberCount { get; set; }
        public IEnumerable<DisukuUser> Users { get; set; }
    }
}
