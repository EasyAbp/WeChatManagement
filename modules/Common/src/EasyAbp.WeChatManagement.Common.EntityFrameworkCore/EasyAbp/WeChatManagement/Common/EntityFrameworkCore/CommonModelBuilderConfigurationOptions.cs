using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.WeChatManagement.Common.EntityFrameworkCore
{
    public class CommonModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public CommonModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}