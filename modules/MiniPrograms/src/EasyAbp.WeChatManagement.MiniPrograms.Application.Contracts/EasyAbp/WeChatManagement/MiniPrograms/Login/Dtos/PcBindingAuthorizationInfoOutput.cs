using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    public class PcBindingAuthorizationInfoOutput : LoginInput
    {
        public string NickName { get; set; }
        public string AvatarUrl { get; set; }
    }
}
