using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.WeChatManagement.Common.WeChatAppUsers.Dtos
{
    [Serializable]
    public class WeChatAppUserDto : FullAuditedEntityDto<Guid>
    {
        public Guid WeChatAppId { get; set; }

        public Guid UserId { get; set; }

        public string UnionId { get; set; }
        
        public string OpenId { get; set; }

        public DateTime? SessionKeyChangedTime { get; set; }
    }
}