using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos.Dtos
{
    [Serializable]
    public class UserInfoDto : FullAuditedEntityDto<Guid>
    {
        public Guid UserId { get; set; }

        public string NickName { get; set; }

        public byte Gender { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string AvatarUrl { get; set; }
    }
}