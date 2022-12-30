using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.WeChatManagement.Officials.MongoDB
{
    public static class OfficialsMongoDbContextExtensions
    {
        public static void ConfigureOfficials(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new OfficialsMongoModelBuilderConfigurationOptions(
                OfficialsDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}