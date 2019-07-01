using System;
using System.Collections.Generic;
using System.Text;

namespace Disuku.Core.Entities.Embeds
{
    public class Author
    {
        public Author(string username, string avatarUrl)
        {
            Username = username;
            AvatarUrl = avatarUrl;
        }

        public Author(string username, string avatarUrl, string description)
        {
            Username = username;
            AvatarUrl = avatarUrl;
            Description = description;
        }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public string Description { get; set; }

    }
}
