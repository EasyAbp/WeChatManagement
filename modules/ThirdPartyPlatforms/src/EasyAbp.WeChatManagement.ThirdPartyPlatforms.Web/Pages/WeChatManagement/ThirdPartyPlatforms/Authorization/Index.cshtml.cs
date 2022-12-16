using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Permissions;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Web.Pages.WeChatManagement.ThirdPartyPlatforms.Authorization.
    ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Web.Pages.WeChatManagement.ThirdPartyPlatforms.Authorization;

[Authorize(ThirdPartyPlatformsPermissions.Authorization.CreateRequest)]
public class IndexModel : ThirdPartyPlatformsPageModel
{
    [BindProperty]
    public AuthorizationViewModel ViewModel { get; set; } = new();

    public void OnGet()
    {
    }
}