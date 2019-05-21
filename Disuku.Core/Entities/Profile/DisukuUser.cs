using System;
using System.Collections.Generic;
using System.Linq;

namespace Disuku.Core.Entities
{
    public class DisukuUser
    {
        public Guid Id
        {
            get
            {
                return GenerateSeededGuid();
            }
        }
        public ulong UserId { get; set; }
        public ulong GuildId { get; set; }
        public DateTime JoinedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public IEnumerable<DisukuRole> Roles { get; set; }

        private Guid GenerateSeededGuid()
        {
            var left = BitConverter.GetBytes(GuildId);
            var right = BitConverter.GetBytes(UserId);
            var bytes = left.Concat(right).ToArray();
            return new Guid(bytes);
        }
    }
}
