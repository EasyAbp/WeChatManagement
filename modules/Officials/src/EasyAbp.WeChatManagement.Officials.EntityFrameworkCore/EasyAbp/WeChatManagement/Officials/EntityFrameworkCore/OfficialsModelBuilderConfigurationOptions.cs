using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.WeChatManagement.Officials.EntityFrameworkCore
{
    public class OfficialsModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public OfficialsModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}
