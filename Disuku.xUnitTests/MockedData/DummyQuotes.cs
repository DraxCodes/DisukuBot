using System;
using Disuku.Core.Entities;

namespace Disuku.xUnitTests.MockedData
{
    public class DummyQuotes
    {
        private const ulong ExampleServerId = 2134124124UL;
        private const ulong ExampleChanId = 3134124124UL;

        public const ulong DraxId = 0UL;
        public const ulong PeterId = 1UL;
        public const string DraxUsername = "Draxis";
        public const string PeterUsername = "Peter";
        public const ulong EmbedId = 12345;

        public readonly Quote DraxQuoteOne = new Quote
        {
            Message = "Some Random stuff",
            MessageId = 420UL,
            Id = new Guid(),
            Name = "DraxQuoteOne",
            ServerId = ExampleServerId,
            ChanId = ExampleChanId,
            AuthorId = DraxId,
            AuthorAvatarUrl = "some_avatar.png",
            AuthorUsername = DraxUsername
        };

        public readonly Quote DraxQuoteTwo = new Quote
        {
            Message = "Some Random stuff",
            MessageId = 420UL,
            Id = new Guid(),
            Name = "DraxQuoteTwo",
            ServerId = ExampleServerId,
            ChanId = ExampleChanId,
            AuthorId = DraxId,
            AuthorAvatarUrl = "some_avatar.png",
            AuthorUsername = DraxUsername
        };

        public readonly Quote PeterQuoteOne = new Quote
        {
            Message = "Some Random stuff",
            MessageId = 420UL,
            Id = new Guid(),
            Name = "DraxQuoteOne",
            ServerId = ExampleServerId,
            ChanId = ExampleChanId,
            AuthorId = DraxId,
            AuthorAvatarUrl = "some_avatar.png",
            AuthorUsername = PeterUsername
        };

        public readonly Quote EmbedQuote = new Quote
        {
            Message = 
@"```cs
var test = await ReplyAsync(Message)
```",
            MessageId = EmbedId,
            Id = new Guid(),
            Name = "Embed Example",
            ServerId = ExampleServerId,
            ChanId = ExampleChanId,
            AuthorId = DraxId,
            AuthorAvatarUrl = "some_avatar.png",
            AuthorUsername = PeterUsername
        };

    }
}
