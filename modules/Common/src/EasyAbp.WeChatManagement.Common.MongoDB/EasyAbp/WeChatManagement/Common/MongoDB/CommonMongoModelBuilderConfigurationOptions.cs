using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.Common.MongoDB
{
    public class CommonMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public CommonMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}