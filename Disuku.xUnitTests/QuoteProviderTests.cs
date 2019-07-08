using System.Collections.Generic;
using System.Threading.Tasks;
using Disuku.Core.Entities;
using Disuku.Core.Providers.Quotes;
using Disuku.Core.Storage;
using Moq;
using Xunit;

namespace Disuku.xUnitTests
{
    public class QuoteProviderTests
    {
        private readonly Mock<IQuoteProvider> _mockedQuoteProvider;
        private readonly Mock<IDataStore> _mockedDataStore;
        
        public QuoteProviderTests()
        {
            _mockedDataStore = new Mock<IDataStore>();
            _mockedQuoteProvider = new Mock<IQuoteProvider>();
        }

        private List<Quote> MockedQuotes()
            => new List<Quote>
            {
                new Quote
                {
                    MessageId = 123,
                    Message = "Testing"
                }
            };

        [Fact]
        public async Task GetQuotes_ShouldReturnCollection()
        {
            var quotes = await _mockedQuoteProvider.Object.GetQuotes(It.IsAny<ulong>());
            Assert.NotNull(quotes);
        }

        [Fact]
        public async Task GetQuotes_ShouldReturnCollectionOfQuotes()
        {
            var expectedQuotes = MockedQuotes();

            _mockedQuoteProvider.Setup(s =>
                s.GetQuotes(1234UL))
                .ReturnsAsync(expectedQuotes);

            var quotes = await _mockedQuoteProvider.Object.GetQuotes(1234UL);

            Assert.Equal(expectedQuotes, quotes);
        }

    }
}
