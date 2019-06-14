using System;
using System.Collections.Generic;
using System.Text;

namespace Disuku.Core.Entities
{
    public class Quote
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string QuotedUserId { get; set; }
    }
}
