using System.Threading.Tasks;
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
    }
}
