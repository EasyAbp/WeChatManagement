using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.AuthorizerSecrets;

public static class AuthorizerSecretEfCoreQueryableExtensions
{
    public static IQueryable<AuthorizerSecret> IncludeDetails(this IQueryable<AuthorizerSecret> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable;
    }
}
