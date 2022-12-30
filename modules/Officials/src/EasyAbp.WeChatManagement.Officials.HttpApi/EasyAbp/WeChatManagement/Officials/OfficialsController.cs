using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.Officials.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.WeChatManagement.Officials
{
    [Area(WeChatManagementRemoteServiceConsts.ModuleName)]
    public abstract class OfficialsController : AbpControllerBase
    {
        protected OfficialsController()
        {
            LocalizationResource = typeof(OfficialsResource);
        }
    }
}
