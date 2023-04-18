using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class BindingAuthorizeTooFrequentlyException : UserFriendlyException
    {
        public BindingAuthorizeTooFrequentlyException() : base("绑定授权过于频繁，请稍后再试。", "BindingAuthorizeTooFrequently")
        {

        }
    }
}
