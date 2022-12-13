using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;

public class AuthorizerSecretRepository : EfCoreRepository<IThirdPartyPlatformsDbContext, AuthorizerSecret, Guid>, IAuthorizerSecretRepository
{
    public AuthorizerSecretRepository(IDbContextProvider<IThirdPartyPlatformsDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<AuthorizerSecret>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}