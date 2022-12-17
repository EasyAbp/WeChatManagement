using System.Threading.Tasks;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Authorization.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.ActionResultProviders;

public interface IAuthCallbackActionResultProvider
{
    Task<ActionResult> GetAsync(HandleCallbackResultDto resultDto);
}