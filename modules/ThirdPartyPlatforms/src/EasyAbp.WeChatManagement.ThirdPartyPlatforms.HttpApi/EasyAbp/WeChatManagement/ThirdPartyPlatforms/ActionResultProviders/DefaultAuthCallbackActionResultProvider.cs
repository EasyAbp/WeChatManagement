using System.Threading.Tasks;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.ActionResultProviders;

public class DefaultAuthCallbackActionResultProvider : IAuthCallbackActionResultProvider, ITransientDependency
{
    public virtual async Task<ActionResult> GetAsync(HandleCallbackResultDto resultDto)
    {
        if (resultDto.ErrorCode == 0)
        {
            return new ContentResult
            {
                Content = "<h1>恭喜您，授权成功</h1>",
                ContentType = "text/html; charset=utf-8",
            };
        }
        else
        {
            return new ContentResult
            {
                Content = $"<h1>授权失败</h1>" +
                          $"<p>错误码：{resultDto.ErrorCode}</p>" +
                          $"<p>错误信息：{resultDto.ErrorMessage}</p>",
                ContentType = "text/html; charset=utf-8"
            };
        }
    }
}