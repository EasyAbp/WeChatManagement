using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class PcUserBindACodeHasExpiredException : UserFriendlyException
    {
        public PcUserBindACodeHasExpiredException() : base("二维码已过期，请重新扫码", "BindCodeHasExpired")
        {

        }
    }
}
