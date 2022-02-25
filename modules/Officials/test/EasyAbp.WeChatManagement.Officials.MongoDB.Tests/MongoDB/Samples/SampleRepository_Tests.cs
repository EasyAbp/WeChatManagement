using EasyAbp.WeChatManagement.Officials.Samples;
using Xunit;

namespace EasyAbp.WeChatManagement.Officials.MongoDB.Samples;

[Collection(MongoTestCollection.Name)]
public class SampleRepository_Tests : SampleRepository_Tests<OfficialsMongoDbTestModule>
{
    /* Don't write custom repository tests here, instead write to
     * the base class.
     * One exception can be some specific tests related to MongoDB.
     */
}
