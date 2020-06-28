using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms
{
    public class MiniProgram : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        [NotNull]
        public virtual string Name { get; protected set; }
        
        [NotNull]
        public virtual string DisplayName { get; protected set; }
        
        [NotNull]
        public virtual string AppId { get; protected set; }

        [NotNull]
        public virtual string AppSecret { get; protected set; }
        
        [CanBeNull]
        public virtual string Token { get; protected set; }

        [CanBeNull]
        public virtual string EncodingAesKey { get; protected set; }
        
        public virtual bool IsStatic { get; protected set; }

        protected MiniProgram()
        {
        }

        public MiniProgram(Guid id, Guid? tenantId, string name, string displayName, string appId, string appSecret, string token, string encodingAesKey, bool isStatic) : base(id)
        {
            TenantId = tenantId;
            Name = name;
            DisplayName = displayName;
            AppId = appId;
            AppSecret = appSecret;
            Token = token;
            EncodingAesKey = encodingAesKey;
            IsStatic = isStatic;
        }
    }
}
