using System;
using System.Linq;

namespace Disuku.Core.Entities
{
    public class DisukuGuild
    {
        public Guid Id
        {
            get
            {
                return GenerateSeededGuid();
            }
        }
        public ulong GuildId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public int TextChannelCount { get; set; }
        public int VoiceChannelCount { get; set; }
        public int MemberCount { get; set; }
        public string GuildAvatar { get; set; }

        private Guid GenerateSeededGuid()
        {
            var guildId = BitConverter.GetBytes(GuildId);
            var bytes = guildId.Concat(guildId).ToArray();
            return new Guid(bytes);
        }
    }
}
