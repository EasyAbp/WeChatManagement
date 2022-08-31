using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    public class GetPcBindACodeOutput : GetPcLoginACodeOutput
    {
        public int ExpiredTime { get; set; }
        public bool HasBound { get; set; }
        public string NickName { get; set; }
        public string AvatarUrl { get; set; }
    }
}
