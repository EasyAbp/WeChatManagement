using WeChatManagementSample.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace WeChatManagementSample.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class WeChatManagementSampleController : AbpControllerBase
    {
        protected WeChatManagementSampleController()
        {
            LocalizationResource = typeof(WeChatManagementSampleResource);
        }
    }
}