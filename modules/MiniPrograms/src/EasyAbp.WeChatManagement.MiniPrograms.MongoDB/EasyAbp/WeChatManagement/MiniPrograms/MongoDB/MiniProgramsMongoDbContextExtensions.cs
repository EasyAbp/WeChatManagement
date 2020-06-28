using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.MiniPrograms.MongoDB
{
    public static class MiniProgramsMongoDbContextExtensions
    {
        public static void ConfigureMiniPrograms(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new MiniProgramsMongoModelBuilderConfigurationOptions(
                MiniProgramsDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}