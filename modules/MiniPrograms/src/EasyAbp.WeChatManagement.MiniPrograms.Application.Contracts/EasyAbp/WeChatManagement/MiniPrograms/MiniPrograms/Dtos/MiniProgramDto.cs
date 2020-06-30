using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms.Dtos
{
    [Serializable]
    public class MiniProgramDto : FullAuditedEntityDto<Guid>
    {
        public Guid? WeChatComponentId { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string OpenAppId { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string Token { get; set; }

        public string EncodingAesKey { get; set; }

        public bool IsStatic { get; set; }
    }
}