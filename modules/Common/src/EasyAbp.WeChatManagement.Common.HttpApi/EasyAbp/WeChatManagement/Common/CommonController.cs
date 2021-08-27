using EasyAbp.WeChatManagement.Common.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.WeChatManagement.Common
{
    public abstract class CommonController : AbpController
    {
        protected CommonController()
        {
            LocalizationResource = typeof(CommonResource);
        }
    }
}
