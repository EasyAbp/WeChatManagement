using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web;
using EasyAbp.WeChatManagement.Common.Web.Pages;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;
using EasyAbp.WeChatManagement.ThirdPartyPlatforms.Web.Pages.WeChatManagement.ThirdPartyPlatforms.Authorization.
    ViewModels;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Web.Pages.WeChatManagement.ThirdPartyPlatforms.Authorization;

public class CreateRequestModel : CommonPageModel
{
    /// <summary>
    /// 如果 api 服务器在外部，请指定这个值
    /// </summary>
    /// <example>https://192.168.1.2</example>
    public static string SpecifiedAuthApiOrigin;

    private readonly IWeChatAppAppService _appService;

    [BindProperty(SupportsGet = true)]
    public AuthorizationViewModel Input { get; set; }

    [BindProperty(SupportsGet = true)]
    public string PreAuthCode { get; set; }

    /// <summary>
    /// 微信管理模块生成的 Token，与微信的设计无关
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public string Token { get; set; }

    [BindProperty]
    public WeChatAppDto ThirdPartyPlatformWeChatApp { get; set; }

    [Display(Name = "PcAuthorizationUrl")]
    [TextArea(Rows = 3)]
    [BindProperty]
    public string PcAuthorizationUrl { get; set; }

    [Display(Name = "H5AuthorizationUrl")]
    [TextArea(Rows = 3)]
    [BindProperty]
    public string H5AuthorizationUrl { get; set; }

    public CreateRequestModel(IWeChatAppAppService appService)
    {
        _appService = appService;
    }

    public virtual async Task OnGetAsync()
    {
        ThirdPartyPlatformWeChatApp = await _appService.GetAsync(Input.ThirdPartyPlatformWeChatAppId);
        PcAuthorizationUrl = await GetPcAuthorizationUrlAsync();
        H5AuthorizationUrl = await GetH5AuthorizationUrlAsync();
    }

    protected virtual async Task<string> GetPcAuthorizationUrlAsync()
    {
        return $"https://mp.weixin.qq.com/cgi-bin/componentloginpage" +
               $"?component_appid={ThirdPartyPlatformWeChatApp.AppId}" +
               $"&pre_auth_code={PreAuthCode}" +
               $"&redirect_uri={await GetEncodedRedirectUriAsync()}" +
               $"&auth_type={await GetAuthTypeAsync()}" +
               $"&biz_appid={Input.SpecifiedAppId}"; // 文档中没有 biz_appid，怀疑不生效
    }

    protected virtual async Task<string> GetH5AuthorizationUrlAsync()
    {
        return $"https://open.weixin.qq.com/wxaopen/safe/bindcomponent" +
               $"?action=bindcomponent" +
               $"&no_scan=1" +
               $"&component_appid={ThirdPartyPlatformWeChatApp.AppId}" +
               $"&pre_auth_code={PreAuthCode}" +
               $"&redirect_uri={await GetEncodedRedirectUriAsync()}" +
               $"&auth_type={await GetAuthTypeAsync()}" +
               $"&biz_appid={Input.SpecifiedAppId}" +
               $"#wechat_redirect";
    }

    protected virtual Task<string> GetAuthTypeAsync()
    {
        if (Input.AllowOfficial)
        {
            return Task.FromResult(Input.AllowMiniProgram ? "3" : "1");
        }

        return Task.FromResult(
            Input.AllowMiniProgram
                ? "2"
                : throw new UserFriendlyException("Should allow one kind of WeChatApp at least.")
        );
    }

    protected virtual Task<string> GetEncodedRedirectUriAsync()
    {
        var origin = SpecifiedAuthApiOrigin ?? $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";

        var url = origin.EnsureEndsWith('/') + "wechat/third-party-platform/auth-callback";

        if (CurrentTenant.Id.HasValue)
        {
            url += $"/tenant-id/{CurrentTenant.Id}";
        }

        url += $"/token/{Token}";

        return Task.FromResult(HttpUtility.UrlEncode(url));
    }
}