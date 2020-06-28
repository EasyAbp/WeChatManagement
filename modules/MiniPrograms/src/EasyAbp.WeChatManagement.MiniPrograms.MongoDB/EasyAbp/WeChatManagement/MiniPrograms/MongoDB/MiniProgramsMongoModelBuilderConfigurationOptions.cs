using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.MiniPrograms.MongoDB
{
    public class MiniProgramsMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public MiniProgramsMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}