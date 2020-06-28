using EasyAbp.WeChatManagement.MiniPrograms.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public abstract class MiniProgramsController : AbpController
    {
        protected MiniProgramsController()
        {
            LocalizationResource = typeof(MiniProgramsResource);
        }
    }
}
