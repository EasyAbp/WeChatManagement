using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers
{
    public class MiniProgramUser : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid MiniProgramId { get; protected set; }
        
        public virtual Guid UserId { get; protected set; }
        
        [NotNull]
        public virtual string OpenId { get; protected set; }

        [CanBeNull]
        public virtual string SessionKey { get; protected set; }
        
        public virtual DateTime? SessionKeyModificationTime { get; protected set; }

        protected MiniProgramUser()
        {
        }

        public MiniProgramUser(Guid id, Guid? tenantId, Guid miniProgramId, Guid userId, string openId, string sessionKey, DateTime? sessionKeyModificationTime) : base(id)
        {
            TenantId = tenantId;
            MiniProgramId = miniProgramId;
            UserId = userId;
            OpenId = openId;
            SessionKey = sessionKey;
            SessionKeyModificationTime = sessionKeyModificationTime;
        }
    }
}
