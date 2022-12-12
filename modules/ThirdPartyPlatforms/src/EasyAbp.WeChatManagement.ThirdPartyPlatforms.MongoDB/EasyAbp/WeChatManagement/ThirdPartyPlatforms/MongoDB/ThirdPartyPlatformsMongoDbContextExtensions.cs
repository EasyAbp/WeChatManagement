using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.MongoDB;

public static class ThirdPartyPlatformsMongoDbContextExtensions
{
    public static void ConfigureThirdPartyPlatforms(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
