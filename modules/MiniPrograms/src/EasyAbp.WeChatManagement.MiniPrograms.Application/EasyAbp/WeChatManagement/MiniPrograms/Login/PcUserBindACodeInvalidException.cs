using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class PcUserBindACodeInvalidException : UserFriendlyException
    {
        public PcUserBindACodeInvalidException() : base("绑定无效", "BindInvalid")
        {

        }
    }
}
