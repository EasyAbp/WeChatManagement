using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers.Dtos
{
    [Serializable]
    public class MiniProgramUserDto : FullAuditedEntityDto<Guid>
    {
        public Guid MiniProgramId { get; set; }

        public Guid UserId { get; set; }

        public string UnionId { get; set; }
        
        public string OpenId { get; set; }

        public string SessionKey { get; set; }

        public DateTime? SessionKeyModificationTime { get; set; }
    }
}