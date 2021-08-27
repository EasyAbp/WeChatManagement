using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace EasyAbp.WeChatManagement.Common.WeChatAppUsers
{
    public class WeChatAppUser : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid WeChatAppId { get; protected set; }
        
        public virtual Guid UserId { get; protected set; }
        
        [CanBeNull]
        public virtual string UnionId { get; protected set; }
        
        [NotNull]
        public virtual string OpenId { get; protected set; }

        [CanBeNull]
        public virtual string SessionKey { get; protected set; }
        
        public virtual DateTime? SessionKeyChangedTime { get; protected set; }

        protected WeChatAppUser()
        {
        }

        public WeChatAppUser(Guid id,
            Guid? tenantId,
            Guid weChatAppId,
            Guid userId,
            [CanBeNull] string unionId,
            [NotNull] string openId) : base(id)
        {
            TenantId = tenantId;
            WeChatAppId = weChatAppId;
            UserId = userId;
            UnionId = unionId;
            OpenId = openId;
        }

        public void SetUnionId(string unionId)
        {
            UnionId = unionId;
        }

        public void SetOpenId(string openId)
        {
            OpenId = openId;
        }
        
        public void UpdateSessionKey([CanBeNull] string sessionKey, IClock clock)
        {
            if (SessionKey == sessionKey)
            {
                return;
            }

            SessionKey = sessionKey;
            SessionKeyChangedTime = clock.Now;
        }
    }
}
