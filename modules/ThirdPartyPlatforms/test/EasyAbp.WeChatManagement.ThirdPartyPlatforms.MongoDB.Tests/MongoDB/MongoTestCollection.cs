using Xunit;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.MongoDB;

[CollectionDefinition(Name)]
public class MongoTestCollection : ICollectionFixture<MongoDbFixture>
{
    public const string Name = "MongoDB Collection";
}
