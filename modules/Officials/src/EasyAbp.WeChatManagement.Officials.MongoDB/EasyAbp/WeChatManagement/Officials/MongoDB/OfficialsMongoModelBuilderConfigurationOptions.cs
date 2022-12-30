using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.Officials.MongoDB
{
    public class OfficialsMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public OfficialsMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}