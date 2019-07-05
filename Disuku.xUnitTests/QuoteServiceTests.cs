using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Disuku.Core.Discord;
using Disuku.Core.Entities;
using Disuku.Core.Services.Quotes;
using Disuku.Core.Storage;
using Disuku.xUnitTests.MockedData;
using Moq;
using Xunit;
using Xunit.Sdk;
using Expression = Castle.DynamicProxy.Generators.Emitters.SimpleAST.Expression;

namespace Disuku.xUnitTests
{
    public class QuoteServiceTests
    {
        private readonly Mock<IDataStore> _dataStoreMock;
        private readonly Mock<IDiscordMessage> _discordMessageMock;
        private readonly Mock<QuoteService> _quoteService;
        private readonly DummyQuotes _dummyQuotes;

        public QuoteServiceTests()
        {
            _dataStoreMock = new Mock<IDataStore>();
            _discordMessageMock = new Mock<IDiscordMessage>();
            _quoteService = new Mock<QuoteService>(_dataStoreMock.Object, _discordMessageMock.Object);
            _dummyQuotes = new DummyQuotes();
        }

        [Fact]
        public async Task AddNewQuote_ShouldCallDbInsertOnce()
        {
            await _quoteService.Object.Add(It.IsAny<ulong>(), It.IsAny<Quote>());

            _dataStoreMock.Verify(x =>
                x.Insert(It.IsAny<Quote>(), It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task AddNewQuote_ShouldSendOneDiscordMessage()
        {
           await _quoteService.Object.Add(It.IsAny<ulong>(), It.IsAny<Quote>());

            _discordMessageMock.Verify(x => 
                x.SendDiscordMessageAsync(It.IsAny<ulong>(), It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task ListQuotes_ShouldCallLoadRecordsOnce()
        {
            await _quoteService.Object.List(It.IsAny<ulong>(), It.IsAny<DisukuUser>());

            _dataStoreMock.Verify(x =>
                x.LoadRecordsAsync(It.IsAny<Expression<Func<Quote, bool>>>(), It.IsAny<string>()),
                    Times.Once());
        }

        [Fact]
        public async Task ListQuotes_ShouldSendOneDiscordMessageRegardlessOfQuoteBeingFound()
        {
            await _quoteService.Object.List(It.IsAny<ulong>(), It.IsAny<DisukuUser>());
            
            _discordMessageMock.Verify(x =>
                x.SendDiscordMessageAsync(It.IsAny<ulong>(), It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task FindQuoteById_ShouldCallLoadRecordsOnce()
        {
            await _quoteService.Object.Find(It.IsAny<ulong>(), It.IsAny<ulong>());

            _dataStoreMock.Verify(x => 
                x.LoadRecordsAsync(It.IsAny<Expression<Func<Quote, bool>>>(), It.IsAny<string>()),
                Times.Once());
        }

        [Fact]
        public async Task FindQuoteById_ShouldSendOneDiscordMessageIfQuoteNotFound()
        {
            const ulong messageId = 0;
            var quotes = new List<Quote>();

            _dataStoreMock.Setup(x => 
                x.LoadRecordsAsync<Quote>(x => x.MessageId == messageId, It.IsAny<string>()))
                .Returns(Task.FromResult(quotes));

            await _quoteService.Object.Find(It.IsAny<ulong>(), messageId);

            _discordMessageMock.Verify(x =>
                x.SendDiscordMessageAsync(It.IsAny<ulong>(), It.IsAny<string>()),
                Times.Once);
        }


    }
}
