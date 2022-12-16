using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.WeChatManagement.Common.WeChatApps
{
    public class WeChatApp : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual WeChatAppType Type { get; protected set; }

        public virtual Guid? ComponentWeChatAppId { get; protected set; }

        [NotNull]
        public virtual string Name { get; protected set; }

        [NotNull]
        public virtual string DisplayName { get; protected set; }

        [NotNull]
        public virtual string OpenAppIdOrName { get; protected set; }

        [NotNull]
        public virtual string AppId { get; protected set; }

        /// <summary>
        /// AppSecret 为空时，系第三方平台管理的微信应用，需设置 ComponentWeChatAppId
        /// </summary>
        [CanBeNull]
        public virtual string AppSecret { get; protected set; }

        [CanBeNull]
        public virtual string Token { get; protected set; }

        [CanBeNull]
        public virtual string EncodingAesKey { get; protected set; }

        public virtual bool IsStatic { get; protected set; }

        protected WeChatApp()
        {
        }

        public WeChatApp(
            Guid id,
            Guid? tenantId,
            WeChatAppType type,
            Guid? componentWeChatAppId,
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
            Type = type;
            ComponentWeChatAppId = componentWeChatAppId;
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
            DisplayName = Check.NotNullOrWhiteSpace(displayName, nameof(displayName));
            OpenAppIdOrName = Check.NotNullOrWhiteSpace(openAppIdOrName, nameof(openAppIdOrName));
            AppId = Check.NotNullOrWhiteSpace(appId, nameof(appId));
            AppSecret = appSecret;
            Token = token;
            EncodingAesKey = encodingAesKey;
            IsStatic = isStatic;
        }

        public void Update(
            Guid? componentWeChatAppId,
            [NotNull] string name,
            [NotNull] string displayName,
            [NotNull] string openAppIdOrName,
            [NotNull] string appId,
            [CanBeNull] string appSecret,
            [CanBeNull] string token,
            [CanBeNull] string encodingAesKey)
        {
            ComponentWeChatAppId = componentWeChatAppId;
            Name = Check.NotNullOrWhiteSpace(name, nameof(name));
            DisplayName = Check.NotNullOrWhiteSpace(displayName, nameof(displayName));
            OpenAppIdOrName = Check.NotNullOrWhiteSpace(openAppIdOrName, nameof(openAppIdOrName));
            AppId = Check.NotNullOrWhiteSpace(appId, nameof(appId));
            AppSecret = appSecret;
            Token = token;
            EncodingAesKey = encodingAesKey;
        }
    }
}