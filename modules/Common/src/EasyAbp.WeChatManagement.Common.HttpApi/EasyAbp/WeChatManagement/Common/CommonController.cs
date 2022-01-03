using EasyAbp.WeChatManagement.Common.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.WeChatManagement.Common
{
    [Area(WeChatManagementRemoteServiceConsts.ModuleName)]
    public abstract class CommonController : AbpControllerBase
    {
        protected CommonController()
        {
            LocalizationResource = typeof(CommonResource);
        }
    }
}
