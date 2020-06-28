using System;
using System.Collections.Generic;
using System.Text;
using WeChatManagementSample.Localization;
using Volo.Abp.Application.Services;

namespace WeChatManagementSample
{
    /* Inherit your application services from this class.
     */
    public abstract class WeChatManagementSampleAppService : ApplicationService
    {
        protected WeChatManagementSampleAppService()
        {
            LocalizationResource = typeof(WeChatManagementSampleResource);
        }
    }
}
