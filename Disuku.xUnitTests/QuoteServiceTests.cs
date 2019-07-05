using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Disuku.Core.Discord;
using Disuku.Core.Entities;
using Disuku.Core.Services.Quotes;
using Disuku.Core.Storage;
using Moq;
using Xunit;
using Xunit.Sdk;
using Expression = Castle.DynamicProxy.Generators.Emitters.SimpleAST.Expression;

namespace Disuku.xUnitTests
{
    class QuoteServiceTests
    public class QuoteServiceTests
    {
        private readonly Mock<IDataStore> _dataStoreMock;
        private readonly Mock<IDiscordMessage> _discordMessageMock;
        private readonly Mock<QuoteService> _quoteService;

        public QuoteServiceTests()
        {
            _dataStoreMock = new Mock<IDataStore>();
            _discordMessageMock = new Mock<IDiscordMessage>();
            _quoteService = new Mock<QuoteService>(_dataStoreMock.Object, _discordMessageMock.Object);
        }

        [Fact]
        public async Task AddNewQuote_ShouldCallDbInsertOnce()
        {
            await _quoteService.Object.Add(It.IsAny<ulong>(), It.IsAny<Quote>());

            _dataStoreMock.Verify(x =>
                x.Insert(It.IsAny<Quote>(), It.IsAny<string>()),
                Times.Once);
        }
    }
}
