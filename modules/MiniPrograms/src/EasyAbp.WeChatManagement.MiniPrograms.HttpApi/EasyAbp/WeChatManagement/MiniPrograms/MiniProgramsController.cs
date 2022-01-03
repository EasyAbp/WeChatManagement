using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [Area(WeChatManagementRemoteServiceConsts.ModuleName)]
    public abstract class MiniProgramsController : AbpControllerBase
    {
        protected MiniProgramsController()
        {
            LocalizationResource = typeof(MiniProgramsResource);
        }
    }
}
