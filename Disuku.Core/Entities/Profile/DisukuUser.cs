using System;
using System.Collections.Generic;

namespace Disuku.Core.Entities.Profile
{
    public class DisukuUser
    {
        public ulong Id { get; set; }
        public DateTime JoinedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public IEnumerable<DisukuRole> Roles { get; set; }
    }
}
