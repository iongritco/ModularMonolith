using Xunit;

namespace ToDoApp.Tests.EndToEnd
{
    [CollectionDefinition("Tests collection")]
    public class TestsCollection : ICollectionFixture<BaseTestsClass>
    {
    }
}
