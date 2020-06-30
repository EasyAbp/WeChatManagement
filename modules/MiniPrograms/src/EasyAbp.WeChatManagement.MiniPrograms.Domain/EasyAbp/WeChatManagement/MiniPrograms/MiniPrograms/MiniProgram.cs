using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms
{
    public class MiniProgram : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid? WeChatComponentId { get; protected set; }
        
        [NotNull]
        public virtual string Name { get; protected set; }
        
        [NotNull]
        public virtual string DisplayName { get; protected set; }

        [NotNull]
        public virtual string OpenAppIdOrName { get; protected set; }

        [NotNull]
        public virtual string AppId { get; protected set; }

        /// <summary>
        /// AppSecret 为空时，需提供开放平台 WeChatComponentId
        /// </summary>
        [CanBeNull]
        public virtual string AppSecret { get; protected set; }
        
        [CanBeNull]
        public virtual string Token { get; protected set; }

        [CanBeNull]
        public virtual string EncodingAesKey { get; protected set; }
        
        public virtual bool IsStatic { get; protected set; }

        protected MiniProgram()
        {
        }

        public MiniProgram(
            Guid id,
            Guid? tenantId,
            Guid? weChatComponentId,
            [NotNull] string name,
            [NotNull] string displayName,
            [NotNull] string openAppIdOrName,
            [NotNull] string appId,
            [CanBeNull] string appSecret,
            [CanBeNull] string token,
            [CanBeNull] string encodingAesKey,
            bool isStatic) : base(id)
        {
            TenantId = tenantId;
            WeChatComponentId = weChatComponentId;
            Name = name;
            DisplayName = displayName;
            OpenAppIdOrName = openAppIdOrName;
            AppId = appId;
            AppSecret = appSecret;
            Token = token;
            EncodingAesKey = encodingAesKey;
            IsStatic = isStatic;
        }
    }
}
