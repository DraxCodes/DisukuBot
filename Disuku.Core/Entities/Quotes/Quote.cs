using System;
using System.Collections.Generic;
using System.Text;

namespace Disuku.Core.Entities
{
    public class Quote
    {
        public Guid Id { get; set; }
        public ulong MessageId { get; set; }
        public ulong ServerId { get; set; }
        public ulong ChanId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string AuthorUsername { get; set; }
        public ulong AuthorId { get; set; }
        public string AuthorAvatarUrl { get; set; }
    }
}
