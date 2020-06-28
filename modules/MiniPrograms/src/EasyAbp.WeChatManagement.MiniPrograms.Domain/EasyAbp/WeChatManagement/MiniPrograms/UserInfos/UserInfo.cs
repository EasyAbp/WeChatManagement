using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    public class UserInfo : FullAuditedAggregateRoot<Guid>,
IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid UserId { get; protected set; }

        [CanBeNull]
        public virtual string NickName { get; protected set; }

        public virtual byte Gender { get; protected set; }

        [CanBeNull]
        public virtual string City { get; protected set; }

        [CanBeNull]
        public virtual string Province { get; protected set; }

        [CanBeNull]
        public virtual string Country { get; protected set; }

        [CanBeNull]
        public virtual string AvatarUrl { get; protected set; }

        protected UserInfo()
        {
        }

        public UserInfo(Guid id,
            Guid? tenantId,
            Guid userId,
            [CanBeNull] string nickName,
            byte gender,
            [CanBeNull] string city,
            [CanBeNull] string province,
            [CanBeNull] string country,
            [CanBeNull] string avatarUrl) : base(id)
        {
            TenantId = tenantId;
            UserId = userId;
            NickName = nickName;
            Gender = gender;
            City = city;
            Province = province;
            Country = country;
            AvatarUrl = avatarUrl;
        }
    }
}
