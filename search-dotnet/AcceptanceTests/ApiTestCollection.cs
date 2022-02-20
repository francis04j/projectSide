using Xunit;

namespace AcceptanceTests
{
    // the instantiator of the types that you are working with
    [CollectionDefinition(nameof(ApiTestCollection))]
    public class ApiTestCollection : ICollectionFixture<SearchApiBroker>
    {

    }
}