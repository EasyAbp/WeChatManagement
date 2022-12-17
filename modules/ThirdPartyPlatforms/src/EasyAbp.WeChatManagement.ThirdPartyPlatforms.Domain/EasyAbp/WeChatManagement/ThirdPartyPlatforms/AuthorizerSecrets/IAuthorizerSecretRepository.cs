using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;

public interface IAuthorizerSecretRepository : IRepository<AuthorizerSecret, Guid>
{
}
