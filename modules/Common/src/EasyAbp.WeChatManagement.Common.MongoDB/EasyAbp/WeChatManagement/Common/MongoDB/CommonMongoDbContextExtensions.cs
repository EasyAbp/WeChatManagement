using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.Common.MongoDB
{
    public static class CommonMongoDbContextExtensions
    {
        public static void ConfigureCommon(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new CommonMongoModelBuilderConfigurationOptions(
                CommonDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}